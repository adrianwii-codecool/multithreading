using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Multithreading
{
    public class Calculator
    {
        private readonly object _lock = new object();

        public int Add(int x, int y)
        {
            return x + y;
        }

        public int AddInLoop(int x, int y)
        {
            int suma = x;
            var (counter, increment) = y >= 0 ? (y,  1) : (-y, -1);

            for(int i = 0; i < counter; i++)
            {
                suma += increment;
            }
            return suma;
        }


        public int AddInLoopWithThreads(int x, int y)
        {
            int suma = x;
            var (counter, increment) = y >= 0 ? (y, 1) : (-y, -1);
            var threads = new List<Thread>();

            for (int i = 0; i < counter; i++)
            {
                var thread = new Thread(() =>
                {
                    int result = suma;
                    Thread.Sleep(new Random().Next(1, 15));
                    result += increment;
                    suma = result;
                });

                thread.Start();
                threads.Add(thread);
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            return suma;
        }


        public int AddInLoopWithThreadsLock(int x, int y)
        {
            int suma = x;
            var (counter, increment) = y >= 0 ? (y, 1) : (-y, -1);
            var threads = new List<Thread>();

            for (int i = 0; i < counter; i++)
            {
                var thread = new Thread(() =>
                {
                    //int result = suma;
                    Thread.Sleep(new Random().Next(1, 15));
                    //result += increment;
                    //suma = result;
                    Interlocked.Add(ref suma, increment);
                });

                thread.Start();
                threads.Add(thread);
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            return suma;
        }


        public int AddInLoopWithThreadsLockPrimitive(int x, int y)
        {
            int suma = x;
            var (counter, increment) = y >= 0 ? (y, 1) : (-y, -1);
            var threads = new List<Thread>();

            for (int i = 0; i < counter; i++)
            {
                var thread = new Thread(() =>
                {
                    //int result = suma;
                    Thread.Sleep(new Random().Next(1, 15));
                    //result += increment;
                    //suma = result;
                    //Interlocked.Add(ref suma, increment);

                    lock (_lock)
                    {
                        suma += increment;

                    }
                });

                thread.Start();
                threads.Add(thread);
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            return suma;
        }


        public int AddInLoopWithThreadsMutex(int x, int y)
        {
            int suma = x;
            var (counter, increment) = y >= 0 ? (y, 1) : (-y, -1);
            var threads = new List<Thread>();

            using var mutex = new Mutex();

            for (int i = 0; i < counter; i++)
            {
                var thread = new Thread(() =>
                {
                    Thread.Sleep(new Random().Next(1, 15));

                    // Complicated operation

                    mutex.WaitOne();
                    suma += increment;
                    mutex.ReleaseMutex();
                });

                thread.Start();
                threads.Add(thread);
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            return suma;
        }
    }
}
