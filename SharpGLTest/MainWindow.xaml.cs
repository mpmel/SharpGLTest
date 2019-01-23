using SharpGL;
using SharpGL.Enumerations;
using SharpGLTest.Samples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SharpGLTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        ISharpGLSample _currentRenderSample;

        internal ISharpGLSample CurrentRenderSample
        {
            get => _currentRenderSample; set
            {
                _currentRenderSample = value;
                if (value != null)
                {
                    value.Initialize(OpenGLControl.OpenGL);
                    value.Resize(OpenGLControl.OpenGL, (int)OpenGLControl.ActualWidth, (int)OpenGLControl.ActualHeight);
                }
            }
        }

        private void openGLControl1_OpenGLDraw(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            var gl = args.OpenGL;
            _currentRenderSample?.Draw(gl);
        }

        private void openGLControl1_Initialized(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            OpenGL gl = args.OpenGL;
            //  Enable the OpenGL depth testing functionality.
            args.OpenGL.Enable(OpenGL.GL_DEPTH_TEST);

            //float[] global_ambient = new float[] { 0.5f, 0.5f, 0.5f, 1.0f };
            //float[] light0pos = new float[] { 0.0f, 5.0f, 10.0f, 1.0f };
            //float[] light0ambient = new float[] { 0.2f, 0.2f, 0.2f, 1.0f };
            //float[] light0diffuse = new float[] { 0.3f, 0.3f, 0.3f, 1.0f };
            //float[] light0specular = new float[] { 0.8f, 0.8f, 0.8f, 1.0f };

            //float[] lmodel_ambient = new float[] { 0.2f, 0.2f, 0.2f, 1.0f };
            //gl.LightModel(OpenGL.GL_LIGHT_MODEL_AMBIENT, lmodel_ambient);

            //gl.LightModel(OpenGL.GL_LIGHT_MODEL_AMBIENT, global_ambient);
            //gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_POSITION, light0pos);
            //gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_AMBIENT, light0ambient);
            //gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_DIFFUSE, light0diffuse);
            //gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_SPECULAR, light0specular);
            //gl.Enable(OpenGL.GL_LIGHTING);
            //gl.Enable(OpenGL.GL_LIGHT0);

            gl.ShadeModel(OpenGL.GL_SMOOTH);
        }

        private void CenteredRectangleButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentRenderSample = new CenteredRectangleSample();
            
        }

        private void CodeProjectSampleButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentRenderSample = new CodeProjectSample();
        }

        private void OpenGLControl_Resized(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            var gl = args.OpenGL;
            if (_currentRenderSample != null)
            {
                _currentRenderSample.Resize(args.OpenGL, (int)OpenGLControl.ActualWidth, (int)OpenGLControl.ActualHeight);
            }
        }

        private void CMPS415SampleButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentRenderSample = new CMPS415Sample(); 
        }

        private void OpenGLControl_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {

            }
        }
    }
}
