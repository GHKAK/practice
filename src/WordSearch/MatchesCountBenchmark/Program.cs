using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Text;
using WordSearch;
namespace MatchesCountBenchmark {
    public class IndexVsRegex {
        private const string sentence = "All happy families are alike; each unhappy family is unhappy in its own way. ";
        StringBuilder sb = new StringBuilder();
        string testString;
        public IndexVsRegex() {
            for(int i = 0; i < 999; i++) {
                sb.Append(sentence);
            }
            testString = sb.ToString();
        }
        [Benchmark]
        public int RegexBench() => testString.AllMatchesCountRegex("way");
        [Benchmark]
        public int IndexBench() => testString.AllMatchesCountIndex("way");
    }
    public class Program {
        public static void Main(string[] args) {
            var summary = BenchmarkRunner.Run<IndexVsRegex>();
        }
    }

}