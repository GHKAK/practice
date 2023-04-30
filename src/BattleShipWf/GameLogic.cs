namespace BattleShipWf {
    public class GameLogic {
        public Controller Controller { get; set; }
        public GameState State { get; private set; }
        private Cell[,] _userBattleFieldDataView;
        private Cell[,] _botBattleFieldView;
        private Cell[,] _botBattleFieldData;
        private int _userHitsCount = 0, _botHitsCount = 0;
        private const int _winHitsCount = 20;
        private string _botName = "bot";
        private string _userName = "you";
        public Bot Bot { get; private set; }
        public GameLogic(BattleFieldModel userBattleField, BattleFieldModel botBattleField) {

            State = GameState.Prepare;
            _userBattleFieldDataView = userBattleField.BattleFieldData;
            _botBattleFieldView = botBattleField.BattleFieldData;
            Bot = new Bot(_botBattleFieldView);
            _botBattleFieldData = Bot.BotBattleFieldData;
        }
        public void StartGame() {
            Random random = new Random();
            Controller.UpdateData(_botHitsCount, _userHitsCount);
            var num = random.Next(1, 3)
            switch {
                1 => this.State = GameState.UserMove,
                2 => this.State = GameState.BotMove,
                _ => throw new Exception(),
            };
            if(State == GameState.BotMove) {
                MakeBotMove();
            }
        }
        private void EndGame() {
            if(_userHitsCount >= _winHitsCount) {
                Controller.EndGame(_userName);
            } else {
                Controller.EndGame(_botName);
            }
            RestartGame();
        }
        public void ResolveAction(int row, int col, Cell[,] BattleFieldData) {
            if(State == GameState.Prepare && BattleFieldData == _userBattleFieldDataView) {
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
            if(State==GameState.UserMove && BattleFieldData== _botBattleFieldView) {
                MakeUserMove(row, col);
            }
        }
        private void MakeBotMove() {
            (int row, int col) = Bot.GetMoveCoordinates();
            MakeShot(_userBattleFieldDataView, _userBattleFieldDataView, row, col);
            Controller.Repaint();
            SwitchMove();
            MoveRoutine();
        }
        private void MakeUserMove(int row, int col) {
            MakeShot(_botBattleFieldData, _botBattleFieldView, row, col);
            Controller.Repaint();
            SwitchMove();
            MoveRoutine();
        }
        private void MoveRoutine() {
            Controller.UpdateData(_botHitsCount, _userHitsCount);
            if(State == GameState.Over) {
                EndGame();
            }
        }
        private void SwitchMove() {
            if(State != GameState.Over) {
                if(State == GameState.UserMove) {
                    State = GameState.BotMove;
                    MakeBotMove();
                } else {
                    State = GameState.UserMove;
                }
            }
        }
        private bool IsGameOverCheck(Cell[,] battleData) {
            if(_userHitsCount >= _winHitsCount || _botHitsCount >= _winHitsCount) {
                return true;
            } else {
                return false;
            }
        }
        private void MakeShot(Cell[,] BattleFieldData, Cell[,] BattleFieldView, int row, int col) {
            if(BattleFieldView[row, col].State != SeaState.Hitted && BattleFieldData[row, col].State == SeaState.Ship ) {
                BattleFieldView[row, col].State = SeaState.Hitted;
                if(BattleFieldData == _botBattleFieldData) {
                    _userHitsCount++;
                    Controller.UpdateUserCount(_userHitsCount);
                } else {
                    _botHitsCount++;
                    Controller.UpdateBotCount(_botHitsCount);
                }
                if(IsGameOverCheck(BattleFieldData)) {
                    State = GameState.Over;
                }
            } else if(BattleFieldData[row, col].State == SeaState.Empty) {
                BattleFieldView[row, col].State = SeaState.Missed;
            } 
        }

        internal void RestartBattleFields() {
            for(int i = 0; i < BattleFieldModel.SIZE; i++) {
                for(int j = 0; j < BattleFieldModel.SIZE; j++) {
                    _userBattleFieldDataView[i, j].State = SeaState.Empty;
                    _botBattleFieldView[i, j].State = SeaState.Empty;
                    Bot.Prepare();
                }
            }
        }
        internal void RestartGame() {
            _userHitsCount = 0;
            _botHitsCount = 0;
            Controller.UpdateData(_botHitsCount, _userHitsCount);

            RestartBattleFields();
            State = GameState.Prepare;
            Controller.Repaint();
        }

        internal bool IsValidLocation() {
            return _userBattleFieldDataView.IsValid();
        }
    }
}
