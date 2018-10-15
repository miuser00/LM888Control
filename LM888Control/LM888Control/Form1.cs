using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Text.RegularExpressions;
using SharpGL;

namespace LM888Control
{
    public partial class Form1 : Form
    {
        Cube cube;
        private int cFrameIndex;
        private Snake[] pSnake;
        private System.Random rnd;
        private int[] _RandChain;
        private Timer tmr;

        private FileWR file = new FileWR();

        public Form1()
        {
            InitializeComponent();
            Init_Serial_Port();
            cube = new Cube(pic_Main,2);
            this.rnd = new System.Random();

            _distance = -18;
            _x = 0;
            _y = 20;
            _z = 0;

            bias_x = -6;
            bias_y = -4.2;

            try
            {
                List<string> cmd = new List<string>();
                cmd = file.ReadToList("MatrixCmd.ini");
                l_lastCmdSEQ = long.Parse(cmd[2].ToString());
            }
            catch
            { }

        }

        
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void Init_Serial_Port()
        {
            //检查是否含有串口  
            string[] str = SerialPort.GetPortNames();
            if (str == null)
            {
                MessageBox.Show("本机没有串口！", "Error");
                return;
            }

            //添加串口项目  
            foreach (string s in System.IO.Ports.SerialPort.GetPortNames())
            {//获取有多少个COM口  
                cbSerial.Items.Add(s);
            }

            try
            {
                //串口设置默认选择项  
                cbSerial.SelectedIndex = cbSerial.Items.Count-1;         //设置cbSerial的默认选项  
            } catch 
            { }
        }

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            if (cube.IsConnected()==false)
            {
                //sPort.PortName = cbSerial.Text;//串口的portname 
                //if (rad_57600.Checked == true) sPort.BaudRate = 57600;
                //if (rad_115200.Checked == true) sPort.BaudRate = 115200;
                //sPort.Open();'
                int i_brate=0;
                if (rad_57600.Checked == true) i_brate = 57600;
                if (rad_115200.Checked == true) i_brate = 115200;
                if (cube.Connect(cbSerial.Text, i_brate))
                {
                    lab_output.Text = "串口初始化成功";
                    //btn_Connect.Text = "断开";
                    Log("串口初始化成功");
                    Log("端口为 " + cbSerial.Text);
                }else
                {
                    lab_output.Text = "串口初始化失败";
                    Log("串口初始化失败");
                }
            }else
            {

            }
        }

        private void btn_Send_Click(object sender, EventArgs e)
        {

        }
        private void funOutput(int iRatate)
        {
            this.pic_Main.Image = this.cube.GetBmp(iRatate,pic_Main.Width,pic_Main.Height);
            this.cube.GetCube(iRatate);
            this.rtb_Output.Text = this.cube.GetPrintOut();
            this.cube.SendData();

        }

        private void btn_AllOn_Click(object sender, EventArgs e)
        {
            this.cube.SetUniqueValue2Column(255);
            this.funOutput(0);
        }

        private void btn_AllOff_Click(object sender, EventArgs e)
        {
            this.cube.SetUniqueValue2Column(0);
            this.funOutput(0);
        }


        private void tmr_Roll(object sender, System.EventArgs e)
        {
            if (this.cFrameIndex == 0)
            {
                this.cube.Clear();
                for (int i = 0; i < 64; i++)
                {
                    this.cube.SetColumn(i, Cube.c_Z[i / 8]);
                }
                this.tmr.Tag = Rotate.Up_Z;
            }
            if (this.cFrameIndex == 112)
            {
                this.tmr.Tag = Rotate.Up_X;
            }
            this.cube.RollAll(this.cFrameIndex % 7, true, true);
            this.funEnding(224);
        }

        private void tmr_Firework(object sender, System.EventArgs e)
        {
            if (this.cFrameIndex == 0)
            {
                this.tmr.Interval = (int)((double)this.tbSpeed.Value * 1.5);
                this.tmr.Tag = Rotate.Up_X;
                this.pSnake = null;
                this.pSnake = new Snake[8];
                for (int i = 0; i < this.pSnake.Length; i++)
                {
                    this.pSnake[i] = new Snake(1);
                    this.pSnake[i].SetHead(this.rnd.Next(64) + (i << 6));
                    this.pSnake[i].Tag = -this.rnd.Next(50);
                }
            }
            this.cube.Clear();
            for (int j = 0; j < 8; j++)
            {
                if (this.pSnake[j].Tag > 0 && this.pSnake[j].Tag < 10)
                {
                    for (int k = 0; k < 8; k++)
                    {
                        for (int l = 0; l < 8; l++)
                        {
                            double num = Cube.DistanceOfDot2Dot(this.pSnake[j].Head_X(), this.pSnake[j].Head_Y(), j, k, l, j);
                            if (num >= (double)this.pSnake[j].Tag - 0.5 && num <= (double)this.pSnake[j].Tag + 0.5)
                            {
                                this.cube.SetDot(k, l, j, true);
                            }
                        }
                    }
                }
                this.pSnake[j].Tag++;
                if (this.pSnake[j].Tag > 11)
                {
                    this.pSnake[j] = new Snake(1);
                    this.pSnake[j].SetHead(this.rnd.Next(64) + (j << 6));
                    this.pSnake[j].Tag = -this.rnd.Next(50);
                }
            }
            this.funEnding(100);

        }

