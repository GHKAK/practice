using BenchmarkDotNet.Running;
using CollectionsBenchmark;
BenchmarkRunner.Run<AddingBenchmark2>();
BenchmarkRunner.Run<CloningBenchmark>();
BenchmarkRunner.Run<AddingBenchmark>();
BenchmarkRunner.Run<RemovingBenchmark>();
BenchmarkRunner.Run<FindingBenchmark>();
BenchmarkRunner.Run<SortingBenchmark>();
BenchmarkRunner.Run<InsertingBenchmark>();