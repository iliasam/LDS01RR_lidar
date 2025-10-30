# LDS01RR_lidar
Some info and soft for LDS01RR Lidar (Roborock) with a Cullinan dev. board - such Lidar has a USB connector.  
  
LDS01RR_WinScan folder - ("LDS Scanning Test") - windows utility for drawing Lidar data.  
**UPD from 20/2025** - "LDS01RR_WinScan" was replaced by this project: https://github.com/iliasam/WinLIDARViewer  !  
  
lds01rr_lidar folder - ROS node sources. Just run "make" command to build ROS node.  
  
See more info about this Lidar here: https://youyeetoo.com/blog/lds01rr-lidar-stdps01rmain-108  
Also some more info about previous Lidar from Roborock: https://github.com/Roborock-OpenSource/Cullinan  
  
You can see tests results in "Pictures/Testing folder"  
20 measurements are analyzed.  
Distance 3m, white wall - Std. Dev.: +-0.7cm, Max-Min=2cm  
Distance 5.84m, white wall - Std. Dev.: +-0.9cm, Max-Min=4cm  
Distance 9.46m, white wall - Std. Dev.: +-1.3cm, Max-Min=5cm  
  
Distance 6m, gray wall - Std. Dev.: +-1.6cm, Max-Min=5cm  
Distance 8.4m, gray wall - Std. Dev.: +-2.1cm, Max-Min=8cm, this was maximum stable distance  
  
Distance 4.4m, dark wall - Std. Dev.: +-1.2cm, Max-Min=4cm, this was maximum stable distance  
  
Distance 11.56m, white wall - Std. Dev.: +-2.0cm, Max-Min=7cm, LDS has inclination angle ~20 deg.  
It looks like that max distance for this Lidar is limited by 12m in its firmware.  
  
Notice that data protocols (between rotating head and integrated STM32F446 MCU) and USB VCP protocols are different.  

# Disassembling
![](https://github.com/iliasam/LDS01RR_lidar/blob/main/Pictures/Disassembling/lds_photo%20(1).jpg)
Lidar with a development board installed. This board is generating power for a coil, controlling speed of the motor, receiving light from the LED at the head. Received data is converted by MCU and transfered to USB.  
  
 ![](https://github.com/iliasam/LDS01RR_lidar/blob/main/Pictures/Disassembling/lds_photo%20(2).jpg)
Photo of the PCB.  
  
![](https://github.com/iliasam/LDS01RR_lidar/blob/main/Pictures/Disassembling/lds_photo%20(3).jpg)
Lidar head with a cover removed.  

![](https://github.com/iliasam/LDS01RR_lidar/blob/main/Pictures/Disassembling/lds_photo%20(4).jpg)
Optics of the Lidar.    

![](https://github.com/iliasam/LDS01RR_lidar/blob/main/Pictures/Disassembling/lds_photo%20(10).jpg)
APD can be seen through the lens.  

![](https://github.com/iliasam/LDS01RR_lidar/blob/main/Pictures/Disassembling/lds_photo%20(6).jpg)
Laser diode is installed at the small PCB.    
  
![](https://github.com/iliasam/LDS01RR_lidar/blob/main/Pictures/Disassembling/lds_photo%20(5).jpg)
Main PCB of the head.  

![](https://github.com/iliasam/LDS01RR_lidar/blob/main/Pictures/Disassembling/lds_photo%20(7).jpg)
Closeup view of the PCB. MCU is GD32F330K8, TDC is TDC7201, receiving channel comparatos is TLV3502. APD amplifier is closed with a shield.  

![](https://github.com/iliasam/LDS01RR_lidar/blob/main/Pictures/Disassembling/lds_photo%20(8).JPG)
Another closeup view of the PCB. TIA is OPA857.

![](https://github.com/iliasam/LDS01RR_lidar/blob/main/Pictures/Disassembling/lds_photo%20(9).jpg)
PCB at the bottom of the head. Transformer coil for powering head is visible at the center.
