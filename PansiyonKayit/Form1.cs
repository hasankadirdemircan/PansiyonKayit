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
        SqlConnection connection = new SqlConnection("Data Source=MSI;Initial Catalog=pansiyon;Integrated Security=True");
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
            textBox4.Clear();
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
            SqlCommand command = new SqlCommand("insert into musteriler (id,Ad,Soyad,OdaNo,GTarih,Telefon,Hesap,CTarih) values('"+textBox1.Text.ToString()+"','"+textBox2.Text.ToString()+"','"+textBox3.Text.ToString()+"','"+textBox4.Text.ToString()+"','"+dateTimePicker1.Text.ToString()+"','"+textBox5.Text.ToString()+"','"+textBox6.Text.ToString()+"','"+dateTimePicker2.Text.ToString()+"')",connection);
            command.ExecuteNonQuery();
            connection.Close();
            textBoxTemizle();
            verileriGoster();
        }
        int id = 0;
        private void button3_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("delete from musteriler where id = (" + id + ")", connection);
            command.ExecuteNonQuery();
            connection.Close();
            textBoxTemizle();
            verileriGoster();

        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);

            textBox1.Text = listView1.SelectedItems[0].SubItems[0].Text;
            textBox2.Text = listView1.SelectedItems[0].SubItems[1].Text;
            textBox3.Text = listView1.SelectedItems[0].SubItems[2].Text;
            textBox4.Text = listView1.SelectedItems[0].SubItems[3].Text;
            dateTimePicker1.Text = listView1.SelectedItems[0].SubItems[4].Text;
            textBox5.Text = listView1.SelectedItems[0].SubItems[5].Text;
            textBox6.Text = listView1.SelectedItems[0].SubItems[6].Text;
            dateTimePicker2.Text = listView1.SelectedItems[0].SubItems[7].Text;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("update musteriler set id ='" + textBox1.Text.ToString() + "',Ad='" + textBox2.Text.ToString() + "',Soyad='" + textBox3.Text.ToString() + "',OdaNo='" + textBox4.Text.ToString() + "',GTarih='" + dateTimePicker1.Text.ToString() + "',Telefon='" + textBox5.Text.ToString() + "',Hesap='" + textBox6.Text.ToString() + "',CTarih='" + dateTimePicker2.Text.ToString()+"'where id ="+id+"",connection);
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
    }
}
