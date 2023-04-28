namespace BattleShipWf {
    public class GameEngine {
        public Controller Controller { get; set; }
        public GameState State { get; private set; }
        private Cell[,] UserBattleFieldData { get; set; }
        private Cell[,] BotBattleFieldData { get; set; }
        private BattleFieldModel UserBattleFieldModel { get; set; }
        private BattleFieldModel BotBattleFieldModel { get; set; }
        public Bot Bot { get; private set; }
        public GameEngine(BattleFieldModel userBattleField, BattleFieldModel botBattleField) {
            State = GameState.Prepare;
            UserBattleFieldData = userBattleField.BattleFieldData;
            BotBattleFieldData = botBattleField.BattleFieldData;
            UserBattleFieldModel = userBattleField;
            BotBattleFieldModel = botBattleField;
            Bot = new Bot(BotBattleFieldData);
        }
        public void StartGame() {
            Random random = new Random();
            var num = random.Next(1, 3)
            switch {
                1 => this.State = GameState.PlayerMove,
                2 => this.State = GameState.BotMove,
                _ => throw new Exception(),
            };
            int i = 0;
            while (i<10) {
                MakeMove();
                i++;
            }
        }
        public void ResolveAction(int row, int col, Cell[,] BattleFieldData) {
            if (State == GameState.Prepare && BattleFieldData == UserBattleFieldData) {
                if (BattleFieldData[row, col].State == SeaState.Ship) {
                    BattleFieldData[row, col].State = SeaState.Empty;
                    Controller.Repaint();
                } else {
                    BattleFieldData[row, col].State = SeaState.Ship;
                    Controller.Repaint();
                }
            }
        }
        public void MakeMove() {
            if (State == GameState.BotMove) {
                (int row, int col) = Bot.GetMoveCoordinates();
                MakeShot(UserBattleFieldData,row,col);
                Controller.Repaint();
                State = GameState.BotMove;
            } if (State == GameState.PlayerMove) {
                (int row, int col) = Bot.GetMoveCoordinates();
                MakeShot(BotBattleFieldData, row, col);
                Controller.Repaint();
                State = GameState.PlayerMove;
            }
        }

        private void MakeShot(Cell[,] UserBattleFieldData, int row,int col) {
            if (UserBattleFieldData[row, col].State == SeaState.Ship) { 
                UserBattleFieldData[row,col].State = SeaState.Hitted;
            } else if (UserBattleFieldData[row,col].State == SeaState.Empty) { 
                UserBattleFieldData[row,col].State = SeaState.Missed;
            } else if (UserBattleFieldData[row,col].State == SeaState.Hitted) {
                UserBattleFieldData[row,col].State = SeaState.Hitted;
            } else if (UserBattleFieldData[row,col].State == SeaState.Missed) {
                UserBattleFieldData[row,col].State = SeaState.Missed;
            }
        }

        public void RestartUserBattleField() {
            for (int i = 0; i < UserBattleFieldData.GetLength(0); i++) {
                for (int j = 0; j < UserBattleFieldData.GetLength(1); j++) {
                    UserBattleFieldData[i, j].State = SeaState.Empty;
                }
            }
        }

        public void RestartGame() {
            UserBattleFieldModel = new BattleFieldModel();
            RestartUserBattleField();
            State = GameState.Prepare;
            Controller.Repaint();
        }
    }
}
