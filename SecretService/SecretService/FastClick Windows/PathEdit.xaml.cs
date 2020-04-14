using System.Windows;

namespace SGet
{
    /// <summary>
    /// Логика взаимодействия для PathEdit.xaml
    /// </summary>
    public partial class PathEdit : Window
    {
        private MainWindow mainWindow;
        public PathEdit(MainWindow mainWin)
        {
            InitializeComponent();
            mainWindow = mainWin;
        }
    }
}
