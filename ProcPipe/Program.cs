/*
BSD 2-Clause License

Copyright (c) 2018, Vladimir Vasiltsov
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:

* Redistributions of source code must retain the above copyright notice, this
  list of conditions and the following disclaimer.

* Redistributions in binary form must reproduce the above copyright notice,
  this list of conditions and the following disclaimer in the documentation
  and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
using System;
using System.Collections.Generic;
using System.Diagnostics;
*/
namespace EzPz
{
    public class ProcPipe
    {
        public string Data;
        public byte Chan;
        
        public static IEnumerable<ProcPipe> Run(string command, string arguments = "")
        {
            var p =
                new Process()
                {
                    StartInfo =
                    {
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        FileName = command,
                        Arguments = arguments
                    }
                };

            p.Start();

            var chan = new[]
            {
                p.StandardOutput,
                p.StandardError
                
            };
            

            var buf = new char[0xff];
            int read;

            while (!p.HasExited)
            {
                for (byte i = 0; i < chan.Length; i++)
                {
                    if ((read = chan[i].Read(buf, 0, buf.Length)) > 0)
                    {
                        yield return new ProcPipe
                        {
                            Data = new string(buf, 0, read),
                            Chan = i
                        };
                    }
                }
            }
            for (byte i = 0; i < chan.Length; i++)
            {
                while ((read = chan[i].Read(buf, 0, buf.Length)) > 0)
                {
                    yield return new ProcPipe
                    {
                        Data = new string(buf, 0, read),
                        Chan = i
                    };
                }
            }
        }
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            foreach (var line in ProcPipe.Run(@"c:\windows\system32\cmd.exe","/c ipconfig /all"))
            {
                Console.Write(line.Data);
            }
            
        }
    }
}