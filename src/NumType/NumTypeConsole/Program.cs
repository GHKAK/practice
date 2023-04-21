using NumCheckLib;
namespace NumTypeConsole {
    class Program {
        static void Main(string[] args) {
            NumberChecker numberChecker = new NumberChecker();
            Executor executor = new Executor(numberChecker);
            while(true) {
                executor.Execute();

            }
        }
    }
}