using System.Windows;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using SGet.Install;
using System;

namespace SGet
{
    public partial class Installation : Window
    {
        string name;
        string path;
        string param;
        private MainWindow mainWindow;
        ObservableCollection<Data> data;
        public Installation(MainWindow mainWin)
        {
            InitializeComponent();
            mainWindow = mainWin;
            data = new ObservableCollection<Data>();
            progressBar1.IsIndeterminate = false;
        }
        public class Data
        {
            public Data(string name, string path, string param)
            {
                this.Name = name;
                this.Path = path;
                this.Param = param;
            }
            public string Name { get; set; }
            public string Path { get; set; }
            public string Param { get; set; }
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            DialogResult result = fd.ShowDialog();

            if (result.ToString().Equals("OK"))
            {
                name = fd.FileName;
                tbInstallFolder.Text = name;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            InstallManager im = new InstallManager();
            if (tbInstallParams.Text == "")
            {
                param = im.Parameters(name, path);
            }
            else
            {
                param = tbInstallParams.Text;
            }
            
            data.Add(new Data(name, path, param));

            installGrid.ItemsSource = data;
        }

        private void btnInstall_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                progressBar1.IsIndeterminate = true;
                progressBar1.Visibility = Visibility.Visible;
                InstallManager im = new InstallManager();
                for (int datas = 0; datas < data.Count; datas++)
                {
                    string n1 = null;
                    string n2 = null;
                    n1 = data[datas].Name;
                    n2 = data[datas].Param;
                    im.Installation(n1, n2);
                }
                progressBar1.IsIndeterminate = false;
                progressBar1.Visibility = Visibility.Hidden;
            }
            catch (Exception ss)
            {
                string message = ss.ToString();
                MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show(message, "SecretService");
            }
        }

        private void btnBrowse_Click1(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbDialog = new FolderBrowserDialog();
            fbDialog.Description = "Выберите папку установки";
            fbDialog.ShowNewFolderButton = true;
            DialogResult result = fbDialog.ShowDialog();

            if (result.ToString().Equals("OK"))
            {
                path = fbDialog.SelectedPath;
                tbInstallOutcome.Text = path;
            }
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (installGrid.SelectedItems.Count > 0)
            {
                var selectedInstall = installGrid.SelectedItems.Cast<Data>();
                var installToDelete = new List<Data>();
                foreach (Data install in selectedInstall)
                {
                    installToDelete.Add(install);
                }
                foreach (var install in installToDelete)
                {
                    data.Remove(install);
                }
            }
        }
        
    }
}