namespace BattleShipWf {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            Controller.Restart();
        }
        private void button2_Click(object sender, EventArgs e) {
            Controller.Start();
        }

        private void label1_Click(object sender, EventArgs e) {

        }
    }
}