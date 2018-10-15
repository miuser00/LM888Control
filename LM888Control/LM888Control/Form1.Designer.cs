namespace LM888Control
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_Connect = new System.Windows.Forms.Button();
            this.rad_115200 = new System.Windows.Forms.RadioButton();
            this.rad_57600 = new System.Windows.Forms.RadioButton();
            this.cbSerial = new System.Windows.Forms.ComboBox();
            this.tbSpeed = new System.Windows.Forms.TrackBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lbAnimation = new System.Windows.Forms.ListBox();
            this.btn_AudioControl = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.sPort = new System.IO.Ports.SerialPort(this.components);
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lab_output = new System.Windows.Forms.Label();
            this.pic_Main = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.tbZ = new System.Windows.Forms.TrackBar();
            this.lab_distance = new System.Windows.Forms.Label();
            this.labZ = new System.Windows.Forms.Label();
            this.t_Distance = new System.Windows.Forms.TrackBar();
            this.labY = new System.Windows.Forms.Label();
            this.tbX = new System.Windows.Forms.TrackBar();
            this.labX = new System.Windows.Forms.Label();
            this.tbY = new System.Windows.Forms.TrackBar();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnReset = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_display = new System.Windows.Forms.ComboBox();
            this.rtb_output2 = new System.Windows.Forms.RichTextBox();
            this.btn_Report = new System.Windows.Forms.Button();
            this.btn_GetCmd = new System.Windows.Forms.Button();
            this.btn_AllOff = new System.Windows.Forms.Button();
            this.btn_AllOn = new System.Windows.Forms.Button();
            this.btn_Send = new System.Windows.Forms.Button();
            this.rtb_Output = new System.Windows.Forms.RichTextBox();
            this.openGLControl1 = new SharpGL.OpenGLControl();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.tmr_Cmd = new System.Windows.Forms.Timer(this.components);
            this.tmr_Report = new System.Windows.Forms.Timer(this.components);
            this.btn_DisConnect = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbSpeed)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Main)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.t_Distance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbY)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl1)).BeginInit();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_DisConnect);
            this.groupBox2.Controls.Add(this.btn_Connect);
            this.groupBox2.Controls.Add(this.rad_115200);
            this.groupBox2.Controls.Add(this.rad_57600);
            this.groupBox2.Controls.Add(this.cbSerial);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(9, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(354, 91);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "通讯设置：";
            // 
            // btn_Connect
            // 
            this.btn_Connect.ForeColor = System.Drawing.Color.DimGray;
            this.btn_Connect.Location = new System.Drawing.Point(236, 19);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(83, 21);
            this.btn_Connect.TabIndex = 3;
            this.btn_Connect.Text = "连接";
            this.btn_Connect.UseVisualStyleBackColor = true;
            this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // rad_115200
            // 
            this.rad_115200.AutoSize = true;
            this.rad_115200.Location = new System.Drawing.Point(101, 55);
            this.rad_115200.Name = "rad_115200";
            this.rad_115200.Size = new System.Drawing.Size(59, 16);
            this.rad_115200.TabIndex = 2;
            this.rad_115200.Text = "115200";
            this.rad_115200.UseVisualStyleBackColor = true;
            // 
            // rad_57600
            // 
            this.rad_57600.AutoSize = true;
            this.rad_57600.Checked = true;
            this.rad_57600.Location = new System.Drawing.Point(30, 55);
            this.rad_57600.Name = "rad_57600";
            this.rad_57600.Size = new System.Drawing.Size(53, 16);
            this.rad_57600.TabIndex = 1;
            this.rad_57600.TabStop = true;
            this.rad_57600.Text = "57600";
            this.rad_57600.UseVisualStyleBackColor = true;
            // 
            // cbSerial
            // 
            this.cbSerial.BackColor = System.Drawing.Color.DimGray;
            this.cbSerial.ForeColor = System.Drawing.Color.White;
            this.cbSerial.FormattingEnabled = true;
            this.cbSerial.Location = new System.Drawing.Point(23, 20);
            this.cbSerial.Name = "cbSerial";
            this.cbSerial.Size = new System.Drawing.Size(178, 20);
            this.cbSerial.TabIndex = 0;
            // 
            // tbSpeed
            // 
            this.tbSpeed.BackColor = System.Drawing.Color.DimGray;
            this.tbSpeed.LargeChange = 20;
            this.tbSpeed.Location = new System.Drawing.Point(30, 20);
            this.tbSpeed.Maximum = 100;
            this.tbSpeed.Minimum = 5;
            this.tbSpeed.Name = "tbSpeed";
            this.tbSpeed.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbSpeed.Size = new System.Drawing.Size(45, 292);
            this.tbSpeed.SmallChange = 5;
            this.tbSpeed.TabIndex = 2;
            this.tbSpeed.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tbSpeed.Value = 50;
            this.tbSpeed.Scroll += new System.EventHandler(this.tbSpeed_Scroll);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbSpeed);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(19, 28);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(102, 322);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "速度";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lbAnimation);
            this.groupBox4.Controls.Add(this.groupBox3);
            this.groupBox4.ForeColor = System.Drawing.Color.White;
            this.groupBox4.Location = new System.Drawing.Point(9, 100);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(354, 379);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "序列：";
            // 
            // lbAnimation
            // 
            this.lbAnimation.BackColor = System.Drawing.Color.DimGray;
            this.lbAnimation.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbAnimation.ForeColor = System.Drawing.Color.White;
            this.lbAnimation.FormattingEnabled = true;
            this.lbAnimation.ItemHeight = 16;
            this.lbAnimation.Items.AddRange(new object[] {
            "0.平面回旋",
            "1.填充立方",
            "2.矩阵爬升",
            "3.微风拂面",
            "4.矩阵下落",
            "5.矩阵上升",
            "6.方框扩展",
            "7.扑克运动",
            "8.倾斜波浪",
            "9.扭动波浪",
            "10.翻滚波浪",
            "11.标准波浪",
            "12.丘陵波浪",
            "13.逐个点亮",
            "14.流动舞蹈",
            "15.随机爬动",
            "16.多维爬动",
            "17.水滴落下",
            "18.光影飞溅",
            "19思考翻滚"});
            this.lbAnimation.Location = new System.Drawing.Point(140, 34);
            this.lbAnimation.Name = "lbAnimation";
            this.lbAnimation.ScrollAlwaysVisible = true;
            this.lbAnimation.Size = new System.Drawing.Size(195, 308);
            this.lbAnimation.TabIndex = 0;
            this.lbAnimation.SelectedIndexChanged += new System.EventHandler(this.lbAnimation_SelectedIndexChanged);
            // 
            // btn_AudioControl
            // 
            this.btn_AudioControl.Location = new System.Drawing.Point(137, 41);
            this.btn_AudioControl.Name = "btn_AudioControl";
            this.btn_AudioControl.Size = new System.Drawing.Size(102, 34);
            this.btn_AudioControl.TabIndex = 4;
            this.btn_AudioControl.Text = "声控模式";
            this.btn_AudioControl.UseVisualStyleBackColor = true;
            this.btn_AudioControl.Visible = false;
            this.btn_AudioControl.Click += new System.EventHandler(this.btn_AudioControl_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(425, 41);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(102, 34);
            this.btnPause.TabIndex = 2;
            this.btnPause.Text = "停止";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(267, 41);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(126, 34);
            this.btnPlay.TabIndex = 1;
            this.btnPlay.Text = "播放";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Location = new System.Drawing.Point(571, 61);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(371, 83);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "调试输出：";
            // 
            // lab_output
            // 
            this.lab_output.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lab_output.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_output.Location = new System.Drawing.Point(23, 395);
            this.lab_output.Name = "lab_output";
            this.lab_output.Size = new System.Drawing.Size(162, 22);
            this.lab_output.TabIndex = 0;
            this.lab_output.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pic_Main
            // 
            this.pic_Main.Location = new System.Drawing.Point(6, 6);
            this.pic_Main.Name = "pic_Main";
            this.pic_Main.Size = new System.Drawing.Size(953, 373);
            this.pic_Main.TabIndex = 6;
            this.pic_Main.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(12, 526);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1004, 642);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox6);
            this.tabPage2.Controls.Add(this.btn_AudioControl);
            this.tabPage2.Controls.Add(this.btnPause);
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Controls.Add(this.btnPlay);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(996, 616);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "控制";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.tbZ);
            this.groupBox6.Controls.Add(this.lab_distance);
            this.groupBox6.Controls.Add(this.labZ);
            this.groupBox6.Controls.Add(this.t_Distance);
            this.groupBox6.Controls.Add(this.labY);
            this.groupBox6.Controls.Add(this.tbX);
            this.groupBox6.Controls.Add(this.labX);
            this.groupBox6.Controls.Add(this.tbY);
            this.groupBox6.Location = new System.Drawing.Point(105, 189);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(549, 83);
            this.groupBox6.TabIndex = 19;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "3D显示";
            // 
            // tbZ
            // 
            this.tbZ.BackColor = System.Drawing.Color.White;
            this.tbZ.Location = new System.Drawing.Point(381, 34);
            this.tbZ.Maximum = 180;
            this.tbZ.Minimum = -180;
            this.tbZ.Name = "tbZ";
            this.tbZ.Size = new System.Drawing.Size(119, 45);
            this.tbZ.TabIndex = 12;
            this.tbZ.Scroll += new System.EventHandler(this.tbZ_Scroll);
            // 
            // lab_distance
            // 
            this.lab_distance.AutoSize = true;
            this.lab_distance.Location = new System.Drawing.Point(13, 19);
            this.lab_distance.Name = "lab_distance";
            this.lab_distance.Size = new System.Drawing.Size(65, 12);
            this.lab_distance.TabIndex = 10;
            this.lab_distance.Text = "Distance:0";
            // 
            // labZ
            // 
            this.labZ.AutoSize = true;
            this.labZ.Location = new System.Drawing.Point(388, 19);
            this.labZ.Name = "labZ";
            this.labZ.Size = new System.Drawing.Size(23, 12);
            this.labZ.TabIndex = 15;
            this.labZ.Text = "Z:0";
            // 
            // t_Distance
            // 
            this.t_Distance.BackColor = System.Drawing.Color.White;
            this.t_Distance.Location = new System.Drawing.Point(15, 34);
            this.t_Distance.Maximum = 180;
            this.t_Distance.Minimum = -180;
            this.t_Distance.Name = "t_Distance";
            this.t_Distance.Size = new System.Drawing.Size(119, 45);
            this.t_Distance.TabIndex = 11;
            this.t_Distance.Value = -50;
            this.t_Distance.Scroll += new System.EventHandler(this.t_Distance_Scroll);
            // 
            // labY
            // 
            this.labY.AutoSize = true;
            this.labY.Location = new System.Drawing.Point(265, 19);
            this.labY.Name = "labY";
            this.labY.Size = new System.Drawing.Size(23, 12);
            this.labY.TabIndex = 16;
            this.labY.Text = "Y:0";
            // 
            // tbX
            // 
            this.tbX.BackColor = System.Drawing.Color.White;
            this.tbX.Location = new System.Drawing.Point(131, 34);
            this.tbX.Maximum = 180;
            this.tbX.Minimum = -180;
            this.tbX.Name = "tbX";
            this.tbX.Size = new System.Drawing.Size(119, 45);
            this.tbX.TabIndex = 14;
            this.tbX.Scroll += new System.EventHandler(this.tbX_Scroll);
            // 
            // labX
            // 
            this.labX.AutoSize = true;
            this.labX.Location = new System.Drawing.Point(145, 19);
            this.labX.Name = "labX";
            this.labX.Size = new System.Drawing.Size(23, 12);
            this.labX.TabIndex = 17;
            this.labX.Text = "X:0";
            // 
            // tbY
            // 
            this.tbY.BackColor = System.Drawing.Color.White;
            this.tbY.Location = new System.Drawing.Point(256, 34);
            this.tbY.Maximum = 180;
            this.tbY.Minimum = -180;
            this.tbY.Name = "tbY";
            this.tbY.Size = new System.Drawing.Size(119, 45);
            this.tbY.TabIndex = 13;
            this.tbY.Scroll += new System.EventHandler(this.tbY_Scroll);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.pic_Main);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(996, 616);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "图像";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.Black;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.ForeColor = System.Drawing.Color.White;
            this.btnReset.Location = new System.Drawing.Point(1072, 71);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 18;
            this.btnReset.Text = "复位";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_Stop);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmb_display);
            this.groupBox1.Controls.Add(this.lab_output);
            this.groupBox1.Controls.Add(this.rtb_output2);
            this.groupBox1.Controls.Add(this.btn_Report);
            this.groupBox1.Controls.Add(this.btn_GetCmd);
            this.groupBox1.Controls.Add(this.btn_AllOff);
            this.groupBox1.Controls.Add(this.btn_AllOn);
            this.groupBox1.Controls.Add(this.btn_Send);
            this.groupBox1.Controls.Add(this.rtb_Output);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(375, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(357, 476);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据输出：";
            // 
            // btn_Stop
            // 
            this.btn_Stop.ForeColor = System.Drawing.Color.DimGray;
            this.btn_Stop.Location = new System.Drawing.Point(240, 420);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(83, 27);
            this.btn_Stop.TabIndex = 32;
            this.btn_Stop.Text = "停止";
            this.btn_Stop.UseVisualStyleBackColor = true;
            this.btn_Stop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(220, 148);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 22);
            this.button1.TabIndex = 31;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 291);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 30;
            this.label1.Text = "显示方向";
            // 
            // cmb_display
            // 
            this.cmb_display.BackColor = System.Drawing.Color.DimGray;
            this.cmb_display.ForeColor = System.Drawing.Color.White;
            this.cmb_display.FormattingEnabled = true;
            this.cmb_display.Items.AddRange(new object[] {
            "前",
            "后",
            "左",
            "右",
            "上",
            "下"});
            this.cmb_display.Location = new System.Drawing.Point(91, 288);
            this.cmb_display.Name = "cmb_display";
            this.cmb_display.Size = new System.Drawing.Size(77, 20);
            this.cmb_display.TabIndex = 29;
            this.cmb_display.Text = "前";
            this.cmb_display.SelectedIndexChanged += new System.EventHandler(this.cmb_display_SelectedIndexChanged);
            // 
            // rtb_output2
            // 
            this.rtb_output2.BackColor = System.Drawing.Color.DimGray;
            this.rtb_output2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtb_output2.Location = new System.Drawing.Point(25, 324);
            this.rtb_output2.Name = "rtb_output2";
            this.rtb_output2.Size = new System.Drawing.Size(298, 68);
            this.rtb_output2.TabIndex = 28;
            this.rtb_output2.Text = "";
            // 
            // btn_Report
            // 
            this.btn_Report.Location = new System.Drawing.Point(240, 11);
            this.btn_Report.Name = "btn_Report";
            this.btn_Report.Size = new System.Drawing.Size(83, 29);
            this.btn_Report.TabIndex = 27;
            this.btn_Report.Text = "Report";
            this.btn_Report.UseVisualStyleBackColor = true;
            this.btn_Report.Visible = false;
            this.btn_Report.Click += new System.EventHandler(this.btn_Report_Click);
            // 
            // btn_GetCmd
            // 
            this.btn_GetCmd.Location = new System.Drawing.Point(151, 11);
            this.btn_GetCmd.Name = "btn_GetCmd";
            this.btn_GetCmd.Size = new System.Drawing.Size(83, 29);
            this.btn_GetCmd.TabIndex = 26;
            this.btn_GetCmd.Text = "GetCMD";
            this.btn_GetCmd.UseVisualStyleBackColor = true;
            this.btn_GetCmd.Visible = false;
            this.btn_GetCmd.Click += new System.EventHandler(this.btn_GetCmd_Click);
            // 
            // btn_AllOff
            // 
            this.btn_AllOff.ForeColor = System.Drawing.Color.DimGray;
            this.btn_AllOff.Location = new System.Drawing.Point(136, 420);
            this.btn_AllOff.Name = "btn_AllOff";
            this.btn_AllOff.Size = new System.Drawing.Size(83, 27);
            this.btn_AllOff.TabIndex = 6;
            this.btn_AllOff.Text = "全部熄灭";
            this.btn_AllOff.UseVisualStyleBackColor = true;
            this.btn_AllOff.Click += new System.EventHandler(this.btn_AllOff_Click);
            // 
            // btn_AllOn
            // 
            this.btn_AllOn.ForeColor = System.Drawing.Color.DimGray;
            this.btn_AllOn.Location = new System.Drawing.Point(34, 420);
            this.btn_AllOn.Name = "btn_AllOn";
            this.btn_AllOn.Size = new System.Drawing.Size(83, 27);
            this.btn_AllOn.TabIndex = 5;
            this.btn_AllOn.Text = "全部点亮";
            this.btn_AllOn.UseVisualStyleBackColor = true;
            this.btn_AllOn.Click += new System.EventHandler(this.btn_AllOn_Click);
            // 
            // btn_Send
            // 
            this.btn_Send.Location = new System.Drawing.Point(253, 449);
            this.btn_Send.Name = "btn_Send";
            this.btn_Send.Size = new System.Drawing.Size(83, 21);
            this.btn_Send.TabIndex = 4;
            this.btn_Send.Text = "发送";
            this.btn_Send.UseVisualStyleBackColor = true;
            this.btn_Send.Visible = false;
            this.btn_Send.Click += new System.EventHandler(this.btn_Send_Click);
            // 
            // rtb_Output
            // 
            this.rtb_Output.BackColor = System.Drawing.Color.DimGray;
            this.rtb_Output.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtb_Output.Location = new System.Drawing.Point(25, 46);
            this.rtb_Output.Name = "rtb_Output";
            this.rtb_Output.Size = new System.Drawing.Size(298, 223);
            this.rtb_Output.TabIndex = 0;
            this.rtb_Output.Text = "";
            // 
            // openGLControl1
            // 
            this.openGLControl1.BackColor = System.Drawing.Color.Black;
            this.openGLControl1.BitDepth = 24;
            this.openGLControl1.DrawFPS = true;
            this.openGLControl1.ForeColor = System.Drawing.Color.Black;
            this.openGLControl1.FrameRate = 20;
            this.openGLControl1.Location = new System.Drawing.Point(11, 19);
            this.openGLControl1.Name = "openGLControl1";
            this.openGLControl1.RenderContextType = SharpGL.RenderContextType.DIBSection;
            this.openGLControl1.Size = new System.Drawing.Size(415, 441);
            this.openGLControl1.TabIndex = 6;
            this.openGLControl1.OpenGLDraw += new System.Windows.Forms.PaintEventHandler(this.openGLControl1_OpenGLDraw);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.openGLControl1);
            this.groupBox7.ForeColor = System.Drawing.Color.White;
            this.groupBox7.Location = new System.Drawing.Point(740, 3);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(432, 476);
            this.groupBox7.TabIndex = 19;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "预览：";
            // 
            // tmr_Cmd
            // 
            this.tmr_Cmd.Enabled = true;
            this.tmr_Cmd.Interval = 30;
            this.tmr_Cmd.Tick += new System.EventHandler(this.tmr_Cmd_Tick);
            // 
            // tmr_Report
            // 
            this.tmr_Report.Enabled = true;
            this.tmr_Report.Tick += new System.EventHandler(this.tmr_Report_Tick);
            // 
            // btn_DisConnect
            // 
            this.btn_DisConnect.ForeColor = System.Drawing.Color.DimGray;
            this.btn_DisConnect.Location = new System.Drawing.Point(236, 46);
            this.btn_DisConnect.Name = "btn_DisConnect";
            this.btn_DisConnect.Size = new System.Drawing.Size(83, 21);
            this.btn_DisConnect.TabIndex = 4;
            this.btn_DisConnect.Text = "断开";
            this.btn_DisConnect.UseVisualStyleBackColor = true;
            this.btn_DisConnect.Click += new System.EventHandler(this.btn_DisConnect_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1181, 487);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox2);
            this.ForeColor = System.Drawing.Color.White;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "花阵 0.1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbSpeed)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_Main)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.t_Distance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbY)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl1)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_Connect;
        private System.Windows.Forms.RadioButton rad_115200;
        private System.Windows.Forms.RadioButton rad_57600;
        private System.Windows.Forms.ComboBox cbSerial;
        private System.Windows.Forms.TrackBar tbSpeed;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.ListBox lbAnimation;
        private System.IO.Ports.SerialPort sPort;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label lab_output;
        private System.Windows.Forms.PictureBox pic_Main;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btn_AudioControl;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_AllOff;
        private System.Windows.Forms.Button btn_AllOn;
        private System.Windows.Forms.Button btn_Send;
        private System.Windows.Forms.RichTextBox rtb_Output;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label labZ;
        private System.Windows.Forms.Label labY;
        private System.Windows.Forms.Label labX;
        private System.Windows.Forms.TrackBar tbZ;
        private System.Windows.Forms.TrackBar tbY;
        private System.Windows.Forms.TrackBar tbX;
        private System.Windows.Forms.TrackBar t_Distance;
        private System.Windows.Forms.Label lab_distance;
        private System.Windows.Forms.GroupBox groupBox6;
        private SharpGL.OpenGLControl openGLControl1;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btn_Report;
        private System.Windows.Forms.Button btn_GetCmd;
        private System.Windows.Forms.RichTextBox rtb_output2;
        private System.Windows.Forms.Timer tmr_Cmd;
        private System.Windows.Forms.Timer tmr_Report;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_display;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_Stop;
        private System.Windows.Forms.Button btn_DisConnect;
    }
}

