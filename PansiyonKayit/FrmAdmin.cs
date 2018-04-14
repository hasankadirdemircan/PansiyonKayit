using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PansiyonKayit
{
    public partial class FrmAdmin : Form
    {
        public FrmAdmin()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-3TC5PRO;Initial Catalog=pansiyon;Integrated Security=True");

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox3.Text = textBox3.Text.Substring(1) + textBox3.Text.Substring(0, 1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String username;
            String password;
            String sorgu;
            username = textBox1.Text.ToString();
            password = textBox1.Text.ToString();
            connection.Open();
            //Buraya veritabanından username password kontrolu yapilacak.
            
        }
    }
}
