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

namespace CG_3D
{
    public partial class ChristmasTree : Form
    {
        double xrot, yrot, zrot;
        double eyeX, eyeY, eyeZ, atX, atY, atZ;


    public ChristmasTree()
        {
            InitializeComponent();
            simpleOpenGlControl1.InitializeContexts();
            int w = simpleOpenGlControl1.Width;
            int h = simpleOpenGlControl1.Height;
            Gl.glViewport(0, 0, w, h);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluPerspective(45.0f, (double)w / h, 0.1f, 5000.0f);
            xrot = yrot = zrot = 0;
            atX = atY = 0; atZ = -1;
            eyeX = eyeZ = 7; eyeY = 4;

        }

        private void simpleOpenGlControl1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'i')
            {
                eyeX -= 0.8;
                eyeZ -= 0.8;
            }

            if (e.KeyChar == 'o')
            {
                eyeX += 0.8;
                eyeZ += 0.8;
            }

            if (e.KeyChar == 'x')
                xrot += 15;
            if (e.KeyChar == 'y')
                yrot += 15;
            if (e.KeyChar == 'z')
                zrot += 15;

            simpleOpenGlControl1.Invalidate();

        }

        private void tree()
        {
            Gl.glColor3f(0, 0, 1);
            //Gl.glRotatef(90, 1, 0, 0);
            Glu.gluCylinder(Glu.gluNewQuadric(), 0.5f, 0.5f, 1, 32, 32);
            Gl.glPushMatrix();
            Gl.glTranslatef(0, 0, -1.5f);
            Glu.gluCylinder(Glu.gluNewQuadric(), 0, 1.5, 1.5f, 32, 32);
            Gl.glPopMatrix();
            Gl.glPushMatrix();
            Gl.glTranslatef(0, 0, -2.5f);
            Glu.gluCylinder(Glu.gluNewQuadric(), 0, 1.2, 1.5f, 32, 32);
            Gl.glPopMatrix();
            Gl.glPushMatrix();
            Gl.glTranslatef(0, 0, -3.5f);
            Glu.gluCylinder(Glu.gluNewQuadric(), 0, 1, 1.5f, 32, 32);
            Gl.glPopMatrix();
        }

        private void simpleOpenGlControl1_Paint(object sender, PaintEventArgs e)
        {
            Gl.glClearColor(0.8f, 0.8f, 0.8f, 0);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            //Glu.gluLookAt(5, 4, 5, 0, 0, 0, 0, 1, 0);
            Glu.gluLookAt(eyeX, eyeY, eyeZ, atX, atY, atZ, 0, 1, 0);

            Gl.glPushMatrix();
            Gl.glRotated(xrot, 1, 0, 0);
            Gl.glRotated(yrot, 0, 1, 0);
            Gl.glRotated(zrot, 0, 0, 1);

            Gl.glBegin(Gl.GL_LINES);
            //X axis
            Gl.glColor3f(1, 0, 0);
            Gl.glVertex3f(-6, 0, 0);
            Gl.glVertex3f(6, 0, 0);
            //Y axis
            Gl.glColor3f(0, 1, 0);
            Gl.glVertex3f(0, -6, 0);
            Gl.glVertex3f(0, 6, 0);
            //z axis
            Gl.glColor3f(0, 0, 1);
            Gl.glVertex3f(0, 0, -6);
            Gl.glVertex3f(0, 0, 6);
            Gl.glEnd();

            // XZ Plane
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glColor3f(0, 0.7f, 0);
            Gl.glVertex3f(5, 0, 5);
            Gl.glVertex3f(5, 0, -5);
            Gl.glVertex3f(-5, 0, -5);
            Gl.glVertex3f(-5, 0, 5);
            Gl.glEnd();

            // Tree 1
            Gl.glPushMatrix();
            Gl.glTranslated(4.3, 0, 4.3);
            Gl.glRotatef(90, 1, 0, 0);
            Gl.glScaled(0.5, 0.5, 0.5);
            tree();
            Gl.glPopMatrix();

            // Tree 2
            Gl.glPushMatrix();
            Gl.glTranslated(4.3, 0, -4.3);
            Gl.glRotatef(90, 1, 0, 0);
            Gl.glScaled(0.5, 0.5, 0.5);
            tree();
            Gl.glPopMatrix();

            // Tree 3
            Gl.glPushMatrix();
            Gl.glTranslated(-4.3, 0, -4.3);
            Gl.glRotatef(90, 1, 0, 0);
            Gl.glScaled(0.5, 0.5, 0.5);
            tree();
            Gl.glPopMatrix();

            // Tree 4
            Gl.glPushMatrix();
            Gl.glTranslated(-4.3, 0, 4.3);
            Gl.glRotatef(90, 1, 0, 0);
            Gl.glScaled(0.5, 0.5, 0.5);
            tree();
            Gl.glPopMatrix();

            Gl.glPopMatrix();

        }
    }
}
