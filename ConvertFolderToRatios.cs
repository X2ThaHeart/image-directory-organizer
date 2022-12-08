using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using static Microsoft.WindowsAPICodePack.Shell.PropertySystem.SystemProperties.System;

namespace photo_directory_organizer
{
    public class ConvertFolderToRatios
    {
        string dirInputFolderForRatios = "";

        public string DirInputFolderForRatios
        { get { return dirInputFolderForRatios; }  set { dirInputFolderForRatios = value; } }

        static System.Collections.Specialized.StringCollection log = new System.Collections.Specialized.StringCollection();

        public void function(DirectoryInfo dir, string filename)
        {
            //DirectoryInfo di = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));


            string[] directories = Directory.GetDirectories(filename);

            for (int i = 0; i <= directories.Length; i++)
            {
                checkEachDirNameAndConvertToRatio(directories[i], filename, dir);

            }


            // Write out all the files that could not be processed.
            Console.WriteLine("Files with restricted access:");
            foreach (string s in log)
            {
                Console.WriteLine(s);
            }


        }

        static void checkEachDirNameAndConvertToRatio(string directories, string filename, DirectoryInfo dir )
        {
            //System.IO.FileInfo[] files = null;
            //System.IO.DirectoryInfo[] subDirs = null;
           
            // First, process all the files directly under this folder
            try
            {
            }
            // This is thrown if even one of the files requires permissions greater
            // than the application provides.
            catch (UnauthorizedAccessException e)
            {
                // This code just writes out the message and continues to recurse.
                // You may decide to do something different here. For example, you
                // can try to elevate your privileges and access the file again.
                log.Add(e.Message);
            }

            catch (System.IO.DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            int length = 0;
            string[] PreWidth = null;
            int width = 0;
            //FileInfo folder = new FileInfo(Path.Combine(root.ToString(), newfolder.ToString()));
            if (Directory.Exists(directories.ToString()))
            {
                PreWidth = directories.Split('X');
                var length2 = PreWidth[1];
                
                Int32.TryParse(length2.Trim(), out length);
                var width2 = PreWidth[0];
                var width3 = width2.Split("\\");
                var width4 = width3.Last();
                Int32.TryParse(width4, out width);
             
            }
            double ratio = 0;
            var ratioNew = ratio.ToString("0.0");
            ratio = (double)Decimal.Divide(width, length);

            var ratioFinal = String.Format("{0:0.0}", ratio);


            //    foreach (FileInfo dir in directories)
            //{
            //    // In this example, we only access the existing FileInfo object. If we
            //    // want to open, delete or modify the file, then
            //    // a try-catch block is required here to handle the case
            //    // where the file has been deleted since the call to TraverseTree().
            //    Console.WriteLine(dir.ToString());
            //    var width = 0;
            //    var height = 0;


            //    //read dimensions of image
            //    using (var imageStream = File.OpenRead(fi.FullName))
            //    {
            //        var decoder = BitmapDecoder.Create(imageStream, BitmapCreateOptions.IgnoreColorProfile,
            //            BitmapCacheOption.Default);
            //        height = decoder.Frames[0].PixelHeight;
            //        width = decoder.Frames[0].PixelWidth;
            //    }

            //    string newfolder = width + " X " + height;

            //    //var filename = Path.GetFileName(fi.FullName);

            //    //check directory for current folder size if so place in if not add folder and add file   dir

            //    FileInfo folder = new FileInfo(Path.Combine(root.ToString(), newfolder.ToString()));
            string destination = string.Empty; 
            if (!Directory.Exists(dir.ToString())) 
                {
                    Directory.CreateDirectory(ratioFinal.ToString());
                    destination = Path.Combine(dir.ToString(), Path.GetFileName(ratioFinal));

                    // To move a file or folder to a new location:
                    System.IO.File.Move(ratioFinal, destination);
               }
                //if dir exists
                else if (Directory.Exists(dir.ToString()))
                {
                   //move into existing folder
                   System.IO.File.Move(Path.Combine(dir.ToString(), Path.GetFileName(ratioFinal)), destination);


            }




            //}

            // Now find all the subdirectories under this directory.
            //subDirs = root.GetDirectories();

            //foreach (System.IO.DirectoryInfo dirInfo in subDirs)
            //{
            //    // Resursive call for each subdirectory.
            //    WalkDirectoryTree(dirInfo);
            //}

        }

    }
}
