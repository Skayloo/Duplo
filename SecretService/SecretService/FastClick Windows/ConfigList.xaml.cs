using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Text;

namespace SGet
{
    /// <summary>
    /// Логика взаимодействия для ConfigList.xaml
    /// </summary>
    public partial class ConfigList : Window
    {
        private MainWindow mainWindow;
        string path = null;
        public ConfigList(MainWindow mainWin)
        {
            InitializeComponent();
            mainWindow = mainWin;
        }

        private void btnAddList_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.ShowDialog();
            File.WriteAllText(saveFileDialog1.FileName, tbConf.Text);
        }


        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            DialogResult result = fd.ShowDialog();

            if (result.ToString().Equals("OK"))
            {
                path = fd.FileName;
                tbInstallFolder.Text = path + '\n';
                tbConf.Text = File.ReadAllText(fd.FileName, Encoding.Default);
            }
        }

    }
}
