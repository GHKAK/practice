namespace BattleShipWf {
    public struct Coordinates {
        public int X;
        public int Y;
        public Coordinates(int X, int Y) {
            this.X = X;
            this.Y = Y;
        }
    }
    public class Bot {
        public Cell[,] BotBattleFieldData { get; private set; }

        private List<List<Coordinates>> _botPossibleMoves;
        public Bot(Cell[,] botBattleFieldData) {
            BotBattleFieldData = (new BattleFieldModel()).BattleFieldData;
            Prepare();
        }
        public SeaState GetBattleFieldCellState(int row, int col) {
            return BotBattleFieldData[row, col].State;
        }
        public (int row, int col) GetMoveCoordinates() {
            Random random= new Random();
            return (random.Next(10), random.Next(10));
        }
        public void Prepare() {
            GenerateMovesList();
            GenerateBattleField();
        }
        public SeaState GetState(int row, int col) {
            return BotBattleFieldData[row, col].State;
        }
        private void GenerateMovesList() {
            _botPossibleMoves = new List<List<Coordinates>>();
            for(int i = 0; i < BattleFieldModel.SIZE; i++) {
                _botPossibleMoves.Add(new List<Coordinates>());
                for(int j = 0; j < BattleFieldModel.SIZE; j++) {
                    _botPossibleMoves[i].Add(new Coordinates(i, j));
                }
            }
        }
        private void GenerateBattleField() {
            for(int i = 0; i < BattleFieldModel.SIZE; i++) {
                for(int j = 0; j < BattleFieldModel.SIZE ; j++) {
                    BotBattleFieldData[i, j].State = SeaState.Ship;
                }
            }
        }

    }
}