        private void tmr_Drop(object sender, System.EventArgs e)
        {
            if (this.cFrameIndex == 0)
            {
                this.tmr.Interval = (int)((double)this.tbSpeed.Value * 1.5);
                this.pSnake = null;
                this.pSnake = new Snake[2];
                for (int i = 0; i < this.pSnake.Length; i++)
                {
                    this.pSnake[i] = new Snake(1);
                    this.pSnake[i].SetHead(this.rnd.Next(64));
                    this.pSnake[i].Tag = this.rnd.Next(8) + 7;
                }
            }
            this.cube.Clear();
            for (int j = 0; j < this.pSnake.Length; j++)
            {
                if (this.pSnake[j].Tag < 7 && this.pSnake[j].Tag >= 0)
                {
                    this.pSnake[j].SetHead(Cube.Index(this.pSnake[j].Head_X(), this.pSnake[j].Head_Y(), this.pSnake[j].Tag));
                    this.cube.SetDot(this.pSnake[j].GetDots(), true);
                }
                this.pSnake[j].Tag--;
                if (this.pSnake[j].Tag < 0 && this.pSnake[j].Tag > -10)
                {
                    for (int k = 0; k < 8; k++)
                    {
                        for (int l = 0; l < 8; l++)
                        {
                            double num = Cube.DistanceOfDot2Dot(this.pSnake[j].Head_X(), this.pSnake[j].Head_Y(), 0, k, l, 0);
                            if (num >= (double)(-(double)this.pSnake[j].Tag) - 0.5 && num <= (double)(-(double)this.pSnake[j].Tag) + 0.5)
                            {
                                this.cube.SetDot(k, l, 0, true);
                            }
                        }
                    }
                }
                if (this.pSnake[j].Tag < -10)
                {
                    this.pSnake[j] = new Snake(1);
                    this.pSnake[j].SetHead(this.rnd.Next(64));
                    this.pSnake[j].Tag = this.rnd.Next(8) + 7;
                }
            }
            this.funEnding(300);
        }

        private void tmr_Dance(object sender, System.EventArgs e)
        {
            if (this.cFrameIndex == 0)
            {
                this.cube.Clear();
                this.tmr.Tag = Rotate.Up_Z;
                this.pSnake = null;
                this.pSnake = new Snake[1];
                this.pSnake[0] = new Snake(1);
                this.pSnake[0].SetHead(this.rnd.Next(64));
            }
            this.cube.Move(0, false);
            this.cube.SetDot(this.pSnake[0].GetDots(), false);
            int num;
            do
            {
                num = this.pSnake[0].Head_X() + this.rnd.Next(3) - 1;
            }
            while (num < 0 || num > 7);
            int num2;
            do
            {
                num2 = this.pSnake[0].Head_Y() + this.rnd.Next(3) - 1;
            }
            while (num2 < 0 || num2 > 7);
            this.pSnake[0].SetHead(Cube.Index(num, num2, 0));
            this.cube.SetDot(this.pSnake[0].GetDots(), true);
            this.funEnding(240);
        }
        private void rand64_Init(int iLength)
        {
            this._RandChain = null;
            this._RandChain = new int[iLength];
            for (int i = 0; i < this._RandChain.Length; i++)
            {
                this._RandChain[i] = i;
            }
            for (int j = 0; j < this._RandChain.Length; j++)
            {
                Cube.Swift(ref this._RandChain[j], ref this._RandChain[this.rnd.Next(this._RandChain.Length)]);
            }
        }
        private void tmr_Bug(object sender, System.EventArgs e)
        {
            int num = this.cFrameIndex % 30;
            if (num == 0)
            {
                this.cube.Clear();
                this.pSnake = new Snake[1];
                this.rand64_Init(21);
                this.tmr.Tag = this.rnd.Next(255);
                this.pSnake = null;
                this.pSnake = new Snake[1];
                this.pSnake[0] = new Snake(6);
                for (int i = 0; i < this._RandChain.Length; i++)
                {
                    this._RandChain[i] %= 3;
                }
            }
            this.cube.SetDot(this.pSnake[0].GetDots(), false);
            if (num < 21)
            {
                switch (this._RandChain[num])
                {
                    case 0:
                        this.pSnake[0].Move(4);
                        break;
                    case 1:
                        this.pSnake[0].Move(2);
                        break;
                    case 2:
                        this.pSnake[0].Move(0);
                        break;
                }
            }
            else
            {
                this.pSnake[0].Follow();
            }
            this.cube.SetDot(this.pSnake[0].GetDots(), true);
            this.funEnding(240);
        }

