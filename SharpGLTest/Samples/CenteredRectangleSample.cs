using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;

namespace SharpGLTest.Samples
{
    class CenteredRectangleSample : SharpGLSampleBase
    {
        public override void Draw(OpenGL gl)
        {

            //  Clear the color and depth buffers.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            //  Reset the modelview matrix.
            gl.LoadIdentity();

            gl.Color(1.0f, 0.0f, 0.0f);
            gl.Rect(100.0f, 150.0f, 150.0f, 100.0f);
            gl.Flush();
        }

        public override void Initialize(OpenGL gl)
        {
            gl.ClearColor(0, 0, 1, 1);
        }

        public override void Resize(OpenGL gl, int width, int height)
        {
            if (height == 0)
                height = 1;


            // Reset the coordinate system
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();

            // Establish the clipping volume 
            if (width <= height)
                gl.Ortho(0, 250, 0, 250 * height / width, 1, -1);
            else
                gl.Ortho(0, 250 * width / height, 0, 250, 1, -1);

            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();

        }
    }
}
