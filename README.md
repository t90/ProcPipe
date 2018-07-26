# ProcPipe
Invoke a process in C#, read STDOUT, STDERR simultaneously, non blocking with IEnumerable 

**ProcPipe.Run(@"c:\windows\system32\cmd.exe","/c ipconfig /all")**
Just use and read it iteratevely to get the chunks of data from the underlying process. You can user Where

```
foreach (var line in ProcPipe.Run(@"c:\windows\system32\cmd.exe","/c ipconfig /all"))
{
    Console.Write(line.Data);
}
```
