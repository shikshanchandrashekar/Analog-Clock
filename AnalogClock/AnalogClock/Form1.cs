using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalogClock
{
    public partial class AnalogClock : Form
    {
        Timer t = new Timer();
        int width = 300, height = 300, SecHand = 140, MinHand = 140, HourHand = 100;
        int CenX, CenY;
        Bitmap btMap;
        Graphics g;

        public AnalogClock()
        {
            InitializeComponent();
        }

        private void AnalogClock_Load(object sender, EventArgs e)
        {
            btMap = new Bitmap(width + 3, height + 3);
            CenX = width / 2;
            CenY = height / 2;
            this.BackColor = Color.Black;
            t.Interval = 1000;
            t.Tick += new EventHandler(this.t_Tick);
            t.Start();
        }
        private void t_Tick(Object sender, EventArgs e)
        {
            g = Graphics.FromImage(btMap);

            int sec = DateTime.Now.Second;
            int min = DateTime.Now.Minute;
            int hour = DateTime.Now.Hour;

            int[] HandSync = new int[2];

            g.Clear(Color.Black);

            g.DrawEllipse(new Pen(Color.Lime,1f),0,0,width,height);

            g.DrawString("1", new Font("Adobe Gothic Std B", 12), Brushes.Lime, new Point(210, 25));
            g.DrawString("2", new Font("Adobe Gothic Std B", 12), Brushes.Lime, new Point(263, 75));
            g.DrawString("3", new Font("Adobe Gothic Std B", 12), Brushes.Lime, new Point(286, 140));
            g.DrawString("4", new Font("Adobe Gothic Std B", 12), Brushes.Lime, new Point(263, 210));
            g.DrawString("5", new Font("Adobe Gothic Std B", 12), Brushes.Lime, new Point(218, 260));
            g.DrawString("6", new Font("Adobe Gothic Std B", 12), Brushes.Lime, new Point(140, 283));
            g.DrawString("7", new Font("Adobe Gothic Std B", 12), Brushes.Lime, new Point(72, 260));
            g.DrawString("8", new Font("Adobe Gothic Std B", 12), Brushes.Lime, new Point(22, 210));
            g.DrawString("9", new Font("Adobe Gothic Std B", 12), Brushes.Lime, new Point(3, 140));
            g.DrawString("10", new Font("Adobe Gothic Std B", 12), Brushes.Lime, new Point(22, 75));
            g.DrawString("11", new Font("Adobe Gothic Std B", 12), Brushes.Lime, new Point(70, 25));
            g.DrawString("12", new Font("Adobe Gothic Std B", 12), Brushes.Lime, new Point(140, 3));

            HandSync = msCoord(sec, SecHand);
            g.DrawLine(new Pen(Color.Red, 1f),new Point(CenX,CenY),new Point(HandSync[0],HandSync[1]));

            HandSync = msCoord(min, MinHand);
            g.DrawLine(new Pen(Color.Lime, 2f), new Point(CenX, CenY), new Point(HandSync[0], HandSync[1]));

            HandSync = hrCoord(hour%12,min, HourHand);
            g.DrawLine(new Pen(Color.Lime, 3f), new Point(CenX, CenY), new Point(HandSync[0], HandSync[1]));

            pictureBox1.Image = btMap;

            this.Text = "Analog Clock - " + hour + ":" + min + ":" + sec;

            g.Dispose();
        }
        private int[] msCoord(int Value, int HandLen)
        {
            int[] coord = new int[2];
            Value *= 6;
            if (Value >= 0 && Value <= 180)
            {
                coord[0] = CenX + (int)(HandLen * Math.Sin(Math.PI * Value / 180));
                coord[1] = CenY- (int)(HandLen * Math.Cos(Math.PI * Value / 180));
            }
            else
            {
                coord[0] = CenX - (int)(HandLen * -Math.Sin(Math.PI * Value / 180));
                coord[1] = CenY - (int)(HandLen * Math.Cos(Math.PI * Value / 180));
            }
            return coord;
        }
        private int[] hrCoord(int hValue,int mValue, int HandLen)
        {
            int[] coord = new int[2];

            int Value = (int)((hValue) * 30+mValue*0.5);

            if (Value >= 0 && Value <= 180)
            {
                coord[0] = CenX + (int)(HandLen * Math.Sin(Math.PI * Value / 180));
                coord[1] = CenY - (int)(HandLen * Math.Cos(Math.PI * Value / 180));
            }
            else
            {
                coord[0] = CenX - (int)(HandLen * -Math.Sin(Math.PI * Value / 180));
                coord[1] = CenY - (int)(HandLen * Math.Cos(Math.PI * Value / 180));
            }
            return coord;
        }
    }
}
