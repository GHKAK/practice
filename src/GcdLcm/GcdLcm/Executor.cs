namespace GcdLcm{
    internal class Executor {
        GcdLcmFinder _gcdLcmFinder;

        public Executor() {
        }

        internal Executor(GcdLcmFinder gcdLcmFinder) {
            _gcdLcmFinder = gcdLcmFinder;
        }
        static void GetNumber(out int number) {
            while(true) {
                try {
                    number = InputParser.ParseToInt(Console.ReadLine());
                    return;
                } catch(ArgumentException) {
                    Console.WriteLine("Please type an Integer");
                    continue;
                }
            }
        }
        static (int, int) GetNumbersFromConsole() {
            int firstNumber, secondNumber;
            GetNumber(out firstNumber);
            GetNumber(out secondNumber);
            return (firstNumber, secondNumber);
        }
        internal void Execute() {

            int firstNumber, secondNumber;
            int gcd, lcm;
            (firstNumber, secondNumber) = GetNumbersFromConsole();
            (gcd, lcm) = GcdLcmFinder.GcdLcmCalculate(firstNumber, secondNumber);
            Console.WriteLine($"GCD: {gcd} /nLCM: {lcm}");
        }
    }
}
