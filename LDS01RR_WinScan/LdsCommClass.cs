using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LidarScanningTest1
{
    class LdsCommClass
    {
        public Action<List<MeasuredPointT>> PacketReceived;

        enum SyncState
        {
            NoSync = 0,
            ReceivedByte1,
            ReceivedByte2,
            ReceivingData,
        }

        public struct MeasuredPointT
        {
            public int DistanceMM;
            public UInt16 Intensity;
        }

        SyncState currSync = SyncState.NoSync;
        int PacketPosCnt = 0;
        List<byte> CurrPacket = new List<byte>();
        byte ExpectedPacketSize = 0;

        List<MeasuredPointT> pointsList = new List<MeasuredPointT>();

        public void ParseData(byte[] receivedData)
        {
            foreach (var item in receivedData)
            {
                ParseReceivedByte(item);
            }
        }


        void ParseReceivedByte(byte rxByte)
        {
            if (currSync == SyncState.NoSync)
            {
                if (rxByte == 0xE7)//231 dec
                {
                    currSync = SyncState.ReceivedByte1;
                    PacketPosCnt = 1;
                    CurrPacket = new List<byte>();
                    CurrPacket.Add(rxByte);
                }
            }
            else if (currSync == SyncState.ReceivedByte1)
            {
                if (rxByte == 0x7E)//126
                {
                    currSync = SyncState.ReceivedByte2;
                    PacketPosCnt = 2;
                    CurrPacket.Add(rxByte);
                }
                else
                    currSync = SyncState.NoSync;
            }
            else if (currSync == SyncState.ReceivedByte2)
            {
                ExpectedPacketSize = rxByte;
                currSync = SyncState.ReceivingData;
                PacketPosCnt = 3;
                CurrPacket.Add(rxByte);
            }
            else if (currSync == SyncState.ReceivingData)
            {
                CurrPacket.Add(rxByte);
                PacketPosCnt++;
                if (PacketPosCnt >= ExpectedPacketSize)
                {
                    currSync = SyncState.NoSync;

                    if (PacketPosCnt == 34)
                        ParseMeasurementDataPacket();
                    //PacketReceived?.Invoke(false);
                }
            }
        }//end of ParseReceivedByte()


        void ParseMeasurementDataPacket()
        {
            byte PacketSeq = CurrPacket[4];
            int speed = ((UInt16)CurrPacket[14] + (UInt16)CurrPacket[15] * 256);
            speed = speed / 20;

            int AngleCode = PacketSeq - 160;
            if (AngleCode < 0)
                return;

            if (AngleCode == 0)
            {
                PacketReceived?.Invoke(pointsList);
                pointsList = new List<MeasuredPointT>();
            }

            for (int i = 0; i < 4; i++)
            {
                int start = 16 + i * 4;
                MeasuredPointT point = ParseMeasuredData(
                    CurrPacket[start], CurrPacket[start + 1],
                    CurrPacket[start + 2], CurrPacket[start + 3]);
                pointsList.Add(point);
            }

            //System.Diagnostics.Debug.WriteLine($"Seq: {PacketSeq}");
        }

        MeasuredPointT ParseMeasuredData(byte byte1, byte byte2, byte byte3, byte byte4)
        {
            MeasuredPointT res;

            UInt16 distance = (UInt16)((UInt16)byte1 + (UInt16)byte2 * 256);
            UInt16 Intensity = (UInt16)((UInt16)byte3 + (UInt16)byte4 * 256);

            if ((byte2 & 128) != 0)
                res.DistanceMM = -1;
            else if ((byte2 & 64) != 0)
                res.DistanceMM = -2;
            else
                res.DistanceMM = distance;

            res.Intensity = Intensity;

            return res;
        }
    }
}
