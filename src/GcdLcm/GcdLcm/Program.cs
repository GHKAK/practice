using GcdLcmLib;

namespace GcdLcm {
    public class Program {
        static void Main(string[] args) {
            Executor executor = new Executor();
            while(true) {
                executor.Execute();
            }
        }
    }
}