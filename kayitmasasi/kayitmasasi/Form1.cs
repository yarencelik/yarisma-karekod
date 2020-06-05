using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using MessagingToolkit.QRCode;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Reader;
using MessagingToolkit.QRCode.Crypt;
using MySql.Data;
using MySql.Data.MySqlClient;
using ZXing;

namespace kayitmasasi
{
    public partial class Form1 : Form
    {
        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCaptureDevice;


        MySqlConnection con;
        MySqlDataAdapter da;
        MySqlCommand cmd;
        DataSet ds;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            timer1.Start();
            con = new MySqlConnection("server=localhost;database=yarisma;Uid=root;Pwd='';");
            da = new MySqlDataAdapter("Select * from yarismaa", con);
            ds = new DataSet();
            con.Open();

            guncelle_kod();
            string komut = "UPDATE yarismaa SET onay = true WHERE kod = 'ys DFG' ";
            cmd = new MySqlCommand(komut, con);
            cmd.ExecuteNonQuery();


            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo Device in filterInfoCollection)
                comboBox1.Items.Add(Device.Name);
            comboBox1.SelectedIndex = 0;
            videoCaptureDevice = new VideoCaptureDevice();
        }
        void cam_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bit = (Bitmap)eventArgs.Frame.Clone();
            pictureBox1.Image = bit;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[comboBox1.SelectedIndex].MonikerString);
            videoCaptureDevice.NewFrame += FinalFrame_NewFrame;
            videoCaptureDevice.Start();
        }
        private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        public void guncelle_kod()
        {
            
            try
            {
                string komut = "UPDATE yarismaa SET onay = true WHERE kod = '" + textBox2.Text + "'";
                cmd = new MySqlCommand(komut, con);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Robot kayıtlı değil!");
            }
            
        }
        public void guncelle_elle()
        {
            try
            {
                
                string komut = "UPDATE yarismaa SET onay = true WHERE kod = '" + textBox1.Text + "' ";
                cmd = new MySqlCommand(komut, con);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Robot kayıtlı değil!");
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Result result=null;
            BarcodeReader Reader = new BarcodeReader();
            try
            {
                result = Reader.Decode((Bitmap)pictureBox1.Image);
                textBox2.Text = result.ToString();
                guncelle_kod();
            }
            catch
            {
                timer1.Stop();
                MessageBox.Show("Lütfen kodu elle giriniz.");
                textBox1.BackColor = Color.Red;
                videoCaptureDevice.Stop();
                
            }
                   
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            try
            {
                string komut = "UPDATE yarismaa SET onay = true WHERE kod = '" + textBox2.Text + "' ";
                cmd = new MySqlCommand(komut, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Onay başarılı.");
            }
            catch
            {
                MessageBox.Show("Robot kayıtlı değil!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            try
            {

                string komut = "UPDATE yarismaa SET onay = true WHERE kod = '" + textBox1.Text + "'";

                cmd = new MySqlCommand(komut, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Onay başarılı.");
            }
            catch
            {
                MessageBox.Show("Robot kayıtlı değil!");
            }
        }
    }
}
