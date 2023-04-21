namespace NumTypeConsole {
    internal class Controller {
        NumberChecker _numberChecker;
        internal Controller(NumberChecker numberChecker) {
            _numberChecker = numberChecker;
        }
        internal void NumberCheck() {
            int number;
            try {
                number = InputParser.ParseToInt(Console.ReadLine());
            } catch(ArgumentException) {
                Console.WriteLine("Please type an Integer");
                throw;
            }
            Console.WriteLine(String.Join(" ", _numberChecker.GetInfo(number)));
        }
    }
}
