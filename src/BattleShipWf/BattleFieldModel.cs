namespace BattleShipWf {
    public enum SeaState {
        Empty,
        Ship,
        Hitted,
        Missed
    }
    public enum GameState {
        Prepare,
        PlayerMove,
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
        public BattleFieldModel() {
            BattleFieldData = new Cell[10,10];
        }
        public Cell[,] BattleFieldData { get; set; }
    }
}