        private void tmr_3Bugs(object sender, System.EventArgs e)
        {
            int num = this.cFrameIndex % 40;
            if (num == 0)
            {
                this.cube.Clear();
                this.pSnake = new Snake[1];
                this.rand64_Init(21);
                for (int i = 0; i < this._RandChain.Length; i++)
                {
                    this._RandChain[i] %= 3;
                }
                while (this.IndexLastOfArray(this._RandChain, 0) > this.IndexFirstOfArray(this._RandChain, 2))
                {
                    Cube.Swift(ref this._RandChain[this.IndexLastOfArray(this._RandChain, 0)], ref this._RandChain[this.IndexFirstOfArray(this._RandChain, 2)]);
                }
                this.tmr.Tag = this.rnd.Next(255);
                this.pSnake = null;
                this.pSnake = new Snake[3];
                for (int j = 0; j < this.pSnake.Length; j++)
                {
                    this.pSnake[j] = new Snake(12);
                }
            }
            for (int k = 0; k < this.pSnake.Length; k++)
            {
                this.cube.SetDot(this.pSnake[k].GetDots(), false);
            }
            if (num < 21)
            {
                switch (this._RandChain[num])
                {
                    case 0:
                        this.pSnake[0].Move(4);
                        this.pSnake[1].Move(2);
                        this.pSnake[2].Move(0);
                        break;
                    case 1:
                        this.pSnake[0].Move(2);
                        this.pSnake[1].Move(0);
                        this.pSnake[2].Move(4);
                        break;
                    case 2:
                        this.pSnake[0].Move(0);
                        this.pSnake[1].Move(4);
                        this.pSnake[2].Move(2);
                        break;
                }
            }
            else
            {
                for (int l = 0; l < this.pSnake.Length; l++)
                {
                    this.pSnake[l].Follow();
                }
            }
            for (int m = 0; m < this.pSnake.Length; m++)
            {
                this.cube.SetDot(this.pSnake[m].GetDots(), true);
            }
            this.funEnding(240);
        }

        private int IndexFirstOfArray(int[] p, int iSearchValue)
        {
            int num = 0;
            while (num < p.Length && p[num] != iSearchValue)
            {
                num++;
            }
            if (num == p.Length)
            {
                return -1;
            }
            return num;
        }

        private int IndexLastOfArray(int[] p, int iSearchValue)
        {
            int num = p.Length - 1;
            while (num >= 0 && p[num] != iSearchValue)
            {
                num--;
            }
            return num;
        }

        private void tmr_Wave_2(object sender, System.EventArgs e)
        {
            int num = this.cFrameIndex % 7;
            if (this.cFrameIndex == 0)
            {
                this.tmr.Tag = Rotate.Up_X;
            }
            this.cube.Move(0, false);
            switch (this.cFrameIndex % 56 / 7)
            {
                case 0:
                    this.cube.SetLine(0, 0, 0, 7, num, 0, true, 0.25);
                    break;
                case 1:
                    this.cube.SetLine(0, 0, 0, 7 - num, 7, 0, true, 0.25);
                    break;
                case 2:
                    this.cube.SetLine(num, 0, 0, 0, 7, 0, true, 0.25);
                    break;
                case 3:
                    this.cube.SetLine(7, num, 0, 0, 7, 0, true, 0.25);
                    break;
                case 4:
                    this.cube.SetLine(7, 7, 0, 0, 7 - num, 0, true, 0.25);
                    break;
                case 5:
                    this.cube.SetLine(7, 7, 0, num, 0, 0, true, 0.25);
                    break;
                case 6:
                    this.cube.SetLine(7 - num, 7, 0, 7, 0, 0, true, 0.25);
                    break;
                case 7:
                    this.cube.SetLine(0, 7 - num, 0, 7, 0, 0, true, 0.25);
                    break;
            }
            this.funEnding(112);
        }

        private void tmr_Wave_0(object sender, System.EventArgs e)
        {
            int num = this.cFrameIndex % 7;
            this.tmr.Tag = Rotate.Up_Z;
            this.cube.Move(0, false);
            switch (this.cFrameIndex % 28 / 7)
            {
                case 0:
                    this.cube.SetLine(0, 0, 0, 7, num, 0, true, 0.25);
                    break;
                case 1:
                    this.cube.SetLine(0, 0, 0, 7 - num, 7, 0, true, 0.25);
                    break;
                case 2:
                    this.cube.SetLine(0, 0, 0, num, 7, 0, true, 0.25);
                    break;
                case 3:
                    this.cube.SetLine(0, 0, 0, 7, 7 - num, 0, true, 0.25);
                    break;
            }
            this.funEnding(210);
        }

