using BenchmarkDotNet.Attributes;
using System.Collections;

namespace CollectionsBenchmark {
    public class Content {
        private readonly long x;
        private readonly long y;
        public Content(int x, int y) {
            this.x = x;
            this.y = y;
        }
    }
    [MemoryDiagnoser]
    public class AddingBenchmark2 : MethodsBenchmark {
        public void DoActionLengthTimes(Action<Content> action) {
            for (int i = 0; i < TEST_LENGTH; i++) {
                Content content = new Content(i, i);
                action(content);
            }
        }
        public void DoActionLengthTimes(Action<Content, Content> action) {
            for (int i = 0; i < TEST_LENGTH; i++) {
                Content content = new Content(i, i);
                action(content, content);
            }
        }
        public virtual void DoActionLengthTimes(Action<List<long>> action, List<long> list) {
            for (int i = 0; i < TEST_LENGTH; i++) {

                action(list);
            }
        }
        public virtual void DoActionLengthTimes(Action<Hashtable, Content> action, Hashtable hashtable) {
            for (int i = 0; i < TEST_LENGTH; i++) {
                Content content = new Content(i, i);
                action(hashtable, content);
            }
        }
        [Benchmark]
        public void ListAdd() {
            _list = new List<long>();
            DoActionLengthTimes(() => _list.Add(0));
        }
        [Benchmark]
        public void ListAdd1() {
            _list = new List<long>();
            for (int i = 0; i < TEST_LENGTH; i++) {
                _list.Add(0);
            }
        }
        [Benchmark]
        public void ListAdd2() {
            _list = new List<long>();
            DoActionLengthTimes((l) => l.Add(0), _list);
        }
        [Benchmark]
        public void HashTableAddObj() {
            var hashTable = new Hashtable();
            DoActionLengthTimes((Content content) => hashTable.Add(content,content));
        }
        [Benchmark]
        public void HashTableAddObj2() {
            var hashTable = new Hashtable();
            DoActionLengthTimes((Hashtable hashTable, Content content) => hashTable.Add(content, content),hashTable);
        }
        public void DictionaryAddObject() {
            Dictionary<Content, Content> _dictionaryContent = new Dictionary<Content, Content>();
            DoActionLengthTimes((Content key, Content value) => _dictionaryContent.Add(key, value));
        }
        [Benchmark]
        public void HashSetAddObject() {
            HashSet<Content> _hashSetContent = new HashSet<Content>();
            DoActionLengthTimes((Content value) => _hashSetContent.Add(value));
        }
    }
}
