using Microsoft.Win32;
using SGet.Install;
using SGet.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Xml;

namespace SGet
{
    /// <summary>
    /// Логика взаимодействия для Update.xaml
    /// </summary>
    public partial class Update : Window
    {
        private MainWindow mainWindow;
        List<ProgName> upd;
        ObservableCollection<ProgVersion> outcome;
        public Update(MainWindow mainWin)
        {
            InitializeComponent();
            mainWindow = mainWin;
            upd = new List<ProgName>();
            outcome = new ObservableCollection<ProgVersion>();
            updateGrid.ItemsSource = outcome;
            if (System.Windows.Clipboard.ContainsText())
            {
                string clipboardText = System.Windows.Clipboard.GetText();

                if (IsUrlValid(clipboardText))
                {
                    tbServerAddr.Text = clipboardText;
                }
            }
        }
        private bool IsUrlValid(string Url)
        {
            if (Url.StartsWith("http") && Url.Contains(":") && (Url.Length > 15)
                && (Utilities.CountOccurence(Url, '/') >= 3) && (Url.LastIndexOf('/') != Url.Length - 1))
            {
                string lastChars = Url.Substring(Url.Length - 9);

                if (lastChars.Contains(".") && (lastChars.LastIndexOf('.') != lastChars.Length - 1))
                {
                    string ext = lastChars.Substring(lastChars.LastIndexOf('.') + 1);

                    string chars = " ?#&%=[]_-+~:;\\/!$<>\"\'*";

                    foreach (char c in ext)
                    {
                        foreach (char s in chars)
                        {
                            if (c == s)
                                return false;
                        }
                    }

                    return true;
                }
                return false;
            }
            return false;
        }
        public class ProgVersion
        {
            public string Name { get; set; }
            public string Version { get; set; }
            public string Versionserver { get; set; }
            public string Url { get; set; }

            public ProgVersion(string name, string version, string versionserver, string url)
            {
                this.Name = name;
                this.Version = version;
                this.Versionserver = versionserver;
                this.Url = url;
            }
        }

        public class ProgName
        {
            public string Name { get; set; }
            public string Version { get; set; }
            public string Url { get; set; }

            public ProgName(string name, string version, string url)
            {
                this.Name = name;
                this.Version = version;
                this.Url = url;
            }

        }

        public class VersionChecker
        {
            public bool NewVersionExists(string localVersion, string versionFromServer)
            {
                Version verLocal = new Version(localVersion);
                Version verWeb = new Version(versionFromServer);
                return verLocal < verWeb;
            }
        }

