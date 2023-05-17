using BenchmarkDotNet.Running;
using FindBenchmark;
using Passports.Repositories;
using System.Collections.Specialized;

//BenchmarkRunner.Run<FindAsyncBenchmark>();

LocalRepositoryNew rep = new();
var x = await rep.FindInChunksAsync(9201, 335501);
Console.WriteLine(x);

x = await rep.FindInChunksAsync(9201, 335501);
Console.WriteLine(x);
x = await rep.FindInChunksAsync(9201, 335501);
Console.WriteLine(x);
x = await rep.FindInChunksAsync(9201, 335501);
Console.WriteLine(x);
x = await rep.FindInChunksAsync(9201, 335501);
Console.WriteLine(x);
x = await rep.FindInChunksAsync(9201, 335501);
Console.WriteLine(x);
x = await rep.FindInChunksAsync(9201, 335501);
Console.WriteLine(x);
x = await rep.FindInChunksAsync(9201, 335501);
Console.WriteLine(x);
x = await rep.FindInChunksAsync(9201, 335501);
Console.WriteLine(x);
x = await rep.FindInChunksAsync(9201, 335501);
Console.WriteLine(x);
x = await rep.FindInChunksAsync(9201, 335501);
Console.WriteLine(x);
x = await rep.FindInChunksAsync(9201, 335501);
Console.WriteLine(x);
x = await rep.FindInChunksAsync(9201, 335501);
Console.WriteLine(x);
Console.ReadLine();
//return;

//FindAsyncBenchmark f = new();
//Console.ReadLine();
