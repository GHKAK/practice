using NumCheckLib;
namespace NumTypeConsole {
    class Program {
        static void Main(string[] args) {
            NumberChecker numberChecker = new NumberChecker();
            while(true) {
                int number;
                try {
                    number = InputParser.ParseToInt(Console.ReadLine());
                } catch(ArgumentException) {
                    Console.WriteLine("Введите Целое число");
                    continue;
                }
                Console.WriteLine(String.Join(" ", numberChecker.GetInfo(number)));
            }
        }
    }
}