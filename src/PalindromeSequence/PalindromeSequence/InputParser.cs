namespace PalindromeSequence {
    public class InputParser {
        public static int ParseToInt(string input) {
            int number;
            if(Int32.TryParse(input, out number)) {
                return number;
            } else {
                throw new ArgumentException("Wrong Number Format");
            }
        }
    }
}
