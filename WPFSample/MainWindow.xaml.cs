using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace WPFSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // const는 변수선언시에만 값할당이 가능
        private const int constValue = 555;

        // readonly는 변수 선언시와 생성자에서만 가능
        private readonly int readonlyValue = 666;

        public MainWindow()
        {
            InitializeComponent();

            // constValue = 444; //const error
            readonlyValue = 777;
        }

        private void Method1()
        {
            //constValue = 444;     //error
            //readonlyValue = 777;  //error
        }

        private void btnStringSample_Click(object sender, RoutedEventArgs e)
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
        }

        private void btnDateTime_Click(object sender, RoutedEventArgs e)
        {
            DateTime startTime = DateTime.Now;
            Console.WriteLine(startTime.ToString("yyyy.MM.dd - HH:mm:ss.FFF"));
            Thread.Sleep(1000);
            DateTime endTime = DateTime.Now;
            Console.WriteLine("endTime : " + endTime.ToString("yyyy.MM.dd - HH:mm:ss.FFF"));
            DateTime nextTime = endTime.AddDays(5);
            Console.WriteLine("nextTime : " + nextTime.ToString("yyyy.MM.dd - HH:mm:ss.FFF"));

            TimeSpan sp = endTime - startTime;
            Console.WriteLine("Total sec : " + sp.TotalSeconds);
        }

        private class Person : INotifyPropertyChanged
        {
            private string name;

            public string Name
            {
                get
                {
                    return name;
                }
                set
                {
                    name = value;
                }
            }

            public string Address { get; private set; }
            public int Age { get; set; }

            private string lastName;
            private string middleName;
            private string firstName;

            public event PropertyChangedEventHandler PropertyChanged;

            public string FullName
            {
                get
                {
                    return $"{lastName}^{firstName}^{middleName}";
                }
            }

            public Person()
            {
            }
        }

        private void btnProperty_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}