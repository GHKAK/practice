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
            if(GameEngine.State == GameState.Prepare || GameEngine.State == GameState.Over) {
                Form.BattleFieldBot.Repaint();
                Form.BattleFieldUser.Repaint();
            } else if(GameEngine.State == GameState.PlayerMove) {
                Form.BattleFieldUser.Repaint();
            } else if(GameEngine.State == GameState.BotMove) {
                Form.BattleFieldBot.Repaint();
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
        public void UpdateUserCount(int count) {
            Form.SetUserCount(count);
        }
        public void UpdateBotCount(int count) {
            Form.SetUserCount(count);
        }
        internal void EndGame(string winnerName) {
            MessageBox.Show("The winner is " + winnerName);
        }
    }
}
