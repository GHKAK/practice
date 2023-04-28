namespace BattleShipWf {
    partial class Form1 {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent() {
            var battleFieldModelBot = new BattleFieldModel();
            var battleFieldModelUser = new BattleFieldModel();
            this.battleField1 = new BattleShipWf.BattleField(battleFieldModelBot);
            this.battleField2 = new BattleShipWf.BattleField(battleFieldModelUser);
            GameEngine gameEngine = new GameEngine(battleFieldModelUser, battleFieldModelBot);
            Controller controller = new Controller(gameEngine,this);
            // 
            // battleField1
            // 
            this.battleField1.IsClickable = false;
            this.battleField1.Location = new System.Drawing.Point(86, 47);
            this.battleField1.Name = "battleField1";
            this.battleField1.Size = new System.Drawing.Size(200, 200);
            this.battleField1.TabIndex = 0;
            // 
            // battleField2
            // 
            this.battleField2.IsClickable = false;
            this.battleField2.Location = new System.Drawing.Point(86, 286);
            this.battleField2.Name = "battleField2";
            this.battleField2.Size = new System.Drawing.Size(200, 200);
            this.battleField2.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 606);
            this.Controls.Add(this.battleField2);
            this.Controls.Add(this.battleField1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        private BattleField battleField1;
        private BattleField battleField2;

        public BattleField BattleFieldBot { get; private set; }
        public BattleField BattleFieldUser { get; private set; }
    }
}