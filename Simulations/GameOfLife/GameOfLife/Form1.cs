using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife
{
	public partial class Form1 : Form
	{
		World world;
		bool pause = true;
		public Form1()
		{
			world = new World(800, 600, 10);
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			
		}

		private void Form1_Paint1(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			world.Draw(g);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			timer1.Enabled = !timer1.Enabled;
		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			world.Update();
			label1.Text = "Temperature: " + world.Temperature;
			Refresh();
		}
	}
}
