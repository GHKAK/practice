namespace PalindromeSequence {
    public class ConsoleInput {
        public static int GetNumber(int maxNumber=100) {
            int number;
            while(true) {
                try {
                    number = InputParser.ParseToInt(Console.ReadLine());
                    if(number > maxNumber) {
                        throw new ArgumentException("Integer must be from 0 to 100");
                    }
                    return number;
                } catch(ArgumentException e) {
                    Console.WriteLine($"Please type an Integer ({e.Message})");
                    continue;
                }
            }
        }
    }
}
