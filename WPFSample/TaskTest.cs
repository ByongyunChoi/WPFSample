using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WPFSample
{
    public static class TaskTest
    {
        public delegate void valueChangedEventHandler(int numValue, string strValue);

        public static event valueChangedEventHandler ValueChanged;

        public static void Execute()
        {
            Console.WriteLine("========= Task start =========");

            Task.Run(() => IniniteFunction());

            // LongRunning 옵션을 사용하여 Threadpool이 아닌 별도의 Thread에서 동작되도록함
            Console.WriteLine("Task1 start");
            Task task1 = new Task(IniniteFunction, TaskCreationOptions.LongRunning);
            task1.Start();

            Console.WriteLine("Task2 start");
            Task.Run(() => Function2(5, 7));

            Console.WriteLine("Task3 start");
            Task<string> task3 = Task.Run(() => Function2(5, 7));
            task3.Wait();
            Console.WriteLine(task3.Result);

            Console.WriteLine("Task4 start");
            Task<string> task4 = Task.Run(() => Function2(5, 7));
            task4.GetAwaiter().OnCompleted(() =>
            {
                Console.WriteLine("Task4 Done");
            });

            Console.WriteLine("========= Task End =========");
        }

        private static void IniniteFunction()
        {
            int num = 0;

            while (true)
            {
                Thread.Sleep(10);

                num++;

                ValueChanged?.Invoke(num, "Hello");
            }
        }

        private static string Function2(int v1, int v2)
        {
            Thread.Sleep(3000);

            int sum = v1 + v2;

            return $"Function2 result : v1 : {v1} , v2 : {v2} , sum : {sum}";
        }
    }
}