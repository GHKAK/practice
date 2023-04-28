using System.Runtime.CompilerServices;

namespace BattleShipWf {
    public class Controller {
        private GameEngine GameEngine { get; set; }
        private Form1 Form { get; set; }
        public Controller(GameEngine gameEngine, Form1 form) {
            this.GameEngine = gameEngine;
            gameEngine.Controller = this;
            this.Form = form;
            form.BattleFieldBot.Controller = this;
            form.BattleFieldUser.Controller = this;

        }
        public void Repaint() {
            if (GameEngine.State == GameState.Prepare) {
                Form.BattleFieldUser.Repaint(true);
            } else if (GameEngine.State == GameState.PlayerMove) {
                Form.BattleFieldUser.Repaint(true);
            } else if (GameEngine.State == GameState.BotMove) {
                Form.BattleFieldBot.Repaint(true);
            } else if (GameEngine.State == GameState.Over) {
                Form.BattleFieldUser.Repaint(false);
                Form.BattleFieldBot.Repaint(false);
            }
        }
        public void Resolve(int row, int col, BattleField battleField) {
            GameEngine.ResolveAction(row, col, battleField.BattleFieldData);
        }
        public void Restart() {
            GameEngine.RestartGame();
        }
        public void Start() {
            GameEngine.StartGame();
        }
    }
}
