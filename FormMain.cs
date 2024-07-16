using System;
using System.Linq;
using System.Windows.Forms;
using System.Management;
using System.IO;
using System.Collections;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using Microsoft.Win32;


namespace WFASystemInformation
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }
        void SaveData()
        {
            try
            {
                if (!Directory.Exists("data"))
                {
                    Directory.CreateDirectory("data");
                }
                string path = Path.Combine(Application.StartupPath, "log" + endTime.ToString("_dd_MM_yyyy_HH_mm_ss") + ".txt");
                rTxt_Information.SaveFile(path, RichTextBoxStreamType.PlainText);
                rTxt_Information.Text += "\vLog file created on: " + path;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Log file cannot be saved. Error: " + ex.Message);
            }
        }
        ManagementObj[] mgmtObjects = new ManagementObj[50];
        //if you insert new values in the string list, don't forget to update above line via the count of string list.
        string[] strMgmtObjects = {
                "Win32_BaseBoard",
                "Win32_Battery",
                "Win32_BIOS",
                "Win32_CDROMDrive",
                "Win32_DiskDrive",
                "Win32_DisplayConfiguration",
                "Win32_DisplayControllerConfiguration",
                "Win32_DMAChannel",
                "Win32_IDEController",
                "Win32_IDEControllerDevice",
                "Win32_Keyboard",
                "Win32_MotherboardDevice",
                "Win32_OnBoardDevice",
                "Win32_OperatingSystem",
                "Win32_PhysicalMedia",
                "Win32_PnPDevice",
                "Win32_PnPEntity",
                "Win32_PointingDevice",
                "Win32_PortConnector",
                "Win32_PowerManagementEvent",
                "Win32_Printer",
                "Win32_PrinterConfiguration",
                "Win32_PrinterController",
                "Win32_PrintJob",
                "Win32_Processor",
                "Win32_Product",
                "Win32_SCSIController",
                "Win32_SCSIControllerDevice",
                "Win32_SerialPort",
                "Win32_SerialPortConfiguration",
                "Win32_SerialPortSetting",
                "Win32_Services",
                "Win32_SoundDevice",
                "Win32_USBController",
                "Win32_USBControllerDevice",
                "Win32_VideoConfiguration",
                "Win32_VideoController",
                "Win32_VideoSettings"};
        DateTime startTime;
        void getirManagement()
        {
            startTime = DateTime.Now;
            for (int i = 0; i < mgmtObjects.Count(); i++)
            {
                if (mgmtObjects[i] == null)
                {
                    continue;
                }
                if (!mgmtObjects[i].mgmtChecked)
                {
                    continue;
                }
                ManagementObjectSearcher MOS = new ManagementObjectSearcher("Select * from " + mgmtObjects[i].mgmtObject);
                try
                {
                    foreach (ManagementObject MO in MOS.Get())
                    {
                        try
                        {
                            if (MO["Name"] != null)
                            {
                                rTxt_Information.Text += MO["Name"];
                            }
                            else
                            {
                                rTxt_Information.Text += MO.ToString();
                            }
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                        if (MO.Properties.Count >= 0)
                        {
                            rTxt_Information.Text += " Properties:\v";
                            foreach (PropertyData PD in MO.Properties)
                            {
                                bool ClassName = false;
                                if (PD.Value != null && PD.Value.ToString() != "")
                                {
                                    rTxt_Information.Text += PD.Name + ": ";
                                    switch (PD.Value.GetType().ToString())
                                    {
                                        case "System.String[]":
                                            string[] str = (string[])PD.Value;
                                            string str2 = "";
                                            foreach (string st in str)
                                                str2 += st + " ";
                                            if (str2.Contains(mgmtObjects[i].mgmtObject)) ClassName = true;
                                            rTxt_Information.Text += str2 + "\v";
                                            break;
                                        case "System.UInt16[]":
                                            ushort[] shortData = (ushort[])PD.Value;
                                            string tstr2 = "";
                                            foreach (ushort st in shortData)
                                                tstr2 += st.ToString() + " ";
                                            if (tstr2.Contains(mgmtObjects[i].mgmtObject)) ClassName = true;
                                            rTxt_Information.Text += tstr2 + "\v";
                                            break;
                                        default:
                                            rTxt_Information.Text += PD.Value.ToString() + "\v";
                                            if (PD.Value.ToString().Contains(mgmtObjects[i].mgmtObject)) ClassName = true;
                                            break;
                                    }
                                    if (ClassName)
                                    {
                                        rTxt_Information.Text += "ClassName: " + mgmtObjects[i].mgmtObject + "\v";
                                    }
                                }
                            }
                        }
                        else
                        {
                            rTxt_Information.Text += "No Information Found\v\v";
                        }
                    }
                }
                catch (Exception e)
                {
                    rTxt_Information.Text += "Error occured while trying to get data from " + mgmtObjects[i] + ". Error: " + e.Message;
                }
                rTxt_Information.Text += "\v\v\v";
            }
        }
        void GetCPU()
        {
            ManagementObjectSearcher MOSProcessor = new ManagementObjectSearcher("Select * From Win32_Processor");
            foreach (ManagementObject Mobject in MOSProcessor.Get())
            {
                rTxt_Information.Text += "CPU Information:\vCPU: " + Mobject["Name"] + "\vCores: " + Mobject["NumberOfCores"];
                rTxt_Information.Text += "\vLogical Processor: " + Mobject["NumberOfLogicalProcessors"].ToString();
            }
        }
        void GetGPU()
        {
            ManagementObjectSearcher MOSGPU = new ManagementObjectSearcher("Select * From Win32_VideoController");
            foreach (ManagementObject Mobject in MOSGPU.Get())
            {
                double bellek = Math.Ceiling(Convert.ToDouble(Mobject["AdapterRam"]) / 1024 / 1024);
                rTxt_Information.Text += "\v\v\vGPU Information: \vModel: " + Mobject["name"].ToString() + "\vMemory: " + bellek + " MB";
                rTxt_Information.Text += "\vGPU Driver Version: " + Mobject["DriverVersion"] + "\vDriver Date: " + Mobject["DriverDate"];
            }
        }
        void GetDisk()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            rTxt_Information.Text += "\v\v\vDrive(s):";
            foreach (DriveInfo disk in drives)
            {
                if (disk.IsReady)
                {
                    rTxt_Information.Text += "\v" + disk.Name + "\vDetails:\vDrive Type: " + disk.DriveType + "\vDrive Format: " + disk.DriveFormat + "\vVolume Label: " + disk.VolumeLabel + "\v" + disk.TotalSize / 1024 / 1024 / 1024 + " GB\v";
                }
                else
                {
                    rTxt_Information.Text += "\v" + disk.Name;
                }
            }
        }
        void GetRAM()
        {
            ManagementObjectSearcher MOSMemory = new ManagementObjectSearcher("Select * From Win32_ComputerSystem");
            foreach (ManagementObject Mobject in MOSMemory.Get())
            {
                double Ram_Bytes = Math.Ceiling((Convert.ToDouble(Mobject["TotalPhysicalMemory"])) / 1024 / 1024);
                rTxt_Information.Text += "\v\v\vRAM Information:\v";
                rTxt_Information.Text += "Total Ram: " + Ram_Bytes.ToString() + " MB";
            }
        }
        void GetOS()
        {
            ManagementObjectSearcher MOSOperatingSystem = new ManagementObjectSearcher("Select * From Win32_OperatingSystem");
            foreach (ManagementObject Mobject in MOSOperatingSystem.Get())
            {
                string[] langs = (string[])Mobject["MUILanguages"];
                rTxt_Information.Text += "\v\v\vOperating System Information:\vOS: "
                    + Mobject["Caption"].ToString().Substring(Mobject["Caption"].ToString().IndexOf("Windows")) + " "
                    + Mobject["OSArchitecture"].ToString() + "\vVersion: " + Mobject["BuildNumber"]
                    + "\vOS Language(s): ";
                foreach (string lang in langs)
                    rTxt_Information.Text += lang + "\v";
                rTxt_Information.Text += "OS Drive: " + Mobject["SystemDrive"].ToString();
                string installdate = Mobject["InstallDate"].ToString();
                DateTime dateTime = new DateTime(Convert.ToInt32(installdate.Substring(0, 4)),
                    Convert.ToInt32(installdate.Substring(4, 2)),
                    Convert.ToInt32(installdate.Substring(6, 2)),
                    Convert.ToInt32(installdate.Substring(8, 2)),
                    Convert.ToInt32(installdate.Substring(10, 2)),
                    Convert.ToInt32(installdate.Substring(12, 2)));
                dateTime.AddMinutes(Convert.ToInt32(installdate.Substring(installdate.IndexOf("+") + 1)));
                rTxt_Information.Text += "\vInstall Date of OS: " + dateTime.ToString("G");
                if (licenceKey)
                {
                    rTxt_Information.Text += "\vLicence Key: " + GetLicenceKey();
                }
            }
        }
        string GetLicenceKey()
        {
            byte[] key = new byte[164];
            string keyPath = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion";
            string valueName = "DigitalProductId";
            try
            {
                using (RegistryKey DigitalProductId = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
                {
                    using (RegistryKey subKey = DigitalProductId.OpenSubKey(keyPath))
                    {
                        if (subKey != null)
                        {
                            key = (byte[])subKey.GetValue(valueName);
                            if (key == null)
                            {
                                return "";
                            }
                        }
                        else
                        {
                            return "";
                        }
                    }
                }
            }
            catch (Exception)
            {
                return "";
            }
            const int KeyOffset = 52;
            bool isWin8 = (key[66] / 6 & 1) == 1;
            key[66] = (byte)((key[66] & 0xF7) | ((isWin8 ? 1 : 0) & 2) * 4);
            int i = 24;
            const string Chars = "BCDFGHJKMPQRTVWXY2346789";
            string keyOutput = string.Empty;
            int last = 0;
            while (i >= 0)
            {
                int cur = 0;
                int x = 14;
                while (x >= 0)
                {
                    cur = cur * 256;
                    cur += key[x + KeyOffset];
                    key[x + KeyOffset] = (byte)(cur / 24);
                    cur %= 24;
                    x--;
                }
                i--;
                keyOutput = Chars[cur] + keyOutput;
                last = cur;
            }
            if (isWin8)
            {
                string keyPart1 = keyOutput.Substring(1, last);
                const string insert = "N";
                string keyPart2 = keyOutput.Substring(last + 1);
                keyOutput = keyPart1 + insert + keyPart2;
            }
            string a = keyOutput.Substring(0, 5);
            string b = keyOutput.Substring(5, 5);
            string c = keyOutput.Substring(10, 5);
            string d = keyOutput.Substring(15, 5);
            string e = keyOutput.Substring(20, 5);
            return $"{a}-{b}-{c}-{d}-{e}";
        }
        void GetInstalledApp()
        {
            rTxt_Information.Text += "\v\v\vInstalled Applications:\v";
            ManagementObjectSearcher MOSProduct = new ManagementObjectSearcher("Select * from Win32_Product");
            ArrayList arr = new ArrayList();
            foreach (ManagementObject MO in MOSProduct.Get())
            {
                try
                {
                    if (MO["Name"] != null)
                        arr.Add(MO["Name"].ToString());
                }
                catch (Exception)
                {
                }
            }
            arr.Sort();
            foreach (string item in arr)
            {
                rTxt_Information.Text += item + "\v";
            }
        }
        void GetService()
        {
            rTxt_Information.Text += "\v\v\vServices:\v";
            ManagementObjectSearcher MOSService = new ManagementObjectSearcher("Select * from Win32_Service");
            foreach (ManagementObject MO in MOSService.Get())
            {
                try
                {
                    if (MO["Name"] != null)
                    {
                        rTxt_Information.Text += MO["Caption"].ToString() + "\v: " + MO["Name"].ToString()
                            + "\v" + MO["PathName"].ToString() + MO["State"].ToString() + "\v\v";
                    }
                }
                catch
                {
                }
            }
        }
        void GetNetworkDetail()
        {
            var interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface intf in interfaces)
            {
                if (intf.OperationalStatus != OperationalStatus.Up)
                {
                    continue;
                }
                if (intf.NetworkInterfaceType == NetworkInterfaceType.Loopback)
                {
                    continue;
                }
                IPInterfaceProperties properties = intf.GetIPProperties();
                if (properties == null)
                {
                    continue;
                }
                GatewayIPAddressInformationCollection gateways = properties.GatewayAddresses;
                if ((gateways == null) || (gateways.Count == 0))
                {
                    continue;
                }
                UnicastIPAddressInformationCollection addresses = properties.UnicastAddresses;
                if ((addresses == null) || (addresses.Count == 0))
                {
                    continue;
                }
                foreach (UnicastIPAddressInformation address in intf.GetIPProperties().UnicastAddresses)
                {
                    if (address.Address.AddressFamily != AddressFamily.InterNetwork)
                    {
                        continue;
                    }
                    if (address.IsTransient)
                    {
                        continue;
                    }
                    if (mac)
                    {
                        string macadress = intf.GetPhysicalAddress().ToString();
                        macadress = macadress.Substring(0, 2) + ":" + macadress.Substring(2, 2) + ":" + macadress.Substring(4, 2) + ":" +
                            macadress.Substring(6, 2) + ":" + macadress.Substring(8, 2) + ":" + macadress.Substring(10, 2);
                        rTxt_Information.Text += "\v\v\vNetwork Information:\v";
                        rTxt_Information.Text += "MAC Address: " + macadress;
                    }
                    if (ip)
                    {
                        if (!mac) rTxt_Information.Text += "\v\v\vNetwork Information:\v";
                        rTxt_Information.Text += "\vIP Address: " + address.Address + "\v";
                    }
                }
            }
            rTxt_Information.Text += "\v\v\v";
        }
        DateTime endTime;
        void GetAllData()
        {
            getirManagement();
            endTime = DateTime.Now;
            TimeSpan ts = endTime - startTime;
            string elapsedTime = String.Format("{0:00}h {1:00}m {2:00}s",
                ts.Hours, ts.Minutes, ts.Seconds);
            rTxt_Information.Text += "Starting Time To Get Data: " + startTime;
            rTxt_Information.Text += "\vEnd Time To Get Data: " + endTime.ToString("G");
            rTxt_Information.Text += "\vElapsed Time: " + elapsedTime;
        }
        private void FormMain_Shown(object sender, EventArgs e)
        {
            cpu = gpu = ram = disk = os = licenceKey = mac = ip = true;
            installedApp = service = false;
            for (int i = 0; i < strMgmtObjects.Count(); i++)
                mgmtObjects[i] = new ManagementObj(strMgmtObjects[i], false);
        }

        private void btn_Detailed_Click(object sender, EventArgs e)
        {
            bool checkedAny = false;
            bool warn = false;
            foreach (ManagementObj mgmt in mgmtObjects)
            {
                if (mgmt == null)
                {
                    continue;
                }
                if (mgmt.mgmtChecked)
                {
                    checkedAny = true;
                    if ((mgmt.mgmtObject == "Win32_PnPEntity") || mgmt.mgmtObject == "Win32_Product" || mgmt.mgmtObject == "Win32_Services")
                    {
                        warn = true;
                    }
                }
            }
            if (checkedAny)
            {
                rTxt_Information.Text = string.Empty;
                if (warn)
                {
                    DialogResult dr = MessageBox.Show("Detailed Informating is about to start.This may take long.", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (dr == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                GetAllData();
                SaveData();
            }
            else
            {
                DialogResult dr = MessageBox.Show("No object selected to get data. Do you want to open setting?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    btn_AllSettings.PerformClick();
                }
            }
        }

        private void btn_DetailedSettings_Click(object sender, EventArgs e)
        {
            FormDetailedSettings detailedSettings = new FormDetailedSettings();
            foreach (ManagementObj mgmtObj in mgmtObjects)
            {
                if (mgmtObj == null)
                {
                    continue;
                }
                detailedSettings.clb_Simple.Items.Add(mgmtObj.mgmtObject, mgmtObj.mgmtChecked);
            }
            detailedSettings.ShowDialog();
            for (int i = 0; i < mgmtObjects.Count(); i++)
            {
                if (mgmtObjects[i] != null)
                {
                    mgmtObjects[i].mgmtChecked = detailedSettings.clb_Simple.GetItemChecked(i);
                }
            }
        }

        private void btn_Simple_Click(object sender, EventArgs e)
        {
            GetSimpleData();
        }
        bool cpu, gpu, ram, disk, os, licenceKey, installedApp, service, mac, ip;
        private void btn_SimpleSettings_Click(object sender, EventArgs e)
        {
            FormSimpleSettings formSimple = new FormSimpleSettings();
            formSimple.clb_Simple.SetItemChecked(0, cpu);
            formSimple.clb_Simple.SetItemChecked(1, gpu);
            formSimple.clb_Simple.SetItemChecked(2, ram);
            formSimple.clb_Simple.SetItemChecked(3, disk);
            formSimple.clb_Simple.SetItemChecked(4, os);
            formSimple.clb_Simple.SetItemChecked(5, licenceKey);
            formSimple.clb_Simple.SetItemChecked(6, installedApp);
            formSimple.clb_Simple.SetItemChecked(7, service);
            formSimple.clb_Simple.SetItemChecked(8, mac);
            formSimple.clb_Simple.SetItemChecked(9, ip);
            formSimple.ShowDialog();
            cpu = formSimple.clb_Simple.GetItemChecked(0);
            gpu = formSimple.clb_Simple.GetItemChecked(1);
            ram = formSimple.clb_Simple.GetItemChecked(2);
            disk = formSimple.clb_Simple.GetItemChecked(3);
            os = formSimple.clb_Simple.GetItemChecked(4);
            licenceKey = formSimple.clb_Simple.GetItemChecked(5);
            installedApp = formSimple.clb_Simple.GetItemChecked(6);
            service = formSimple.clb_Simple.GetItemChecked(7);
            mac = formSimple.clb_Simple.GetItemChecked(8);
            ip = formSimple.clb_Simple.GetItemChecked(9);
        }

        void GetSimpleData()
        {
            rTxt_Information.Text = string.Empty;
            if (cpu) GetCPU();
            if (gpu) GetGPU();
            if (ram) GetRAM();
            if (disk) GetDisk();
            if (os) GetOS();
            if (installedApp) GetInstalledApp();
            if (service) GetService();
            if (mac || ip) GetNetworkDetail();
            if (cpu || gpu || ram || disk || os || installedApp || service || mac || ip)
            {
                endTime = DateTime.Now;
                TimeSpan ts = endTime - startTime;
                string elapsedTime = String.Format("{0:00}h {1:00}m {2:00}s",
                    ts.Hours, ts.Minutes, ts.Seconds);
                rTxt_Information.Text += "Starting Time To Get Data: " + startTime;
                rTxt_Information.Text += "\vEnd Time To Get Data: " + endTime.ToString("G");
                rTxt_Information.Text += "\vElapsed Time: " + elapsedTime;
                SaveData();
            }
        }
    }
}