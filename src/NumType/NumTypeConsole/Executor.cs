namespace NumTypeConsole {
    internal class Executor {
        NumberChecker _numberChecker;
        internal Executor(NumberChecker numberChecker) {
            _numberChecker = numberChecker;
        }
        static int GetNumber() {
            int number;
            while(true) {
                try {
                    number = InputParser.ParseToInt(Console.ReadLine());
                    return number;
                } catch(ArgumentException) {
                    Console.WriteLine("Please type an Integer");
                    continue;
                }
            }
        }
        internal void Execute() {
            int number;
            number = GetNumber();
            Console.WriteLine(String.Join(" ", _numberChecker.GetInfo(number)));
        }
    }
}
