namespace NumTypeConsole {
    internal class InputParser {
        internal static int ParseToInt(string input) {
            int number;
            if(Int32.TryParse(input,out number)) {
                return number;
            } else {
                throw new ArgumentException();
            }
        }
    }
}
