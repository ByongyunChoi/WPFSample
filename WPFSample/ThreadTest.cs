using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WPFSample
{
    public static class ThreadTest
    {
        public enum ChangeType
        {
            Enqueue,
            Dequeue
        }

        public delegate void QueueChangedEventHandler(ChangeType type, int numValue);

        public static event QueueChangedEventHandler QueueChanged;

        private static Thread enqueueThread;
        private static Thread dequeueThread;
        private static Queue<int> queue = new Queue<int>();
        private static ConcurrentQueue<int> safeQueue = new ConcurrentQueue<int>();

        public static void Execute()
        {
            int minWorkerThreads = 0;
            int minCompletionPortThreads = 0;
            ThreadPool.GetMinThreads(out minWorkerThreads, out minCompletionPortThreads);

            int maxWorkerThreads = 0;
            int maxCompletionPortThreads = 0;
            ThreadPool.GetMaxThreads(out maxWorkerThreads, out maxCompletionPortThreads);

            Console.WriteLine("Min Worker Threads : {0}, max Worker Threads : {1}", minWorkerThreads, maxWorkerThreads);
            Console.WriteLine("Min CompletionPort Threads : {0}, max CompletionPort Threads : {1}", minCompletionPortThreads, maxCompletionPortThreads);

            enqueueThread = new Thread(EnqueueThreadProc);
            enqueueThread.IsBackground = true;
            enqueueThread.Start();

            dequeueThread = new Thread(DequeueThreadProc);
            dequeueThread.IsBackground = true;
            dequeueThread.Start();
        }

        private static void DequeueThreadProc()
        {
            while (true)
            {
                Thread.Sleep(1);

                int value = 0;

                lock (queue)
                {
                    if (queue.Count == 0) continue;
                    value = queue.Dequeue();
                }

                if (safeQueue.TryDequeue(out value) == false)
                {
                    Console.WriteLine("Dequeue Failed");
                }

                QueueChanged?.Invoke(ChangeType.Dequeue, value);
            }
        }

        private static void EnqueueThreadProc()
        {
            int value = 0;

            while (true)
            {
                Thread.Sleep(1);

                lock (queue)
                {
                    queue.Enqueue(value);
                }

                safeQueue.Enqueue(value);

                value++;

                QueueChanged?.Invoke(ChangeType.Enqueue, value);
            }
        }
    }
}