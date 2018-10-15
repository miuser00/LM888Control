
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO.Ports;
using System.Windows.Forms;
using System.Threading;
using System.Collections;

namespace LM888Control
{
    internal class Cube
    {
        private int dot_direction=1;  //1-6 矩阵放置的方向

        private bool[] _dot;

        private byte[] _cube;

        private Bitmap _bmp;

        private Graphics _g;

        private static SerialPort _sp;

        private static int _BaudRate = 57600;

        private Bitmap _bmpCustomized;

        private Graphics _gCustomized;

        public static readonly byte[] c_Z = new byte[]
        {
            1,
            2,
            4,
            8,
            16,
            32,
            64,
            128
        };

        public static readonly byte[] c_Z1 = new byte[]
        {
            254,
            253,
            251,
            247,
            239,
            223,
            191,
            127
        };

        public static readonly byte[] c_Z2 = new byte[]
        {
            1,
            3,
            7,
            15,
            31,
            63,
            127,
            255
        };

        public static readonly int[] c_Circle_0 = new int[]
        {
            27,
            28,
            36,
            35
        };

        public static readonly int[] c_Circle_1 = new int[]
        {
            18,
            19,
            20,
            21,
            29,
            37,
            45,
            44,
            43,
            42,
            34,
            26
        };

        public static readonly int[] c_Circle_2 = new int[]
        {
            9,
            10,
            11,
            12,
            13,
            14,
            22,
            30,
            38,
            46,
            54,
            53,
            52,
            51,
            50,
            49,
            41,
            33,
            25,
            17
        };

        public static readonly int[] c_Circle_3 = new int[]
        {
            0,
            1,
            2,
            3,
            4,
            5,
            6,
            7,
            15,
            23,
            31,
            39,
            47,
            55,
            63,
            62,
            61,
            60,
            59,
            58,
            57,
            56,
            48,
            40,
            32,
            24,
            16,
            8
        };

        private static Brush brushOff = new SolidBrush(Color.DarkGray);

        private static Brush brushOn = new SolidBrush(Color.White);

        private static Pen penOff = new Pen(Cube.brushOff, 12f);

        private static Pen penOn = new Pen(Cube.brushOn, 12f);

        public void SetDirection(int i_direction)
        {
            dot_direction = i_direction;
        }

        public Cube(PictureBox pb, int i_direction)
        {
            this._dot = new bool[512];
            this._cube = new byte[64];
            this._bmp = new Bitmap(pb.Width, pb.Height);
            this._g = Graphics.FromImage(this._bmp);
            this._g.SmoothingMode = SmoothingMode.HighSpeed;
            Cube._sp = new SerialPort();
            Cube._sp.BaudRate = Cube._BaudRate;
            Cube._sp.DtrEnable = true;
            this._bmpCustomized = new Bitmap(390, 130);
            this._gCustomized = Graphics.FromImage(this._bmpCustomized);
            this._gCustomized.SmoothingMode = SmoothingMode.HighSpeed;

            dot_direction = i_direction;

        }

        ~Cube()
        {
            this.DisConnect();
        }

        public bool IsConnected()
        {
            return Cube._sp.IsOpen;
        }

