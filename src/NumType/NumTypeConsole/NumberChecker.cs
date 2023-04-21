using NumCheckLib;
namespace NumTypeConsole {
    public enum NumberTypes{
        Odd,
        Even,
        Prime,
        Composite
    }
    internal class NumberChecker {
        internal List<string> GetInfo(int number) {
            List<string> result = new List<string>();
            result.Add(TypeCheck.IsOddNumber(number) ? NumberTypes.Odd.ToString() : NumberTypes.Even.ToString());
            if(TypeCheck.IsCompositeNumber(number)) {
                result.Add(NumberTypes.Composite.ToString());
            } else if(TypeCheck.IsPrimeNumber(number)) {
                result.Add(NumberTypes.Prime.ToString());
            }
            return result;
        }
    }
}
