using BattleShipWf;
using System.Runtime.CompilerServices;

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
        public static int SIZE = 10;
        public BattleFieldModel() {
            BattleFieldData = new Cell[SIZE, SIZE];
        }
        public void RestartBattleField() {
            BattleFieldData = new Cell[SIZE, SIZE];
        }
        public Cell[,] BattleFieldData { get; set; }
    }
    public static class BattleFieldDataExtensions{
        public static bool IsValid(this Cell[,] battleFieldData) {
            int shipCellCount = 0;
            for(int i = 0; i < BattleFieldModel.SIZE; i++) {
                for(int j = 0; j < BattleFieldModel.SIZE; j++) {
                    shipCellCount++;
                }
            }
            if(shipCellCount == 20) {
                return true;
            }
            return false;
        }
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
