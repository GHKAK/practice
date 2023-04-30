namespace BattleShipWf {
    public class GameEngine {
        public Controller Controller { get; set; }
        public GameState State { get; private set; }
        private Cell[,] UserBattleFieldDataView { get; set; }
        private Cell[,] BotBattleFieldView { get; set; }
        private Cell[,] BotBattleFieldData { get; set; }
        private int _userHitsCount=0, _botHitsCount=0;
        private string _botName = "bot";
        private string _userName = "you";
        //private BattleFieldModel UserBattleFieldModel { get; set; }
        //private BattleFieldModel BotBattleFieldModel { get; set; }
        public Bot Bot { get; private set; }
        public GameEngine(BattleFieldModel userBattleField, BattleFieldModel botBattleField) {

            State = GameState.Prepare;
            UserBattleFieldDataView = userBattleField.BattleFieldData;
            BotBattleFieldView = botBattleField.BattleFieldData;
            //UserBattleFieldModel = userBattleField;
            //BotBattleFieldModel = botBattleField;
            Bot = new Bot(BotBattleFieldView);
            BotBattleFieldData = Bot.BotBattleFieldData;
        }
        public void StartGame() {
            Random random = new Random();
            _userHitsCount = 0;
            _botHitsCount = 0;
            var num = random.Next(1, 3)
            switch {
                1 => this.State = GameState.PlayerMove,
                2 => this.State = GameState.BotMove,
                _ => throw new Exception(),
            };
            while(State != GameState.Over) {
                MakeMove();
            }
            EndGame();
        }
        private void EndGame() {
            if(_userHitsCount >= 20) {
                Controller.EndGame(_userName);
            } else {
                Controller.EndGame(_botName);
            }
            RestartGame();
        }
        public void ResolveAction(int row, int col, Cell[,] BattleFieldData) {
            if(State == GameState.Prepare && BattleFieldData == UserBattleFieldDataView) {
                if(BattleFieldData[row, col].State == SeaState.Ship) {
                    BattleFieldData[row, col].State = SeaState.Empty;
                    Console.Write("Empty");
                    Controller.Repaint();
                } else {
                    BattleFieldData[row, col].State = SeaState.Ship;
                    Console.Write("Ship");
                    Controller.Repaint();
                }
            }
        }
        public async void MakeMove() {
            if(State == GameState.BotMove) {
                (int row, int col) = Bot.GetMoveCoordinates();
                MakeShot(UserBattleFieldDataView, UserBattleFieldDataView, row, col);
                Controller.Repaint();
                SwitchMove();
            }
            if(State == GameState.PlayerMove) {
                (int row, int col) = Bot.GetMoveCoordinates();
                MakeShot(BotBattleFieldData, BotBattleFieldView, row, col);
                Controller.Repaint();
                SwitchMove();
            }
        }
        private void SwitchMove() {
            if(State != GameState.Over) {
                if(State == GameState.PlayerMove) {
                    State = GameState.BotMove;
                } else {
                    State = GameState.PlayerMove;
                }
            }
        }
        private bool IsGameOverCheck(Cell[,] battleData) {
            if(_userHitsCount >= 20 || _botHitsCount >= 20) {
                return true;
            } else {
                return false;
            }
        }
        private void MakeShot(Cell[,] BattleFieldData, Cell[,] BattleFieldView, int row,int col) {
            if (BattleFieldData[row, col].State == SeaState.Ship) {
                BattleFieldView[row,col].State = SeaState.Hitted;
                if(BattleFieldData == BotBattleFieldData) {
                    _userHitsCount++;
                    Controller.UpdateUserCount(_userHitsCount);
                } else {
                    _botHitsCount++;
                    Controller.UpdateBotCount(_botHitsCount);
                }
                if(IsGameOverCheck(BattleFieldData)) {
                    State = GameState.Over;
                }
            } else if (BattleFieldData[row,col].State == SeaState.Empty) {
                BattleFieldView[row,col].State = SeaState.Missed;
            } else if(BattleFieldData[row, col].State == SeaState.Hitted) {
                BattleFieldView[row, col].State = SeaState.Hitted;
            } else if(BattleFieldData[row, col].State == SeaState.Missed) {
                BattleFieldView[row, col].State = SeaState.Missed;
            }
        }

        public void RestartBattleFields() {
            for (int i = 0; i < BattleFieldModel.SIZE; i++) {
                for (int j = 0; j < BattleFieldModel.SIZE; j++) {
                    UserBattleFieldDataView[i, j].State = SeaState.Empty;
                    BotBattleFieldView[i, j].State = SeaState.Empty;
                    Bot.Prepare();
                }
            }
        }
        public void RestartGame() {
            RestartBattleFields();
            State = GameState.Prepare;
            Controller.Repaint();
        }
    }
}
