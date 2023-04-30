namespace BattleShipWf {
    public partial class Battleship : Form {
        public Battleship() {
            GameInitialize();
            InitializeComponent();
        }
        private void GameInitialize() {
            var battleFieldModelBot = new BattleFieldModel();
            var battleFieldModelUser = new BattleFieldModel();
            this.battleField1 = new BattleField(battleFieldModelBot);
            this.battleField2 = new BattleField(battleFieldModelUser);
            BotBattleField = battleField1;
            UserBattleField = battleField2;
            GameEngine = new GameLogic(battleFieldModelUser, battleFieldModelBot);
            Controller = new Controller(GameEngine, this);
        }
        private void button1_Click(object sender, EventArgs e) {
            Controller.Restart();
        }
        private void button2_Click(object sender, EventArgs e) {
            Controller.Start();
        }
        private void label1_Click(object sender, EventArgs e) {

        }
        public void SetUserCount(int count) {
            UserHitsCountLabel.Text = count.ToString();
            UserHitsCountLabel.Update();
        }
        public void SetBotCount(int count) {
            BotHitsCountLabel.Text = count.ToString();
            BotHitsCountLabel.Update();
        }
        private void Form1_Load(object sender, EventArgs e) {

        }

        private void label3_Click(object sender, EventArgs e) {

        }
    }
}