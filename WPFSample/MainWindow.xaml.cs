﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
            StringTest.Execute();
        }

        private void btnDateTime_Click(object sender, RoutedEventArgs e)
        {
            DateTimeTest.Execute();
        }

        private void btnProperty_Click(object sender, RoutedEventArgs e)
        {
            PropertyTest.Execute();
        }

        private void btnFileDirectory_Click(object sender, RoutedEventArgs e)
        {
            IOTest.Execute();
        }

        private void btnWindowMessage_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnAsync_Click(object sender, RoutedEventArgs e)
        {
            AsyncTest();
        }

        private async void AsyncTest()
        {
            int startValue = 0;

            while (true)
            {
                await Task.Delay(100);

                lblAsyncValue.Content = startValue.ToString();

                startValue++;
            }
        }

        private void btnTask_Click(object sender, RoutedEventArgs e)
        {
            TaskTest.ValueChanged += TaskTest_valueChanged;
            TaskTest.Execute();
        }

        private void TaskTest_valueChanged(int numValue, string strValue)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                lblTaskValue.Content = $"numValue : {numValue} , strValue : {strValue}";
            }));
        }

        private void btnThread_Click(object sender, RoutedEventArgs e)
        {
            ThreadTest.QueueChanged += ThreadTest_QueueChanged;
            ThreadTest.Execute();
        }

        private void ThreadTest_QueueChanged(ThreadTest.ChangeType type, int value)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (type == ThreadTest.ChangeType.Dequeue)
                {
                    lblDequeueValue.Content = value.ToString();
                }
                else
                {
                    lblEnqueueValue.Content = value.ToString();
                }
            }));
        }
    }
}