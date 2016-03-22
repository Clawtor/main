using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SegregationModel
{
	public partial class Form1 : Form
	{
		World world;
        public Form1()
		{
			InitializeComponent();
			world = new World(800,600,5, 0.75, 0.25);
			typeof(Panel).InvokeMember("DoubleBuffered",
				BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
				null, this.panel1, new object[] { true });
			timer1.Start();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.world.Update();
			this.Refresh();
		}

		private void panel1_Paint(object sender, PaintEventArgs e)
		{
			this.world.Draw(e.Graphics);
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			this.world.Update();
			this.Refresh();
		}
	}
}
