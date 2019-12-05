using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace L12019v3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            Bitmap image;

            OpenFileDialog open_dialog = new OpenFileDialog();
            open_dialog.Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG|All files (*.*)|*.*";
            if (open_dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    image = new Bitmap(open_dialog.FileName);

                    this.pictureBox1.Size = image.Size;
                    pictureBox1.Image = image;
                    pictureBox1.Invalidate();
                }
                catch
                {
                    DialogResult rezult = MessageBox.Show("Невозможно открыть выбранный файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap input = new Bitmap(pictureBox1.Image);
                Bitmap output = new Bitmap(input.Width, input.Height);
                for (int j = 0; j < input.Height; j++)
                    for (int i = 0; i < input.Width; i++)
                    {
                        UInt32 pixel = (UInt32)(input.GetPixel(i, j).ToArgb());
                        double R = (float)((pixel & 0x00FF0000) >> 16); // красный
                        double G = (float)((pixel & 0x0000FF00) >> 8); // зеленый
                        double B = (float)(pixel & 0x000000FF); // синий
                        R = G = B = 0.3*R + 0.59*G + 0.11*B;
                        UInt32 newPixel = 0xFF000000 | ((UInt32)R << 16) | ((UInt32)G << 8) | ((UInt32)B);
                        output.SetPixel(i, j, Color.FromArgb((int)newPixel));
                    }
                pictureBox1.Image = output;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap input = new Bitmap(pictureBox1.Image);
                Bitmap output = new Bitmap(input.Width, input.Height);
                for (int j = 0; j < input.Height; j++)
                    for (int i = 0; i < input.Width; i++)
                    {
                        UInt32 pixel = (UInt32)(input.GetPixel(i, j).ToArgb());
                        float R = (float)((pixel & 0x00FF0000) >> 16); // красный
                        float G = (float)((pixel & 0x0000FF00) >> 8); // зеленый
                        float B = (float)(pixel & 0x000000FF); // синий

                        int c = 255-(int)(R * 0.3 + G * 0.59 + B * 0.11);
                        UInt32 newPixel = 0xFF000000 | ((UInt32)c << 16) | ((UInt32)c << 8) | ((UInt32)c);
                        output.SetPixel(i, j, Color.FromArgb((int)newPixel));
                    }
                pictureBox1.Image = output;

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(pictureBox1.Image);
            Bitmap binary = binaryGo(image);

            pictureBox1.Image = binary;

        }

        private Bitmap binaryGo(Bitmap image)
        {
            Bitmap ser = serGo(image);
            int temp = 0;
            int bin = 127;
            Color color;

            for (int i = 0; i < ser.Height - 1; i++)
            {
                for (int j = 0; j < ser.Width - 1; j++)
                {
                    temp = ser.GetPixel(j, i).R;

                    if (temp < bin)
                    {

                        color = Color.FromArgb(0, 0, 0);
                        ser.SetPixel(j, i, color);
                    }
                    else
                    {
                        color = Color.FromArgb(255, 255, 255);
                        ser.SetPixel(j, i, color);
                    }

                }
            }
            return ser;
        }

        private Bitmap serGo(Bitmap bmp)
        {

            int number = 0;

            Color color;

            for (int i = 0; i < bmp.Height - 1; i++)
            {
                for (int j = 0; j < bmp.Width - 1; j++)
                {
                    number = (int)(bmp.GetPixel(j, i).R*0.3 + bmp.GetPixel(j, i).G*0.59 + bmp.GetPixel(j, i).B*0.11) ;

                    color = Color.FromArgb(number, number, number);

                    bmp.SetPixel(j, i, color);


                }
            }
            return bmp;
        }
    }
}
    