        public Bitmap GetBmp(int iRotate)
        {
            this._g.Clear(Color.LightGray);
            Point[] array = new Point[3];
            Point[] array2 = new Point[3];
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            for (int i = 0; i < this._dot.Length; i++)
            {
                this.Index2xyzByRotate(iRotate, i, ref num, ref num2, ref num3);
                num = 7 - num;
                num2 = 7 - num2;
                num3 = 7 - num3;
                array[0] = new Point(19 + num2 * 117 + 13 * num, 13 + 13 * num3);
                array2[0] = new Point(array[0].X, array[0].Y + 12);
                array[1] = new Point(19 + num * 117 + 13 * (7 - num2), 130 + 13 * num3);
                array2[1] = new Point(array[1].X, array[1].Y + 12);
                array[2] = new Point(19 + num3 * 117 + 13 * num, 247 + 13 * (7 - num2));
                array2[2] = new Point(array[2].X, array[2].Y + 12);
                for (int j = 0; j < 3; j++)
                {
                    this._g.DrawLine(this._dot[i] ? Cube.penOn : Cube.penOff, array[j], array2[j]);
                }
            }

            return this._bmp;
        }
        public Bitmap GetBmp(int iRotate,int width, int height)
        {
            System.Drawing.Bitmap initImage = GetBmp(iRotate);
            Bitmap newBMP = KiResizeImage(initImage, width, height);
            return newBMP;
        }
        public static Bitmap KiResizeImage(Bitmap bmp, int newW, int newH)
        {
            try
            {
                Bitmap b = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(b);
                // 插值算法的质量
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();
                return b;
            }
            catch
            {
                return null;
            }
        }
        public Bitmap GetBmpCustomized()
        {
            this._gCustomized.Clear(Color.LightGray);
            Point pt = default(Point);
            Point pt2 = default(Point);
            for (int i = 0; i < Cube.c_Circle_3.Length; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int num = 7 - j;
                    pt = new Point(19 + 13 * i, 13 + 13 * num);
                    pt2 = new Point(pt.X, pt.Y + 12);
                    this._gCustomized.DrawLine(this._dot[Cube.c_Circle_3[i] + (j << 6)] ? Cube.penOn : Cube.penOff, pt, pt2);
                }
            }
            return this._bmpCustomized;
        }

        public void Clear()
        {
            System.Array.Clear(this._dot, 0, this._dot.Length);
        }

        public void GetCube(int iRotate)
        {
            System.Array.Clear(this._cube, 0, this._cube.Length);
            int x = 0;
            int y = 0;
            int num = 0;
            for (int i = 0; i < this._dot.Length; i++)
            {
                this.Index2xyzByRotate(iRotate, i, ref x, ref y, ref num);
                if (this._dot[i])
                {
                    byte[] expr_49_cp_0 = this._cube;
                    int expr_49_cp_1 = Cube.Index(x, y, 0);
                    expr_49_cp_0[expr_49_cp_1] |= Cube.c_Z[num];
                }
            }
        }




        private void Index2xyzByRotate(int iRotate, int i, ref int x, ref int y, ref int z)
        {
            switch (iRotate & 3)
            {
                case 1:
                    Cube.Index2xyz(i, ref y, ref z, ref x);
                    break;
                case 2:
                    Cube.Index2xyz(i, ref z, ref x, ref y);
                    break;
                default:
                    Cube.Index2xyz(i, ref x, ref y, ref z);
                    break;
            }
            if (this.GetBit(iRotate, 2))
            {
                Cube.Swift(ref y, ref z);
            }
            if (this.GetBit(iRotate, 3))
            {
                Cube.Swift(ref z, ref x);
            }
            if (this.GetBit(iRotate, 4))
            {
                Cube.Swift(ref x, ref y);
            }
            if (this.GetBit(iRotate, 5))
            {
                x = 7 - x;
            }
            if (this.GetBit(iRotate, 6))
            {
                y = 7 - y;
            }
            if (this.GetBit(iRotate, 7))
            {
                z = 7 - z;
            }
        }

        public static void Index2xyz(int i, ref int x, ref int y, ref int z)
        {
            z = (i >> 6 & 7);
            y = (i >> 3 & 7);
            x = (i & 7);
        }

        public string GetPrintOut()
        {
            string text = "0xF2\r\n\r\n";
            for (int i = 0; i < this._cube.Length; i++)
            {
                text = text + "0x" + this._cube[i].ToString("X2") + ", ";
                if (i % 8 == 7)
                {
                    text += "\r\n";
                }
            }
            return text;
        }

        public class Matrix
        {
            public bool[,,] matrix = new bool[8, 8, 8];
        }

