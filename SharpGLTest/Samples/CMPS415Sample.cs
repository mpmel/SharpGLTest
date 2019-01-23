using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;

namespace SharpGLTest.Samples
{
    class CMPS415Sample : SharpGLSampleBase
    {
        public override void Draw(OpenGL gl)
        {
            setlights(gl);

            //  Clear the color and depth buffers.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            //invert camera pose and store it in M
            make2D(C, c);
            tform_invertRigidBody(c, m);
            make1D(m, M);

            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            gl.Frustum(-1.0, 1.0, -1.0, 1.0, 1.5, 200.0);

            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            //load camera pose in the modelview matrix
            gl.LoadMatrix(M);

            ////drawLine(gl, 0, 0, 0, 4, 0, 0, 1, 0, 0);
            ////drawLine(gl, 0, 0, 0, 0, 4, 0, 0, 1, 0);
            ////drawLine(gl, 0, 0, 0, 0, 0, 4, 0, 0, 1);

            gl.MultMatrix(P);
            drawobjects(gl);
            gl.Flush();
        }

        void drawobjects(OpenGL gl)
        {
            //draws the plane and its axes
            drawLine(gl, 0, 0, 0, 2.5, 0, 0, 1, 0, 0);
            drawLine(gl, 0, 0, 0, 0, 2.5, 0, 0, 1, 0);
            drawLine(gl, 0, 0, 0, 0, 0, 2.5, 0, 0, 1);

            gl.Begin(OpenGL.GL_TRIANGLES);
            gl.Color(0, 0, 1);
            gl.Vertex(0, 0, 1);

            gl.Color(0, 0, 1);
            gl.Vertex(1, 0, -1);

            gl.Color(0, 0, 1);
            gl.Vertex(-1, 0, -1);
            gl.End();

            gl.Begin(OpenGL.GL_TRIANGLES);
            gl.Color(0, 1, 0);
            gl.Vertex(0, 0, 1);

            gl.Color(0, 1, 0);
            gl.Vertex(.5, 0, -1);

            gl.Color(0, 1, 0);
            gl.Vertex(0, 0.5, -1);
            gl.End();

            gl.Begin(OpenGL.GL_TRIANGLES);
            gl.Color(0, 1, 0);
            gl.Vertex(0, 0, 1);

            gl.Color(0, 1, 0);
            gl.Vertex(-.5, 0, -1);

            gl.Color(0, 1, 0);
            gl.Vertex(0, 0.5, -1);
            gl.End();

            gl.Begin(OpenGL.GL_TRIANGLES);
            gl.Color(1, 0, 0);
            gl.Vertex(0, 0, .5);

            gl.Color(1, 0, 0);
            gl.Vertex(-.5, 0, -1);

            gl.Color(1, 0, 0);
            gl.Vertex(-.75, 0.25, -1);
            gl.End();


            gl.Begin(OpenGL.GL_TRIANGLES);
            gl.Color(1, 0, 0);
            gl.Vertex(0, 0, .5);

            gl.Color(1, 0, 0);
            gl.Vertex(.5, 0, -1);

            gl.Color(1, 0, 0);
            gl.Vertex(.75, 0.25, -1);
            gl.End();

            gl.Begin(OpenGL.GL_TRIANGLES);
            gl.Color(1, 0, 0);
            gl.Vertex(0, 0, .5);

            gl.Color(1, 0, 0);
            gl.Vertex(-.25, 0.25, -1);

            gl.Color(1, 0, 0);
            gl.Vertex(-.5, 0.5, -1);
            gl.End();


            gl.Begin(OpenGL.GL_TRIANGLES);
            gl.Color(1, 0, 0);
            gl.Vertex(0, 0, .5);

            gl.Color(1, 0, 0);
            gl.Vertex(.25, 0.25, -1);

            gl.Color(1, 0, 0);
            gl.Vertex(.5, 0.5, -1);
            gl.End();

            gl.Begin(OpenGL.GL_TRIANGLES);
            gl.Color(0, .75, 0);
            gl.Vertex(0.5, 0, -1);

            gl.Color(0, .75, 0);
            gl.Vertex(-.5, 0, -1);

            gl.Color(0, .75, 0);
            gl.Vertex(0, 0.5, -1);
            gl.End();
        }

        void drawLine(OpenGL gl, double x1, double y1, double z1, double x2, double y2, double z2, double r, double g, double b)
        {
            gl.Begin(OpenGL.GL_LINES);
            gl.Color(r, g, b);
            gl.Vertex((x1), (y1), (z1));
            gl.Vertex((x2), (y2), (z2));
            gl.End();
        }

