namespace BattleShipWf {
    public class Controller {
        private GameLogic GameEngine { get; set; }
        private Battleship FormBattleship { get; set; }
        public Controller(GameLogic gameEngine, Battleship form) {
            this.GameEngine = gameEngine;
            gameEngine.Controller = this;
            this.FormBattleship = form;
            form.BotBattleField.Controller = this;
            form.UserBattleField.Controller = this;
        }
        internal void Repaint() {
            if(GameEngine.State == GameState.Prepare || GameEngine.State == GameState.Over) {
                FormBattleship.BotBattleField.Repaint();
                FormBattleship.UserBattleField.Repaint();
            } else if(GameEngine.State == GameState.UserMove) {
                FormBattleship.BotBattleField.Repaint();
            } else if(GameEngine.State == GameState.BotMove) {
                FormBattleship.UserBattleField.Repaint();
            }
        }
        internal void Resolve(int row, int col, BattleField battleField) {
            GameEngine.ResolveAction(row, col, battleField.BattleFieldData);
        }
        internal void Restart() {
            GameEngine.RestartGame();
        }
        internal void Start() {
            if(GameEngine.IsValidLocation()) {
                GameEngine.StartGame();
            } else {
                MessageBox.Show("The position and number of the ships is not according to the rules");
            }
        }
        internal void UpdateUserCount(int count) {
            FormBattleship.SetUserCount(count);
        }
        internal void UpdateBotCount(int count) {
            FormBattleship.SetBotCount(count);
        }
        internal void EndGame(string winnerName) {
            MessageBox.Show("The winner is " + winnerName);
        }
        internal void UpdateData(int botCount, int userCount) {
            FormBattleship.SetBotCount(botCount);
            FormBattleship.SetUserCount(userCount);
        }
    }
}