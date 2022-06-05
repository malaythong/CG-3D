using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tao.OpenGl;
using System.Media;
using System.IO;
using System.Drawing.Imaging;


namespace CG_3D
{
    public partial class Texture_Mapping : Form
    {
        string soundfile, soundChoice;
        Boolean s;
        Bitmap image;
        public Texture_Mapping()
        {
            InitializeComponent();
            simpleOpenGlControl1.InitializeContexts();
            int w = simpleOpenGlControl1.Width;
            int h = simpleOpenGlControl1.Height;
            Gl.glViewport(0, 0, w, h);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluPerspective(40, (double)(w / h), 0.1, 5000);

        }


        

        private void simpleOpenGlControl1_Paint(object sender, PaintEventArgs e)
        {
            Gl.glClearColor(0.8f, 0.8f, 0.8f, 0.0f);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            Glu.gluLookAt(3, 3, 7, 0, 0, -1, 0, 1, 0);
            play();
            //Texture Mapping //step1
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            if (soundChoice == "all-day all-night")
                image = new Bitmap("Oil.jpg");
            else if (soundChoice == "ຕື່ນຈາກຝັນ")
                image = new Bitmap("Exchange.jpg");
            else if (soundChoice == "BeLao")
                image = new Bitmap("LaoLovely.jpg");
            else
                image = new Bitmap("LaoSuSu.jpg");
            BitmapData bitmapdata;
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
            bitmapdata = image.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            uint[] texture = new uint[1]; //step2
            Gl.glGenTextures(1, texture);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[0]);
            Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, (int)Gl.GL_RGB8, image.Width, image.Height, 0,
                            Gl.GL_BGR_EXT, Gl.GL_UNSIGNED_BYTE, bitmapdata.Scan0);

            Gl.glTexParameteri((int)Gl.GL_TEXTURE_2D, (int)Gl.GL_TEXTURE_MIN_FILTER, (int)Gl.GL_LINEAR);//GL_LINEAR

            float sz = 3.0f; //step3
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3f(-sz, -sz, 0.0f);
            Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3f(sz, -sz, 0.0f);
            Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3f(sz, sz, 0.0f);
            Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3f(-sz, sz, 0.0f);
            Gl.glEnd();

            image.UnlockBits(bitmapdata);
            image.Dispose();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            soundChoice = "all-day all-night"; simpleOpenGlControl1.Invalidate();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            soundChoice = "ຕື່ນຈາກຝັນ"; simpleOpenGlControl1.Invalidate();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            soundChoice = "BeLao"; simpleOpenGlControl1.Invalidate();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            soundChoice = "LaoSuSu"; simpleOpenGlControl1.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            s = true; simpleOpenGlControl1.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            s = false; simpleOpenGlControl1.Invalidate();
        }

        private void play()
        {
            if (soundChoice == "all-day all-night")
            {
                soundfile = @"All day all night.wav";
            }
            else if (soundChoice == "ຕື່ນຈາກຝັນ")
            {
                soundfile = @"ຕື່ນຈາກຝັນ.wav";
            }
            else if (soundChoice == "BeLao")
            {
                soundfile = @"Be Lao.wav";
            }
            else
            {
                soundfile = @"ຄຽງຂ້າງຕະຫລອດໄປ.wav";
            }
            byte[] bt = File.ReadAllBytes(soundfile);
            var sound = new System.Media.SoundPlayer(soundfile);
            if (s == true)
            {
                sound.PlayLooping();
            }
            else
                sound.Stop();
        }
    }
}
