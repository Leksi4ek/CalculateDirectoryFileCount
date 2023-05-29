using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DirectoryFileCountCalculator
{
    class Program
    {
        static bool HasValidAttributes(FileInfo file)
        {
            var attributes = file.Attributes;
            return attributes != FileAttributes.Hidden &&
                   attributes != FileAttributes.ReadOnly &&
                   attributes != FileAttributes.Archive;
        }

        static int CalculateDirectoryFileCount(DirectoryInfo directory, string filePattern)
        {
            if (!directory.Exists)
            {
                Console.WriteLine("Invalid directory path.");
                return 0;
            }

            int fileCount = 0;

            foreach (var file in directory.GetFiles(filePattern, SearchOption.AllDirectories))
            {
                if (HasValidAttributes(file))
                {
                    fileCount++;
                }
            }

            return fileCount;
        }

        static void Main(string[] args)
        {
            List<string> directoryPaths = new List<string>();
            string directoryPath;
            string filePattern;

            Console.WriteLine("Enter directory paths (enter 'done' to finish):");

            while (true)
            {
                Console.Write("Directory path: ");
                directoryPath = Console.ReadLine();

                if (directoryPath == "done")
                {
                    break;
                }

                directoryPaths.Add(directoryPath);
            }

            Console.Write("Enter file pattern (For example: *.txt): ");
            filePattern = Console.ReadLine();

            if (string.IsNullOrEmpty(filePattern))
            {
                Console.WriteLine("Invalid file pattern.");
                return;
            }

            int totalFileCount = 0;

            foreach (var path in directoryPaths)
            {
                var directory = new DirectoryInfo(path);
                totalFileCount += CalculateDirectoryFileCount(directory, filePattern);
            }

            Console.WriteLine($"Total number of files matching pattern {filePattern} in the directories: {totalFileCount}");
        }
    }
}


