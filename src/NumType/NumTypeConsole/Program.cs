using NumCheckLib;
namespace NumTypeConsole {
    class Program {
        static void Main(string[] args) {
            NumberChecker numberChecker = new NumberChecker();
            Controller controller = new Controller(numberChecker);
            while(true) {
                try {
                    controller.NumberCheck();
                } catch(ArgumentException) {
                    continue;
                }
            }
        }
    }
}