        public Matrix GetMatrix()
        {
            Matrix output = new Matrix();
            for (int i = 0; i < this._cube.Length; i++)
            {
                int x, y;
                x = (i / 8) % 8;
                y = i % 8;
                for (int z = 0; z < 8; z++)
                {
                    byte[] b = { _cube[i] };
                    BitArray bb = new BitArray(b );
                    if (bb.Length == 8)
                    {
                        output.matrix[x, y, z] = bb[z];
                    }
                }
            }
            return output;
        }
        public void Roll(int r, bool bDirection, bool bRecycle)
        {
            if (r < 0 || r > 3)
            {
                return;
            }
            int[] array;
            switch (r)
            {
                case 1:
                    array = Cube.c_Circle_1;
                    goto IL_43;
                case 2:
                    array = Cube.c_Circle_2;
                    goto IL_43;
                case 3:
                    array = Cube.c_Circle_3;
                    goto IL_43;
            }
            array = Cube.c_Circle_0;
            IL_43:
            byte cData;
            if (bDirection)
            {
                cData = (byte)(bRecycle ? this.GetColumn(array[0]) : 0);
                for (int i = 0; i < array.Length - 1; i++)
                {
                    this.Col2Col(array[i], array[i + 1]);
                }
                this.SetColumn(array[array.Length - 1], cData);
                return;
            }
            cData = (byte)(bRecycle ? this.GetColumn(array[array.Length - 1]) : 0);
            for (int j = array.Length - 1; j > 0; j--)
            {
                this.Col2Col(array[j], array[j - 1]);
            }
            this.SetColumn(array[0], cData);
        }

        public void RollAll(int iStep, bool bDirection, bool bRecycle)
        {
            switch (iStep)
            {
                case 0:
                case 6:
                    this.Roll(3, bDirection, bRecycle);
                    this.Roll(2, bDirection, bRecycle);
                    return;
                case 1:
                case 5:
                    this.Roll(3, bDirection, bRecycle);
                    this.Roll(1, bDirection, bRecycle);
                    return;
                case 2:
                case 4:
                    this.Roll(3, bDirection, bRecycle);
                    this.Roll(2, bDirection, bRecycle);
                    return;
                case 3:
                    this.Roll(3, bDirection, bRecycle);
                    this.Roll(2, bDirection, bRecycle);
                    this.Roll(1, bDirection, bRecycle);
                    this.Roll(0, bDirection, bRecycle);
                    return;
                default:
                    return;
            }
        }

        public void SetUniqueValue2Column(byte cData)
        {
            for (int i = 0; i < this._cube.Length; i++)
            {
                this.SetColumn(i, cData);
            }
        }

        public void SetDot(int x, int y, int z, bool b)
        {
            this._dot[Cube.Index(x, y, z)] = b;
        }

        public void SetDot(int iIndex, bool b)
        {
            this._dot[iIndex] = b;
        }

        public void SetDot(int[] p, bool b)
        {
            for (int i = 0; i < p.Length; i++)
            {
                if (p[i] >= 0 && p[i] <= 512)
                {
                    this._dot[p[i]] = b;
                }
            }
        }

        public void SetColumn(int c, byte cData)
        {
            for (int i = 0; i < Cube.c_Z.Length; i++)
            {
                this._dot[Cube.Index(c, i)] = Cube.GetBit(cData, i);
            }
        }

        public byte GetColumn(int c)
        {
            byte b = 0;
            for (int i = 0; i < Cube.c_Z.Length; i++)
            {
                if (this._dot[Cube.Index(c, i)])
                {
                    b |= Cube.c_Z[i];
                }
            }
            return b;
        }

        public static bool GetBit(byte c, int i)
        {
            return (c & Cube.c_Z[i]) == Cube.c_Z[i];
        }

        private bool GetBit(int j, int i)
        {
            byte b = (byte)j;
            return (b & Cube.c_Z[i]) == Cube.c_Z[i];
        }

        public bool GetDot(int x, int y, int z)
        {
            return this._dot[Cube.Index(x, y, z)];
        }

