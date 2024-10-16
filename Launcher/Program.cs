using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Launcher
{
    internal class Program
    {
        [STAThreadAttribute]
        static void Main(string[] args)
        {
            //ルートパスを取得する
            DirectoryInfo assemblyInfo = new DirectoryInfo(Assembly.GetExecutingAssembly().Location);
            string rootPath = assemblyInfo.Parent.Parent.FullName;

            if (args.Length > 2)
            {
                //ランチャーから起動する外部プログラムによって処理を分岐する
                switch (args[2])
                {
                    case "execMain":

                        //引数と識別キーを取得する
                        string parameter = (string)ApplicationData.Current.LocalSettings.Values["parameter"];
                        string key = (string)ApplicationData.Current.LocalSettings.Values["key"];

                        //起動する外部プログラムの設定を行う
                        ProcessStartInfo processInfo = new ProcessStartInfo
                        {
                            FileName = rootPath + @"\PythonCommand\mean_interval\mean_interval.exe",
                            Arguments = parameter,
                            CreateNoWindow = true,
                            RedirectStandardOutput = true,
                            UseShellExecute = false
                        };

                        //外部プログラムを起動して処理を待つ
                        Process process = Process.Start(processInfo);
                        process.WaitForExit();

                        break;
                }
    }
}
