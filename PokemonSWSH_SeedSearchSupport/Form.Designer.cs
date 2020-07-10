namespace PokemonSWSH_SeedSearchSupport
{
    partial class PokemonSWSH_SeedSearchSupport
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.up = new System.Windows.Forms.Button();
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.down = new System.Windows.Forms.Button();
            this.left = new System.Windows.Forms.Button();
            this.right = new System.Windows.Forms.Button();
            this.comPortComboBox = new System.Windows.Forms.ComboBox();
            this.ButtonZR = new System.Windows.Forms.Button();
            this.ButtonA = new System.Windows.Forms.Button();
            this.ButtonB = new System.Windows.Forms.Button();
            this.ButtonY = new System.Windows.Forms.Button();
            this.ButtonX = new System.Windows.Forms.Button();
            this.LeftN = new System.Windows.Forms.Button();
            this.LeftS = new System.Windows.Forms.Button();
            this.LeftE = new System.Windows.Forms.Button();
            this.LeftNE = new System.Windows.Forms.Button();
            this.LeftSE = new System.Windows.Forms.Button();
            this.LeftW = new System.Windows.Forms.Button();
            this.LeftSW = new System.Windows.Forms.Button();
            this.LeftNW = new System.Windows.Forms.Button();
            this.RightNW = new System.Windows.Forms.Button();
            this.RightSW = new System.Windows.Forms.Button();
            this.RightW = new System.Windows.Forms.Button();
            this.RightSE = new System.Windows.Forms.Button();
            this.RightNE = new System.Windows.Forms.Button();
            this.RightE = new System.Windows.Forms.Button();
            this.RightS = new System.Windows.Forms.Button();
            this.RightN = new System.Windows.Forms.Button();
            this.ButtonR = new System.Windows.Forms.Button();
            this.ButtonL = new System.Windows.Forms.Button();
            this.ButtonZL = new System.Windows.Forms.Button();
            this.ButtonMinus = new System.Windows.Forms.Button();
            this.ButtonPlus = new System.Windows.Forms.Button();
            this.ButtonHome = new System.Windows.Forms.Button();
            this.ButtonCapture = new System.Windows.Forms.Button();
            this.Button_plus3Days = new System.Windows.Forms.Button();
            this.Button_resetPlus4Days = new System.Windows.Forms.Button();
            this.Button_plus1Day = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.Button_plusNDays = new System.Windows.Forms.Button();
            this.Button_savePlus3days = new System.Windows.Forms.Button();
            this.Button_resetPlus5Days = new System.Windows.Forms.Button();
            this.Button_resetPlus3Days_2 = new System.Windows.Forms.Button();
            this.Button_dmax1 = new System.Windows.Forms.Button();
            this.Button_dmax2 = new System.Windows.Forms.Button();
            this.Button_stop = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.Button_dayReset = new System.Windows.Forms.Button();
            this.DateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label_count = new System.Windows.Forms.Label();
            this.Button_dateToday = new System.Windows.Forms.Button();
            this.Button_startRaid = new System.Windows.Forms.Button();
            this.Button_levelUp = new System.Windows.Forms.Button();
            this.Button_startRaidSelf = new System.Windows.Forms.Button();
            this.Button_resetPlus3Days = new System.Windows.Forms.Button();
            this.Button_repeatA = new System.Windows.Forms.Button();
            this.Button_displayStatus = new System.Windows.Forms.Button();
            this.Button_repeatB = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.DayTextbox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label_input = new System.Windows.Forms.Label();
            this.button_xInput = new System.Windows.Forms.RadioButton();
            this.button_directInput = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // up
            // 
            this.up.Location = new System.Drawing.Point(111, 368);
            this.up.Name = "up";
            this.up.Size = new System.Drawing.Size(59, 47);
            this.up.TabIndex = 11;
            this.up.TabStop = false;
            this.up.Text = "↑";
            this.up.UseVisualStyleBackColor = true;
            this.up.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Up_MouseDown);
            this.up.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Up_MouseUp);
            // 
            // serialPort
            // 
            this.serialPort.DtrEnable = true;
            this.serialPort.PortName = "COM7";
            this.serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort_DataReceived);
            // 
            // down
            // 
            this.down.Location = new System.Drawing.Point(111, 474);
            this.down.Name = "down";
            this.down.Size = new System.Drawing.Size(59, 47);
            this.down.TabIndex = 14;
            this.down.TabStop = false;
            this.down.Text = "↓";
            this.down.UseVisualStyleBackColor = true;
            this.down.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Down_MouseDown);
            this.down.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Down_MouseUp);
            // 
            // left
            // 
            this.left.Location = new System.Drawing.Point(46, 421);
            this.left.Name = "left";
            this.left.Size = new System.Drawing.Size(59, 47);
            this.left.TabIndex = 12;
            this.left.TabStop = false;
            this.left.Text = "←";
            this.left.UseVisualStyleBackColor = true;
            this.left.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Left_MouseDown);
            this.left.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Left_MouseUp);
            // 
            // right
            // 
            this.right.Location = new System.Drawing.Point(176, 421);
            this.right.Name = "right";
            this.right.Size = new System.Drawing.Size(59, 47);
            this.right.TabIndex = 13;
            this.right.TabStop = false;
            this.right.Text = "→";
            this.right.UseVisualStyleBackColor = true;
            this.right.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Right_MouseDown);
            this.right.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Right_MouseUp);
            // 
            // comPortComboBox
            // 
            this.comPortComboBox.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.comPortComboBox.FormattingEnabled = true;
            this.comPortComboBox.Location = new System.Drawing.Point(55, 22);
            this.comPortComboBox.Name = "comPortComboBox";
            this.comPortComboBox.Size = new System.Drawing.Size(75, 24);
            this.comPortComboBox.TabIndex = 5;
            this.comPortComboBox.SelectedIndexChanged += new System.EventHandler(this.comPortComboBox_SelectedIndexChanged);
            this.comPortComboBox.Click += new System.EventHandler(this.comPortComboBox_Click);
            this.comPortComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Keypressed);
            // 
            // ButtonZR
            // 
            this.ButtonZR.Location = new System.Drawing.Point(409, 77);
            this.ButtonZR.Name = "ButtonZR";
            this.ButtonZR.Size = new System.Drawing.Size(59, 37);
            this.ButtonZR.TabIndex = 21;
            this.ButtonZR.TabStop = false;
            this.ButtonZR.Text = "ZR";
            this.ButtonZR.UseVisualStyleBackColor = true;
            this.ButtonZR.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonZR_MouseDown);
            this.ButtonZR.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonZR_MouseUp);
            // 
            // ButtonA
            // 
            this.ButtonA.Location = new System.Drawing.Point(411, 245);
            this.ButtonA.Name = "ButtonA";
            this.ButtonA.Size = new System.Drawing.Size(59, 47);
            this.ButtonA.TabIndex = 26;
            this.ButtonA.TabStop = false;
            this.ButtonA.Text = "A";
            this.ButtonA.UseVisualStyleBackColor = true;
            this.ButtonA.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonA_MouseDown);
            this.ButtonA.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonA_MouseUp);
            // 
            // ButtonB
            // 
            this.ButtonB.Location = new System.Drawing.Point(346, 298);
            this.ButtonB.Name = "ButtonB";
            this.ButtonB.Size = new System.Drawing.Size(59, 47);
            this.ButtonB.TabIndex = 27;
            this.ButtonB.TabStop = false;
            this.ButtonB.Text = "B";
            this.ButtonB.UseVisualStyleBackColor = true;
            this.ButtonB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonB_MouseDown);
            this.ButtonB.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonB_MouseUp);
            // 
            // ButtonY
            // 
            this.ButtonY.Location = new System.Drawing.Point(281, 245);
            this.ButtonY.Name = "ButtonY";
            this.ButtonY.Size = new System.Drawing.Size(59, 47);
            this.ButtonY.TabIndex = 25;
            this.ButtonY.TabStop = false;
            this.ButtonY.Text = "Y";
            this.ButtonY.UseVisualStyleBackColor = true;
            this.ButtonY.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonY_MouseDown);
            this.ButtonY.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonY_MouseUp);
            // 
            // ButtonX
            // 
            this.ButtonX.Location = new System.Drawing.Point(346, 192);
            this.ButtonX.Name = "ButtonX";
            this.ButtonX.Size = new System.Drawing.Size(59, 47);
            this.ButtonX.TabIndex = 24;
            this.ButtonX.TabStop = false;
            this.ButtonX.Text = "X";
            this.ButtonX.UseVisualStyleBackColor = true;
            this.ButtonX.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonX_MouseDown);
            this.ButtonX.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonX_MouseUp);
            // 
            // LeftN
            // 
            this.LeftN.Location = new System.Drawing.Point(111, 192);
            this.LeftN.Name = "LeftN";
            this.LeftN.Size = new System.Drawing.Size(59, 47);
            this.LeftN.TabIndex = 5;
            this.LeftN.TabStop = false;
            this.LeftN.Text = "↑";
            this.LeftN.UseVisualStyleBackColor = true;
            this.LeftN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LeftN_MouseDown);
            this.LeftN.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LeftN_MouseUp);
            // 
            // LeftS
            // 
            this.LeftS.Location = new System.Drawing.Point(111, 298);
            this.LeftS.Name = "LeftS";
            this.LeftS.Size = new System.Drawing.Size(59, 47);
            this.LeftS.TabIndex = 10;
            this.LeftS.TabStop = false;
            this.LeftS.Text = "↓";
            this.LeftS.UseVisualStyleBackColor = true;
            this.LeftS.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LeftS_MouseDown);
            this.LeftS.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LeftS_MouseUp);
            // 
            // LeftE
            // 
            this.LeftE.Location = new System.Drawing.Point(176, 245);
            this.LeftE.Name = "LeftE";
            this.LeftE.Size = new System.Drawing.Size(59, 47);
            this.LeftE.TabIndex = 8;
            this.LeftE.TabStop = false;
            this.LeftE.Text = "→";
            this.LeftE.UseVisualStyleBackColor = true;
            this.LeftE.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LeftE_MouseDown);
            this.LeftE.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LeftE_MouseUp);
            // 
            // LeftNE
            // 
            this.LeftNE.Location = new System.Drawing.Point(176, 192);
            this.LeftNE.Name = "LeftNE";
            this.LeftNE.Size = new System.Drawing.Size(59, 47);
            this.LeftNE.TabIndex = 6;
            this.LeftNE.TabStop = false;
            this.LeftNE.Text = "↗";
            this.LeftNE.UseVisualStyleBackColor = true;
            this.LeftNE.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LeftNE_MouseDown);
            this.LeftNE.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LeftNE_MouseUp);
            // 
            // LeftSE
            // 
            this.LeftSE.Location = new System.Drawing.Point(176, 298);
            this.LeftSE.Name = "LeftSE";
            this.LeftSE.Size = new System.Drawing.Size(59, 47);
            this.LeftSE.TabIndex = 17;
            this.LeftSE.TabStop = false;
            this.LeftSE.Text = "↘";
            this.LeftSE.UseVisualStyleBackColor = true;
            this.LeftSE.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LeftSE_MouseDown);
            this.LeftSE.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LeftSE_MouseUp);
            // 
            // LeftW
            // 
            this.LeftW.Location = new System.Drawing.Point(46, 245);
            this.LeftW.Name = "LeftW";
            this.LeftW.Size = new System.Drawing.Size(59, 47);
            this.LeftW.TabIndex = 7;
            this.LeftW.TabStop = false;
            this.LeftW.Text = "←";
            this.LeftW.UseVisualStyleBackColor = true;
            this.LeftW.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LeftW_MouseDown);
            this.LeftW.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LeftW_MouseUp);
            // 
            // LeftSW
            // 
            this.LeftSW.Location = new System.Drawing.Point(46, 298);
            this.LeftSW.Name = "LeftSW";
            this.LeftSW.Size = new System.Drawing.Size(59, 47);
            this.LeftSW.TabIndex = 9;
            this.LeftSW.TabStop = false;
            this.LeftSW.Text = "↙";
            this.LeftSW.UseVisualStyleBackColor = true;
            this.LeftSW.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LeftSW_MouseDown);
            this.LeftSW.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LeftSW_MouseUp);
            // 
            // LeftNW
            // 
            this.LeftNW.Location = new System.Drawing.Point(46, 192);
            this.LeftNW.Name = "LeftNW";
            this.LeftNW.Size = new System.Drawing.Size(59, 47);
            this.LeftNW.TabIndex = 4;
            this.LeftNW.TabStop = false;
            this.LeftNW.Text = "↖";
            this.LeftNW.UseVisualStyleBackColor = true;
            this.LeftNW.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LeftNW_MouseDown);
            this.LeftNW.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LeftNW_MouseUp);
            // 
            // RightNW
            // 
            this.RightNW.Location = new System.Drawing.Point(281, 368);
            this.RightNW.Name = "RightNW";
            this.RightNW.Size = new System.Drawing.Size(59, 47);
            this.RightNW.TabIndex = 28;
            this.RightNW.TabStop = false;
            this.RightNW.Text = "↖";
            this.RightNW.UseVisualStyleBackColor = true;
            this.RightNW.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RightNW_MouseDown);
            this.RightNW.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightNW_MouseUp);
            // 
            // RightSW
            // 
            this.RightSW.Location = new System.Drawing.Point(281, 474);
            this.RightSW.Name = "RightSW";
            this.RightSW.Size = new System.Drawing.Size(59, 47);
            this.RightSW.TabIndex = 33;
            this.RightSW.TabStop = false;
            this.RightSW.Text = "↙";
            this.RightSW.UseVisualStyleBackColor = true;
            this.RightSW.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RightSW_MouseDown);
            this.RightSW.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightSW_MouseUp);
            // 
            // RightW
            // 
            this.RightW.Location = new System.Drawing.Point(281, 421);
            this.RightW.Name = "RightW";
            this.RightW.Size = new System.Drawing.Size(59, 47);
            this.RightW.TabIndex = 31;
            this.RightW.TabStop = false;
            this.RightW.Text = "←";
            this.RightW.UseVisualStyleBackColor = true;
            this.RightW.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RightW_MouseDown);
            this.RightW.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightW_MouseUp);
            // 
            // RightSE
            // 
            this.RightSE.Location = new System.Drawing.Point(411, 474);
            this.RightSE.Name = "RightSE";
            this.RightSE.Size = new System.Drawing.Size(59, 47);
            this.RightSE.TabIndex = 35;
            this.RightSE.TabStop = false;
            this.RightSE.Text = "↘";
            this.RightSE.UseVisualStyleBackColor = true;
            this.RightSE.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RightSE_MouseDown);
            this.RightSE.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightSE_MouseUp);
            // 
            // RightNE
            // 
            this.RightNE.Location = new System.Drawing.Point(411, 368);
            this.RightNE.Name = "RightNE";
            this.RightNE.Size = new System.Drawing.Size(59, 47);
            this.RightNE.TabIndex = 30;
            this.RightNE.TabStop = false;
            this.RightNE.Text = "↗";
            this.RightNE.UseVisualStyleBackColor = true;
            this.RightNE.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RightNE_MouseDown);
            this.RightNE.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightNE_MouseUp);
            // 
            // RightE
            // 
            this.RightE.Location = new System.Drawing.Point(411, 421);
            this.RightE.Name = "RightE";
            this.RightE.Size = new System.Drawing.Size(59, 47);
            this.RightE.TabIndex = 32;
            this.RightE.TabStop = false;
            this.RightE.Text = "→";
            this.RightE.UseVisualStyleBackColor = true;
            this.RightE.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RightE_MouseDown);
            this.RightE.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightE_MouseUp);
            // 
            // RightS
            // 
            this.RightS.Location = new System.Drawing.Point(346, 474);
            this.RightS.Name = "RightS";
            this.RightS.Size = new System.Drawing.Size(59, 47);
            this.RightS.TabIndex = 34;
            this.RightS.TabStop = false;
            this.RightS.Text = "↓";
            this.RightS.UseVisualStyleBackColor = true;
            this.RightS.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RightS_MouseDown);
            this.RightS.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightS_MouseUp);
            // 
            // RightN
            // 
            this.RightN.Location = new System.Drawing.Point(346, 368);
            this.RightN.Name = "RightN";
            this.RightN.Size = new System.Drawing.Size(59, 47);
            this.RightN.TabIndex = 29;
            this.RightN.TabStop = false;
            this.RightN.Text = "↑";
            this.RightN.UseVisualStyleBackColor = true;
            this.RightN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RightN_MouseDown);
            this.RightN.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightN_MouseUp);
            // 
            // ButtonR
            // 
            this.ButtonR.Location = new System.Drawing.Point(409, 129);
            this.ButtonR.Name = "ButtonR";
            this.ButtonR.Size = new System.Drawing.Size(59, 37);
            this.ButtonR.TabIndex = 23;
            this.ButtonR.TabStop = false;
            this.ButtonR.Text = "R";
            this.ButtonR.UseVisualStyleBackColor = true;
            this.ButtonR.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonR_MouseDown);
            this.ButtonR.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonR_MouseUp);
            // 
            // ButtonL
            // 
            this.ButtonL.Location = new System.Drawing.Point(46, 129);
            this.ButtonL.Name = "ButtonL";
            this.ButtonL.Size = new System.Drawing.Size(59, 37);
            this.ButtonL.TabIndex = 2;
            this.ButtonL.TabStop = false;
            this.ButtonL.Text = "L";
            this.ButtonL.UseVisualStyleBackColor = true;
            this.ButtonL.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonL_MouseDown);
            this.ButtonL.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonL_MouseUp);
            // 
            // ButtonZL
            // 
            this.ButtonZL.Location = new System.Drawing.Point(46, 77);
            this.ButtonZL.Name = "ButtonZL";
            this.ButtonZL.Size = new System.Drawing.Size(59, 37);
            this.ButtonZL.TabIndex = 1;
            this.ButtonZL.TabStop = false;
            this.ButtonZL.Text = "ZL";
            this.ButtonZL.UseVisualStyleBackColor = true;
            this.ButtonZL.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonZL_MouseDown);
            this.ButtonZL.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonZL_MouseUp);
            // 
            // ButtonMinus
            // 
            this.ButtonMinus.Location = new System.Drawing.Point(176, 129);
            this.ButtonMinus.Name = "ButtonMinus";
            this.ButtonMinus.Size = new System.Drawing.Size(59, 37);
            this.ButtonMinus.TabIndex = 3;
            this.ButtonMinus.TabStop = false;
            this.ButtonMinus.Text = "-";
            this.ButtonMinus.UseVisualStyleBackColor = true;
            this.ButtonMinus.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonMinus_MouseDown);
            this.ButtonMinus.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMinus_MouseUp);
            // 
            // ButtonPlus
            // 
            this.ButtonPlus.Location = new System.Drawing.Point(281, 129);
            this.ButtonPlus.Name = "ButtonPlus";
            this.ButtonPlus.Size = new System.Drawing.Size(59, 37);
            this.ButtonPlus.TabIndex = 22;
            this.ButtonPlus.TabStop = false;
            this.ButtonPlus.Text = "+";
            this.ButtonPlus.UseVisualStyleBackColor = true;
            this.ButtonPlus.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonPlus_MouseDown);
            this.ButtonPlus.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonPlus_MouseUp);
            // 
            // ButtonHome
            // 
            this.ButtonHome.Location = new System.Drawing.Point(281, 538);
            this.ButtonHome.Name = "ButtonHome";
            this.ButtonHome.Size = new System.Drawing.Size(59, 36);
            this.ButtonHome.TabIndex = 36;
            this.ButtonHome.TabStop = false;
            this.ButtonHome.Text = "Home";
            this.ButtonHome.UseVisualStyleBackColor = true;
            this.ButtonHome.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonHome_MouseDown);
            this.ButtonHome.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonHome_MouseUp);
            // 
            // ButtonCapture
            // 
            this.ButtonCapture.Location = new System.Drawing.Point(176, 537);
            this.ButtonCapture.Name = "ButtonCapture";
            this.ButtonCapture.Size = new System.Drawing.Size(59, 37);
            this.ButtonCapture.TabIndex = 15;
            this.ButtonCapture.TabStop = false;
            this.ButtonCapture.Text = "Capture";
            this.ButtonCapture.UseVisualStyleBackColor = true;
            this.ButtonCapture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonCapture_MouseDown);
            this.ButtonCapture.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonCapture_MouseUp);
            // 
            // Button_plus3Days
            // 
            this.Button_plus3Days.Location = new System.Drawing.Point(1046, 135);
            this.Button_plus3Days.Name = "Button_plus3Days";
            this.Button_plus3Days.Size = new System.Drawing.Size(135, 35);
            this.Button_plus3Days.TabIndex = 54;
            this.Button_plus3Days.TabStop = false;
            this.Button_plus3Days.Text = "+3 Days";
            this.Button_plus3Days.UseVisualStyleBackColor = true;
            this.Button_plus3Days.Click += new System.EventHandler(this.Button_plus3Days_Click);
            // 
            // Button_resetPlus4Days
            // 
            this.Button_resetPlus4Days.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.Button_resetPlus4Days.Location = new System.Drawing.Point(45, 287);
            this.Button_resetPlus4Days.Name = "Button_resetPlus4Days";
            this.Button_resetPlus4Days.Size = new System.Drawing.Size(184, 36);
            this.Button_resetPlus4Days.TabIndex = 64;
            this.Button_resetPlus4Days.TabStop = false;
            this.Button_resetPlus4Days.Text = "5日目の個体\r\n（リセットして +4 Days）";
            this.Button_resetPlus4Days.UseVisualStyleBackColor = true;
            this.Button_resetPlus4Days.Click += new System.EventHandler(this.Button_resetPlus4Days_Click);
            // 
            // Button_plus1Day
            // 
            this.Button_plus1Day.Location = new System.Drawing.Point(1046, 92);
            this.Button_plus1Day.Name = "Button_plus1Day";
            this.Button_plus1Day.Size = new System.Drawing.Size(135, 35);
            this.Button_plus1Day.TabIndex = 53;
            this.Button_plus1Day.TabStop = false;
            this.Button_plus1Day.Text = "+1 Day";
            this.Button_plus1Day.UseVisualStyleBackColor = true;
            this.Button_plus1Day.Click += new System.EventHandler(this.Button_plus1Day_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 12);
            this.label4.TabIndex = 53;
            this.label4.Text = "Port";
            // 
            // Button_plusNDays
            // 
            this.Button_plusNDays.Location = new System.Drawing.Point(948, 93);
            this.Button_plusNDays.Name = "Button_plusNDays";
            this.Button_plusNDays.Size = new System.Drawing.Size(70, 21);
            this.Button_plusNDays.TabIndex = 52;
            this.Button_plusNDays.TabStop = false;
            this.Button_plusNDays.Text = "日進める";
            this.Button_plusNDays.UseVisualStyleBackColor = true;
            this.Button_plusNDays.Click += new System.EventHandler(this.Button_plusNDays_Click);
            // 
            // Button_savePlus3days
            // 
            this.Button_savePlus3days.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Button_savePlus3days.ForeColor = System.Drawing.Color.Black;
            this.Button_savePlus3days.Location = new System.Drawing.Point(556, 277);
            this.Button_savePlus3days.Name = "Button_savePlus3days";
            this.Button_savePlus3days.Size = new System.Drawing.Size(184, 36);
            this.Button_savePlus3days.TabIndex = 61;
            this.Button_savePlus3days.TabStop = false;
            this.Button_savePlus3days.Text = "レポート後次の4日目へ";
            this.Button_savePlus3days.UseVisualStyleBackColor = true;
            this.Button_savePlus3days.Click += new System.EventHandler(this.Button_chanselAndSave_Click);
            this.Button_savePlus3days.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Keypressed);
            // 
            // Button_resetPlus5Days
            // 
            this.Button_resetPlus5Days.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.Button_resetPlus5Days.Location = new System.Drawing.Point(45, 329);
            this.Button_resetPlus5Days.Name = "Button_resetPlus5Days";
            this.Button_resetPlus5Days.Size = new System.Drawing.Size(184, 36);
            this.Button_resetPlus5Days.TabIndex = 65;
            this.Button_resetPlus5Days.TabStop = false;
            this.Button_resetPlus5Days.Text = "6日目の個体\r\n（リセットして +5 Days）";
            this.Button_resetPlus5Days.UseVisualStyleBackColor = true;
            this.Button_resetPlus5Days.Click += new System.EventHandler(this.Button_resetPlus5Days_Click);
            // 
            // Button_resetPlus3Days_2
            // 
            this.Button_resetPlus3Days_2.Location = new System.Drawing.Point(556, 337);
            this.Button_resetPlus3Days_2.Name = "Button_resetPlus3Days_2";
            this.Button_resetPlus3Days_2.Size = new System.Drawing.Size(184, 36);
            this.Button_resetPlus3Days_2.TabIndex = 62;
            this.Button_resetPlus3Days_2.TabStop = false;
            this.Button_resetPlus3Days_2.Text = "リセットし1日ずらした後の4日目へ\r\n";
            this.Button_resetPlus3Days_2.UseVisualStyleBackColor = true;
            this.Button_resetPlus3Days_2.Click += new System.EventHandler(this.Button_resetSlidPlus3Days_Click);
            // 
            // Button_dmax1
            // 
            this.Button_dmax1.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.Button_dmax1.Location = new System.Drawing.Point(184, 24);
            this.Button_dmax1.Name = "Button_dmax1";
            this.Button_dmax1.Size = new System.Drawing.Size(191, 54);
            this.Button_dmax1.TabIndex = 72;
            this.Button_dmax1.TabStop = false;
            this.Button_dmax1.Text = "ダイマックスして\r\n1番目の技使用";
            this.Button_dmax1.UseVisualStyleBackColor = true;
            this.Button_dmax1.Click += new System.EventHandler(this.Button_dmax1_Click);
            // 
            // Button_dmax2
            // 
            this.Button_dmax2.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.Button_dmax2.Location = new System.Drawing.Point(184, 81);
            this.Button_dmax2.Name = "Button_dmax2";
            this.Button_dmax2.Size = new System.Drawing.Size(191, 54);
            this.Button_dmax2.TabIndex = 73;
            this.Button_dmax2.TabStop = false;
            this.Button_dmax2.Text = "ダイマックスして\r\n2番目の技使用";
            this.Button_dmax2.UseVisualStyleBackColor = true;
            this.Button_dmax2.Click += new System.EventHandler(this.Button_dmax2_Click);
            // 
            // Button_stop
            // 
            this.Button_stop.BackColor = System.Drawing.Color.Gold;
            this.Button_stop.Location = new System.Drawing.Point(806, 503);
            this.Button_stop.Margin = new System.Windows.Forms.Padding(0);
            this.Button_stop.Name = "Button_stop";
            this.Button_stop.Size = new System.Drawing.Size(353, 84);
            this.Button_stop.TabIndex = 0;
            this.Button_stop.Text = "マクロ停止";
            this.Button_stop.UseVisualStyleBackColor = false;
            this.Button_stop.Click += new System.EventHandler(this.Button_stop_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.Location = new System.Drawing.Point(861, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(135, 13);
            this.label6.TabIndex = 64;
            this.label6.Text = "高速消費（ランクマ後）";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pictureBox1.Location = new System.Drawing.Point(25, 61);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(229, 526);
            this.pictureBox1.TabIndex = 65;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pictureBox2.Location = new System.Drawing.Point(260, 61);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(229, 526);
            this.pictureBox2.TabIndex = 66;
            this.pictureBox2.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.Location = new System.Drawing.Point(1043, 57);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(138, 13);
            this.label7.TabIndex = 67;
            this.label7.Text = "日付調整（レイド使用）";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.SystemColors.Control;
            this.label9.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label9.Location = new System.Drawing.Point(521, 397);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(113, 13);
            this.label9.TabIndex = 69;
            this.label9.Text = "4日目（2体目）以降";
            // 
            // Button_dayReset
            // 
            this.Button_dayReset.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.Button_dayReset.Location = new System.Drawing.Point(165, 76);
            this.Button_dayReset.Name = "Button_dayReset";
            this.Button_dayReset.Size = new System.Drawing.Size(134, 37);
            this.Button_dayReset.TabIndex = 44;
            this.Button_dayReset.TabStop = false;
            this.Button_dayReset.Text = "本体日付変更（初期値）";
            this.Button_dayReset.UseVisualStyleBackColor = true;
            this.Button_dayReset.Click += new System.EventHandler(this.Button_dayReset_Click);
            // 
            // DateTimePicker1
            // 
            this.DateTimePicker1.CalendarFont = new System.Drawing.Font("MS UI Gothic", 10F);
            this.DateTimePicker1.CausesValidation = false;
            this.DateTimePicker1.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.DateTimePicker1.Location = new System.Drawing.Point(533, 92);
            this.DateTimePicker1.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.DateTimePicker1.MinimumSize = new System.Drawing.Size(4, 35);
            this.DateTimePicker1.Name = "DateTimePicker1";
            this.DateTimePicker1.Size = new System.Drawing.Size(137, 35);
            this.DateTimePicker1.TabIndex = 41;
            this.DateTimePicker1.TabStop = false;
            this.DateTimePicker1.ValueChanged += new System.EventHandler(this.DateTimePicker1_ValueChanged);
            this.DateTimePicker1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Keypressed);
            // 
            // label_count
            // 
            this.label_count.AutoSize = true;
            this.label_count.Location = new System.Drawing.Point(862, 129);
            this.label_count.Name = "label_count";
            this.label_count.Size = new System.Drawing.Size(81, 12);
            this.label_count.TabIndex = 73;
            this.label_count.Text = "変更回数： 0/0";
            this.label_count.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Button_dateToday
            // 
            this.Button_dateToday.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.Button_dateToday.Location = new System.Drawing.Point(165, 35);
            this.Button_dateToday.Name = "Button_dateToday";
            this.Button_dateToday.Size = new System.Drawing.Size(134, 35);
            this.Button_dateToday.TabIndex = 42;
            this.Button_dateToday.TabStop = false;
            this.Button_dateToday.Text = "本体日付変更（今日）";
            this.Button_dateToday.UseVisualStyleBackColor = true;
            this.Button_dateToday.Click += new System.EventHandler(this.Button_machineDateToday_click);
            // 
            // Button_startRaid
            // 
            this.Button_startRaid.BackColor = System.Drawing.SystemColors.Control;
            this.Button_startRaid.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.Button_startRaid.Location = new System.Drawing.Point(104, 24);
            this.Button_startRaid.Name = "Button_startRaid";
            this.Button_startRaid.Size = new System.Drawing.Size(74, 114);
            this.Button_startRaid.TabIndex = 71;
            this.Button_startRaid.TabStop = false;
            this.Button_startRaid.Text = "レイド開始\r\n（みんなで）";
            this.Button_startRaid.UseVisualStyleBackColor = true;
            this.Button_startRaid.Click += new System.EventHandler(this.Button_startRaid_Click);
            // 
            // Button_levelUp
            // 
            this.Button_levelUp.Location = new System.Drawing.Point(806, 387);
            this.Button_levelUp.Name = "Button_levelUp";
            this.Button_levelUp.Size = new System.Drawing.Size(156, 44);
            this.Button_levelUp.TabIndex = 74;
            this.Button_levelUp.TabStop = false;
            this.Button_levelUp.Text = "レベル100にする";
            this.Button_levelUp.UseVisualStyleBackColor = true;
            this.Button_levelUp.Click += new System.EventHandler(this.Button_levelUp_Click);
            // 
            // Button_startRaidSelf
            // 
            this.Button_startRaidSelf.BackColor = System.Drawing.SystemColors.Control;
            this.Button_startRaidSelf.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.Button_startRaidSelf.Location = new System.Drawing.Point(22, 24);
            this.Button_startRaidSelf.Name = "Button_startRaidSelf";
            this.Button_startRaidSelf.Size = new System.Drawing.Size(76, 114);
            this.Button_startRaidSelf.TabIndex = 66;
            this.Button_startRaidSelf.TabStop = false;
            this.Button_startRaidSelf.Text = "レイド開始\r\n（ひとりで）";
            this.Button_startRaidSelf.UseVisualStyleBackColor = true;
            this.Button_startRaidSelf.Click += new System.EventHandler(this.Button_startRaidSelf_Click);
            // 
            // Button_resetPlus3Days
            // 
            this.Button_resetPlus3Days.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.Button_resetPlus3Days.Location = new System.Drawing.Point(45, 212);
            this.Button_resetPlus3Days.Name = "Button_resetPlus3Days";
            this.Button_resetPlus3Days.Size = new System.Drawing.Size(184, 69);
            this.Button_resetPlus3Days.TabIndex = 63;
            this.Button_resetPlus3Days.TabStop = false;
            this.Button_resetPlus3Days.Text = "4日目（2体目）の個体\r\n（リセットして+3 days）";
            this.Button_resetPlus3Days.UseVisualStyleBackColor = true;
            this.Button_resetPlus3Days.Click += new System.EventHandler(this.Button_resetPlus3Days_Click);
            // 
            // Button_repeatA
            // 
            this.Button_repeatA.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Button_repeatA.Location = new System.Drawing.Point(980, 387);
            this.Button_repeatA.Name = "Button_repeatA";
            this.Button_repeatA.Size = new System.Drawing.Size(179, 44);
            this.Button_repeatA.TabIndex = 76;
            this.Button_repeatA.TabStop = false;
            this.Button_repeatA.Text = "Aボタン\r\n連打";
            this.Button_repeatA.UseVisualStyleBackColor = false;
            this.Button_repeatA.Click += new System.EventHandler(this.Button_repeatA_Click);
            // 
            // Button_displayStatus
            // 
            this.Button_displayStatus.Location = new System.Drawing.Point(806, 437);
            this.Button_displayStatus.Name = "Button_displayStatus";
            this.Button_displayStatus.Size = new System.Drawing.Size(156, 44);
            this.Button_displayStatus.TabIndex = 75;
            this.Button_displayStatus.TabStop = false;
            this.Button_displayStatus.Text = "ステータス表示（2体目）";
            this.Button_displayStatus.UseVisualStyleBackColor = true;
            this.Button_displayStatus.Click += new System.EventHandler(this.Button_displayStatus_Click);
            // 
            // Button_repeatB
            // 
            this.Button_repeatB.BackColor = System.Drawing.Color.DarkSalmon;
            this.Button_repeatB.Location = new System.Drawing.Point(980, 437);
            this.Button_repeatB.Name = "Button_repeatB";
            this.Button_repeatB.Size = new System.Drawing.Size(179, 44);
            this.Button_repeatB.TabIndex = 77;
            this.Button_repeatB.TabStop = false;
            this.Button_repeatB.Text = "Bボタン\r\n連打";
            this.Button_repeatB.UseVisualStyleBackColor = false;
            this.Button_repeatB.Click += new System.EventHandler(this.Button_repeatB_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(521, 234);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 87;
            this.label3.Text = "4日目（1体目）";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(37, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 13);
            this.label2.TabIndex = 88;
            this.label2.Text = "目的のポケモンでない場合";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.SystemColors.Control;
            this.label8.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.Location = new System.Drawing.Point(548, 321);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(142, 13);
            this.label8.TabIndex = 89;
            this.label8.Text = "目的の個体値でない場合";
            // 
            // DayTextbox
            // 
            this.DayTextbox.CausesValidation = false;
            this.DayTextbox.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.DayTextbox.Location = new System.Drawing.Point(860, 93);
            this.DayTextbox.Name = "DayTextbox";
            this.DayTextbox.Size = new System.Drawing.Size(86, 21);
            this.DayTextbox.TabIndex = 51;
            this.DayTextbox.TabStop = false;
            this.DayTextbox.Text = "0";
            this.DayTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.DayTextbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Keypressed);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Button_dateToday);
            this.groupBox1.Controls.Add(this.Button_dayReset);
            this.groupBox1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(511, 57);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(324, 123);
            this.groupBox1.TabIndex = 90;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "日付設定";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Button_resetPlus4Days);
            this.groupBox2.Controls.Add(this.Button_resetPlus5Days);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.Button_resetPlus3Days);
            this.groupBox2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBox2.Location = new System.Drawing.Point(511, 210);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(244, 377);
            this.groupBox2.TabIndex = 91;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "seed特定（レイド使用）";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Button_startRaid);
            this.groupBox3.Controls.Add(this.Button_dmax1);
            this.groupBox3.Controls.Add(this.Button_dmax2);
            this.groupBox3.Controls.Add(this.Button_startRaidSelf);
            this.groupBox3.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBox3.Location = new System.Drawing.Point(784, 210);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(397, 154);
            this.groupBox3.TabIndex = 91;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "レイドバトル";
            // 
            // label_input
            // 
            this.label_input.AutoSize = true;
            this.label_input.Location = new System.Drawing.Point(388, 26);
            this.label_input.Name = "label_input";
            this.label_input.Size = new System.Drawing.Size(38, 12);
            this.label_input.TabIndex = 82;
            this.label_input.Text = "INPUT";
            // 
            // button_xInput
            // 
            this.button_xInput.AutoSize = true;
            this.button_xInput.Checked = true;
            this.button_xInput.Location = new System.Drawing.Point(176, 24);
            this.button_xInput.Name = "button_xInput";
            this.button_xInput.Size = new System.Drawing.Size(54, 16);
            this.button_xInput.TabIndex = 92;
            this.button_xInput.TabStop = true;
            this.button_xInput.Text = "xInput";
            this.button_xInput.UseVisualStyleBackColor = true;
            this.button_xInput.CheckedChanged += new System.EventHandler(this.Button_ContlrolChange);
            // 
            // button_directInput
            // 
            this.button_directInput.AutoSize = true;
            this.button_directInput.Location = new System.Drawing.Point(246, 25);
            this.button_directInput.Name = "button_directInput";
            this.button_directInput.Size = new System.Drawing.Size(77, 16);
            this.button_directInput.TabIndex = 93;
            this.button_directInput.Text = "directInput";
            this.button_directInput.UseVisualStyleBackColor = true;
            this.button_directInput.CheckedChanged += new System.EventHandler(this.Button_ContlrolChange);
            // 
            // PokemonSWSH_SeedSearchSupport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1206, 611);
            this.Controls.Add(this.button_directInput);
            this.Controls.Add(this.button_xInput);
            this.Controls.Add(this.DayTextbox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label_input);
            this.Controls.Add(this.Button_repeatB);
            this.Controls.Add(this.Button_displayStatus);
            this.Controls.Add(this.Button_repeatA);
            this.Controls.Add(this.Button_levelUp);
            this.Controls.Add(this.label_count);
            this.Controls.Add(this.DateTimePicker1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Button_stop);
            this.Controls.Add(this.Button_resetPlus3Days_2);
            this.Controls.Add(this.Button_savePlus3days);
            this.Controls.Add(this.Button_plusNDays);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Button_plus1Day);
            this.Controls.Add(this.Button_plus3Days);
            this.Controls.Add(this.ButtonCapture);
            this.Controls.Add(this.ButtonHome);
            this.Controls.Add(this.ButtonPlus);
            this.Controls.Add(this.ButtonMinus);
            this.Controls.Add(this.ButtonZL);
            this.Controls.Add(this.ButtonL);
            this.Controls.Add(this.ButtonR);
            this.Controls.Add(this.RightNW);
            this.Controls.Add(this.RightSW);
            this.Controls.Add(this.RightW);
            this.Controls.Add(this.RightSE);
            this.Controls.Add(this.RightNE);
            this.Controls.Add(this.RightE);
            this.Controls.Add(this.RightS);
            this.Controls.Add(this.RightN);
            this.Controls.Add(this.LeftNW);
            this.Controls.Add(this.LeftSW);
            this.Controls.Add(this.LeftW);
            this.Controls.Add(this.LeftSE);
            this.Controls.Add(this.LeftNE);
            this.Controls.Add(this.LeftE);
            this.Controls.Add(this.LeftS);
            this.Controls.Add(this.LeftN);
            this.Controls.Add(this.ButtonX);
            this.Controls.Add(this.ButtonY);
            this.Controls.Add(this.ButtonB);
            this.Controls.Add(this.ButtonA);
            this.Controls.Add(this.ButtonZR);
            this.Controls.Add(this.comPortComboBox);
            this.Controls.Add(this.right);
            this.Controls.Add(this.left);
            this.Controls.Add(this.down);
            this.Controls.Add(this.up);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "PokemonSWSH_SeedSearchSupport";
            this.Text = "PokemonSWSH_SeedSearchSupport";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button up;
        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.Button down;
        private System.Windows.Forms.Button left;
        private System.Windows.Forms.Button right;
        private System.Windows.Forms.ComboBox comPortComboBox;
        private System.Windows.Forms.Button ButtonZR;
        private System.Windows.Forms.Button ButtonA;
        private System.Windows.Forms.Button ButtonB;
        private System.Windows.Forms.Button ButtonY;
        private System.Windows.Forms.Button ButtonX;
        private System.Windows.Forms.Button LeftN;
        private System.Windows.Forms.Button LeftS;
        private System.Windows.Forms.Button LeftE;
        private System.Windows.Forms.Button LeftNE;
        private System.Windows.Forms.Button LeftSE;
        private System.Windows.Forms.Button LeftW;
        private System.Windows.Forms.Button LeftSW;
        private System.Windows.Forms.Button LeftNW;
        private System.Windows.Forms.Button RightNW;
        private System.Windows.Forms.Button RightSW;
        private System.Windows.Forms.Button RightW;
        private System.Windows.Forms.Button RightSE;
        private System.Windows.Forms.Button RightNE;
        private System.Windows.Forms.Button RightE;
        private System.Windows.Forms.Button RightS;
        private System.Windows.Forms.Button RightN;
        private System.Windows.Forms.Button ButtonR;
        private System.Windows.Forms.Button ButtonL;
        private System.Windows.Forms.Button ButtonZL;
        private System.Windows.Forms.Button ButtonMinus;
        private System.Windows.Forms.Button ButtonPlus;
        private System.Windows.Forms.Button ButtonHome;
        private System.Windows.Forms.Button ButtonCapture;
        private System.Windows.Forms.Button Button_plus3Days;
        private System.Windows.Forms.Button Button_resetPlus4Days;
        private System.Windows.Forms.Button Button_plus1Day;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Button_plusNDays;
        private System.Windows.Forms.Button Button_savePlus3days;
        private System.Windows.Forms.Button Button_resetPlus5Days;
        private System.Windows.Forms.Button Button_resetPlus3Days_2;
        private System.Windows.Forms.Button Button_dmax1;
        private System.Windows.Forms.Button Button_dmax2;
        private System.Windows.Forms.Button Button_stop;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button Button_dayReset;
        private System.Windows.Forms.DateTimePicker DateTimePicker1;
        private System.Windows.Forms.Label label_count;
        private System.Windows.Forms.Button Button_dateToday;
        private System.Windows.Forms.Button Button_startRaid;
        private System.Windows.Forms.Button Button_levelUp;
        private System.Windows.Forms.Button Button_startRaidSelf;
        private System.Windows.Forms.Button Button_resetPlus3Days;
        private System.Windows.Forms.Button Button_repeatA;
        private System.Windows.Forms.Button Button_displayStatus;
        private System.Windows.Forms.Button Button_repeatB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox DayTextbox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label_input;
        private System.Windows.Forms.RadioButton button_xInput;
        private System.Windows.Forms.RadioButton button_directInput;
    }
}

