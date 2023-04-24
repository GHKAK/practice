using PalindromeSequence;

class App {
    static List<int> FindPalindromes(int end) {

        return FindPalindromes(0, end);
    }
    static List<int> FindPalindromes(int start, int end) {
        List<int> palindromes = new List<int>();
        for(int num = start; num <= end; num++) {
            bool isPalindrome = true;
            string stringNumber = num.ToString();
            for(int i = 0; i < Math.Ceiling((double)stringNumber.Length / 2); i++) {
                if(stringNumber[i] != stringNumber[^(i + 1)]) {
                    isPalindrome = false;
                    break;
                }
            }
            if(isPalindrome) {
                palindromes.Add(num);
            }
        }
        return palindromes;
    }
    static void Main() {
        while(true) {
            Console.WriteLine("Enter an integer from 0 to 100");
            int n = ConsoleInput.GetNumber();
            Console.WriteLine($"Palindromes in range [0,{n}]:  {String.Join(" ", FindPalindromes(n))}");
        }
    }

}