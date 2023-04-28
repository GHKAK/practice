﻿namespace BattleShipWf {
    public class GameEngine {
        private Controller Controller { get; set; }
        public GameState State { get; private set; }
        private Cell[,] UserBattleFieldData { get; set; }
        private Cell[,] BotBattleFieldData { get; set; }
        private BattleFieldModel UserBattleFieldModel { get; set; }
        private BattleFieldModel BotBattleFieldModel { get; set; }
        public GameEngine(BattleFieldModel userBattleField, BattleFieldModel botBattleField, Controller controller) {
            State = GameState.Prepare;
            UserBattleFieldData = userBattleField.BattleFieldData;
            BotBattleFieldData = botBattleField.BattleFieldData;
            UserBattleFieldModel = userBattleField;
            BotBattleFieldModel = botBattleField;
            Controller = controller;
        }
        private void StartGame() {
            Random random = new Random();
            var num = random.Next(1, 3)
            switch {
                1 => this.State = GameState.PlayerMove,
                2 => this.State = GameState.BotMove,
                _ => throw new Exception(),
            };
        }
        public void ResolveUserControlMouseDown() {

        }
        public void RestartGame() {
            UserBattleFieldModel = new BattleFieldModel();
            State = GameState.Prepare;
            Controller.Repaint();
        }
    }
}