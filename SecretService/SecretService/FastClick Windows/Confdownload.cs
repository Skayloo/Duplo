using SGet.Properties;
using System;
using System.IO;
using System.Windows;

namespace SGet
{
    public partial class Confdownload
    {
        MainWindow mainWindow = new MainWindow();

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

        public void Donwload_and_install(string url, string path)
        {
            if (IsUrlValid(url))
            {
                try
                {
                    WebDownloadClient download = new WebDownloadClient(url);
                    string updateablefile = url.Substring(url.LastIndexOf("/") + 1);
                    if (path != null)
                    {
                        download.execpath = @path;
                    }
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