        private void tmr_Wave_3(object sender, System.EventArgs e)
        {
            if (this.cFrameIndex == 0)
            {
                this.tmr.Tag = Rotate.Up_Z;
            }
            for (int i = 14; i > 0; i--)
            {
                for (int j = 0; j < 8; j++)
                {
                    for (int k = 0; k < 8; k++)
                    {
                        if (j + k == i)
                        {
                            if (i < 8)
                            {
                                this.cube.Col2Col(j, k, 0, i - 1);
                            }
                            else
                            {
                                this.cube.Col2Col(j, k, i - 8, 7);
                            }
                        }
                    }
                }
                this.funEnding(210);
            }
            this.cube.SetColumn(0, 0);
            if (this.cFrameIndex < 84)
            {
                this.cube.SetDot(0, 0, System.Convert.ToInt32((System.Math.Cos(6.2831853071795862 * (double)this.cFrameIndex / 21.0) + 1.0) * 3.5), true);
            }
            else if (this.cFrameIndex < 140)
            {
                this.cube.SetDot(0, 0, System.Convert.ToInt32((System.Math.Cos(6.2831853071795862 * (double)this.cFrameIndex / 28.0) + 1.0) * 3.5), true);
            }
            else if (this.cFrameIndex < 210)
            {
                this.cube.SetDot(0, 0, System.Convert.ToInt32((System.Math.Cos(6.2831853071795862 * (double)this.cFrameIndex / 35.0) + 1.0) * 3.5), true);
            }
        }

        private void tmr_Wave_4(object sender, System.EventArgs e)
        {
            if (this.cFrameIndex == 0)
            {
                this.tmr.Tag = Rotate.Up_Z;
            }
            for (int i = 7; i > 4; i--)
            {
                for (int j = 7; j >= 4; j--)
                {
                    this.cube.Col2Col(j, i, j, i - 1);
                }
            }
            for (int k = 7; k > 4; k--)
            {
                this.cube.Col2Col(k, 4, k - 1, 4);
            }
            this.cube.SetColumn(Cube.Index(4, 4, 0), 0);
            if (this.cFrameIndex < 84)
            {
                this.cube.SetDot(4, 4, System.Convert.ToInt32((System.Math.Cos(6.2831853071795862 * (double)this.cFrameIndex / 21.0) + 1.0) * 3.5), true);
            }
            else if (this.cFrameIndex < 140)
            {
                this.cube.SetDot(4, 4, System.Convert.ToInt32((System.Math.Cos(6.2831853071795862 * (double)this.cFrameIndex / 28.0) + 1.0) * 3.5), true);
            }
            else if (this.cFrameIndex < 210)
            {
                this.cube.SetDot(4, 4, System.Convert.ToInt32((System.Math.Cos(6.2831853071795862 * (double)this.cFrameIndex / 35.0) + 1.0) * 3.5), true);
            }
            for (int l = 4; l < 8; l++)
            {
                for (int m = 0; m < 4; m++)
                {
                    this.cube.Col2Col(m, l, 7 - m, l);
                }
            }
            for (int n = 0; n < 4; n++)
            {
                for (int num = 0; num < 8; num++)
                {
                    this.cube.Col2Col(num, n, num, 7 - n);
                }
            }
            this.funEnding(210);
        }

        private void tmr_Wave_1(object sender, System.EventArgs e)
        {
            int num = this.cFrameIndex % 7;
            this.tmr.Tag = 0;
            this.cube.Move(0, false);
            switch (this.cFrameIndex % 28 / 7)
            {
                case 0:
                    this.cube.SetLine(num, 0, 0, 7 - num, 7, 0, true, 0.25);
                    break;
                case 1:
                    this.cube.SetLine(7, num, 0, 0, 7 - num, 0, true, 0.25);
                    break;
                case 2:
                    this.cube.SetLine(7 - num, 7, 0, num, 0, 0, true, 0.25);
                    break;
                case 3:
                    this.cube.SetLine(0, 7 - num, 0, 7, num, 0, true, 0.25);
                    break;
            }
            this.funEnding(112);
        }

        private void tmr_OneByOne(object sender, System.EventArgs e)
        {
            int x = 0;
            int y = 0;
            int z = 0;
            if (this.cFrameIndex == 0)
            {
                this.cube.SetUniqueValue2Column(0);
                this.rand64_Init(512);
                this.tmr.Tag = Rotate.Up_Z;
            }
            if (this.cFrameIndex == 540)
            {
                this.rand64_Init(512);
                this.tmr.Tag = Rotate.Up_Z;
            }
            if (this.cFrameIndex < 540)
            {
                int num = this.cFrameIndex - 20;
                if (num >= 0 && num < 512)
                {
                    Cube.Index2xyz(this._RandChain[num], ref x, ref y, ref z);
                    this.cube.SetDot(x, y, z, true);
                }
            }
            else
            {
                int num = this.cFrameIndex - 540;
                if (num >= 0 && num < 512)
                {
                    Cube.Index2xyz(this._RandChain[num], ref x, ref y, ref z);
                    this.cube.SetDot(x, y, z, false);
                }
            }
            this.funEnding(1080);
        }

