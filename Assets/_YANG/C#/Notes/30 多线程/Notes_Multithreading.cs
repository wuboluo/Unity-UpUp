﻿using System;
using System.Threading;

namespace Yang.CSharp.Notes
{
    internal class Notes_Multithreading
    {
        private static readonly bool isRunning = true;
        private static readonly object obj = new();

        private static void Main(string[] args)
        {
            // ---------------------------------------- 了解线程之前先要了解进程
            // 进程（Process）是计算机中的程序关于某数据集合的一次运行活动
            // 是系统进行资源分配和调度的基本单位，是操纵系统结构的基础
            // 简单理解：
            // 打开一个应用程序就是在操作系统上开启了一个进程
            // 进程之间可以相互独立运行，互不干扰
            // 进程之间也可以相互访问，操作


            // ---------------------------------------- 什么是线程
            // 操作系统能够进行运算调度的最小单位
            // 它被包含在进程之中，是进程中的实际运作单位
            // 一条线程指的是进程中一个单一顺序的控制流，一个进程中可以并发个线程
            // 我们目前写的程序都是在主线程中
            // 简单理解：
            // 就是代码从上到下运行的一条“管道”


            // ---------------------------------------- 什么是多线程
            // 我们可以通过代码，开启新的线程
            // 可以同时运行代码的多条“管道”，就叫多线程


            // ---------------------------------------- 语法相关
            // Thread
            // 需要 using System.Threading
            // 1，声明一个新的线程
            // 注意：线程执行的代码，需要封装到一个函数当中
            Thread t = new Thread(NewThreadLogic);

            // 2，开启线程
            t.Start();

            // 3，设置为后台线程
            // 当前台线程都结束了的时候，整个程序也就结束了，即使还有后台线程正在运行
            // 后台线程不会防止应用程序的进程被终止掉
            // 如果不设置为后台线程，可能导致进程无法正常关闭
            t.IsBackground = true;

            // 4，关闭释放一个线程
            // 如果开启的线程中不是死循环，是能够结束的逻辑，那么不用刻意的去关闭它
            // 如果是死循环，想要中止这个线程：
            // 4.1，死循环中的 bool标识
            //isRunning = false;
            // 4.2，通过线程提供的方法（注意在.Net core版本中无法中止，会报错）
            try
            {
                //t.Abort();
                //t = null;
            }
            catch
            {
            }


            // ---------------------------------------- 线程之间共享数据
            // 多个线程使用的内存是共享的，都属于该应用程序（进程）
            // 所以要注意：当多线程，同时操纵同一片内存区域时可能会出问题
            // 可以通过加锁的形式避免问题
            // lock
            // 当我们在多个线程当中，想要访问同样的东西，在进行逻辑处理时，为了避免不必要的逻辑顺序执行差错
            // lock(引用类型对象)

            while (true)
                lock (obj)
                {
                    Console.SetCursorPosition(0, 0);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("●");
                }


            // ---------------------------------------- 多线程对我们的意义
            // 可以用多线程专门处理一些复杂耗时的逻辑，比如寻路，网络通信等
        }


        private static void NewThreadLogic()
        {
            // 新开线程，执行的代码逻辑，在该函数的语句块中
            while (isRunning)
                // ---------------------------------------- 线程休眠
                // 让线程休眠多少毫秒
                // 在哪个线程里执行，就休眠哪个线程
                //Thread.Sleep(20);

                //Console.WriteLine("这是一个新开线程代码逻辑");

                lock (obj)
                {
                    Console.SetCursorPosition(10, 10);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("■");
                }
        }
    }
}