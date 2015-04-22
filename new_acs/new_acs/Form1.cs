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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=tra.mdb");    //使用指定的連接字串，初始化 OleDbConnection 類別的新執行個體
        private void Form1_Load(object sender, EventArgs e)
        {
            con.Open();                                           //開啟資料庫連接

            OleDbDataReader dr;
            string search = "SELECT * FROM Sights";
            OleDbCommand cmd = new OleDbCommand(search, con);     //使用查詢文字和 OleDbConnection 來初始化 OleDbCommand 類別的新執行個體。
            dr = cmd.ExecuteReader();           //OleDbDataReader:從資料庫獲取行

            while (dr.Read())
            {
                comboBox1.Items.Add(dr["attractions"].ToString());
                comboBox2.Items.Add(dr["attractions"].ToString());
            }
            dr.Close();                 //關閉 OleDbDataReader 物件         
            cmd.Dispose();              //釋放所使用的所有資源
            con.Close();                //關閉資料連接
        }
        
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            con.Open();

            OleDbDataReader dr;
            string search = "select addr from Sights where attractions='" + comboBox1.Text + "'";
            OleDbCommand cmd = new OleDbCommand(search, con);
            dr = cmd.ExecuteReader();
            int count = dr.FieldCount;

            while (dr.Read())
                textBox1.Text = dr["addr"].ToString();
            dr.Close();
            cmd.Dispose();
            con.Close();   
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();

            OleDbDataReader dr;
            string search = "select addr from Sights where attractions='" + comboBox2.Text + "'";
            OleDbCommand cmd = new OleDbCommand(search, con);
            dr = cmd.ExecuteReader();
            int count = dr.FieldCount;

            while (dr.Read())
                    textBox2.Text = dr["addr"].ToString();
            dr.Close();                        
            cmd.Dispose();              
            con.Close();                
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
} 
