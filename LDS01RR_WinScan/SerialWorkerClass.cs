using System;
using System.IO.Ports;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;
using System.Linq;
using System.Management;

namespace LidarScanningTest1
{
	/// <summary>
	/// Class for working with serial port
	/// </summary>
	public class SerialWorkerClass
	{
		// Global variable that handle all operations with COM PORT
        SerialPort port;
        
        public bool ConnectionState = false;
        public IniParser settings_holder;//используется для работы с файлом настроек
        public Action<byte[]> AnswerReceived;//callback
		public Action<bool> SerialFailSignal;//callback
		

		int answer_received = 0;

        /// <summary>
		/// Создает объект для работы с портом
		/// </summary>
		/// <param name="ini_file_path"></param> Путь, по которому находится INI-файл
		/// <returns></returns>
		public SerialWorkerClass(string ini_file_path)
		{
            settings_holder = new IniParser(ini_file_path);//для работы с файлом настрое
		}
		
		void SerialWorkerFail(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e == null)
			{
				MessageBox.Show("SerialWorkerFail_0: null");
			}
			else if (e.Error != null) 
			{
				MessageBox.Show("SerialWorkerFail_1:" + e.Error.Message);
			} 
			else if (e.Cancelled) 
			{
				MessageBox.Show("SerialWorkerFail_2: Cancel");
			}
			else
			{
				// Finally, handle the case where the operation 
        		// succeeded.
				if (e.Result == null)
				{
					MessageBox.Show("SerialWorkerFail_3: null");
				}
				else
				{
					MessageBox.Show("SerialWorkerFail_3:" + e.Result.ToString());
				}
				
			}

            SerialFailSignal?.Invoke(true);//вызываем callback SerialFailSignal
        }
		

		//вызывается из port, когда в порт приходят данные
		void SerialPortReceivedHandler(object sender, SerialDataReceivedEventArgs e)
		{
			try
			{
				int bytes_to_read = port.BytesToRead;
				byte[]	rx_data = new byte[bytes_to_read];
				port.Read(rx_data, 0, bytes_to_read);

                AnswerReceived?.Invoke(rx_data);//callback
            }
			catch (SystemException ex)
			{
				MessageBox.Show(ex.Message, "Data Received Event");
			}
		}
		
		
		/// <summary>
		/// Try to open port
		/// </summary>
		/// <param name="port_name"></param> Port name - COM1
		/// <param name="baud"></param> Baudrate
		/// <param name="cur_parity"></param> Serial parity
		/// <param name="cur_stop"></param> Serial stop bits
		/// <returns></returns>
		public int OpenSerialPort(string port_name, int baud)
		{
        	try
            {
                // Create object and connect to selected port
                port = new SerialPort(port_name, baud, Parity.None, 8, StopBits.One);
                port.NewLine = "\r\n";
                port.DataReceived += SerialPortReceivedHandler;
                
                port.Open();
				ConnectionState = true;
				
                if (port.IsOpen)
                {
                	Save_Serial_settings();
                	ConnectionState = true;
                	return 1;
                }
                return -1;
            }
        	catch (Exception ex)
            {
                // Free resource
                MessageBox.Show(ex.Message, "Error in open port", MessageBoxButtons.OK);// Show message about error
                port.Dispose();
                ConnectionState = false;
				return 0;
            } //end of try
		}//end of function
		
		
		/// <summary>
		/// Cохраняет настройки порта в конфигурационный файл
		/// </summary>
		/// <returns></returns>
        public void Save_Serial_settings()
        {
        	string serial = port.PortName;
        	settings_holder.AddSetting("SERIAL_SETTINGS", "serial", serial);
        	string baud = port.BaudRate.ToString();
        	settings_holder.AddSetting("SERIAL_SETTINGS", "baud", baud);
        	string parity = port.Parity.ToString();
        	settings_holder.AddSetting("SERIAL_SETTINGS", "parity", parity);
        	string stop_bits = port.StopBits.ToString();
        	settings_holder.AddSetting("SERIAL_SETTINGS", "stopbits", stop_bits);
        	settings_holder.SaveSettings();
        }
        
