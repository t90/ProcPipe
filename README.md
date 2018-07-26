# ProcPipe
Invoke a process in C#, read STDOUT, STDERR simultaneously, non blocking with IEnumerable 

Just use and read it iteratevely to get the chunks of data from the underlying process **ProcPipe.Run(@"c:\windows\system32\cmd.exe","/c ipconfig /all")**

```
foreach (var line in ProcPipe.Run(@"c:\windows\system32\cmd.exe","/c ipconfig /all"))
{
    Console.Write(line.Data);
}
```
