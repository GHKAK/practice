namespace BattleShipWf {
    public class Controller {
        private GameEngine GameEngine { get; set; }
        private Form1 Form { get; set; }
        public Controller(GameEngine gameEngine, Form1 form) {
            this.GameEngine = gameEngine;
            this.Form = form;
        }
        public void Repaint() {
            if (GameEngine.State == GameState.PlayerMove) {
                Form.BattleFieldUser.Repaint();
            } else if (GameEngine.State == GameState.BotMove) {
                Form.BattleFieldBot.Repaint();
            } else if (GameEngine.State == GameState.Over) {
                Form.BattleFieldUser.Repaint();
                Form.BattleFieldBot.Repaint();
            }
        }
    }
}
