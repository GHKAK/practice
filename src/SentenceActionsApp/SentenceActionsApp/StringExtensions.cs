using System.Text;
using System.Text.RegularExpressions;

namespace SentenceActionsApp {
    public static class StringExtensions {
        public static int CountWords(this string sentence) {
            return Regex.Matches(sentence, @"\b\w+\b").Count;
        }
        public static string FirstLettersToUpperCase(this string sentence) {
            StringBuilder sb = new StringBuilder();
            bool newWord = true;
            for(int i = 0; i < sentence.Length; i++) {
                char c = sentence[i];
                if(newWord && char.IsLetter(c)) {
                    sb.Append(char.ToUpper(c));
                    newWord = false;
                } else {
                    if(char.IsWhiteSpace(c) || char.IsPunctuation(c)) {
                        newWord = true;
                    }
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
        public static string SortSentence(this string sentence) {
            char[] delimiters = { ' ', '.', ',', '!', '?', ';', ':', '-', '_', '(', ')', '[', ']', '{', '}', '<', '>' };
            string[] words = sentence.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            Array.Sort(words, StringComparer.OrdinalIgnoreCase);
            return string.Join(' ',words);
        }
    }
}
