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
            BattleFieldBot = battleField1;
            BattleFieldUser = battleField2;
            GameEngine = new GameEngine(battleFieldModelUser, battleFieldModelBot);
            Controller = new Controller(GameEngine, this);
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(548, 47);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Restart";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 606);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.battleField2);
            this.Controls.Add(this.battleField1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        public BattleField battleField1;
        public BattleField battleField2;
        private Button button1;

        public BattleField BattleFieldBot { get; private set; }
        public Controller Controller { get; private set; }
        public BattleField BattleFieldUser { get; private set; }
        public GameEngine GameEngine { get; set; }
    }
}