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
        private int cellsCount = 10;
        private int gridRightBottomPadding = 10;
        private int cellWidth, cellHeight;
        private int lastCellRow, lastCellColumn;
        public bool IsClickable { get; set; }
        public Controller Controller { get; set; }
        public Cell[,] BattleFieldData { get; private set; }
        //public BattleField() {
        //    this.BattleFieldData = (new BattleFieldModel()).BattleFieldData;
        //    InitializeComponent();
        //    SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint |
        //    ControlStyles.AllPaintingInWmPaint, true);
        //    UpdateStyles();
        //}
        public BattleField(BattleFieldModel battleFieldModel) {
            this.BattleFieldData = battleFieldModel.BattleFieldData;
            InitializeComponent();
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
        }
        protected override void OnPaint(PaintEventArgs e) {
            using (Pen pen = new Pen(Color.Fuchsia)) {
            cellHeight = (this.Height - gridRightBottomPadding / cellsCount) / cellsCount;
            cellWidth = (this.Width - gridRightBottomPadding / cellsCount) / cellsCount;
                for (int y = 0; y <= 10; y++) {
                    int yPos = y * cellHeight;
                    e.Graphics.DrawLine(pen, 0, yPos, this.Width - gridRightBottomPadding, yPos);
                }

                for (int x = 0; x <= 10; x++) {
                    int xPos = x * cellWidth;
                    e.Graphics.DrawLine(pen, xPos, 0, xPos, this.Height - gridRightBottomPadding);
                }
                FillAllCells(e);
            }
        }
        private void FillAllCells(PaintEventArgs e) {
            Brush blueBrush = new SolidBrush(Color.Aqua);
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
        protected void OnRepaintCell(PaintEventArgs e) {

        }
        protected override void OnMouseDown(MouseEventArgs e) {
            base.OnMouseDown(e);
            int cellWidth = this.Width / 10;
            int cellHeight = this.Height / 10;
            lastCellRow = e.Y / cellHeight;
            lastCellColumn = e.X / cellWidth;
            Controller.Resolve(lastCellRow, lastCellColumn, this);

            //BattleFieldData[row, col].State = SeaState.Ship;

            //Repaint();
        }
        public void Repaint(bool repaintOne) {
            
            this.Invalidate();
        }
    }
}
