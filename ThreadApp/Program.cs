using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace ThreadApp
{
    class Program
    {
        static void Main(string[] args)
        {
            File.WriteAllText(@"..\..\FileByThread_1.txt", "");
            File.WriteAllText(@"..\..\FileByThread_22.txt", "");

            Thread[] threadArray = new Thread[4];

            for (int i = 1; i <= 4; i++)
            {
                Thread t = new Thread(() => Method());
                threadArray[i - 1] = t;
                if (i % 2 != 0)
                {
                    t.Name = "THREAD_" + Convert.ToString(i);
                    Console.WriteLine("Thread_{0} is created", i);
                }
                else
                {
                    t.Name = "THREAD_" + Convert.ToString(i) + Convert.ToString(i);
                    Console.WriteLine("Thread_{0}{1} is created", i, i);
                }
            }
            Stopwatch s = new Stopwatch();

            s.Start();

            for (int i = 0; i <= 1; i++)
            {
                threadArray[i].Start();
            }
            threadArray[0].Join();
            threadArray[1].Join();

            s.Stop();
            TimeSpan ts = s.Elapsed;
            Console.WriteLine("\nElapsed time for first two threads is {0}\n", ts);

            for (int i = 2; i <= 3; i++)
            {
                threadArray[i].Start();
            }

            Console.ReadLine();
        }

        public static void Method()
        {
            if (Thread.CurrentThread.Name == "THREAD_1")
            {
                int[,] matrix = new int[100, 100];

                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    matrix[i, i] = 1;
                }
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    try
                    {
                        File.AppendAllText(@"..\..\FileByThread_1.txt", "\n");
                        for (int j = 0; j < matrix.GetLength(0); j++)
                        {
                            File.AppendAllText(@"..\..\FileByThread_1.txt", Convert.ToString(matrix[i, j]));
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            else if (Thread.CurrentThread.Name == "THREAD_22")
            {
                Random random = new Random();
                int i = 0;

                while (i < 1000)
                {
                    int number = random.Next(0, 10001);
                    if (number % 2 != 0)
                    {
                        try
                        {
                            File.AppendAllText(@"..\..\FileByThread_22.txt", Convert.ToString(number) + "\n");
                            i++;
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            else if (Thread.CurrentThread.Name == "THREAD_3")
            {
                string[] s = File.ReadAllLines(@"..\..\FileByThread_1.txt");

                for (int i = 0; i < s.Length; i++)
                {
                    Console.WriteLine(s[i]);
                }
            }
            else if (Thread.CurrentThread.Name == "THREAD_44")
            {
                string[] s = File.ReadAllLines(@"..\..\FileByThread_22.txt");

                int sum = 0;

                for (int i = 0; i < s.Length; i++)
                {
                    sum = sum + Convert.ToInt32(s[i]);
                }
                Console.WriteLine("\nsum of odd numbers is {0}", sum);
            }
        }
    }
}
