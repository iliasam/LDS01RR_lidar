/*********************************************************************
 * Software License Agreement (BSD License)
 *
 *  Copyright (c) 2011, Eric Perko, Chad Rockey, iliasam
 *  All rights reserved.
 *
 *  Redistribution and use in source and binary forms, with or without
 *  modification, are permitted provided that the following conditions
 *  are met:
 *
 *   * Redistributions of source code must retain the above copyright
 *     notice, this list of conditions and the following disclaimer.
 *   * Redistributions in binary form must reproduce the above
 *     copyright notice, this list of conditions and the following
 *     disclaimer in the documentation and/or other materials provided
 *     with the distribution.
 *   * Neither the name of Case Western Reserve University nor the names of its
 *     contributors may be used to endorse or promote products derived
 *     from this software without specific prior written permission.
 *
 *  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
 *  "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
 *  LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS
 *  FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE
 *  COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
 *  INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
 *  BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 *  LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
 *  CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT
 *  LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN
 *  ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
 *  POSSIBILITY OF SUCH DAMAGE.
 *********************************************************************/
//by iliasam 

#include <lds01rr_lidar.h>
#include <cmath>
#include <ros/ros.h>

namespace lidar_driver
{
  Lds01rrDriver::Lds01rrDriver(const std::string& port, uint32_t baud_rate, boost::asio::io_service& io): 
    port_(port), baud_rate_(baud_rate), shutting_down_(false), serial_(io, port_) 
  {
    serial_.set_option(boost::asio::serial_port_base::baud_rate(baud_rate_));
    CurrSync = NoSync;
    PacketPosCnt = 0;
    ExpectedPacketSize = 0;
  }
  

  void Lds01rrDriver::poll(sensor_msgs::LaserScan::Ptr scan) 
  {
    uint8_t temp_char;
    bool got_scan = false;
    
    CurrScan = scan;
    
    while (!shutting_down_ && !got_scan)
    {
      boost::asio::read(serial_, boost::asio::buffer(&temp_char,1));
      got_scan = parseByte(temp_char);
    }
    
  }//end of poll function
  
  bool Lds01rrDriver::parseByte(uint8_t rxByte) 
  {
    if (CurrSync == NoSync)
    {
        if (rxByte == 0xE7)//231 dec
        {
            CurrSync = ReceivedByte1;
            PacketPosCnt = 1;
            CurrPacket.clear();
            CurrPacket.push_back(rxByte);
        }
    }
    else if (CurrSync == ReceivedByte1)
    {
        if (rxByte == 0x7E)//126
        {
            CurrSync = ReceivedByte2;
            PacketPosCnt++;
            CurrPacket.push_back(rxByte);
        }
        else
            CurrSync = NoSync;
    }
    else if (CurrSync == ReceivedByte2)
    {
        ExpectedPacketSize = rxByte;
        CurrSync = ReceivingData;
        PacketPosCnt++;
        CurrPacket.push_back(rxByte);
    }
    else if (CurrSync == ReceivingData)
    {
        CurrPacket.push_back(rxByte);
        PacketPosCnt++;
        if (PacketPosCnt >= ExpectedPacketSize)
        {
            CurrSync = NoSync;

            if (PacketPosCnt == 34)
            {
                bool res = ParseMeasurementDataPacket();
                return res;
            }
        }
    }
    return false;
  }
  
  //return true if new scan found
    bool Lds01rrDriver::ParseMeasurementDataPacket(void)
    {
        bool res = false;
        
        uint8_t packetSeq = CurrPacket[4];
        //int speed = ((UInt16)CurrPacket[14] + (UInt16)CurrPacket[15] * 256);
        //speed = speed / 20;

        int AngleCode = packetSeq - 160;
        if (AngleCode < 0)
            return false;

        if (AngleCode == 0)
        {
            ProcessLidarData(PointsList);
            PointsList.clear();
            res = true;
        }

        for (int i = 0; i < 4; i++)
        {
            int start = 16 + i * 4;
            MeasuredPointT point = ParseMeasuredData(
                CurrPacket[start], CurrPacket[start + 1],
                CurrPacket[start + 2], CurrPacket[start + 3]);
            PointsList.push_back(point);
        }
        
        return res;
    }
    
    Lds01rrDriver::MeasuredPointT Lds01rrDriver::ParseMeasuredData(uint8_t byte1, uint8_t byte2, uint8_t byte3, uint8_t byte4)
    {
        Lds01rrDriver::MeasuredPointT res;

        uint16_t distance = (uint16_t)((uint16_t)byte1 + (uint16_t)byte2 * 256);
        uint16_t Intensity = (uint16_t)((uint16_t)byte3 + (uint16_t)byte4 * 256);

        if ((byte2 & 128) != 0)
            res.DistanceMM = -1;
        else if ((byte2 & 64) != 0)
            res.DistanceMM = -2;
        else
            res.DistanceMM = distance;

        res.Intensity = Intensity;

        return res;
    }
  
  void Lds01rrDriver::ProcessLidarData(std::deque<MeasuredPointT> pointsList)
  {
    uint16_t scanPointsCnt = pointsList.size();
    double angularStep = 360.0 / scanPointsCnt;
    
    CurrScan->angle_min = 0.0;
    CurrScan->angle_max = 2.0 * M_PI;
    //CurrScan->angle_min = -M_PI ;
    //CurrScan->angle_max =  M_PI;
    
    CurrScan->angle_increment = angularStep * M_PI /  180.0;
    CurrScan->time_increment =  1.0/(scanPointsCnt * 15.0);//1 sec
    CurrScan->range_min = 0.05;
    CurrScan->range_max = 15.0;
    CurrScan->scan_time = 1.0/5.0;//seconds
    CurrScan->ranges.reserve(scanPointsCnt);
    CurrScan->intensities.reserve(scanPointsCnt);
    
    int i;
    double distBuf[scanPointsCnt];
    for (i = 0; i < scanPointsCnt; i++)
    {
      double dist_m = 0.0;
      if (pointsList[i].DistanceMM >= 0)
        dist_m = (double)pointsList[i].DistanceMM / 1000.0;
      double angleDeg = 180.0 - (double)i * angularStep;
      
      double correctedAngleDeg = angleDeg;
      if (correctedAngleDeg > 359.0)
        correctedAngleDeg = correctedAngleDeg - 359.0;
      if (correctedAngleDeg < 0.0)
	correctedAngleDeg = 360 + correctedAngleDeg; 
      uint16_t pos = round(correctedAngleDeg / angularStep);
      distBuf[pos] = dist_m;
    }
    
    for (i = 0; i < scanPointsCnt; i++)
    {
      CurrScan->ranges.push_back(distBuf[i]);
      CurrScan->intensities.push_back(0);
    }
  }
  
};//end of namespace
