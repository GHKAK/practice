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
                Form.BattleFieldUser.Repaint();
            } else if (GameEngine.State == GameState.PlayerMove) {
                Form.BattleFieldUser.Repaint();
            } else if (GameEngine.State == GameState.BotMove) {
                Form.BattleFieldBot.Repaint();
            } else if (GameEngine.State == GameState.Over) {
                Form.BattleFieldUser.Repaint();
                Form.BattleFieldBot.Repaint();
            }
        }
        public void Resolve(int row,int col, BattleField battleField) {
            GameEngine.ResolveUserControlMouseDown(row,col, battleField.BattleFieldData);
        }
    }
}
