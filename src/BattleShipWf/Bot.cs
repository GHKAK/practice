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
        private Cell[,] _botBattleFieldData;
        private List<List<Coordinates>> _botPossibleMoves;
        public Bot(Cell[,] botBattleFieldData) {
            _botBattleFieldData = botBattleFieldData;
            _botPossibleMoves = new List<List<Coordinates>>();
            for (int i = 0; i < botBattleFieldData.GetLength(0); i++) {
            _botPossibleMoves.Add(new List<Coordinates>());
                for (int j = 0; j < botBattleFieldData.GetLength(1); j++) {
                    _botPossibleMoves[i].Add(new Coordinates(i,j));
                }
            }
        }
        public (int row, int col) GetMoveCoordinates() {
            Random random= new Random();
            return (random.Next(10), random.Next(10));
        }
    }
}
