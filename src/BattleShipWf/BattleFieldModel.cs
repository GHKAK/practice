using BattleShipWf;

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
            BattleFieldData = new Cell[10, 10];
        }
        public void RestartBattleField(){
            BattleFieldData = new Cell[10, 10];
        }
        public Cell[,] BattleFieldData { get; set; }
    }
}
//var battleFieldModelBot = new BattleFieldModel();
//var battleFieldModelUser = new BattleFieldModel();
//this.battleField1 = new BattleShipWf.BattleField(battleFieldModelBot);
//this.battleField2 = new BattleShipWf.BattleField(battleFieldModelUser);
//BattleFieldBot = battleField1;
//BattleFieldUser = battleField2;
//GameEngine = new GameEngine(battleFieldModelUser, battleFieldModelBot);
//Controller = new Controller(GameEngine, this);
//this.button1 = new System.Windows.Forms.Button();
//this.label1 = new System.Windows.Forms.Label();
//this.label2 = new System.Windows.Forms.Label();
