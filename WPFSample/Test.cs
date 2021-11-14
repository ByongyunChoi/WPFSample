using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WPFSample
{
    internal class StringTest
    {
        public static void Execute()
        {
            try
            {
                int numValue = 12;
                string strValue = "abc";
                double doubleValue = 5.12345;

                string testValue = "C:\\VUNO\\Test";
                testValue = @"C:\VUNO\Test";
                testValue = $@"C:\VUNO\Test\{strValue}";
                string testValue1 = string.Format("numValue : {0}, {1}, {2} , {2:0.00}", numValue, strValue, doubleValue);
                string testValue2 = string.Format($"numValue : {numValue}, {strValue}, {doubleValue} , {doubleValue.ToString("0.00")}");
                string testValue3 = $"numValue : {numValue}, {strValue}, {doubleValue} , {doubleValue:0.00}";
                Console.WriteLine(testValue1);
                Console.WriteLine(testValue2);
                Console.WriteLine(testValue3);

                StringBuilder sb = new StringBuilder();
                sb.Append("aaaaa");
                sb.Append("bbbbb").Append("cccc").AppendLine();
                sb.AppendLine("dddd");
                sb.AppendLine("eeee");
                Console.WriteLine(sb.ToString());

                int intValue = Convert.ToInt32("555");      // Exception
                int intValue2 = Convert.ToInt32("aaa");
                int intValue3 = 0;

                if (int.TryParse("555", out intValue3) == false)
                {
                    Console.WriteLine("TryParse failed");
                }

                int intValue4 = 0;
                if (int.TryParse("aaa", out intValue4))
                {
                    Console.WriteLine("TryParse failed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    internal class DateTimeTest
    {
        public static void Execute()
        {
            DateTime startTime = DateTime.Now;
            Console.WriteLine("startTime : " + startTime.ToString("yyyy.MM.dd - HH:mm:ss.FFF"));

            Thread.Sleep(1000);

            DateTime endTime = DateTime.Now;
            Console.WriteLine("endTime : " + endTime.ToString("yyyy.MM.dd - HH:mm:ss.FFF"));

            DateTime nextTime = endTime.AddDays(5);
            Console.WriteLine("nextTime : " + nextTime.ToString("yyyy.MM.dd - HH:mm:ss.FFF"));

            TimeSpan ts = endTime - startTime;
            Console.WriteLine("Total sec : " + ts.TotalSeconds);
        }
    }

    internal class IOTest
    {
        public static void Execute()
        {
            string dirPath = @"C:\VUNO\DIR1";
            string fileName = "FileName.txt";
            string filePath = Path.Combine(dirPath, fileName);

            if (Directory.Exists(dirPath) == false)
            {
                Directory.CreateDirectory(dirPath);
            }

            Console.WriteLine(Path.GetDirectoryName(filePath));         // C:\VUNO\DIR1
            Console.WriteLine(Path.GetFileName(filePath));              // FileName.txt
            Console.WriteLine(Path.GetExtension(filePath));             // .txt

            File.WriteAllText(filePath, "sdkfjsdldkgjfghkdjfgh");
            string readValue = File.ReadAllText(filePath);

            FileInfo fi = new FileInfo(filePath);
            Console.WriteLine(fi.FullName);                             // C:\VUNO\DIR1\FileName.txt
            Console.WriteLine(fi.Name);                                 // FileName.txt
            Console.WriteLine(fi.Extension);                            //.txt

            DirectoryInfo di = fi.Directory;
            Console.WriteLine(di.FullName);                             // C:\VUNO\DIR1
            Console.WriteLine(di.Name);                                 // DIR1

            string temPath = Path.GetTempPath();
            string temFileName = Path.GetTempFileName();

            Console.WriteLine(temPath);                                 // C:\Users\user\AppData\Local\Temp\
            Console.WriteLine(temFileName);                             // C:\Users\user\AppData\Local\Temp\tmp2049.tmp
        }
    }
}