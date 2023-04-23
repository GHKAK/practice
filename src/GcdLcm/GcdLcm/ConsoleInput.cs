namespace GcdLcm {
    public static class ConsoleInput {
        public static void GetNumber(out int number) {
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
        public static (int, int) GetNumbersFromConsole() {
            int firstNumber, secondNumber;
            GetNumber(out firstNumber);
            GetNumber(out secondNumber);
            return (firstNumber, secondNumber);
        }
    }
}