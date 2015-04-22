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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=tra.mdb");
        private void Form2_Load(object sender, EventArgs e) //讀資料(want資料表)
        {
            con.Open();                                           

            OleDbDataReader dr;
            string search = "SELECT * FROM Want";
            OleDbCommand cmd = new OleDbCommand(search, con);     
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                listBox1.Items.Add(dr["name"].ToString());
            }
            dr.Close();                  
            cmd.Dispose();              
            con.Close();

            button2.Enabled = false;
            button3.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e) //開啟form3(新增想去的景點)
        {
            Form3 add = new Form3();
            this.Visible = false;
            add.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e) //刪除想去(want資料表)的景點
        {
            con.Open();

            for(int i = 0; i<listBox1.Items.Count;i++)
            {
                if (listBox1.GetSelected(i))
                {
                    string search = "DELETE * FROM Want WHERE name = '" + listBox1.Items[i] + "'";
                    
                    OleDbDataAdapter da = new OleDbDataAdapter(search, con);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //OleDbCommand cmd = new OleDbCommand(search, con);
                    //cmd.ExecuteNonQuery();
                    //cmd.Dispose();
                    
                    listBox1.Items.Remove(listBox1.Items[i]);
                }
            }
            con.Close();
            MessageBox.Show("已成功將景點刪除", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (listBox1.Items.Count == 0)
            {
                button2.Enabled = false;
                button3.Enabled = false;
            } 
        }

        private void button3_Click(object sender, EventArgs e)  //在form4顯示景點的資訊 
        {
            Form4 form4 = new Form4();
            
            con.Open();

            OleDbDataReader dr;
            
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.GetSelected(i))
                {
                    string search = "SELECT * FROM Sights WHERE attractions = '" + listBox1.Items[i] + "' ";
                    OleDbCommand cmd = new OleDbCommand(search, con);
                    dr = cmd.ExecuteReader();
                    cmd.Dispose();

                    while (dr.Read())
                    {
                        form4.label1.Text = dr["attractions"].ToString();
                        form4.label2.Text = dr["tel"].ToString();
                        form4.linkLabel1.Text = dr["website"].ToString();
                        form4.label4.Text = dr["addr"].ToString();
                        form4.label5.Text = dr["opening_hour"].ToString();
                        form4.label6.Text = dr["star"].ToString();
                    }
                    dr.Close();
                    con.Close();
                }
            }
            this.Visible = false;
            form4.ShowDialog();
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.GetSelected(i))
                {
                    button2.Enabled = true;
                    button3.Enabled = true;
                }
            }
        }
    }
}