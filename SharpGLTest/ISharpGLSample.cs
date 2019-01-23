using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGLTest
{
    interface ISharpGLSample
    {
        void Initialize(OpenGL gl);
        void Draw(OpenGL gl);
        void Resize(OpenGL gl, int width, int height);
    }

    abstract class SharpGLSampleBase : ISharpGLSample
    {
        public abstract void Draw(OpenGL gl);
        public virtual void Initialize(OpenGL gl) { gl.ClearColor(0, 0, 0, 1); }
        public virtual void Resize(OpenGL gl, int width, int height) {

            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();

            // Calculate The Aspect Ratio Of The Window
            gl.Perspective(45.0f, (float)width / (float)height, 0.1f, 100.0f);

            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();
        }
    }
}