        public override void Initialize(OpenGL gl)
        {
            gl.ClearColor(0.7f, 0.7f, 0.7f, 1.0f);
            gl.Enable(OpenGL.GL_DEPTH_TEST);
            gl.Enable(OpenGL.GL_LIGHT0);
            gl.ShadeModel(OpenGL.GL_SMOOTH);
            
        }

        void setlights(OpenGL gl)
        {
            float[] position = new float[] { 3.0f, 3.0f, 3.0f };
                   float[]    white = new float[] { 1.0f, 1.0f, 1.0f, 1.0f };

            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_POSITION, position);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_DIFFUSE, white);
        }

        void tform_invertRigidBody(double[,] T, double[,] I)
        {
            I[0,0] = T[0,0];
            I[1,0] = T[0,1];
            I[2,0] = T[0,2];

            I[0,1] = T[1,0];
            I[1,1] = T[1,1];
            I[2,1] = T[1,2];

            I[0,2] = T[2,0];
            I[1,2] = T[2,1];
            I[2,2] = T[2,2];

            I[0,3] = -(I[0,0] * T[0,3] + I[0,1] * T[1,3] + I[0,2] * T[2,3]);
            I[1,3] = -(I[1,0] * T[0,3] + I[1,1] * T[1,3] + I[1,2] * T[2,3]);
            I[2,3] = -(I[2,0] * T[0,3] + I[2,1] * T[1,3] + I[2,2] * T[2,3]);
        }


        void make2D(double[] a, double[,] b)
        {
            //converts a one dimensional array into a two dimensional array
            b[0, 0] = a[0];
            b[1, 0] = a[1];
            b[2, 0] = a[2];
            b[0, 1] = a[4];
            b[1, 1] = a[5];
            b[2, 1] = a[6];
            b[0, 2] = a[8];
            b[1, 2] = a[9];
            b[2, 2] = a[10];
            b[0, 3] = a[12];
            b[1, 3] = a[13];
            b[2, 3] = a[14];
        }

        void make1D(double[,] a, double[] b)
        {
            //converts two dimensional array back to one dimensional
            b[0] = a[0, 0];
            b[1] = a[1, 0];
            b[2] = a[2, 0];
            b[4] = a[0, 1];
            b[5] = a[1, 1];
            b[6] = a[2, 1];
            b[8] = a[0, 2];
            b[9] = a[1, 2];
            b[10] = a[2, 2];
            b[12] = a[0, 3];
            b[13] = a[1, 3];
            b[14] = a[2, 3];
            b[3] = 0;
            b[7] = 0;
            b[11] = 0;
            b[15] = 1;
        }

        const double costheta = .9998;
        const double sintheta = .0175;
        const double translation = .2;
        double[] C = new double[16] { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0.1, .1, 12, 1 };
        double[,] c = new double[3, 4];


        double[] P = new double[] { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1, 1, 1, 1 };
        double[,] p = new double[3, 4];
        double[] M = new double[] { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 };
        double[,] m = new double[3, 4];
        double[] I = new double[] { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0.1, 0.1, 12, 1 };
        double[,] i = new double[3, 4];
        double[] I1 = new double[] { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1, 1, 1, 1 };
        double[,] i1 = new double[3, 4];
        double[] FPL = new double[] { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, translation, 1 };
        double[,] f_pl = new double[3, 4];
        double[] FC = new double[] { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, -translation, 1 };
        double[,] f_c = new double[3, 4];
        double[] FCN = new double[] { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, translation, 1 };
        double[,] f_cn = new double[3, 4];
        double[] YP = new double[] { costheta, 0, -sintheta, 0, 0, 1, 0, 0, sintheta, 0, costheta, 0, 0, 0, 0, 1 };
        double[,] y_p = new double[3, 4];
        double[] YN = new double[] { costheta, 0, sintheta, 0, 0, 1, 0, 0, -sintheta, 0, costheta, 0, 0, 0, 0, 1 };
        double[,] y_n = new double[3, 4];
        double[] XP = new double[] { 1, 0, 0, 0, 0, costheta, sintheta, 0, 0, -sintheta, costheta, 0, 0, 0, 0, 1 };
        double[,] x_p = new double[3, 4];
        double[] XN = new double[] { 1, 0, 0, 0, 0, costheta, -sintheta, 0, 0, sintheta, costheta, 0, 0, 0, 0, 1 };
        double[,] x_n = new double[3, 4];
        double[] ZP = new double[] { costheta, sintheta, 0, 0, -sintheta, costheta, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 };
        double[,] z_p = new double[3, 4];
        double[] ZN = new double[] { costheta, -sintheta, 0, 0, sintheta, costheta, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 };
        double[,] z_n = new double[3, 4];
        double[] L = new double[16];
        double[,] l = new double[3, 4];

        /// <summary>
        ///   Multiply two homogeneous transforms a and b.
        /// c is the result.
        ///  c is assumed to not reference the same matrix as a or b.
        /// </summary>
        /// <param name="a"></param>
        /// <param name=""></param>
        /// <param name="b"></param>
        /// <param name=""></param>
        /// <param name="c"></param>
        /// <param name=""></param>
        void tform_mult(double[,] a, double[,] b, double[,] c)
        {
            int i, j, k;

            for (i = 0; i < 3; i++)
                for (j = 0; j < 4; j++)
                {
                    c[i, j] = 0.0;
                    for (k = 0; k < 3; k++)
                        c[i, j] += a[i, k] * b[k, j];
                }

            c[0, 3] += a[0, 3];
            c[1, 3] += a[1, 3];
            c[2, 3] += a[2, 3];
        }


        void forwardCamera()
        {
            // translates the camera forward
            make2D(C, c);
            make2D(FC, f_c);
            tform_mult(f_c, c, l);
            make1D(l, C);
        }

        void reverseCamera()
        {
            //translates the camera backwards
            make2D(C, c);
            make2D(FCN, f_cn);
            tform_mult(f_cn, c, l);
            make1D(l, C);
        }

        void flyPlane()
        {
            //translates the plane forward
            make2D(P, p);
            make2D(FPL, f_pl);
            tform_mult(p, f_pl, l);
            make1D(l, P);
        }


        void yawPlanePlus()
        {
            //yaws the plane in the positive direction
            make2D(P, p);
            make2D(YP, y_p);
            tform_mult(p, y_p, l);
            make1D(l, P);
        }

        void yawPlaneMinus()
        {
            //yaws the plane in the negative direction
            make2D(P, p);
            make2D(YN, y_n);
            tform_mult(p, y_n, l);
            make1D(l, P);

        }

        void pitchPlanePlus()
        {
            //pitches the plane in the positive direction
            make2D(P, p);
            make2D(XP, x_p);
            tform_mult(p, x_p, l);
            make1D(l, P);
        }

        void pitchPlaneMinus()
        {
            //pitches the plane in the negative direction
            make2D(P, p);
            make2D(XN, x_n);
            tform_mult(p, x_n, l);
            make1D(l, P);
        }

        void rollPlanePlus()
        {
            //rolls the plane in the positive direction
            make2D(P, p);
            make2D(ZP, z_p);
            tform_mult(p, z_p, l);
            make1D(l, P);
        }

        void rollPlaneMinus()
        {
            //rolls the plane in the negative direction
            make2D(P, p);
            make2D(ZN, z_n);
            tform_mult(p, z_n, l);
            make1D(l, P);
        }

        void yawCameraPlus()
        {
            //yaws the camera in the positive direction
            make2D(C, c);
            make2D(YP, y_p);
            tform_mult(c, y_p, l);
            make1D(l, C);
        }

        void yawCameraMinus()
        {
            //yaws the camera in the negative direction
            make2D(C, c);
            make2D(YN, y_n);
            tform_mult(c, y_n, l);
            make1D(l, C);
        }


        void pitchCameraPlus()
        {
            //pitches the camera in the positive direction
            make2D(C, c);
            make2D(XP, x_p);
            tform_mult(c, x_p, l);
            make1D(l, C);
        }

        void pitchCameraMinus()
        {
            //pitches the camera in the negative direction
            make2D(C, c);
            make2D(XN, x_n);
            tform_mult(c, x_n, l);
            make1D(l, C);
        }

        void rollCameraMinus()
        {
            //rolls the camera in the negative direction
            make2D(C, c);
            make2D(ZP, z_p);
            tform_mult(c, z_p, l);
            make1D(l, C);
        }

        void rollCameraPlus()
        {
            //rolls the camera in the positive direction
            make2D(C, c);
            make2D(ZN, z_n);
            tform_mult(c, z_n, l);
            make1D(l, C);
        }
    }
}