        private void tmr_Poker(object sender, System.EventArgs e)
        {
            int num = this.cFrameIndex % 8;
            int num2 = this.cFrameIndex / 8 % 8;
            switch (this.cFrameIndex / 64)
            {
                case 0:
                    this.tmr.Tag = Rotate.Up_Z;
                    break;
                case 1:
                    this.tmr.Tag = Rotate.Reverse_Z;
                    break;
                case 2:
                    this.tmr.Tag = Rotate.Swift_XY;
                    break;
                case 3:
                    this.tmr.Tag = (Rotate)144;
                    break;
                case 4:
                    this.tmr.Tag = Rotate.Reverse_Y;
                    break;
                case 5:
                    this.tmr.Tag = (Rotate)192;
                    break;
                case 6:
                    this.tmr.Tag = (Rotate)48;
                    break;
                case 7:
                    this.tmr.Tag = (Rotate)176;
                    break;
            }
            this.cube.Clear();
            this.cube.SetCube(0, 0, 0, 7, 6 - num2, 0, true);
            this.cube.SetCube(7, 7 - num2, num, 0, 7 - num2, num, true);
            this.cube.SetCube(7, 7, 7, 0, 8 - num2, 7, true);
            this.funEnding(512);
        }

        private void tmr_Move_Up(object sender, System.EventArgs e)
        {
            switch (this.cFrameIndex / 7)
            {
                case 0:
                    this.tmr.Tag = Rotate.Up_Z;
                    break;
                case 1:
                    this.tmr.Tag = Rotate.Reverse_Z;
                    break;
                case 2:
                    this.tmr.Tag = Rotate.Up_Y;
                    break;
                case 3:
                    this.tmr.Tag = (Rotate)33;
                    break;
                case 4:
                    this.tmr.Tag = Rotate.Up_X;
                    break;
                case 5:
                    this.tmr.Tag = (Rotate)66;
                    break;
            }
            if (this.cFrameIndex % 7 == 0)
            {
                this.cube.SetUniqueValue2Column(1);
            }
            this.cube.Move(0, true);
            this.funEnding(42);
        }

        private void tmr_Block(object sender, System.EventArgs e)
        {
            int num = this.cFrameIndex % 9;
            switch (this.cFrameIndex / 18)
            {
                case 0:
                    this.tmr.Tag = Rotate.Up_Z;
                    break;
                case 1:
                    this.tmr.Tag = Rotate.Up_Y;
                    break;
                case 2:
                    this.tmr.Tag = Rotate.Up_X;
                    break;
            }
            switch (this.cFrameIndex / 9 % 2)
            {
                case 0:
                    this.cube.Clear();
                    if (num < 8)
                    {
                        this.cube.SetCube(0, 0, 0, 7, 7, num, true);
                    }
                    else
                    {
                        this.cube.SetCube(0, 0, 0, 7, 7, 7, true);
                    }
                    break;
                case 1:
                    this.cube.Clear();
                    if (num < 8)
                    {
                        this.cube.SetCube(0, 0, num, 7, 7, 7, true);
                    }
                    break;
            }
            this.funEnding(54);
        }

        private void tmr_Rise(object sender, System.EventArgs e)
        {
            this.cube.Move(0, false);
            for (int i = 0; i < this.rnd.Next(6); i++)
            {
                this.cube.SetDot(this.rnd.Next(8), this.rnd.Next(8), 0, true);
            }
            this.funEnding(40); 
        }

