using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShipWf {

    public partial class BattleField : UserControl {
        public bool IsClickable { get; set; }
        public BattleField() {
            this.BattleFieldData = (new BattleFieldModel()).BattleFieldData;
            InitializeComponent();
        }
        public BattleField(BattleFieldModel battleFieldModel){
            this.BattleFieldData = battleFieldModel.BattleFieldData;
            InitializeComponent();
        }
        public Cell[,] BattleFieldData { get;private set; }
        protected override void OnPaint(PaintEventArgs e) {
            int cellsCount = 10;
            int gridRightBottomPadding = 10;
            int cellWidth = (this.Width - gridRightBottomPadding / cellsCount) / cellsCount;
            int cellHeight = (this.Height - gridRightBottomPadding / cellsCount) / cellsCount;

            using (Pen pen = new Pen(Color.White)) {
                for (int y = 0; y <= 10; y++) {
                    int yPos = y * cellHeight;
                    e.Graphics.DrawLine(pen, 0, yPos, this.Width - gridRightBottomPadding, yPos);
                }

                for (int x = 0; x <= 10; x++) {
                    int xPos = x * cellWidth;
                    e.Graphics.DrawLine(pen, xPos, 0, xPos, this.Height - gridRightBottomPadding);
                }
                Brush blueBrush = new SolidBrush(Color.Blue);
                Brush grayBrush = new SolidBrush(Color.Gray);
                Brush redBrush = new SolidBrush(Color.Red);
                Brush blackBrush = new SolidBrush(Color.Black);

                for (int y = 0; y < this.BattleFieldData.GetLength(0); y++) {
                    for (int x = 0; x < BattleFieldData.GetLength(1); x++) {
                        Rectangle cellRect = new Rectangle(x * cellWidth + 1, y * cellHeight + 1, cellWidth - 1, cellHeight - 1);
                        switch (BattleFieldData[y, x].State) {
                            case SeaState.Empty:
                                e.Graphics.FillRectangle(blueBrush, cellRect);
                                break;
                            case SeaState.Ship:
                                e.Graphics.FillRectangle(grayBrush, cellRect);
                                break;
                            case SeaState.Hitted:
                                e.Graphics.FillRectangle(redBrush, cellRect);
                                break;
                            case SeaState.Missed:
                                e.Graphics.FillRectangle(blackBrush, cellRect);
                                break;
                        }
                    }
                }
            }
        }
        protected void OnRePaintCell(PaintEventArgs e) {

        }
        protected override void OnMouseDown(MouseEventArgs e) {
            base.OnMouseDown(e);

            int cellWidth = this.Width / 10;
            int cellHeight = this.Height / 10;
            int row = e.Y / cellHeight;
            int col = e.X / cellWidth;

            BattleFieldData[row, col].State = SeaState.Ship;

            this.Invalidate();
        }
        public void Repaint() {
            this.Invalidate();
        }
    }
}
