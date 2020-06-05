using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QRCoder;
using MySql.Data.MySqlClient;
namespace robotkayit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        MySqlConnection con;
        MySqlDataAdapter da;
        MySqlCommand cmd;
        DataSet ds;
        
        
        public void Fill_Grid()
        {
            con.Open();
            da.Fill(ds, "yarismaa");
            con.Close();
        }


        Boolean yuklumu = false;
        string kategori;
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || comboBox1.SelectedIndex == -1)
            {

                MessageBox.Show("Lütfen gerekli kısımları doldurunuz.");

            }
            else
            {
                
                QRCodeGenerator qr = new QRCodeGenerator();
                if (comboBox1.SelectedIndex == 0)
                    kategori = "ci";
                else if (comboBox1.SelectedIndex == 1)
                    kategori = "lc";
                else if (comboBox1.SelectedIndex == 2)
                    kategori = "ys";
                else if (comboBox1.SelectedIndex == 3)
                    kategori = "ms";
                else if (comboBox1.SelectedIndex == 4)
                    kategori = "sk";
                else
                    kategori = "rc";

                string yazi = textBox1.Text + kategori;
                QRCodeData data = qr.CreateQrCode(yazi, QRCodeGenerator.ECCLevel.Q);
                QRCode code = new QRCode(data);
                pictureBox1.Image = code.GetGraphic(5);
                con.Open();
                string sql = "Insert into yarismaa (kategori,kod,robot_adi,onay) Values (@kategori,@kod,@robot_adi,@onay) ";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                
                cmd.Parameters.AddWithValue("@kategori", kategori);
                cmd.Parameters.AddWithValue("@kod", yazi);
                cmd.Parameters.AddWithValue("@robot_adi", textBox1.Text.ToString());
                cmd.Parameters.AddWithValue("@onay", false);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            
            sfd.Filter = "jpeg dosyası(*.jpg)|*.jpg|Bitmap(*.bmp)|*.bmp";
            sfd.Title = "Kayıt";
            sfd.FileName = "barkodum";
            if (textBox1.Text == "" || comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen gerekli kısımları doldurunuz.");
            }
            else
            {
                DialogResult sonuc = sfd.ShowDialog();
                if (sonuc == DialogResult.OK && yuklumu)
                {
                    pictureBox1.Image.Save(sfd.FileName);
                }
            }
            
            
        }

        private void basla(object sender, EventArgs e)
        {
            con = new MySqlConnection("server=localhost;database=yarisma;Uid=root;Pwd=''; AllowUserVariables=True;");
            da = new MySqlDataAdapter("Select * from yarismaa", con);
            ds = new DataSet();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            /*
            System.Drawing.Image img = System.Drawing.Image.FromFile("C:\\Users\\yaren\\Desktop\\barkodum.jpg");
            Point loc = new Point(100, 100);
            e.Graphics.DrawImage(img, loc);
            */
        }
    }
}