        private void tmr_Climb(object sender, System.EventArgs e)
        {
            int num = this.cFrameIndex % 80;
            switch (this.cFrameIndex / 80)
            {
                case 0:
                    this.tmr.Tag = Rotate.Up_Z;
                    break;
                case 1:
                    this.tmr.Tag = Rotate.Reverse_Z;
                    break;
                case 2:
                    this.tmr.Tag = Rotate.Up_Y;
                    break;
                case 3:
                    this.tmr.Tag = (Rotate)33;
                    break;
                case 4:
                    this.tmr.Tag = Rotate.Up_X;
                    break;
                case 5:
                    this.tmr.Tag = (Rotate)66;
                    break;
            }
            if (num == 0)
            {
                this.cube.SetUniqueValue2Column(1);
                this.rand64_Init(64);
            }
            if (this.cube.AndValue(Rotate.Up_Z) != 128)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        for (int k = 7; k > 1; k--)
                        {
                            if (this.cube.GetDot(i, j, k - 1))
                            {
                                this.cube.SetDot(i, j, k, true);
                                this.cube.SetDot(i, j, k - 1, false);
                            }
                        }
                    }
                }
            }
            if (num > 0 && num < 65)
            {
                int num2 = this._RandChain[num - 1];
                this.cube.SetDot(num2 % 8, num2 / 8, 1, true);
                this.cube.SetDot(num2 % 8, num2 / 8, 0, false);
            }
            this.funEnding(480);
        }

        private void tmr_Cube(object sender, System.EventArgs e)
        {
            int num = this.cFrameIndex % 16;
            switch (this.cFrameIndex / 16)
            {
                case 0:
                    this.tmr.Tag = Rotate.Up_Z;
                    break;
                case 1:
                    this.tmr.Tag = Rotate.Reverse_Y;
                    break;
                case 2:
                    this.tmr.Tag = Rotate.Reverse_X;
                    break;
                case 3:
                    this.tmr.Tag = (Rotate)96;
                    break;
            }
            this.cube.Clear();
            if (num < 8)
            {
                this.cube.SetFrame(0, 0, 0, num, num, num, true);
            }
            else
            {
                this.cube.SetFrame(7, 7, 7, num - 8, num - 8, num - 8, true);
            }
            this.funEnding(64);
        }



        private void tmr_Output(object sender, System.EventArgs e)
        {
            int iRatate = (sender == null) ? 0 : ((int)((Timer)sender).Tag);
            this.funOutput(iRatate);
        }
        private void tbSpeed_Scroll(object sender, System.EventArgs e)
        {
            try
            {
                if (this.tmr != null)
                {
                    this.tmr.Interval = this.tbSpeed.Value;
                }
                Log("速度设置为:" + this.tbSpeed.Value);
            }
            catch
            { }
        }
        private void btnPlay_Click(object sender, System.EventArgs e)
        {
            lab_output.Text = "运行动画序列" + lbAnimation.SelectedIndex.ToString();
            Log("运行动画序列"+lbAnimation.SelectedIndex.ToString());
            if (this.tmr != null)
            {
                if (!this.tmr.Enabled)
                {
                    this.tmr.Enabled = true;
                }
                return;
            }
            this.tmr = new Timer();
            this.tmr.Interval = this.tbSpeed.Value;
            this.tmr.Tag = Rotate.Up_Z;
            if (this.lbAnimation.SelectedIndex == -1)
            {
                this.lbAnimation.SelectedIndex = 0;
            }
            this.cFrameIndex = 0;
            switch (this.lbAnimation.SelectedIndex)
            {
                case 0:
                    this.tmr.Tick += new System.EventHandler(this.tmr_Move_Up);
                    break;
                case 1:
                    this.tmr.Tick += new System.EventHandler(this.tmr_Block);
                    break;
                case 2:
                    this.tmr.Tag = 0;
                    this.tmr.Tick += new System.EventHandler(this.tmr_Rise);
                    break;
                case 3:
                    this.tmr.Tag = 1;
                    this.tmr.Tick += new System.EventHandler(this.tmr_Rise);
                    break;
                case 4:
                    this.tmr.Tag = 128;
                    this.tmr.Tick += new System.EventHandler(this.tmr_Rise);
                    break;
                case 5:
                    this.tmr.Tick += new System.EventHandler(this.tmr_Climb);
                    break;
                case 6:
                    this.tmr.Tag = 0;
                    this.tmr.Tick += new System.EventHandler(this.tmr_Cube);
                    break;
                case 7:
                    this.tmr.Interval /= 4;
                    this.tmr.Tick += new System.EventHandler(this.tmr_Poker);
                    break;
                case 8:
                    this.tmr.Tick += new System.EventHandler(this.tmr_Wave_0);
                    break;
                case 9:
                    this.tmr.Tick += new System.EventHandler(this.tmr_Wave_1);
                    break;
                case 10:
                    this.tmr.Tick += new System.EventHandler(this.tmr_Wave_2);
                    break;
                case 11:
                    this.tmr.Tick += new System.EventHandler(this.tmr_Wave_3);
                    break;
                case 12:
                    this.tmr.Tick += new System.EventHandler(this.tmr_Wave_4);
                    break;
                case 13:
                    this.tmr.Tick += new System.EventHandler(this.tmr_OneByOne);
                    break;
                case 14:
                    this.tmr.Tick += new System.EventHandler(this.tmr_Dance);
                    break;
                case 15:
                    this.tmr.Tick += new System.EventHandler(this.tmr_Bug);
                    break;
                case 16:
                    this.tmr.Tick += new System.EventHandler(this.tmr_3Bugs);
                    break;
                case 17:
                    this.tmr.Tick += new System.EventHandler(this.tmr_Drop);
                    break;
                case 18:
                    this.tmr.Tick += new System.EventHandler(this.tmr_Firework);
                    break;
                case 19:
                    this.tmr.Tick += new System.EventHandler(this.tmr_Roll);
                    break;
            }
            this.tmr.Tick += new System.EventHandler(this.tmr_Output);
            this.tmr.Enabled = true;
        }

        private void funEnding(int iFrameIndexEnd)
        {
            this.cFrameIndex++;
            if (this.cFrameIndex < iFrameIndexEnd)
            {
                return;
            }
            this.cFrameIndex = 0;
            //if (this.lbAnimation.SelectedIndex < this.lbAnimation.Items.Count - 1)
            //{
            //    //this.lbAnimation.SelectedIndex++;
            //    return;
            //}
            //this.lbAnimation.SelectedIndex = 0;
        }

        private void btnStop_Click(object sender, System.EventArgs e)
        {
            lab_output.Text = "停止动画";
            Log("停止动画");
            if (this.tmr == null)
            {
                return;
            }
            this.tmr.Enabled = false;
            this.tmr = null;
        }

        private void btnPause_Click(object sender, System.EventArgs e)
        {
            if (this.tmr != null)
            {
                this.tmr.Enabled = !this.tmr.Enabled;
            }
        }



        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.cube.DisConnect();
        }

        private void lbAnimation_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnStop_Click(sender, e);
            this.btnPlay_Click(sender, e);
        }

        private void btn_AudioControl_Click(object sender, EventArgs e)
        {
            btnPause_Click(sender, e);
            this.btn_AllOff_Click(sender, e);
            this.cube.SetDot(0, 0, 0, true);
            this.cube.SetDot(0, 0, 7, false);
            this.cube.SetDot(0, 7, 0, true);
            this.cube.SetDot(0, 7, 7, true);
            this.cube.SetDot(7, 0, 0, true);
            this.cube.SetDot(7, 0, 7, true);
            this.cube.SetDot(7, 7, 0, true);
            this.cube.SetDot(7, 7, 7, true);
            this.funOutput(0);
        }

        private void openGLControl1_OpenGLDraw(object sender, PaintEventArgs e)
        {
            Cube.Matrix matrix = new Cube.Matrix();
            matrix = cube.GetMatrix();
            

            // 创建一个GL对象
            SharpGL.OpenGL gl = openGLControl1.OpenGL;
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);	// 清空屏幕
            gl.LoadIdentity();                  // 重置


            gl.Translate(bias_x, bias_y, _distance); // 设置坐标，距离屏幕距离为6

            gl.Rotate(_x, 1.0f, 0.0f, 0.0f);    // 绕X轴旋转
            gl.Rotate(_y, 0.0f, 1.0f, 0.0f);    // 绕Y轴旋转
            gl.Rotate(_z, 0.0f, 0.0f, 1.0f);    // 绕Z轴旋转

            gl.Rotate(_x, 1.0f, 0.0f, 0.0f);    // 绕X轴旋转
            gl.Rotate(_y, 0.0f, 1.0f, 0.0f);    // 绕Y轴旋转
            gl.Rotate(_z, 0.0f, 0.0f, 1.0f);    // 绕Z轴旋转

            for (int i = 0; i < 8; i = i + 1)
            {
                for (int j = 0; j < 8; j = j + 1)
                {
                    for (int k = 0; k < 8; k = k + 1)
                    {

                        _model = OpenGL.GL_QUADS;

                        if (matrix.matrix[i, k, j] == true)
                        {
                            Color color2 = new Color();
                            color2.R = 1; color2.G = 0; color2.B = 0;
                            Point point2 = new Point();
                            point2.X = i * 1.05 + 0.4; point2.Y = j * 1.05 + 0.4; point2.Z = k * 1.05 + 0.4;
                            DrawSquire(gl, point2, color2, 0.2);
                        }
                        _model = OpenGL.GL_LINE_LOOP;

                        Color color = new Color();
                        color.R = 0.3; color.G = 0.3; color.B = 0.3;
                        Point point = new Point();
                        point.X = i * 1.05; point.Y = j * 1.05; point.Z = k * 1.05;
                        DrawSquire(gl, point, color, 1);

                    }
                }
            }
            ;
            // 结束绘制
            //System.Threading.Thread.Sleep(1000);
        }

        private void t_Distance_Scroll(object sender, EventArgs e)
        {
            int distance = t_Distance.Value;
            _distance = distance;
            lab_distance.Text = "disance:" + distance;
        }

        private void tbX_Scroll(object sender, EventArgs e)
        {
            int x = tbX.Value;
            _x = x;
            labX.Text = "X:" + x;
        }

        private void tbY_Scroll(object sender, EventArgs e)
        {
            int y = tbY.Value;
            _y = y;
            labY.Text = "Y:" + y;
        }

        private void tbZ_Scroll(object sender, EventArgs e)
        {
            int z = tbZ.Value;
            _z = z;
            labZ.Text = "Z:" + z;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            _distance = -18;
            _x = 0;
            _y = 22;
            _z = 0;

            bias_x = -6;
            bias_y = -4.5;
        }
        long l_lastCmdSEQ = 0;
        long reportno = 0;
        private void btn_GetCmd_Click(object sender, EventArgs e)
        {
            //////////////数据格式
            // 指令 0  RUN STOP CONNECT SPEED
            // 参数 1  SEQ (0-19） COM1-COM20
            // 序列号 2

            try
            {

                List<string> cmd = new List<string>();
                cmd = file.ReadToList("MatrixCmd.ini");
                long l_currCmdSEQ = long.Parse(cmd[3].ToString());

                //接收到新指令
                if (l_lastCmdSEQ != l_currCmdSEQ)
                {
                    l_lastCmdSEQ = l_currCmdSEQ;
                    Log("发现新指令");

                    if (cmd[0] == "STOP")
                    {

                        //停止自动化
                        btnStop_Click(sender, e);

                    }

                    if (cmd[0] == "RUN")
                    {
                        if (cmd[1] == "20")
                        {
                            btnStop_Click(sender, e);
                            btn_AllOff_Click(sender, e);
                            return;
                        }else if (cmd[1] == "21")
                        {
                            btn_AllOn_Click(sender, e);
                            return;
                        }
                        else
                        {
                            lbAnimation.SelectedIndex = Convert.ToInt16(cmd[1]);
                        }
                        if (cmd[2] == "0")
                        {
                            //停止自动化
                            btnStop_Click(sender, e);
                            return;
                        }
                        else if (cmd[2] == "")
                        {
                        }
                        else
                        {
                            double rawspeed = Convert.ToDouble(cmd[2]);
                            if (rawspeed > 20) rawspeed = 20;
                            tbSpeed.Value = (int)(100 / rawspeed);
                            tbSpeed_Scroll(sender, e);
                        }

                        //运行自动化
                        btnPlay_Click(sender, e);
                    }

                    if (cmd[0] == "SPEED")
                    {
                        if (cmd[1] == "0")
                        {
                            //停止自动化
                            btnStop_Click(sender, e);
                        }
                        else
                        {
                            double rawspeed = Convert.ToDouble(cmd[1]);
                            tbSpeed.Value = (int)(100 / rawspeed);
                            tbSpeed_Scroll(sender, e);
                        }

                    }

                    if (cmd[0] == "CONNECT")
                    {
                        Log("连接串口 "+cbSerial.Text);
                        cbSerial.Text = cmd[1].ToString();
                        btn_Connect_Click(sender, e);
                    }

                }

            }
            catch
            { }
        }
        string s_lastLog = "";
        private void Log(string txt)
        {
            if (txt != s_lastLog)
            {
                rtb_output2.Text = rtb_output2.Text + txt + "\n";
                rtb_output2.SelectionStart = rtb_output2.Text.Length;
                rtb_output2.ScrollToCaret();
                s_lastLog = txt;
                if (rtb_output2.Text.Length>500)
                {
                    rtb_output2.Text = rtb_output2.Text.Substring(rtb_output2.Text.Length - 500, 500);
                }
            }
        }

        private void btn_Report_Click(object sender, EventArgs e)
        {
            tmr_Report.Enabled = false;
            try
            {
                List<string> report = new List<string>();
                report.Add("");
                report.Add("");
                if (lab_output.Text.Contains("运行动画"))
                {
                    report[0] = ("运行");
                }
                else if (lab_output.Text.Contains("停止"))
                {
                    report[0] = ("停止");
                }
                else
                {
                    report[0] = ("忙");
                }
                report[1] = reportno.ToString();
                reportno++;
                file.WriteToFile(report, "MatrixReport.ini");
            }
            catch { }
            tmr_Report.Enabled = true;
        }

        private void tmr_Cmd_Tick(object sender, EventArgs e)
        {
            btn_GetCmd_Click(sender, e);
        }

        private void tmr_Report_Tick(object sender, EventArgs e)
        {
            btn_Report_Click(sender, e);
        }
        public static bool GetBits(uint num, int bit)
        {
            uint v = bit == 0 ? 1 : 2u << (bit - 1);
            return ((num & v) != 0);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 8; i++)
            {
                rtb_output2.Text = rtb_output2.Text+ GetBits(255, i).ToString();
            }
        }

        private void cmb_display_SelectedIndexChanged(object sender, EventArgs e)
        {
            cube.SetDirection(cmb_display.SelectedIndex + 1);
        }

        private void btn_DisConnect_Click(object sender, EventArgs e)
        {
            cube.DisConnect();
            lab_output.Text = "串口被关闭";
            Log("串口被关闭");
            //btn_Connect.Text = "连接";
        }
    }
}
