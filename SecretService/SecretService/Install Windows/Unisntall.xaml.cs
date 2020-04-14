using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Diagnostics;
using System.Linq;
using Microsoft.Deployment.WindowsInstaller;

namespace SGet
{
    public partial class Uninstall : Window
    {
        private MainWindow mainWindow;
        ObservableCollection<ProgsToDeleteClass> del;
        Dictionary<string, string> guid;
        public Uninstall(MainWindow mainWin)
        {
            InitializeComponent();
            mainWindow = mainWin;
            guid = new Dictionary<string, string>();
            del = new ObservableCollection<ProgsToDeleteClass>();
            ProgsToDelete();
            UninstallGrid.ItemsSource = del;
        }
        public class ProgsToDeleteClass
        {
            public ProgsToDeleteClass(string name,string version, string path)
            {
                this.Name = name;
                this.Version = version;
                this.Path = path;
            }
            public string Name { get; set; }
            public string Version { get; set; }
            public string Path { get; set; }
        }
        private void ProgsToDelete()
        {
            string sb1 = null;
            string sb2 = null;
            string sb3 = null;
            string sb4 = null;

            using (var user = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser,RegistryView.Registry64))
            using (var key = user.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"))
                foreach (String keyName in key.GetSubKeyNames())
                {
                    RegistryKey subkey = key.OpenSubKey(keyName);
                    sb1 = subkey.GetValue("DisplayName") as string;
                    sb2 = subkey.GetValue("DisplayVersion") as string;
                    sb3 = subkey.GetValue("UninstallString") as string;
                    sb4 = subkey.GetValue("InstallLocation") as string;
                    if (sb1 == null)
                    {
                        continue;
                    }
                    else
                    {
                        del.Add(new ProgsToDeleteClass(sb1, sb2, sb4));
                        if (sb3 == null || guid.ContainsKey(sb3))
                        {
                            continue;
                        }
                        else
                        {
                            guid.Add(sb3, sb1);
                        }
                    }
                }

            using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,RegistryView.Registry64))
            using (var key = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"))
                foreach (String keyName in key.GetSubKeyNames())
            {
                RegistryKey subkey = key.OpenSubKey(keyName);
                sb1 = subkey.GetValue("DisplayName") as string;
                sb2 = subkey.GetValue("DisplayVersion") as string;
                sb3 = subkey.GetValue("UninstallString") as string;
                sb4 = subkey.GetValue("InstallLocation") as string;
                if (sb1 == null)
                {
                    continue;
                }
                else
                {
                    del.Add(new ProgsToDeleteClass(sb1, sb2, sb4));
                    if (sb3 == null || guid.ContainsKey(sb3))
                    {
                        continue;
                    }
                    else
                    {
                        guid.Add(sb3, sb1);
                    }
                }
            }

            using (var hklm32 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,RegistryView.Registry32))
            using (var key = hklm32.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"))
                foreach (String keyName in key.GetSubKeyNames())
            {

                RegistryKey subkey = key.OpenSubKey(keyName);
                sb1 = subkey.GetValue("DisplayName") as string;
                sb2 = subkey.GetValue("DisplayVersion") as string;
                sb3 = subkey.GetValue("UninstallString") as string;
                sb4 = subkey.GetValue("InstallLocation") as string;
                if (sb1 == null)
                {
                    continue;
                }
                else
                {
                    del.Add(new ProgsToDeleteClass(sb1, sb2, sb4));
                    if (sb3 == null || guid.ContainsKey(sb3))
                    {
                        continue;
                    }
                    else
                    {
                        guid.Add(sb3, sb1);
                    }
                }
            }
        }
        private void btnUninstall_Click(object sender, RoutedEventArgs e)
        {
            if (UninstallGrid.SelectedItems.Count > 0)
            {
                var selectedUninstall = UninstallGrid.SelectedItems.Cast<ProgsToDeleteClass>();
                var selectedToUninstall = new List<ProgsToDeleteClass>();
                foreach (ProgsToDeleteClass install in selectedUninstall)
                {
                    string productcode = guid.Where(x => x.Value == install.Name).FirstOrDefault().Key;
                    if (productcode != null)
                    {
                            if (productcode.Contains("MsiExec.exe"))
                            {
                                try
                                {
                                    string guid = getBetween(productcode, "{", "}");
                                    guid = "{" + guid + "}";
                                    Installer.SetInternalUI(InstallUIOptions.Full);
                                    Installer.ConfigureProduct(guid, 0, InstallState.Absent, "IGNOREDEPENDENCIES=\"ALL\"");
                                }
                                catch (Exception ss)
                                {
                                    string message = ss.ToString();
                                    MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show(message, "SecretService");
                                }
                            }
                            else
                            {
                                try
                                {
                                    ProcessStartInfo iInstall = new ProcessStartInfo();
                                    string path = getBetween(productcode, "\\", ".exe");
                                    path = "C:\\" + path + ".exe";
                                    if (productcode.Contains("--uninstall"))
                                    {
                                        iInstall.Arguments = "--uninstall";
                                    }
                                    else if (productcode.Contains("/uninstall"))
                                    {
                                        iInstall.Arguments = "/uninstall";
                                    }
                                    else if (productcode.Contains("/UNINSTALL"))
                                    {
                                        iInstall.Arguments = "/UNINSTALL";
                                    }

                                    iInstall.FileName = path;
                                    Process inst = Process.Start(iInstall);
                                    inst.WaitForExit();
                                }
                                catch (Exception ss)
                                {
                                    string message = ss.ToString();
                                    MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show(message, "SecretService");
                                }

                            }
                       
                    }
                    else
                    {
                        MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show("Невозможно удалить приложение. Вероятнее всего это делается через инсталлятор", "SecretService");
                        return;
                    }

                }
            }
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

