using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace photo_directory_organizer
{
    class ConvertFilesToAspectRatioFolders
    {

        private static readonly System.Collections.Specialized.StringCollection log = new System.Collections.Specialized.StringCollection();
        private static readonly List<(int width, int height)> aspectRatios = new List<(int width, int height)>
        {
            (1, 1), (3, 2), (4, 3), (16, 9), (5, 4), (7, 5), (16, 10), (2, 1), (3, 1), (21, 9),
            (9, 16), (5, 3), (4, 1), (11, 8) // Added new aspect ratios

        };

        public void ProcessFiles(DirectoryInfo root, string aspectRatioFolder)
        {
            WalkDirectoryTree(root, aspectRatioFolder);

            // Write out all the files that could not be processed.
            Console.WriteLine("Files with restricted access:");
            foreach (string s in log)
            {
                Console.WriteLine(s);
            }
        }

        private static void WalkDirectoryTree(DirectoryInfo root, string aspectRatioFolder)
        {
            FileInfo[] files = null;
            DirectoryInfo[] subDirs = null;

            try
            {
                files = root.GetFiles("*.*");
            }
            catch (UnauthorizedAccessException e)
            {
                log.Add(e.Message);
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }

            if (files != null)
            {
                foreach (FileInfo fi in files)
                {
                    ProcessFile(fi, aspectRatioFolder);
                }

                subDirs = root.GetDirectories();
                foreach (DirectoryInfo dirInfo in subDirs)
                {
                    WalkDirectoryTree(dirInfo, aspectRatioFolder);
                }
            }
        }

        private static void ProcessFile(FileInfo fi, string aspectRatioFolder)
        {
            int width = 0;
            int height = 0;

            // Read dimensions of image
            using (var imageStream = File.OpenRead(fi.FullName))
            {
                var decoder = BitmapDecoder.Create(imageStream, BitmapCreateOptions.IgnoreColorProfile, BitmapCacheOption.Default);
                height = decoder.Frames[0].PixelHeight;
                width = decoder.Frames[0].PixelWidth;
            }

            string aspectRatio = CalculateAspectRatio(width, height);
            string newFolder = Path.Combine(aspectRatioFolder, aspectRatio);

            if (!Directory.Exists(newFolder))
            {
                Directory.CreateDirectory(newFolder);
            }

            string destination = Path.Combine(newFolder, Path.GetFileName(fi.FullName));
            if (File.Exists(destination))
            {
                File.Delete(destination);
            }
            File.Move(fi.FullName, destination);

           
        }

        private static string CalculateAspectRatio(int width, int height)
        {
            double ratio = (double)width / height;
            (int, int)? closestMatch = null;
            double smallestDifference = double.MaxValue;

            foreach (var (arWidth, arHeight) in aspectRatios)
            {
                double arRatio = (double)arWidth / arHeight;
                double difference = Math.Abs(ratio - arRatio);

                if (difference < smallestDifference)
                {
                    smallestDifference = difference;
                    closestMatch = (arWidth, arHeight);
                }
            }

            if (closestMatch.HasValue)
            {
                return $"{closestMatch.Value.Item1}x{closestMatch.Value.Item2}";
            }
            else
            {
                return "Unknown";
            }
        }
    }

}
