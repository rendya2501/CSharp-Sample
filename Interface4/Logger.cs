using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Interface4
{
    enum LogLevel
    {
        Information,
        Warning,
        Error
    }

    /// <summary>
    /// 今まではILoggerを実装したクラスに共通の処理を実装するためには、
    /// 抽象クラスを継承させるしかなかったが、
    /// デフォルトメソッドにより、柔軟性の高い設計にすることができるようになったという例
    /// </summary>
    interface ILogger
    {
        void WriteCore(LogLevel level, string message);

        /// <summary>
        /// 今まではこの実装を抽象クラスで定義して各クラスで再定義しなければならなかった
        /// </summary>
        /// <param name="message"></param>
        void WriteInformation(string message)
        {
            WriteCore(LogLevel.Information, message);
        }

        void WriteWarning(string message)
        {
            WriteCore(LogLevel.Warning, message);
        }

        void WriteError(string message)
        {
            WriteCore(LogLevel.Error, message);
        }
    }

    class ConsoleLogger : ILogger
    {
        public void WriteCore(LogLevel level, string message)
        {
            Console.WriteLine($"{level}: {message}");
        }
    }

    class TraceLogger : ILogger
    {
        public void WriteCore(LogLevel level, string message)
        {
            switch (level)
            {
                case LogLevel.Information:
                    Trace.TraceInformation(message);
                    break;
                case LogLevel.Warning:
                    Trace.TraceWarning(message);
                    break;
                case LogLevel.Error:
                    Trace.TraceError(message);
                    break;
            }
        }
    }
}


/// <summary>
/// デフォルトメソッドがなければ抽象クラス使ってこんなことしなければならなかったという例
/// </summary>
namespace hoge
{
    enum LogLevel
    {
        Information,
        Warning,
        Error
    }

    interface ILogger
    {
        void WriteCore(LogLevel level, string message);
    }

    /// <summary>
    /// 抽象クラスでインタフェースを実装し、各共通処理を定義する
    /// </summary>
    abstract class WriteClass : ILogger
    {
        /// <summary>
        /// abstract宣言を忘れずに
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        public abstract void WriteCore(LogLevel level, string message);

        public void WriteInformation(string message)
        {
            WriteCore(LogLevel.Information, message);
        }

        public void WriteWarning(string message)
        {
            WriteCore(LogLevel.Warning, message);
        }

        public void WriteError(string message)
        {
            WriteCore(LogLevel.Error, message);
        }
    }

    /// <summary>
    /// 抽象クラスを継承してインターフェースと共通処理を実装する
    /// </summary>
    internal class ConsoleLogger : WriteClass
    {
        /// <summary>
        /// abstractなのでoverrideする
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        public override void WriteCore(LogLevel level, string message)
        {
            Console.WriteLine($"{level}: {message}");
        }
    }

    class TraceLogger : WriteClass
    {
        public override void WriteCore(LogLevel level, string message)
        {
            switch (level)
            {
                case LogLevel.Information:
                    Trace.TraceInformation(message);
                    break;
                case LogLevel.Warning:
                    Trace.TraceWarning(message);
                    break;
                case LogLevel.Error:
                    Trace.TraceError(message);
                    break;
            }
        }
    }

    class Program
    {
        static void Dummy(string[] args)
        {
            WriteClass consoleLogger = new ConsoleLogger();
            consoleLogger.WriteWarning("Cool no code duplication!");

            WriteClass traceLogger = new TraceLogger();
            traceLogger.WriteInformation("Cool no code duplication!");
        }
    }
}