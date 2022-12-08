using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.WindowsAPICodePack.Dialogs;


namespace photo_directory_organizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ConvertFolderToRatios convertFolderToRatios = new ConvertFolderToRatios();
        public string filename = null;
        public DirectoryInfo Dir { get; private set; }

        public MainWindow()
        {
            //can work without for some reason other wise an error
            //InitializeComponent();
        }

        private void rootfolder_button_Click(object sender, RoutedEventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = path;
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                MessageBox.Show("You selected: " + dialog.FileName);
            }

            var filename = dialog.FileName;

            DirectoryInfo di = new DirectoryInfo(filename);

            if (di.Exists)
                fileSizeFoldersAndMove(di);

        }

        public ConvertFilesToImageFolders fileSizeFoldersAndMove(DirectoryInfo dir)
        {
            ConvertFilesToImageFolders recursiveFileSearch = new ConvertFilesToImageFolders();
            recursiveFileSearch.function(dir);
            return recursiveFileSearch;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = path;
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                MessageBox.Show("You selected: " + dialog.FileName);
            }

            filename = dialog.FileName;

            Dir = new DirectoryInfo(filename);

            if (Dir.Exists)
                convertFolderToRatios.DirInputFolderForRatios = filename;
                

        }

        public ConvertFolderToRatios foldersToRatioSizes(DirectoryInfo dir)
        {
            ConvertFolderToRatios recursiveFileSearch = new ConvertFolderToRatios();

            recursiveFileSearch.function(dir, filename);


            return recursiveFileSearch;


        }

        private void convertFoldersToRatioFolders_Click(object sender, RoutedEventArgs e)
        {
            foldersToRatioSizes(Dir);
        }
    }
}
