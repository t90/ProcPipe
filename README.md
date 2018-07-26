# ProcPipe
Invoke a process in C#, read STDOUT, STDERR simultaneously, non blocking with IEnumerable 

**ProcPipe.Run(@"c:\windows\system32\cmd.exe","/c ipconfig /all")**

Just use this method and read from it iteratevely, to get the chunks of data from the underlying process. You can user Where(ch => ch.Handler = 0) to keep only STDOUT output or Where(ch => ch.Handler = 1) to read stderr only

```
foreach (var line in ProcPipe.Run(@"c:\windows\system32\cmd.exe","/c ipconfig /all"))
{
    Console.Write(line.Data);
}
```
