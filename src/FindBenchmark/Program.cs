using BenchmarkDotNet.Running;
using FindBenchmark;
using System.Collections.Specialized;

//BenchmarkRunner.Run<FIndAsyncBenchmark>();
FIndAsyncBenchmark f = new();
Console.ReadLine();
await f.BenchmarkFindAsync();
