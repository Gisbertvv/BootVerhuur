namespace DigitalSignature
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        float PointX = 0;
        float PointY = 0;

        float LastX = 0;
        float LastY = 0;

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = panel1.CreateGraphics();
            g.DrawLine(Pens.Black, PointX, PointY, LastX, LastY);
            LastX = PointX;
            LastY = PointY;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            // Reset last X & Y position to be the current mouse position when button is clicked
            LastX = e.X;
            LastY = e.Y;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Update point X & Y to current mouse position
                PointX = e.X;
                PointY = e.Y;
                panel1_Paint(this, null);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Save as image
            Image bmp = new Bitmap(panel1.Width, panel1.Height);
            var gg = Graphics.FromImage(bmp);
            var rect = panel1.RectangleToScreen(panel1.ClientRectangle);
            gg.CopyFromScreen(rect.Location, Point.Empty, panel1.Size);

            //Add to local folder and make screen blank
            bmp.Save("D:\\OOSDDb\\BootVerhuur\\DigitalSignature\\Images\\Test.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            Graphics g = panel1.CreateGraphics();
            g.Clear(Color.White);
        }
    }
}