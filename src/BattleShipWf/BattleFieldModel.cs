namespace BattleShipWf {
    public enum SeaState {
        Empty,
        Ship,
        Hitted,
        Missed
    }
    public enum GameState {
        Prepare,
        UserMove,
        BotMove,
        Over,
    }
    public struct Cell {
        public SeaState State { get; set; }
        public Cell() {
            this.State = SeaState.Empty;
        }
    }
    public class BattleFieldModel {
        public static int SIZE = 10;
        public BattleFieldModel() {
            BattleFieldData = new Cell[SIZE, SIZE];
        }
        public void RestartBattleField() {
            BattleFieldData = new Cell[SIZE, SIZE];
        }
        public Cell[,] BattleFieldData { get; set; }
    }
    public static class BattleFieldDataExtensions {
        public static bool IsValid(this Cell[,] battleFieldData) {
            int shipCellCount = 0;
            for(int i = 0; i < BattleFieldModel.SIZE; i++) {
                for(int j = 0; j < BattleFieldModel.SIZE; j++) {
                    if(battleFieldData[i, j].State == SeaState.Ship) {
                        shipCellCount++;
                    }
                }
            }
            if(shipCellCount == 20) {
                return true;
            }
            return false;
        }
    }
}