using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;

namespace LM888Control
{
    public  partial class Form1 
    {
        /// <summary>
        /// 默认绘画模式为线条
        /// </summary>
        private uint _model = OpenGL.GL_LINE_LOOP;
        /// <summary>
        /// X轴坐标
        /// </summary>
        private float _x = 0;
        /// <summary>
        /// Y轴坐标
        /// </summary>
        private float _y = 0;

        /// <summary>
        /// Z轴坐标
        /// </summary>
        private float _z = 0;
        private float _distance = 0;
        private double bias_x, bias_y;



        public class Rect
        {
            public Point A, B, C, D;
            public Rect()
            {
                A = new Point();
                B = new Point();
                C = new Point();
                D = new Point();
            }

        }
        public class Point
        {
            public double X, Y, Z;
        }
        public class Color
        {
            public double R, G, B;
        }
        public class Squire
        {
            public Rect S1, S2, S3, S4, S5, S6;
            public Squire()
            {
                S1 = new Rect();
                S2 = new Rect();
                S3 = new Rect();
                S4 = new Rect();
                S5 = new Rect();
                S6 = new Rect();
            }

        }
        public void DrawSquire(SharpGL.OpenGL glPoint, Point location, Color color, double size)
        {
            //画三个正交面
            DrawRect(glPoint, location, color, size, 1);
            DrawRect(glPoint, location, color, size, 2);
            DrawRect(glPoint, location, color, size, 3);

            Point Vertex2 = new Point();

            Vertex2.X = location.X + size;
            Vertex2.Y = location.Y + size;
            Vertex2.Z = location.Z + size;

            DrawRect(glPoint, Vertex2, color, size, 4);
            DrawRect(glPoint, Vertex2, color, size, 5);
            DrawRect(glPoint, Vertex2, color, size, 6);


        }
        public void DrawRect(SharpGL.OpenGL gl, Point location, Color color, double size, int direction)
        {
            Rect rect = new Rect();

            if (direction == 1)
            {
                gl.Begin(_model);
                gl.Color(color.R, color.G, color.B);

                rect.A.X = 0 * size + location.X;
                rect.A.Y = 0 * size + location.Y;
                rect.A.Z = 0 * size + location.Z;

                rect.B.X = 1 * size + location.X;
                rect.B.Y = 0 * size + location.Y;
                rect.B.Z = 0 * size + location.Z;

                rect.C.X = 1 * size + location.X;
                rect.C.Y = 1 * size + location.Y;
                rect.C.Z = 0 * size + location.Z;

                rect.D.X = 0 * size + location.X;
                rect.D.Y = 1 * size + location.Y;
                rect.D.Z = 0 * size + location.Z;

                gl.Vertex(rect.A.X, rect.A.Y, rect.A.Z);
                gl.Vertex(rect.B.X, rect.B.Y, rect.B.Z);
                gl.Vertex(rect.C.X, rect.C.Y, rect.C.Z);
                gl.Vertex(rect.D.X, rect.D.Y, rect.D.Z);
                gl.End();
            }

            if (direction == 2)
            {
                gl.Begin(_model);
                gl.Color(color.R, color.G, color.B);

                rect.A.X = 0 * size + location.X;
                rect.A.Y = 0 * size + location.Y;
                rect.A.Z = 0 * size + location.Z;

                rect.B.X = 0 * size + location.X;
                rect.B.Y = 1 * size + location.Y;
                rect.B.Z = 0 * size + location.Z;

                rect.C.X = 0 * size + location.X;
                rect.C.Y = 1 * size + location.Y;
                rect.C.Z = 1 * size + location.Z;

                rect.D.X = 0 * size + location.X;
                rect.D.Y = 0 * size + location.Y;
                rect.D.Z = 1 * size + location.Z;

                gl.Vertex(rect.A.X, rect.A.Y, rect.A.Z);
                gl.Vertex(rect.B.X, rect.B.Y, rect.B.Z);
                gl.Vertex(rect.C.X, rect.C.Y, rect.C.Z);
                gl.Vertex(rect.D.X, rect.D.Y, rect.D.Z);
                gl.End();
            }

            if (direction == 3)
            {
                gl.Begin(_model);
                gl.Color(color.R, color.G, color.B);

                rect.A.X = 0 * size + location.X;
                rect.A.Y = 0 * size + location.Y;
                rect.A.Z = 0 * size + location.Z;

                rect.B.X = 0 * size + location.X;
                rect.B.Y = 0 * size + location.Y;
                rect.B.Z = 1 * size + location.Z;

                rect.C.X = 1 * size + location.X;
                rect.C.Y = 0 * size + location.Y;
                rect.C.Z = 1 * size + location.Z;

                rect.D.X = 1 * size + location.X;
                rect.D.Y = 0 * size + location.Y;
                rect.D.Z = 0 * size + location.Z;

                gl.Vertex(rect.A.X, rect.A.Y, rect.A.Z);
                gl.Vertex(rect.B.X, rect.B.Y, rect.B.Z);
                gl.Vertex(rect.C.X, rect.C.Y, rect.C.Z);
                gl.Vertex(rect.D.X, rect.D.Y, rect.D.Z);
                gl.End();
            }

            if (direction == 4)
            {
                gl.Begin(_model);
                gl.Color(color.R, color.G, color.B);

                rect.A.X = 0 * size + location.X;
                rect.A.Y = 0 * size + location.Y;
                rect.A.Z = 0 * size + location.Z;

                rect.B.X = -1 * size + location.X;
                rect.B.Y = 0 * size + location.Y;
                rect.B.Z = 0 * size + location.Z;

                rect.C.X = -1 * size + location.X;
                rect.C.Y = -1 * size + location.Y;
                rect.C.Z = 0 * size + location.Z;

                rect.D.X = 0 * size + location.X;
                rect.D.Y = -1 * size + location.Y;
                rect.D.Z = 0 * size + location.Z;

                gl.Vertex(rect.A.X, rect.A.Y, rect.A.Z);
                gl.Vertex(rect.B.X, rect.B.Y, rect.B.Z);
                gl.Vertex(rect.C.X, rect.C.Y, rect.C.Z);
                gl.Vertex(rect.D.X, rect.D.Y, rect.D.Z);
                gl.End();
            }
            if (direction == 5)
            {
                gl.Begin(_model);
                gl.Color(color.R, color.G, color.B);

                rect.A.X = 0 * size + location.X;
                rect.A.Y = 0 * size + location.Y;
                rect.A.Z = 0 * size + location.Z;

                rect.B.X = 0 * size + location.X;
                rect.B.Y = -1 * size + location.Y;
                rect.B.Z = 0 * size + location.Z;

                rect.C.X = 0 * size + location.X;
                rect.C.Y = -1 * size + location.Y;
                rect.C.Z = -1 * size + location.Z;

                rect.D.X = 0 * size + location.X;
                rect.D.Y = 0 * size + location.Y;
                rect.D.Z = -1 * size + location.Z;

                gl.Vertex(rect.A.X, rect.A.Y, rect.A.Z);
                gl.Vertex(rect.B.X, rect.B.Y, rect.B.Z);
                gl.Vertex(rect.C.X, rect.C.Y, rect.C.Z);
                gl.Vertex(rect.D.X, rect.D.Y, rect.D.Z);
                gl.End();
            }

            if (direction == 6)
            {
                gl.Begin(_model);
                gl.Color(color.R, color.G, color.B);

                rect.A.X = 0 * size + location.X;
                rect.A.Y = 0 * size + location.Y;
                rect.A.Z = 0 * size + location.Z;

                rect.B.X = 0 * size + location.X;
                rect.B.Y = 0 * size + location.Y;
                rect.B.Z = -1 * size + location.Z;

                rect.C.X = -1 * size + location.X;
                rect.C.Y = 0 * size + location.Y;
                rect.C.Z = -1 * size + location.Z;

                rect.D.X = -1 * size + location.X;
                rect.D.Y = 0 * size + location.Y;
                rect.D.Z = 0 * size + location.Z;

                gl.Vertex(rect.A.X, rect.A.Y, rect.A.Z);
                gl.Vertex(rect.B.X, rect.B.Y, rect.B.Z);
                gl.Vertex(rect.C.X, rect.C.Y, rect.C.Z);
                gl.Vertex(rect.D.X, rect.D.Y, rect.D.Z);
                gl.End();
            }
            return;
        }
    }
}
