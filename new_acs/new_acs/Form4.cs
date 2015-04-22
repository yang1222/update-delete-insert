using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace new_acs
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=tra.mdb");
        private void Form4_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            textBox6.Visible = false;
            button2.Visible = false;
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox6.Visible = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (textBox6.Text == "manage")
            {
                button2.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox4.Visible = true;
                textBox5.Visible = true;
            }
            if (textBox1.Text != "" || textBox2.Text != "" || textBox3.Text != "" || textBox4.Text != "" || textBox5.Text != "")
                button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e) //更新
        {
            for (int x = 0; x < 6; x++)
            {
                if (textBox1.Text == "")
                    textBox1.Text = label4.Text;
                if (textBox2.Text == "")
                    textBox2.Text = linkLabel1.Text;
                if (textBox3.Text == "")
                    textBox3.Text = label5.Text;
                if (textBox4.Text == "")
                    textBox4.Text = label2.Text;
                if (textBox5.Text == "")
                    textBox5.Text = label6.Text;
            }
            con.Open();
            string search = "UPDATE Sights SET addr = '" + textBox1.Text + "', website = '" + textBox2.Text + "', opening_hour = '" + textBox3.Text + "', tel ='" + textBox4.Text + "', star = '" + textBox5.Text + "' WHERE attractions = '" + label1.Text + "'";
            OleDbCommand cmd = new OleDbCommand(search, con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();

            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 main = new Form2();
            this.Visible = false;
            main.ShowDialog();
        }
    }
}