        public bool Connect(string sPortName)
        {
            Cube._sp.PortName = sPortName;
            Cube._sp.BaudRate = Cube._BaudRate;
            if (Cube._sp.IsOpen)
            {
                this.DisConnect();
            }
            try
            {
                Cube._sp.Open();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return Cube._sp.IsOpen;
        }

        public bool Connect(string sPortName, int baud)
        {
            Cube._sp.PortName = sPortName;
            Cube._BaudRate = baud;
            Cube._sp.BaudRate = Cube._BaudRate;
            if (Cube._sp.IsOpen)
            {
                this.DisConnect();
            }
            try
            {
                Cube._sp.Open();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return Cube._sp.IsOpen;
        }

        public bool DisConnect()
        {
            try
            {
                if (Cube._sp.IsOpen)
                {
                    Cube._sp.Close();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return Cube._sp.IsOpen;
        }

        private void funSend(byte[] b)
        {
            try
            {
                Cube._sp.Write(b, 0, b.Length);
                               
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }

        public static bool GetBits(uint num, int bit)
        {
            uint v = bit == 0 ? 1 : 2u << (bit - 1);
            return ((num & v) != 0);
        }

        public byte[] PlaceCube(byte[] inMatrix)
        {
            bool[,,] b_Matrix = new bool[8,8,8];
            byte[] outMatrix = new byte[64];

            //变换成3维 bit 矩阵
            for (int i=0;i<8;i++) //X
            {
                for (int j=0;j<8;j++)  //Y
                {
                    for (int k=0;k<8;k++) //Z
                    {
                        b_Matrix[i, j, k] = GetBits(inMatrix[i * 8 + j], k);
                    }
                }
            }

            //根据放置方向 变换
            if (dot_direction == 1)
            {
                for (int i = 0; i < 8; i++) //X
                {
                    for (int j = 0; j < 8; j++)  //Y
                    {
                        int sub = 0;
                        for (int k = 0; k < 8; k++) //Z
                        {
                            if (b_Matrix[i, j, k] == true)
                            {
                                sub = sub + (int)Math.Pow(2, k);
                            }
                        }
                        outMatrix[i * 8 + j] = (byte)sub;
                    }
                }
            }
            else if (dot_direction == 2)
            {
                for (int i = 0; i < 8; i++) //X
                {
                    for (int j = 0; j < 8; j++)  //Y
                    {
                        int sub = 0;
                        for (int k = 0; k < 8; k++) //Z
                        {
                            if (b_Matrix[i, k,j] == true)
                            {
                                sub = sub + (int)Math.Pow(2, k);
                            }
                        }
                        outMatrix[i * 8 + j] = (byte)sub;
                    }
                }
            }
            else if (dot_direction == 3)
            {
                for (int i = 0; i < 8; i++) //X
                {
                    for (int j = 0; j < 8; j++)  //Y
                    {
                        int sub = 0;
                        for (int k = 0; k < 8; k++) //Z
                        {
                            if (b_Matrix[j,k,i] == true)
                            {
                                sub = sub + (int)Math.Pow(2, k);
                            }
                        }
                        outMatrix[i * 8 + j] = (byte)sub;
                    }
                }
            }
            else if (dot_direction == 4)
            {
                for (int i = 0; i < 8; i++) //X
                {
                    for (int j = 0; j < 8; j++)  //Y
                    {
                        int sub = 0;
                        for (int k = 0; k < 8; k++) //Z
                        {
                            if (b_Matrix[j,i,k] == true)
                            {
                                sub = sub + (int)Math.Pow(2, k);
                            }
                        }
                        outMatrix[i * 8 + j] = (byte)sub;
                    }
                }
            }
            else if (dot_direction == 5)
            {
                for (int i = 0; i < 8; i++) //X
                {
                    for (int j = 0; j < 8; j++)  //Y
                    {
                        int sub = 0;
                        for (int k = 0; k < 8; k++) //Z
                        {
                            if (b_Matrix[k,i,j] == true)
                            {
                                sub = sub + (int)Math.Pow(2, k);
                            }
                        }
                        outMatrix[i * 8 + j] = (byte)sub;
                    }
                }
            }
            else if (dot_direction == 6)
            {
                for (int i = 0; i < 8; i++) //X
                {
                    for (int j = 0; j < 8; j++)  //Y
                    {
                        int sub = 0;
                        for (int k = 0; k < 8; k++) //Z
                        {
                            if (b_Matrix[k,j,i] == true)
                            {
                                sub = sub + (int)Math.Pow(2, k);
                            }
                        }
                        outMatrix[i * 8 + j] = (byte)sub;
                    }
                }
            }


            return outMatrix;
        }

        public void SendData()
        {
            if (!Cube._sp.IsOpen)
            {
                return;
            }
            this.funSend(new byte[]
            {
                242
            });
            this.funSend(PlaceCube(this._cube));
        }

        private void Byte2bits(byte c, bool[] b)
        {
            for (int i = 0; i < 8; i++)
            {
                b[i] = Cube.GetBit(c, i);
            }
        }

        private byte Bits2Byte(bool[] b)
        {
            byte b2 = 0;
            for (int i = 0; i < 8; i++)
            {
                if (b[i])
                {
                    b2 |= Cube.c_Z[i];
                }
            }
            return b2;
        }

        public unsafe byte AndValue(Rotate r)
        {
            bool[] array = new bool[8];
            this.Byte2bits(255, array);
            int* ptr;
            switch (r)
            {
                case Rotate.Up_Y:
                    {
                        int i;
                        ptr = &i;
                        break;
                    }
                case Rotate.Up_X:
                    {
                        int j;
                        ptr = &j;
                        break;
                    }
                default:
                    {
                        int k;
                        ptr = &k;
                        break;
                    }
            }
            for (int j = 0; j < 8; j++)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int k = 0; k < 8; k++)
                    {
                        array[*ptr] &= this._dot[Cube.Index(j, i, k)];
                    }
                }
            }
            return this.Bits2Byte(array);
        }

        public unsafe byte OrValue(Rotate r)
        {
            bool[] array = new bool[8];
            System.Array.Clear(array, 0, array.Length);
            int* ptr;
            switch (r)
            {
                case Rotate.Up_Y:
                    {
                        int i;
                        ptr = &i;
                        break;
                    }
                case Rotate.Up_X:
                    {
                        int j;
                        ptr = &j;
                        break;
                    }
                default:
                    {
                        int k;
                        ptr = &k;
                        break;
                    }
            }
            for (int j = 0; j < 8; j++)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int k = 0; k < 8; k++)
                    {
                        array[*ptr] |= this._dot[Cube.Index(j, i, k)];
                    }
                }
            }
            return this.Bits2Byte(array);
        }

        public static int Index(int x, int y, int z)
        {
            return z << 6 | y << 3 | x;
        }

        public static int Index(int c, int z)
        {
            return z << 6 | c;
        }

        public void Col2Col(int DestinationX, int DestinationY, int SourceX, int SourceY)
        {
            if (SourceX < 0 || SourceX > 7)
            {
                return;
            }
            if (SourceY < 0 || SourceY > 7)
            {
                return;
            }
            if (DestinationX < 0 || DestinationX > 7)
            {
                return;
            }
            if (DestinationY < 0 || DestinationY > 7)
            {
                return;
            }
            for (int i = 0; i < 8; i++)
            {
                this._dot[Cube.Index(DestinationX, DestinationY, i)] = this._dot[Cube.Index(SourceX, SourceY, i)];
            }
        }

        public void Col2Col(int DestionationC, int SourceC)
        {
            if (DestionationC < 0 || DestionationC >= this._dot.Length)
            {
                return;
            }
            if (SourceC < 0 || SourceC >= this._dot.Length)
            {
                return;
            }
            for (int i = 0; i < 8; i++)
            {
                this._dot[Cube.Index(DestionationC, i)] = this._dot[Cube.Index(SourceC, i)];
            }
        }

        public void Move(int iDirection, bool bRecycle)
        {
            bool flag = (iDirection & 1) == 0;
            int num = iDirection >> 1;
            for (int i = 0; i < Cube.c_Z.Length; i++)
            {
                for (int j = 0; j < Cube.c_Z.Length; j++)
                {
                    if (flag)
                    {
                        int k = 7;
                        bool flag2;
                        switch (num)
                        {
                            case 1:
                                flag2 = this._dot[Cube.Index(k, i, j)];
                                while (k > 0)
                                {
                                    this._dot[Cube.Index(k, i, j)] = this._dot[Cube.Index(k - 1, i, j)];
                                    k--;
                                }
                                this._dot[Cube.Index(k, i, j)] = (bRecycle && flag2);
                                goto IL_294;
                            case 2:
                                flag2 = this._dot[Cube.Index(j, k, i)];
                                while (k > 0)
                                {
                                    this._dot[Cube.Index(j, k, i)] = this._dot[Cube.Index(j, k - 1, i)];
                                    k--;
                                }
                                this._dot[Cube.Index(j, k, i)] = (bRecycle && flag2);
                                goto IL_294;
                        }
                        flag2 = this._dot[Cube.Index(i, j, k)];
                        while (k > 0)
                        {
                            this._dot[Cube.Index(i, j, k)] = this._dot[Cube.Index(i, j, k - 1)];
                            k--;
                        }
                        this._dot[Cube.Index(i, j, k)] = (bRecycle && flag2);
                    }
                    else
                    {
                        int l = 0;
                        bool flag2;
                        switch (num)
                        {
                            case 1:
                                flag2 = this._dot[Cube.Index(l, i, j)];
                                while (l < 7)
                                {
                                    this._dot[Cube.Index(l, i, j)] = this._dot[Cube.Index(l + 1, i, j)];
                                    l++;
                                }
                                this._dot[Cube.Index(l, i, j)] = (bRecycle && flag2);
                                goto IL_294;
                            case 2:
                                flag2 = this._dot[Cube.Index(j, l, i)];
                                while (l < 7)
                                {
                                    this._dot[Cube.Index(j, l, i)] = this._dot[Cube.Index(j, l + 1, i)];
                                    l++;
                                }
                                this._dot[Cube.Index(j, l, i)] = (bRecycle && flag2);
                                goto IL_294;
                        }
                        flag2 = this._dot[Cube.Index(i, j, l)];
                        while (l < 7)
                        {
                            this._dot[Cube.Index(i, j, l)] = this._dot[Cube.Index(i, j, l + 1)];
                            l++;
                        }
                        this._dot[Cube.Index(i, j, l)] = (bRecycle && flag2);
                    }
                IL_294:;
                }
            }
        }

        public void SetFrame(int x1, int y1, int z1, int x2, int y2, int z2, bool bOn)
        {
            this.SetCube(x1, y1, z1, x2, y1, z1, bOn);
            this.SetCube(x1, y1, z1, x1, y2, z1, bOn);
            this.SetCube(x1, y2, z1, x2, y2, z1, bOn);
            this.SetCube(x2, y1, z1, x2, y2, z1, bOn);
            this.SetCube(x1, y1, z1, x1, y1, z2, bOn);
            this.SetCube(x2, y1, z1, x2, y1, z2, bOn);
            this.SetCube(x1, y2, z1, x1, y2, z2, bOn);
            this.SetCube(x2, y2, z1, x2, y2, z2, bOn);
            this.SetCube(x1, y1, z2, x2, y1, z2, bOn);
            this.SetCube(x1, y1, z2, x1, y2, z2, bOn);
            this.SetCube(x1, y2, z2, x2, y2, z2, bOn);
            this.SetCube(x2, y1, z2, x2, y2, z2, bOn);
        }

        public void SetCube(int x1, int y1, int z1, int x2, int y2, int z2, bool bOn)
        {
            if (x1 < 0 || y1 < 0 || z1 < 0)
            {
                return;
            }
            if (x2 < 0 || y2 < 0 || z2 < 0)
            {
                return;
            }
            if (x1 > 7 || y1 > 7 || z1 > 7)
            {
                return;
            }
            if (x2 > 7 || y2 > 7 || z2 > 7)
            {
                return;
            }
            if (x1 > x2)
            {
                Cube.Swift(ref x1, ref x2);
            }
            if (y1 > y2)
            {
                Cube.Swift(ref y1, ref y2);
            }
            if (z1 > z2)
            {
                Cube.Swift(ref z1, ref z2);
            }
            for (int i = x1; i <= x2; i++)
            {
                for (int j = y1; j <= y2; j++)
                {
                    for (int k = z1; k <= z2; k++)
                    {
                        this.SetDot(i, j, k, bOn);
                    }
                }
            }
        }

        public static double DistanceOfDot2Dot(int xA, int yA, int zA, int xB, int yB, int zB)
        {
            return System.Math.Sqrt((double)((xA - xB) * (xA - xB) + (yA - yB) * (yA - yB) + (zA - zB) * (zA - zB)));
        }

        public static double DistanceOfDot2Line(int x, int y, int x1, int y1, int x2, int y2)
        {
            if (x1 == x2 && y1 == y2)
            {
                return 0.0;
            }
            double num = (double)System.Math.Abs((y2 - y1) * x - (x2 - x1) * y - (y2 * x1 - y1 * x2));
            return num / System.Math.Sqrt((double)((y2 - y1) * (y2 - y1) + (x2 - x1) * (x2 - x1)));
        }

        public void SetLine(int x1, int y1, int z1, int x2, int y2, int z2, bool bOn, double dRate)
        {
            if (x1 < 0 || y1 < 0 || z1 < 0)
            {
                return;
            }
            if (x2 < 0 || y2 < 0 || z2 < 0)
            {
                return;
            }
            if (x1 > 7 || y1 > 7 || z1 > 7)
            {
                return;
            }
            if (x2 > 7 || y2 > 7 || z2 > 7)
            {
                return;
            }
            int num = x1;
            int num2 = x2;
            int num3 = y1;
            int num4 = y2;
            int num5 = z1;
            int num6 = z2;
            if (num > num2)
            {
                Cube.Swift(ref num, ref num2);
            }
            if (num3 > num4)
            {
                Cube.Swift(ref num3, ref num4);
            }
            if (num5 > num6)
            {
                Cube.Swift(ref num5, ref num6);
            }
            for (int i = num; i <= num2; i++)
            {
                for (int j = num3; j <= num4; j++)
                {
                    for (int k = num5; k <= num6; k++)
                    {
                        if (Cube.DotOnLine(i, j, k, x1, y1, z1, x2, y2, z2, dRate))
                        {
                            this.SetDot(i, j, k, bOn);
                        }
                    }
                }
            }
        }

        private static bool DotOnLine(int x, int y, int z, int x1, int y1, int z1, int x2, int y2, int z2, double dRate)
        {
            double[] array = new double[]
            {
                Cube.DistanceOfDot2Line(x, y, x1, y1, x2, y2),
                Cube.DistanceOfDot2Line(y, z, y1, z1, y2, z2),
                Cube.DistanceOfDot2Line(z, x, z1, x1, z2, x2)
            };
            return array[0] * array[0] + array[1] * array[1] + array[2] * array[2] <= 3.0 * dRate * dRate;
        }

        public static void Swift(ref int a, ref int b)
        {
            int num = a;
            a = b;
            b = num;
        }

    }
    internal enum Rotate
    {
        Up_Z,
        Up_Y,
        Up_X,
        Swift_YZ = 4,
        Swift_ZX = 8,
        Swift_XY = 16,
        Reverse_X = 32,
        Reverse_Y = 64,
        Reverse_Z = 128
    }
}
