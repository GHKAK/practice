using BenchmarkDotNet.Attributes;

namespace CollectionsBenchmark
{
    [MemoryDiagnoser]
    public class ListVSDictionary
    {
        const int count = 50;
        [Benchmark, BenchmarkCategory("Add")]
        public void ListAdd()
        {
            var list = new List<MyClass>();
            for (int i = 0; i < count; i++)
            {
                list.Add(new MyClass { X = i, Y = i });
            }
        }
        [Benchmark, BenchmarkCategory("Add")]
        public void DictionaryAdd()
        {
            var dict = new Dictionary<MyClass, int>();
            for (int i = 0; i < count; i++)
            {
                dict.Add(new MyClass { X = i, Y = i }, i);
            }
        }
        List<MyClass> _l = new List<MyClass>
        {
            new MyClass { X = 0, Y = 0 },
            new MyClass { X = 1, Y = 1 },
            new MyClass { X = 2, Y = 2 },
            new MyClass { X = 3, Y = 3 },
            new MyClass { X = 4, Y = 4 },
            new MyClass { X = 5, Y = 5 },
            new MyClass { X = 6, Y = 6 },
            new MyClass { X = 7, Y = 7 },
            new MyClass { X = 8, Y = 8 },
            new MyClass { X = 9, Y = 9 },
            new MyClass { X = 10, Y = 10 },
            new MyClass { X = 11, Y = 11 },
            new MyClass { X = 12, Y = 12 },
            new MyClass { X = 13, Y = 13 },
            new MyClass { X = 14, Y = 14 },
        };
        Dictionary<MyClass, int> _dict = new Dictionary<MyClass, int>
        {
            { new MyClass { X = 0, Y = 0 }, 0 },
            { new MyClass { X = 1, Y = 1 }, 1 },
            { new MyClass { X = 2, Y = 2 }, 2 },
            { new MyClass { X = 3, Y = 3 }, 3 },
            { new MyClass { X = 4, Y = 4 }, 4 },
            { new MyClass { X = 5, Y = 5 }, 5 },
            { new MyClass { X = 6, Y = 6 }, 6 },
            { new MyClass { X = 7, Y = 7 }, 7 },
            { new MyClass { X = 8, Y = 8 }, 8 },
            { new MyClass { X = 9, Y = 9 }, 9 },
            { new MyClass { X = 10, Y = 10 }, 10 },
            { new MyClass { X = 11, Y = 11 }, 11 },
            { new MyClass { X = 12, Y = 12 }, 12 },
            { new MyClass { X = 13, Y = 13 }, 13 },
            { new MyClass { X = 14, Y = 14 }, 14 },
        };

        [Benchmark, BenchmarkCategory("Find")]
        public void ListFind()
        {
            var i = _l.IndexOf(new MyClass { X = 14, Y = 14 });
            //Console.WriteLine(i);
        }
        [Benchmark, BenchmarkCategory("Find")]
        public void DictionaryFind()
        {
            var i = _dict[new MyClass { X = 14, Y = 14 }];
            //Console.WriteLine(i);
        }
    }
    public class MyClass
    {
        public int X { get; set; }

        public int Y { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is MyClass @class &&
                   X == @class.X &&
                   Y == @class.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}
