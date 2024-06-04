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
        public string rootFolderPath = null;
        public string aspectRatioFolderPath = null;



        public MainWindow()
        {
            //can work without for some reason other wise an error
            InitializeComponent();
        }

        private void metasize_folders_button_Click(object sender, RoutedEventArgs e)
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
            MessageBox.Show("Folder Sizes Completed");
            return;

        }

        public ConvertFilesToImageFolders fileSizeFoldersAndMove(DirectoryInfo dir)
        {
            ConvertFilesToImageFolders recursiveFileSearch = new ConvertFilesToImageFolders();
            recursiveFileSearch.function(dir);
            return recursiveFileSearch;
        }


        //first choose source button for where folders are stored
        private void SelectRootFolderButton_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                rootFolderPath = dialog.FileName;
                MessageBox.Show("Selected Root Folder: " + rootFolderPath);
            }
        }


        private void SelectAspectRatioFolderButton_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                aspectRatioFolderPath = dialog.FileName;
                MessageBox.Show("Selected Aspect Ratio Folder: " + aspectRatioFolderPath);
            }
        }




        private void CreateAspectRatiosButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(rootFolderPath) || string.IsNullOrEmpty(aspectRatioFolderPath))
            {
                MessageBox.Show("Please select both root and aspect ratio folders.");
                return;
            }

            DirectoryInfo rootDir = new DirectoryInfo(rootFolderPath);
            if (rootDir.Exists)
            {
                ConvertFilesToAspectRatioFolders fileProcessor = new ConvertFilesToAspectRatioFolders();
                fileProcessor.ProcessFiles(rootDir, aspectRatioFolderPath);
                MessageBox.Show("Aspect Ratio conversion complete");
                return;
            }
        }
    }


   
}
