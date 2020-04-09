using System;
using System.Diagnostics;
using System.Windows;
using System.IO;

namespace SGet.Install
{
    class InstallManager
    {
        public void Installation(string name, string parameters)
        {
            try
            {
                ProcessStartInfo iInstall = new ProcessStartInfo();
                iInstall.FileName = @name;
                iInstall.Arguments = parameters;
                Process inst = Process.Start(iInstall);
                if (Path.GetExtension(@name) == ".exe" || Path.GetExtension(@name) == ".msi")
                {
                    inst.WaitForExit();
                }
                
            }
            catch (Exception e)
            {
                string message = e.ToString();
                MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show(message, "SecretService");
            }
        }


        public string Parameters(string name, string path)
        {
            string appDataPath = Environment.UserName;
            string folderPath = "C:\\Users\\" + appDataPath + "\\Desktop\\SecretService\\SecretService\\base\\nfdc.exe";
            if (!File.Exists(folderPath))
            {
                MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show("Невозможно найти файл " + folderPath, "SecretService");
                return null;
            }
            ProcessStartInfo pr = new ProcessStartInfo();
            pr.FileName = @folderPath;
            pr.Arguments = @name;
            pr.UseShellExecute = false;
            pr.RedirectStandardOutput = true;
            pr.CreateNoWindow = true;

            Process p = Process.Start(pr);
            string ss = null;
            ss = p.StandardOutput.ReadToEnd();
            string installer = null;
            installer = getBetween(ss, "Installer:", "(");
            string linker = null;
            linker = getBetween(ss, "Linker:", "(");
            string e = Path.GetExtension(@name);
            
            string parameters = null;
            if (installer == " InstallShield")
            {
                parameters = "/s /v\"/qb REBOOT=ReallySuppress";
                if (path != null)
                {
                    parameters += " TARGETDIR=\"" + path + "\"\"";
                }
                else
                {
                    parameters += "\"";
                }
            }
            else if (installer == " Inno Setup")
            {
                parameters = "/SILENT /NORESTART";
                if (path != null)
                {
                    parameters += " /DIR=\"" + path + "\"";
                }
            }
            else if (installer == " Nullsoft Scriptable Install System")
            {
                parameters = "/S";
                if (path != null)
                {
                    parameters += " /D=" + path;
                }
            }
            else if (installer == " WISE Installer")
            {
                parameters = "/s";
            }
            else if (e == ".msi")
            {
                parameters = "/qb REBOOT=ReallySuppress";
                if (path != null)
                {
                    parameters += " TARGETDIR=\"" + path + "\"\"";
                }
            }
            else if (e == ".msi" && installer==" InstallShield")
            {
                parameters = "/s /v\" /qb REBOOT=ReallySuppress\"";
            }
            else if (e == ".msu")
            {
                parameters = "/quiet /norestart";
            }
            else if (installer == "")
            {
                parameters = "";
            }
            p.WaitForExit();
            return parameters;
        }

        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }
    }
}
