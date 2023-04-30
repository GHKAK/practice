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

        private int[,] directions = new int[,] { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } };
        private List<List<Coordinates>> _botPossibleMoves;
        private int length;
        public Bot(Cell[,] botBattleFieldData) {
            length = BattleFieldModel.SIZE;
            //BotBattleFieldData = botBattleFieldData; //Cheat Vision comment next
            BotBattleFieldData = (new BattleFieldModel()).BattleFieldData;
            Prepare();
        }
        public (int row, int col) GetMoveCoordinates() {
            Random random = new Random();
            return (random.Next(10), random.Next(10));
        }
        public void Prepare() {
            EmptyBattleField();
            GenerateMovesList();
            GenerateBattleField();
        }
        private void EmptyBattleField() {
            for(int i = 0; i < length; i++) {
                for(int j = 0; j < length; j++) {
                BotBattleFieldData[i, j].State = SeaState.Empty;
                }
            }
        }
        private void GenerateMovesList() {
            _botPossibleMoves = new List<List<Coordinates>>();
            for(int i = 0; i < length; i++) {
                _botPossibleMoves.Add(new List<Coordinates>());
                for(int j = 0; j < length; j++) {
                    _botPossibleMoves[i].Add(new Coordinates(i, j));
                }
            }
        }
        private void GenerateBattleField() {
            Random random = new Random();
            for(int shipSize = 4; shipSize > 0; shipSize--) {
                for(int i = 5 - shipSize; i > 0; i--) {
                    bool isPlaced = false;
                    while(!isPlaced) {
                        isPlaced = PlaceShip(shipSize, random.Next(0, 10), random.Next(0, 10));
                    }
                }
            }
        }
        private bool PlaceShip(int shipSize, int row, int col) {
            List<int> directionIndexes = new List<int>() { 0, 1, 2, 3 };
            Random random = new Random();
            for(int i = 0; i < directions.GetLength(0); i++) {
                int directionIndex = random.Next(0, directionIndexes.Count);
                directionIndexes.RemoveAt(directionIndex);
                int newRow = row + directions[directionIndex, 0] * (shipSize);
                int newCol = col + directions[directionIndex, 1] * (shipSize);
                if(newRow >= 0 && newRow < length && newCol >= 0 && newCol < length) {
                    bool isValid = true;
                    for(int l = 0; l < shipSize; l++) {
                        for(int j = -1; j <= 1; j++) {
                            for(int k = -1; k <= 1; k++) {
                                int checkRow = row + j + directions[directionIndex, 0] * l;
                                int checkCol = col + k + directions[directionIndex, 1] * l;
                                if(checkRow >= 0 && checkRow < length && checkCol >= 0 && checkCol < length) {
                                    if(BotBattleFieldData[checkRow, checkCol].State != SeaState.Empty) {
                                        isValid = false;
                                    }
                                }
                            }
                        }
                    }
                    if(isValid) {
                        for(int j = 0; j < shipSize; j++) {
                            BotBattleFieldData[row + directions[directionIndex, 0] * j, col + directions[directionIndex, 1] * j].State = SeaState.Ship;
                        }
                        return true;
                    }
                }
            }
            return false;
        }
    }
}