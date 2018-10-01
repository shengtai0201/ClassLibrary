using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Windows
{
    public static class ProcessPrint
    {
        // 需查詢 WMI 記得加入參照及 using System.Management; 
        private static bool CheckPrintQueue(string file)
        {
            // 尋找PrintQueue有沒有檔案相同的列印工作
            var printJobs = new ManagementObjectSearcher("SELECT * FROM Win32_PrintJob").Get().Cast<ManagementObject>();
            return printJobs.Any(o => (string)o.Properties["Document"].Value == file);
        }

        public static void Print(string filePath, int timeOutValue = 180)
        {
            Console.WriteLine($"Printing... {filePath}");
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                Verb = "print",
                FileName = filePath,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };
            Process process = new Process { StartInfo = startInfo };

            try
            {
                process.Start();
                process.WaitForInputIdle();

                var timeOut = DateTime.Now.AddSeconds(timeOutValue);
                bool printing = false;  // 是否開始列印
                bool done = false;  // 是否列印完成
                string pureFileName = Path.GetFileName(filePath);   // 取純檔名部分，跟PrintQueue進行比對

                // 限定最大等待時間
                while (DateTime.Now.CompareTo(timeOut) < 0)
                {
                    if (!printing)
                    {
                        // 未開始列印前發現檔名相同的列印工作
                        if (CheckPrintQueue(pureFileName))
                        {
                            printing = true;
                            Console.WriteLine($"[{pureFileName}]列印中...");
                        }
                    }
                    else
                    {
                        // 已開始列印後，同檔名列印工作消失表示列印完成
                        if (!CheckPrintQueue(pureFileName))
                        {
                            done = true;
                            Console.WriteLine($"[{pureFileName}]列印完成");
                            break;
                        }
                    }
                    System.Threading.Thread.Sleep(100);
                }

                try
                {
                    //若程序尚未關閉，強制關閉之
                    if (!process.CloseMainWindow())
                        process.Kill();
                }
                catch { }
                if (!done)
                {
                    Console.WriteLine($"無法確認報表[{pureFileName}]列印狀態！");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error: {DateTime.Now:HH:mm:ss} {e.Message}");
            }
        }
    }
}
