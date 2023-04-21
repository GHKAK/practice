using NumCheckLib;
namespace NumTypeConsole {
    internal class NumberChecker {
        string odd = "нечетное";
        string even = "четное";
        string prime = "простое";
        string composite = "составное";
        internal List<string> GetInfo(int number) {
            List<string> result = new List<string>();
            result.Add(TypeCheck.IsOddNumber(number) ? odd : even);
            if(TypeCheck.IsCompositeNumber(number)) {
                result.Add(composite);
            } else if(TypeCheck.IsPrimeNumber(number)) {
                result.Add(prime);
            }
            return result;
        }
    }
}
