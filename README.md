# LDS01RR_lidar
Some info and soft for LDS01RR Lidar (Roborock)  
LDS01RR_WinScan folder - ("LDS Scanning Test") - windows utility for drawing Lidar data.  
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
  
Distance 11.56m, white wall - Std. Dev.: +-2.0cm, Max-Min=7cm, LDS was inclinated ~20 deg.  
It looks like that max distance for this Lidar is limeted by 12m in its firmware.  

