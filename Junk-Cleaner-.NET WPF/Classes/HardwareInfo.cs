using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Microsoft.VisualBasic.Devices;

namespace Junk_Cleaner_.NET_WPF
{
    public class HardwareInfo
    {
        private delegate void displayInfo();
        private Thread trdOSInfo;
        private Thread trdCPUInfo;
        private Thread trdGPUInfo;
        private TextBlock textBlock;
        private string strGPUOutput;
        private string strCPUOutput;
        private string strOSFullName;
        private string strServicePack;
        private string strOSBits;
        private string strProcessorBits;
        private string strVersionString;
        private string strRamInfo;
        private string strGPURamInfo;

        public HardwareInfo(TextBlock textBlock)
        {
            strGPUOutput = string.Empty;
            strCPUOutput = string.Empty;
            strOSFullName = string.Empty;
            strServicePack = string.Empty;
            strOSBits = string.Empty;
            strVersionString = string.Empty;
            strProcessorBits = string.Empty;
            strRamInfo = string.Empty;
            strGPURamInfo = string.Empty;
            this.textBlock = textBlock;
            SetOutputString();
            run();
        }

        private void run()
        {
            try
            {
                trdOSInfo = new Thread(new ThreadStart(OSRunner));
                trdOSInfo.IsBackground = true;
                trdOSInfo.Start();
                trdCPUInfo = new Thread(new ThreadStart(CPURunner));
                trdCPUInfo.IsBackground = true;
                trdCPUInfo.Start();
                trdGPUInfo = new Thread(new ThreadStart(GPURunner));
                trdGPUInfo.IsBackground = true;
                trdGPUInfo.Start();
            }
            catch (Exception){throw;}
        }

        private void OSRunner()
        {
            try
            {
                ComputerInfo ci = new ComputerInfo();
                strOSFullName = ci.OSFullName;
                strServicePack = OSVersionInfo.ServicePack;
                strOSBits = OSVersionInfo.OSBits.ToString();
                strVersionString = OSVersionInfo.VersionString;
                strProcessorBits = OSVersionInfo.ProcessorBits.ToString();
                strRamInfo = Globals.sizeFix((long)ci.TotalPhysicalMemory, 0);
                textBlock.Dispatcher.Invoke(new displayInfo(SetOutputString));
            }
            catch (Exception) { throw; }
        }

        private void CPURunner()
        {
            try
            {
                GetGPUInfo(); 
                textBlock.Dispatcher.Invoke(new displayInfo(SetOutputString));
            }
            catch (Exception){throw;}
        }

        private void GPURunner()
        {
            try
            {
                strCPUOutput = GetProcessorID();
                textBlock.Dispatcher.Invoke(new displayInfo(SetOutputString));
            }
            catch (Exception) { throw; }
        }

        private string GetProcessorID()
        {
            try
            {
                ManagementClass mgt = new ManagementClass("Win32_Processor");
                ManagementObjectCollection procs = mgt.GetInstances();
                foreach (ManagementObject item in procs)
                    return item.Properties["Name"].Value.ToString().Trim();

                return "Unknown";
            }
            catch (Exception) { return "Unknown"; }
        }

        private void SetOutputString()
        {
            try
            {
                Brush brName = Brushes.SteelBlue; 
                 Brush brValue = Brushes.Black;
                textBlock.Inlines.Clear();
                textBlock.Inlines.Add(Globals.coloredRun("OS  : ", brName));
                textBlock.Inlines.Add(Globals.coloredRun(strOSFullName + " " , brValue));
                if(!string.IsNullOrEmpty(strServicePack.Trim()))
                    textBlock.Inlines.Add(Globals.coloredRun(strServicePack + " ", brValue));
                if (!string.IsNullOrEmpty(strOSBits.Trim()))
                    textBlock.Inlines.Add(Globals.coloredRun(strOSBits + " ", brValue));
                if (!string.IsNullOrEmpty(strVersionString.Trim()))
                {
                    textBlock.Inlines.Add(Globals.coloredRun("[ Version : ", brName));
                    textBlock.Inlines.Add(Globals.coloredRun(strVersionString, brValue));
                    textBlock.Inlines.Add(Globals.coloredRun(" ]", brName));
                }
                textBlock.Inlines.Add(new Run(Environment.NewLine));
                textBlock.Inlines.Add(Globals.coloredRun("CPU : ", brName));
                textBlock.Inlines.Add(Globals.coloredRun(strCPUOutput + " ", brValue));
                textBlock.Inlines.Add(Globals.coloredRun(strProcessorBits + " ", brValue));
                textBlock.Inlines.Add(new Run(Environment.NewLine));
                textBlock.Inlines.Add(Globals.coloredRun("RAM : ", brName));
                textBlock.Inlines.Add(Globals.coloredRun(strRamInfo + " ", brValue));
                textBlock.Inlines.Add(new Run(Environment.NewLine));
                textBlock.Inlines.Add(Globals.coloredRun("VGA : ", brName));
                textBlock.Inlines.Add(Globals.coloredRun(strGPUOutput + " ", brValue));
                textBlock.Inlines.Add(Globals.coloredRun("[ Ram : ", brName));
                textBlock.Inlines.Add(Globals.coloredRun(strGPURamInfo + " ", brValue));
                textBlock.Inlines.Add(Globals.coloredRun(" ]", brName));
                //StringBuilder sb = new StringBuilder(String.Empty);
                //sb.AppendLine(String.Format("OS  : {0} {1} {2} [ {3} ]", strOSFullName, strServicePack, strOSBits, strVersionString));
                //sb.AppendLine(String.Format("CPU : {0} {1}", strCPUOutput, strProcessorBits));
                //sb.AppendLine(String.Format("RAM : {0}", strRamInfo));
                //sb.AppendLine(String.Format("VGA : {0} {1}", strGPUOutput, strGPURamInfo));
                //textBlock.Text = sb.ToString();
            }
            catch (Exception) { throw; }
        }

        private void GetGPUInfo()
        {
            try
            {
                ManagementObjectSearcher objvide = new ManagementObjectSearcher("select Name, AdapterRAM from Win32_VideoController");

                foreach (ManagementObject obj in objvide.Get())
                {
                    strGPUOutput = obj["Name"].ToString();
                    long VGARam = 0;
                    long.TryParse(obj["AdapterRAM"].ToString(), out VGARam);
                    strGPURamInfo = Globals.sizeFix(VGARam, 0);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
