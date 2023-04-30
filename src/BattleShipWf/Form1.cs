namespace BattleShipWf {
    public partial class Form1 : Form {
        public Form1() {
            //GameInitialize();
            InitializeComponent();
        }
        private void GameInitialize() {
            var battleFieldModelBot = new BattleFieldModel();
            var battleFieldModelUser = new BattleFieldModel();
            this.battleField1 = new BattleField(battleFieldModelBot);
            this.battleField2 = new BattleField(battleFieldModelUser);
            BattleFieldBot = battleField1;
            BattleFieldUser = battleField2;
            GameEngine = new GameEngine(battleFieldModelUser, battleFieldModelBot);
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
        }
        public void SetBotCount(int count) {
            BotHitsCountLabel.Text = count.ToString();
        }
        private void Form1_Load(object sender, EventArgs e) {

        }

        private void label3_Click(object sender, EventArgs e) {

        }
    }
}