        public void Checker()
        {
            if (IsUrlValid(tbServerAddr.Text.Trim()))
            {
                string version = null;
                string url = null;
                VersionChecker verChecker = new VersionChecker();
                WebDownloadClient download = new WebDownloadClient(tbServerAddr.Text.Trim());
                download.CheckUrl();
                if (download.HasError)
                    return;
                try
                {
                    HttpWebRequest rq = (HttpWebRequest)WebRequest.Create(tbServerAddr.Text.Trim());
                    HttpWebResponse response = (HttpWebResponse)rq.GetResponse();
                    string addr = tbServerAddr.Text.Trim();
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        addr = reader.ReadToEnd();
                    }
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(addr);
                    XmlElement xdoc = doc.DocumentElement;
                    foreach (XmlNode node in xdoc)
                    {
                        string name = node.Attributes[0].Value;

                        foreach (XmlNode childnode in node.ChildNodes)
                        {
                            if (childnode.Name == "Version")
                            {
                                version = childnode.InnerText;
                            }
                            if (childnode.Name == "Url")
                            {
                                url = childnode.InnerText;
                            }
                        }
                        upd.Add(new ProgName(name, version, url));
                    }
                }
                catch (Exception ss)
                {
                    string message = ss.ToString();
                    MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show(message, "SecretService");
                }

                string sb1 = null;
                string sb2 = null;

                RegistryKey key;
                key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
                foreach (String keyName in key.GetSubKeyNames())
                {
                    try
                    {
                        sb1 = null;
                        sb2 = null;
                        RegistryKey subkey = key.OpenSubKey(keyName);
                        sb1 = subkey.GetValue("DisplayName") as string;
                        sb2 = subkey.GetValue("DisplayVersion") as string;
                        var outcomeupd = upd.FindIndex(x => string.Equals(x.Name, sb1, StringComparison.CurrentCultureIgnoreCase));
                        if (outcomeupd != -1)
                        {
                            string vers = upd[outcomeupd].Version;
                            string realurl = upd[outcomeupd].Url;
                            if (verChecker.NewVersionExists(sb2, vers))
                            {
                                if (upd.Exists(x => x.Name == sb1))
                                {
                                    outcome.Add(new ProgVersion(sb1, sb2, vers, realurl));
                                }
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                    catch (Exception ss)
                    {
                        string message = ss.ToString();
                        MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show(message, "SecretService");
                    }
                }

                key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
                foreach (String keyName in key.GetSubKeyNames())
                {
                    try
                    {
                        sb1 = null;
                        sb2 = null;
                        RegistryKey subkey = key.OpenSubKey(keyName);
                        sb1 = subkey.GetValue("DisplayName") as string;
                        sb2 = subkey.GetValue("DisplayVersion") as string;
                        var outcomeupd = upd.FindIndex(x => string.Equals(x.Name, sb1, StringComparison.CurrentCultureIgnoreCase));
                        if (outcomeupd != -1)
                        {
                            string vers = upd[outcomeupd].Version;
                            string realurl = upd[outcomeupd].Url;
                            if (verChecker.NewVersionExists(sb2, vers))
                            {
                                if (upd.Exists(x => x.Name == sb1))
                                {
                                    outcome.Add(new ProgVersion(sb1, sb2, vers, realurl));
                                }
                            }
                        }
                        else
                        {
                            continue;
                        }
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
                Xceed.Wpf.Toolkit.MessageBox.Show("Указанный URL недействителен.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnServerAddr_Click(object sender, RoutedEventArgs e)
        {
            upd.Clear();
            outcome.Clear();
            Checker();
        }

        private void btnUpdateSelected_Click(object sender, RoutedEventArgs e)
        {
            if (updateGrid.SelectedItems.Count > 0)
            {
                var selectedUpdate = updateGrid.SelectedItems.Cast<ProgVersion>();
                foreach (ProgVersion updatable in selectedUpdate)
                {
                    if (IsUrlValid(updatable.Url.Trim()))
                    {
                        try
                        {
                            WebDownloadClient download = new WebDownloadClient(updatable.Url);
                            string updateablefile = updatable.Name;
                            updateablefile = updateablefile + ".exe";
                            download.FileName = updateablefile;
                            download.DownloadProgressChanged += download.DownloadProgressChangedHandler;
                            download.DownloadCompleted += download.DownloadCompletedHandler;
                            download.PropertyChanged += this.mainWindow.PropertyChangedHandler;
                            download.StatusChanged += this.mainWindow.StatusChangedHandler;
                            download.DownloadCompleted += this.mainWindow.DownloadCompletedHandler;

                            if (!Directory.Exists(Settings.Default.DownloadLocation))
                            {
                                Directory.CreateDirectory(Settings.Default.DownloadLocation);
                            }
                            string filePath = Settings.Default.DownloadLocation + download.FileName;
                            string tempPath = filePath + ".tmp";

                            if (File.Exists(tempPath))
                            {
                                string message = "Уже выполняется загрузка по указнному пути.";
                                Xceed.Wpf.Toolkit.MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                            if (File.Exists(filePath))
                            {
                                File.Delete(filePath);
                            }

                            download.CheckUrl();
                            if (download.HasError)
                                return;

                            download.TempDownloadPath = tempPath;

                            download.AddedOn = DateTime.UtcNow;
                            download.CompletedOn = DateTime.MinValue;
                            download.OpenFileOnCompletion = true;
                            DownloadManager.Instance.DownloadsList.Add(download);

                            download.Start();
                            this.Close();

                            if (download.Status == DownloadStatus.Completed)
                            {
                                try
                                {
                                    foreach (Process proc in Process.GetProcessesByName(updatable.Name))
                                    {
                                        proc.Kill();
                                    }
                                }
                                catch (Exception ss)
                                {
                                    string message = ss.ToString();
                                    MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show(message, "SecretService");
                                }
                                InstallManager im = new InstallManager();

                                string n1 = null;
                                string n2 = null;
                                string n3 = null;
                                n1 = filePath;
                                n2 = im.Parameters(n1, n3);
                                im.Installation(n1, n2);
                            }
                        }
                        catch (Exception ex)
                        {
                            Xceed.Wpf.Toolkit.MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                    }
                    else
                    {
                        Xceed.Wpf.Toolkit.MessageBox.Show("Указанный в файле путь загрузки файла обновления недействителен", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}
