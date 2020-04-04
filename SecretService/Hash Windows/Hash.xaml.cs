using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Forms;

namespace SGet
{
    public partial class Hash : Window
    {
        string path = @"C:\SecretService";
        string name;
        string status;
        private MainWindow mainWindow;
        ObservableCollection<Data> data;
        public Hash(MainWindow mainWin)
        {
            InitializeComponent();
            mainWindow = mainWin;
            data = new ObservableCollection<Data>();
            Checker();
            ExistedFiles();
        }

        public class Data
        {
            public Data(string name, string time, string status)
            {
                this.Name = name;
                this.Time = time;
                this.Status = status;
            }
            public string Name { get; set; }
            public string Time { get; set; }
            public string Status { get; set; }
        }

        private void Checker()
        {
            if (!Directory.Exists(path))
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }
        }

        private void btnAddHashFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            DialogResult result = fd.ShowDialog();

            if (result.ToString().Equals("OK"))
            {
                name = fd.FileName;
                AddFiles(name);
                tbHash.Text = name;
            }
            
        }

        private void ExistedFiles()
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            foreach (var item in dir.GetFiles())
            {
                string existing = Path.ChangeExtension(item.Name, "exe");
                status = "Необходима проверка";
                data.Add(new Data(existing, item.LastWriteTimeUtc.ToString(), status));
            }
            hashGrid.ItemsSource = data;
        }

        private void AddFiles(string name)
        {
            string hashed = path + "\\" + Path.GetFileName(name);
            hashed = Path.ChangeExtension(@hashed, "txt");

            StreamReader st;
            FileInfo fi = new FileInfo(hashed);
            if (!File.Exists(hashed))
            {
                st = new StreamReader(File.Create(hashed));
                st.Close();
                fi.Attributes = FileAttributes.Normal;
                bool flag_not_to_do_go_to = true;
                while (flag_not_to_do_go_to)
                {
                    try
                    {
                        string resolution = ComputeMD5Hash(name);
 
                        using (StreamWriter sw = new StreamWriter(hashed, true, System.Text.Encoding.Default))
                        {
                            sw.WriteLine(resolution);
                            sw.WriteLine(@name);
                            sw.Close();
                        }
                        fi.Attributes = FileAttributes.System | FileAttributes.Hidden | FileAttributes.ReadOnly;
                        flag_not_to_do_go_to = false;
                        status = "OK";
                    }
                    catch (System.IO.IOException e)
                    {
                        flag_not_to_do_go_to = true;
                    }
                }
                data.Add(new Data(Path.GetFileName(name), fi.LastWriteTimeUtc.ToString(), status));
                hashGrid.ItemsSource = data;
            }
        }


        private string ComputeMD5Hash(string path)
        {
            using (FileStream fs = System.IO.File.OpenRead(path))
            using (MD5 md5 = new MD5CryptoServiceProvider())
            {
                byte[] checkSum = md5.ComputeHash(fs);
                string result = BitConverter.ToString(checkSum).Replace("-", String.Empty);
                return result;
            }
        }

        private void btnCheckSelectedFiles_Click(object sender, RoutedEventArgs e)
        {
            if (hashGrid.SelectedItems.Count > 0)
            {
                var selectedHash = hashGrid.SelectedItems.Cast<Data>();
                var hashToClear = new List<Data>();
                foreach (Data hashing in selectedHash.ToArray())
                {
                    DirectoryInfo dir = new DirectoryInfo(path);
                    foreach (var item in dir.GetFiles())
                    {
                        string existing = Path.ChangeExtension(item.Name, "exe");
                        if (hashing.Name == existing)
                        {
                            try
                            {
                                string[] read = File.ReadAllLines(item.FullName, System.Text.Encoding.Default);
                                string n1 = read[0];
                                string n2 = read[1];
                                string checking = null;
                                if (File.Exists(n2))
                                {
                                    checking = ComputeMD5Hash(n2);
                                }
                                else
                                {
                                    MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show("Невозможно найти файл проверки", "SecretService");
                                }
                                if (checking == n1)
                                {
                                    hashToClear.Add(hashing);
                                    status = "OK";
                                    data.Add(new Data(existing, item.LastWriteTimeUtc.ToString(), status));
                                }
                                else
                                {
                                    hashToClear.Add(hashing);
                                    status = "Файл искажен";
                                    data.Add(new Data(existing, item.LastWriteTimeUtc.ToString(), status));
                                }
                            }
                            catch (Exception ss)
                            {
                                string message = ss.ToString();
                                MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show(message, "SecretService");
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                    foreach (var clearhash in hashToClear)
                    {
                        data.Remove(clearhash);
                    }
                }
                hashGrid.ItemsSource = data;
            }
        }
    }
}
