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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        int n;
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=tra.mdb");
        private void Form3_Load(object sender, EventArgs e)
        {
            checkedListBox2.Visible =false;
            con.Open();

            OleDbDataReader dr;
            string search = "SELECT * FROM Sights";
            OleDbCommand cmd = new OleDbCommand(search, con);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                checkedListBox1.Items.Add(dr["attractions"].ToString());
                checkedListBox2.Items.Add(dr["addr"].ToString());
            }
            dr.Close();
            cmd.Dispose();
            
            //
            OleDbDataReader dr_n;
            string search_n = "SELECT name FROM Want";
            OleDbCommand cmd_n = new OleDbCommand(search_n, con);
            dr_n = cmd_n.ExecuteReader();

            while (dr_n.Read())
                n++;
            dr_n.Close();
            cmd_n.Dispose();
            con.Close();
            //算出目前資料表中有幾個資料
            button1.Enabled = false;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            
            try
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        n++;
                        string search = "insert into Want (n, name, addr) values ('" + n + "','" + checkedListBox1.Items[i] + "', '"+checkedListBox2.Items[i]+"')"; // "SELECT name FROM Want WHERE name ='"+checkedListBox1.Items[i]+"'"
                        OleDbCommand cmd = new OleDbCommand(search, con);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        checkedListBox1.SetItemChecked(i, false);
                    }
                }
                con.Close();
                MessageBox.Show("已成功將景點增加到行程規劃", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("此景點已存在" , "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    checkedListBox1.SetItemChecked(i, false);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 home = new Form2();
            this.Visible = false;
            home.ShowDialog();
        }

        private void checkedListBox1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                    button1.Enabled = true;
            }
        }
    }
}
