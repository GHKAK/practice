namespace NumTypeConsole {
    internal class Executor {
        NumberChecker _numberChecker;
        internal Executor(NumberChecker numberChecker) {
            _numberChecker = numberChecker;
        }
        internal void Execute() {
            int number;
            number = ConsoleInput.GetNumber();
            Console.WriteLine(String.Join(" ", _numberChecker.GetInfo(number)));
        }
    }
}