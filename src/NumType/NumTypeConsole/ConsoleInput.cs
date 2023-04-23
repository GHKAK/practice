using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumTypeConsole {
    public class ConsoleInput {
        public static int GetNumber() {
            int number;
            while(true) {
                try {
                    number = InputParser.ParseToInt(Console.ReadLine());
                    return number;
                } catch(ArgumentException) {
                    Console.WriteLine("Please type an Integer");
                    continue;
                }
            }
        }
    }
}