        /// <summary>
        /// Записывает данные в порт
        /// </summary>
        /// <returns>возвращает 1, если записать в порт можно, иначе возвращает -1</returns>
        public int send_data_to_serial(string data)
        {
        	if (String.IsNullOrEmpty(data)) return 1;//проверка на нулевую строку	
        	
        	try
        	{
        		if ((port != null) && (port.IsOpen))
        		{
        			port.Write(data);
        			answer_received = 0;//ожидаем ответа
        			return 1;
        		}
        		else
        		{
        			ConnectionState = false;
        			return -1;
        		}
        	} catch (Exception e) {
        		ConnectionState = false;
        		return -1;
        		throw;
        	}
        }
        
        public int send_bin_data_to_serial(byte[] data)
        {
        	if (data.Length == 0) 
        		return 1;
        	
        	try
        	{
        		if ((port != null) && (port.IsOpen))
        		{
        			port.Write(data, 0, data.Length);
        			answer_received = 0;//ожидаем ответа
        			return 1;
        		}
        		else
        		{
        			ConnectionState = false;
        			return -1;
        		}
        	} catch (Exception e) {
        		ConnectionState = false;
        		return -1;
        		throw;
        	}
        }
        
        /// <summary>
        /// Закрывает порт
        /// </summary>
        /// <returns></returns>
        public void ClosePort()
        {
        	if (port != null)
        	{
        		port.Close();
        	}
        	ConnectionState = false;
        }
        

        //*************************************************************************************************************************
        

        //OLD
        public List<String> GetSerialPortInfo2()
        {
        	List<String > portList = new List<string>();
        	
        	using (var searcher = new ManagementObjectSearcher("SELECT * FROM WIN32_SerialPort"))
        	{
        		string[] portnames = SerialPort.GetPortNames();
        		var ports = searcher.Get().Cast<ManagementBaseObject>().ToList();
        		
        		var tList = (from n in portnames
                            join p in ports on n equals p["DeviceID"].ToString()
                            select n + " - " + p["Caption"]).ToList();
        		
        		foreach (string s in tList)
                {
        			portList.Add(s);
                }

        	}
        	return portList;
        }

        //You need to add System.Management.dll to your project references.
        internal class ProcessConnection
        {

        	public static ConnectionOptions ProcessConnectionOptions()
        	{
        		ConnectionOptions options = new ConnectionOptions();
        		options.Impersonation = ImpersonationLevel.Impersonate;
        		options.Authentication = AuthenticationLevel.Default;
        		options.EnablePrivileges = true;
        		return options;
        	}
        	
        	public static ManagementScope ConnectionScope(string machineName, ConnectionOptions options, string path)
        	{
        		ManagementScope connectScope = new ManagementScope();
        		connectScope.Path = new ManagementPath(@"\\" + machineName + path);
        		connectScope.Options = options;
        		connectScope.Connect();
        		return connectScope;
        	}
        }
        
        
        public List<String> GetSerialPortInfo()
        {
        	List<String> comPortInfoList = new List<String>();
        	
        	ConnectionOptions options = ProcessConnection.ProcessConnectionOptions();
        	ManagementScope connectionScope = ProcessConnection.ConnectionScope(Environment.MachineName, options, @"\root\CIMV2");
        	
        	ObjectQuery objectQuery = new ObjectQuery("SELECT * FROM Win32_PnPEntity WHERE ConfigManagerErrorCode = 0");
        	ManagementObjectSearcher comPortSearcher = new ManagementObjectSearcher(connectionScope, objectQuery);
        	
        	using (comPortSearcher)
        	{
        		string caption = null;
        		foreach (ManagementObject obj in comPortSearcher.Get())
        		{
        			if (obj != null)
        			{
        				object captionObj = obj["Caption"];
        				if (captionObj != null)
        				{
        					caption = captionObj.ToString();
        					if (caption.Contains("(COM"))
        					{
        						String comPortInfo = "";
        						comPortInfo = caption.Substring(caption.LastIndexOf("(COM")).Replace("(", string.Empty).Replace(")", string.Empty);
        						comPortInfo = comPortInfo + " " + caption;
        						comPortInfoList.Add(comPortInfo);
        					}
        				}
        			}
        		}
        	}
        	return comPortInfoList;
        }
        
        
        
        //"COM1 - xxxxxxx" -> "COM1"
        public String GetSerialName(String info_str)
        {
        	String result = "";
        	int pos = info_str.IndexOf(' ');
        	if (pos < 0) return info_str;
        	result = info_str.Substring(0,pos);
        	return result;
        }
        
        
	}//end of CLASS
}
