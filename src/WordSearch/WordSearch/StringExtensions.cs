using System.Text.RegularExpressions;
namespace WordSearch {
    public static class StringExtensions {
        public static int AllMatchesCountRegex(this string text, string word) {
            if(word == "") {
                return 0;
            }
            Regex regex = new Regex("(?i)\\b" + Regex.Escape(word) + "\\b");
            return regex.Matches(text).Count;
        }
        public static int AllMatchesCountIndex(this string text, string word) {
            if(word == "") {
                return 0;
            }
            int count = 0;
            int index = text.IndexOf(word, StringComparison.OrdinalIgnoreCase);
            while(index != -1) {
                bool isWholeWord = text.IsWholeWord(index, word.Length);
                if(isWholeWord) {
                    count++;
                }
                index = text.IndexOf(word, index + 1, StringComparison.OrdinalIgnoreCase);
            }
            
            return count;
        }
        private static bool IsWholeWord(this string text, int index, int wordLength) {
            if(wordLength <= text.Length - 1 && index == 0 && char.IsWhiteSpace(text[wordLength])) {
                return true;
            }
            if(index == 0 || char.IsWhiteSpace(text[index - 1])) {
                if(index + wordLength == text.Length) {
                    return true;
                }
                if(char.IsWhiteSpace(text[index + wordLength]) || char.IsPunctuation(text[index + wordLength])) {
                    return true;
                }
            }
            return false;
        }
    }
}