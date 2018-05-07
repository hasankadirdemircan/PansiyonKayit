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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-3TC5PRO;Initial Catalog=pansiyon;Integrated Security=True");
        private void verileriGoster()
        {
            listView1.Items.Clear();
            connection.Open();
            SqlCommand command = new SqlCommand("select *from musteriler",connection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = reader["id"].ToString();
                ekle.SubItems.Add(reader["Ad"].ToString());
                ekle.SubItems.Add(reader["Soyad"].ToString());
                ekle.SubItems.Add(reader["OdaNo"].ToString());
                ekle.SubItems.Add(reader["GTarih"].ToString());
                ekle.SubItems.Add(reader["Telefon"].ToString());
                ekle.SubItems.Add(reader["Hesap"].ToString());
                ekle.SubItems.Add(reader["CTarih"].ToString());

                listView1.Items.Add(ekle);
            }
            connection.Close();

        }

        private void textBoxTemizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
          
            textBox5.Clear();
            textBox6.Clear();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            verileriGoster();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            connection.Open();
            DateTime tarih1 = dateTimePicker1.Value; // 
            DateTime tarih2 = dateTimePicker2.Value; //
            TimeSpan fark = tarih2 - tarih1;
         
            int farkInt = Convert.ToInt32(fark.Days);
            int hesap = farkInt * 100;
            String hesapString = hesap.ToString() + "  TL";
            SqlCommand command = new SqlCommand("insert into musteriler (Ad,Soyad,OdaNo,GTarih,Telefon,Hesap,CTarih) values('"+textBox2.Text.ToString()+"','"+textBox3.Text.ToString()+"','"+comboBox1.Text.ToString()+"','"+dateTimePicker1.Text.ToString()+"','"+textBox5.Text.ToString()+"','"+ hesapString.ToString()+"','"+dateTimePicker2.Text.ToString()+"')",connection);
            command.ExecuteNonQuery();
            command.CommandText = "insert into doluoda(doluyerler) values('" + comboBox1.Text + "')";
            command.ExecuteNonQuery();
            command.CommandText = ("delete from bosoda where bosyerler='" + comboBox1.Text + "'");
            command.ExecuteNonQuery();
            connection.Close();
            textBoxTemizle();
            verileriGoster();
            comboBoxGuncelle();
        }
        int id = 0;
        private void button3_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("delete from musteriler where id = (" + id + ")", connection);
            command.ExecuteNonQuery();
            command.CommandText = "insert into bosoda(bosyerler) values('" + comboBox1.Text + "')";
            command.ExecuteNonQuery();
            command.CommandText = ("delete from doluoda where doluyerler='" + comboBox1.Text + "'");
            command.ExecuteNonQuery();
            connection.Close();
            textBoxTemizle();
            verileriGoster();
            comboBoxGuncelle();

        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);

            textBox1.Text = listView1.SelectedItems[0].SubItems[0].Text;
            textBox2.Text = listView1.SelectedItems[0].SubItems[1].Text;
            textBox3.Text = listView1.SelectedItems[0].SubItems[2].Text;
            comboBox1.Text = listView1.SelectedItems[0].SubItems[3].Text;
            dateTimePicker1.Text = listView1.SelectedItems[0].SubItems[4].Text;
            textBox5.Text = listView1.SelectedItems[0].SubItems[5].Text;
            textBox6.Text = listView1.SelectedItems[0].SubItems[6].Text;
            dateTimePicker2.Text = listView1.SelectedItems[0].SubItems[7].Text;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("update musteriler set Ad='" + textBox2.Text.ToString() + "',Soyad='" + textBox3.Text.ToString() + "',OdaNo='" + comboBox1.Text.ToString() + "',GTarih='" + dateTimePicker1.Text.ToString() + "',Telefon='" + textBox5.Text.ToString() + "',Hesap='" + textBox6.Text.ToString() + "',CTarih='" + dateTimePicker2.Text.ToString()+"'where id ="+id+"",connection);
            command.ExecuteNonQuery();
            connection.Close();
            verileriGoster();
            textBoxTemizle();
        }
        private void listViewGoster(SqlDataReader reader)
        {
            listView1.Items.Clear();
            while (reader.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = reader["id"].ToString();
                ekle.SubItems.Add(reader["Ad"].ToString());
                ekle.SubItems.Add(reader["Soyad"].ToString());
                ekle.SubItems.Add(reader["OdaNo"].ToString());
                ekle.SubItems.Add(reader["GTarih"].ToString());
                ekle.SubItems.Add(reader["Telefon"].ToString());
                ekle.SubItems.Add(reader["Hesap"].ToString());
                ekle.SubItems.Add(reader["CTarih"].ToString());

                listView1.Items.Add(ekle);
            }
            connection.Close();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("select *from musteriler where Ad like '%" + textBox7.Text + "%'", connection);
            command.ExecuteNonQuery();
            SqlDataReader reader = command.ExecuteReader();
            listViewGoster(reader);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox4.Text = textBox4.Text.Substring(1) + textBox4.Text.Substring(0, 1);
        }


        private void comboBoxTemizle()
        {
            comboBox1.Items.Clear();
        }

        private void comboBoxGuncelle()
        {
            comboBoxTemizle();
            connection.Open();
            SqlCommand command = new SqlCommand("select *from bosoda order by bosyerler", connection);
            SqlDataReader oda = command.ExecuteReader();

            while (oda.Read())
            {
                comboBox1.Items.Add(oda[0].ToString());
            }
            connection.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            comboBoxGuncelle();
        }

    }
}
