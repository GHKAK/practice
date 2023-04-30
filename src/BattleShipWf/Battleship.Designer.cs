namespace BattleShipWf {
    partial class Battleship {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent() {
            
            button1 = new Button();
            label1 = new Label();
            label2 = new Label();
            button2 = new Button();
            label3 = new Label();
            label5 = new Label();
            BotHitsCountLabel = new Label();
            UserHitsCountLabel = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(690, 560);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 2;
            button1.Text = "Restart";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(37, 47);
            label1.Name = "label1";
            label1.Size = new Size(37, 24);
            label1.TabIndex = 3;
            label1.Text = "Bot";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(37, 311);
            label2.Name = "label2";
            label2.Size = new Size(44, 24);
            label2.TabIndex = 4;
            label2.Text = "You";
            // 
            // button2
            // 
            button2.Location = new Point(690, 467);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 5;
            button2.Text = "Start";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(463, 47);
            label3.Name = "label3";
            label3.Size = new Size(88, 24);
            label3.TabIndex = 6;
            label3.Text = "Bot Hits : ";
            label3.Click += label3_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("MS Reference Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(463, 311);
            label5.Name = "label5";
            label5.Size = new Size(114, 24);
            label5.TabIndex = 8;
            label5.Text = "Your Hits :";
            // 
            // BotHitsCountLabel
            // 
            BotHitsCountLabel.AutoSize = true;
            BotHitsCountLabel.Font = new Font("MS Reference Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            BotHitsCountLabel.Location = new Point(544, 47);
            BotHitsCountLabel.Name = "BotHitsCountLabel";
            BotHitsCountLabel.Size = new Size(0, 24);
            BotHitsCountLabel.TabIndex = 9;
            // 
            // PlayerHitsCountLabel
            // 
            UserHitsCountLabel.AutoSize = true;
            UserHitsCountLabel.Font = new Font("MS Reference Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            UserHitsCountLabel.Location = new Point(574, 311);
            UserHitsCountLabel.Name = "PlayerHitsCountLabel";
            UserHitsCountLabel.Size = new Size(0, 24);
            UserHitsCountLabel.TabIndex = 10;
            // 
            // battleField1
            // 
            battleField1.IsClickable = false;
            battleField1.Location = new Point(96, 47);
            battleField1.Name = "battleField1";
            battleField1.Size = new Size(250, 250);
            battleField1.TabIndex = 11;
            // 
            // battleField2
            // 
            battleField2.IsClickable = false;
            battleField2.Location = new Point(96, 321);
            battleField2.Name = "battleField2";
            battleField2.Size = new Size(250, 250);
            battleField2.TabIndex = 12;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 606);
            Controls.Add(battleField2);
            Controls.Add(battleField1);
            Controls.Add(UserHitsCountLabel);
            Controls.Add(BotHitsCountLabel);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(button2);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button1);
            Name = "Battleship";
            Text = "Battleship";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        public BattleField battleField1;
        public BattleField battleField2;
        private Button button1;
        private Label label1;
        private Label label2;
        private Button button2;
        private Label label3;
        private Label label5;
        private Label BotHitsCountLabel;
        private Label UserHitsCountLabel;

        public BattleField BotBattleField { get; private set; }
        public Controller Controller { get; private set; }
        public BattleField UserBattleField { get; private set; }
        public GameLogic GameEngine { get; set; }
    }
}