using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WordSearch {
    public static class StringExtensions {
        public static int AllMatchesCount(this string text, string word) {
            if(word =="") {
                return 0;
            }
            Regex regex = new Regex("(?i)\\b" + Regex.Escape(word) + "\\b");
            return regex.Matches(text).Count;
        }
    }
}
