namespace GameOfLife.Forms
{
    public partial class GameOfLifeGrid : Form
    {
        public GameOfLifeGrid()
        {
            InitializeComponent();
        }

        private void GameOfLifeGrid_Paint(object sender, PaintEventArgs e)
        {
            using (var graphics = e.Graphics)
            {
                for(int i = 0; i < 1000; i+= 10)
                {
                    graphics.DrawLine(Pens.Red, new Point(i, 0), new Point(i, 100));
                }
            };
        }
    }
}