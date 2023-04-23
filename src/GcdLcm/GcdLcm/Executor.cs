using GcdLcmLib;

namespace GcdLcm {
    internal class Executor {
        internal void Execute() {
            int firstNumber, secondNumber;
            int gcd, lcm;
            (firstNumber, secondNumber) = ConsoleInput.GetNumbersFromConsole();
            (gcd, lcm) = (GcdLcmAlgorithms.CalculateGcd(firstNumber, secondNumber), GcdLcmAlgorithms.CalculateLcm(firstNumber, secondNumber));
            Console.WriteLine($"GCD: {gcd} /nLCM: {lcm}");
        }
    }
}