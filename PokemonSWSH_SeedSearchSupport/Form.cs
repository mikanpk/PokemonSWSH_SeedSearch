﻿using System;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using System.Threading.Tasks;
using SharpDX.DirectInput;

namespace PokemonSWSH_SeedSearchSupport
{
    public partial class PokemonSWSH_SeedSearchSupport : Form
    {
        ///  <summary>
        /// マクロ停止用トークン
        /// </summary>
        private CancellationTokenSource token_source;
        private CancellationToken cancel_token;
        ///  <summary>
        /// LOOP停止用トークン
        /// </summary>
        private bool unUsedInput = false;

        /// <summary>
        /// 日付情報
        /// </summary>
        private DateTime current_date;

        /// <summary>
        /// 入力周りの初期化
        ///  </summary>
        private Joystick _joy;
        DirectInput directInput;
        DirectInput directInputKey;
        Keyboard keyboard;
        ButtonType[] array = new ButtonType[15];

        /// 
        /// DirectXデバイスの初期化
        /// 
        public void InputInitialize()
        {
            directInput = new DirectInput();
            directInputKey = new DirectInput();

            if (directInput != null)
            {
                // 使用するゲームパッドのID
                var joystickGuid = Guid.Empty;
                // ゲームパッドからゲームパッドを取得する
                if (joystickGuid == Guid.Empty)
                {
                    foreach (DeviceInstance device in directInput.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices))
                    {
                        joystickGuid = device.InstanceGuid;
                        break;
                    }
                }
                // ジョイスティックからゲームパッドを取得する
                if (joystickGuid == Guid.Empty)
                {
                    foreach (DeviceInstance device in directInput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AllDevices))
                    {
                        joystickGuid = device.InstanceGuid;
                        break;
                    }
                }
                // 見つかった場合
                if (joystickGuid != Guid.Empty)
                {
                    // パッド入力周りの初期化
                    _joy = new Joystick(directInput, joystickGuid);
                    if (_joy != null)
                    {
                        // バッファサイズを指定
                        _joy.Properties.BufferSize = 128;

                        // 相対軸・絶対軸の最小値と最大値を
                        // 指定した値の範囲に設定する
                        foreach (DeviceObjectInstance deviceObject in _joy.GetObjects())
                        {
                            switch (deviceObject.ObjectId.Flags)
                            {
                                case DeviceObjectTypeFlags.Axis:
                                // 絶対軸or相対軸
                                case DeviceObjectTypeFlags.AbsoluteAxis:
                                // 絶対軸
                                case DeviceObjectTypeFlags.RelativeAxis:
                                    // 相対軸
                                    var ir = _joy.GetObjectPropertiesById(deviceObject.ObjectId);
                                    if (ir != null)
                                    {
                                        try
                                        {
                                            ir.Range = new InputRange(-1000, 1000);
                                        }
                                        catch (Exception) { }
                                    }
                                    break;
                            }
                        }
                    }
                }
            }

            if (directInputKey != null)
            {
                //接続されているキーボードを探す
                Guid keyboardGuid = Guid.Empty;
                foreach (var g in directInputKey.GetDevices(DeviceType.Keyboard, DeviceEnumerationFlags.AllDevices))
                {
                    keyboardGuid = g.InstanceGuid;
                    break;
                }

                //キーボードが見つからない時は終了する
                if (keyboardGuid == Guid.Empty)
                {
                    Console.WriteLine("キーボードが見つかりません。終了します。");
                    System.Threading.Thread.Sleep(5000);
                }

                //デバイスを初期化
                keyboard = new Keyboard(directInputKey);
            }
        }

        /// 
        /// 日付出力の更新時
        /// 
        delegate void delegateUpdateDateInput(ButtonType s, bool mode);
        private void UpdateDateInput(ButtonType s, bool mode)
        {
            if (mode)
            {
                PressButton(s);
                label_input.Text = s.ToString();
            }
            else
            {
                ReleaseButton(s);
            }
        }

        /// 
        /// デバイス入力の更新時
        /// 
        delegate void delegateUpdateInput(String s);
        private void UpdateInput(String s)
        {
            label_input.Text = s.ToString();
        }

        /// 
        /// デバイス入力受付
        /// 
        private async void Loop()
        {
            try
            {

                // 初期化が出来ていない場合、処理終了
                if (_joy == null) { return; }

                // キャプチャするデバイスを取得
                _joy.Acquire();
                _joy.Poll();

                // ゲームパッドのデータ取得
                var jState = _joy.GetCurrentState();
                var jState_before = jState;

                //キー入力の検知開始
                keyboard.Acquire();

                while (true)
                {

                    while (unUsedInput) ;
                    jState = _joy.GetCurrentState();
                    KeyboardState state = keyboard.GetCurrentState();

                    // 取得できない場合、処理終了
                    if (jState == null) { return; }

                    //ZR.ZL
                    if (jState.Z != jState_before.Z)
                    {
                        if (jState.Z > 500)
                        {
                            Invoke(new delegateUpdateDateInput(UpdateDateInput), ButtonType.ZL, true);
                        }
                        else if (jState.Z < -500)
                        {
                            Invoke(new delegateUpdateDateInput(UpdateDateInput), ButtonType.ZR, true);
                        }
                        else
                        {
                            Invoke(new delegateUpdateDateInput(UpdateDateInput), ButtonType.ZL, false);
                            Invoke(new delegateUpdateDateInput(UpdateDateInput), ButtonType.ZR, false);
                        }
                    }

                    //左スティック
                    if (jState.X != jState_before.X || jState.Y != jState_before.Y)
                    {

                        bool jL = false;
                        bool jR = false;
                        bool jU = false;
                        bool jD = false;

                        if (jState.X > 300)
                        {
                            //right
                            jR = true;
                        }
                        if (jState.X < -300)
                        {
                            //left
                            jL = true;
                        }
                        if (jState.Y > 300)
                        {
                            //down
                            jD = true;
                        }
                        if (jState.Y < -300)
                        {
                            //up
                            jU = true;
                        }

                        // アナログスティックの左右軸
                        if (jR)
                        {
                            //right
                            if (jU)
                            {
                                MoveStick(ButtonType.LSTICK, Stick.MAX, Stick.MIN);
                                Invoke(new delegateUpdateInput(UpdateInput), "RU");
                            }
                            else if (jD)
                            {
                                MoveStick(ButtonType.LSTICK, Stick.MAX, Stick.MAX);
                                Invoke(new delegateUpdateInput(UpdateInput), "RD");
                            }
                            else
                            {
                                MoveStick(ButtonType.LSTICK, Stick.MAX, Stick.CENTER);
                                Invoke(new delegateUpdateInput(UpdateInput), "R");
                            }
                        }
                        else if (jL)
                        {
                            if (jU)
                            {
                                MoveStick(ButtonType.LSTICK, Stick.MIN, Stick.MIN);
                                Invoke(new delegateUpdateInput(UpdateInput), "LU");
                            }
                            else if (jD)
                            {
                                MoveStick(ButtonType.LSTICK, Stick.MIN, Stick.MAX);
                                Invoke(new delegateUpdateInput(UpdateInput), "LU");
                            }
                            else
                            {
                                MoveStick(ButtonType.LSTICK, Stick.MIN, Stick.CENTER);
                                Invoke(new delegateUpdateInput(UpdateInput), "L");
                            }
                        }
                        else if (jU)
                        {
                            MoveStick(ButtonType.LSTICK, Stick.CENTER, Stick.MIN);
                            Invoke(new delegateUpdateInput(UpdateInput), "U");
                        }
                        else if (jD)
                        {
                            MoveStick(ButtonType.LSTICK, Stick.CENTER, Stick.MAX);
                            Invoke(new delegateUpdateInput(UpdateInput), "D");
                        }
                        else
                        {
                            MoveStick(ButtonType.LSTICK, Stick.CENTER, Stick.CENTER);
                            Invoke(new delegateUpdateInput(UpdateInput), "");
                        }
                    }
                    //右スティック
                    if (jState.RotationY != jState_before.RotationY || jState.RotationX != jState_before.RotationX)
                    {
                        bool rL = false;
                        bool rR = false;
                        bool rU = false;
                        bool rD = false;
                        if (jState.RotationY > 400)
                        {
                            rD = true;
                        }
                        if (jState.RotationY < -400)
                        {
                            rU = true;
                        }
                        if (jState.RotationX > 400)
                        {
                            rR = true;
                        }
                        if (jState.RotationX < -400)
                        {
                            rL = true;
                        }
                        if (rR)
                        {
                            //right
                            if (rU)
                            {
                                MoveStick(ButtonType.RSTICK, Stick.MAX, Stick.MIN);
                                Invoke(new delegateUpdateInput(UpdateInput), "RU");
                            }
                            else if (rD)
                            {
                                MoveStick(ButtonType.RSTICK, Stick.MAX, Stick.MAX);
                                Invoke(new delegateUpdateInput(UpdateInput), "RD");
                            }
                            else
                            {
                                MoveStick(ButtonType.RSTICK, Stick.MAX, Stick.CENTER);
                                Invoke(new delegateUpdateInput(UpdateInput), "R");
                            }
                        }
                        else if (rL)
                        {
                            if (rU)
                            {
                                MoveStick(ButtonType.RSTICK, Stick.MIN, Stick.MIN);
                                Invoke(new delegateUpdateInput(UpdateInput), "LU");
                            }
                            else if (rD)
                            {
                                MoveStick(ButtonType.RSTICK, Stick.MIN, Stick.MAX);
                                Invoke(new delegateUpdateInput(UpdateInput), "LU");
                            }
                            else
                            {
                                MoveStick(ButtonType.RSTICK, Stick.MIN, Stick.CENTER);
                                Invoke(new delegateUpdateInput(UpdateInput), "L");
                            }
                        }
                        else if (rU)
                        {
                            MoveStick(ButtonType.RSTICK, Stick.CENTER, Stick.MIN);
                            Invoke(new delegateUpdateInput(UpdateInput), "U");
                        }
                        else if (rD)
                        {
                            MoveStick(ButtonType.RSTICK, Stick.CENTER, Stick.MAX);
                            Invoke(new delegateUpdateInput(UpdateInput), "D");
                        }
                        else
                        {
                            MoveStick(ButtonType.RSTICK, Stick.CENTER, Stick.CENTER);
                            Invoke(new delegateUpdateInput(UpdateInput), "");
                        }
                    }
                    // キー入力
                    if (jState.Buttons != jState_before.Buttons)
                    {
                        for (int i = 0; i < 15; i++)
                        {
                            if (jState.Buttons[i])
                            {
                                Invoke(new delegateUpdateDateInput(UpdateDateInput), array[i], true);
                            }
                            else if (jState.Buttons[i] != jState_before.Buttons[i])
                            {
                                Invoke(new delegateUpdateDateInput(UpdateDateInput), array[i], false);
                                Invoke(new delegateUpdateInput(UpdateInput), "");
                            }
                        }
                    }

                    //左パッド入力
                    if (jState.PointOfViewControllers[0] != jState_before.PointOfViewControllers[0])
                    {
                        bool lL = false;
                        bool lR = false;
                        bool lU = false;
                        bool lD = false;
                        if (jState.PointOfViewControllers[0] <= -1)
                        {
                            Invoke(new delegateUpdateInput(UpdateInput), "");
                            Invoke(new delegateUpdateDateInput(UpdateDateInput), ButtonType.LEFT, false);
                            Invoke(new delegateUpdateDateInput(UpdateDateInput), ButtonType.RIGHT, false);
                            Invoke(new delegateUpdateDateInput(UpdateDateInput), ButtonType.UP, false);
                            Invoke(new delegateUpdateDateInput(UpdateDateInput), ButtonType.DOWN, false);
                        }
                        else if (jState.PointOfViewControllers[0] <= 500 && jState.PointOfViewControllers[0] > -1)
                        {
                            Invoke(new delegateUpdateDateInput(UpdateDateInput), ButtonType.LEFT, false);
                            Invoke(new delegateUpdateDateInput(UpdateDateInput), ButtonType.RIGHT, false);
                            Invoke(new delegateUpdateDateInput(UpdateDateInput), ButtonType.DOWN, false);
                            Invoke(new delegateUpdateDateInput(UpdateDateInput), ButtonType.UP, true);
                        }
                        else if (jState.PointOfViewControllers[0] <= 9500 && jState.PointOfViewControllers[0] > 8500)
                        {
                            Invoke(new delegateUpdateDateInput(UpdateDateInput), ButtonType.LEFT, false);
                            Invoke(new delegateUpdateDateInput(UpdateDateInput), ButtonType.UP, false);
                            Invoke(new delegateUpdateDateInput(UpdateDateInput), ButtonType.DOWN, false);
                            Invoke(new delegateUpdateDateInput(UpdateDateInput), ButtonType.RIGHT, true);

                        }
                        else if (jState.PointOfViewControllers[0] <= 18500 && jState.PointOfViewControllers[0] > 17500)
                        {
                            Invoke(new delegateUpdateDateInput(UpdateDateInput), ButtonType.LEFT, false);
                            Invoke(new delegateUpdateDateInput(UpdateDateInput), ButtonType.RIGHT, false);
                            Invoke(new delegateUpdateDateInput(UpdateDateInput), ButtonType.UP, false);
                            Invoke(new delegateUpdateDateInput(UpdateDateInput), ButtonType.DOWN, true);

                        }
                        else if (jState.PointOfViewControllers[0] <= 27500 && jState.PointOfViewControllers[0] > 26500)
                        {
                            Invoke(new delegateUpdateDateInput(UpdateDateInput), ButtonType.RIGHT, false);
                            Invoke(new delegateUpdateDateInput(UpdateDateInput), ButtonType.UP, false);
                            Invoke(new delegateUpdateDateInput(UpdateDateInput), ButtonType.DOWN, false);
                            Invoke(new delegateUpdateDateInput(UpdateDateInput), ButtonType.LEFT, true);

                        }

                    }

/*

                    if (state.IsPressed(Key.Down))
                    {
                        Invoke(new delegateUpdateDateInput(UpdateDateInput), ButtonType.DOWN, true);
                    }
                    else if (state.IsPressed(Key.Up))
                    {
                        Invoke(new delegateUpdateDateInput(UpdateDateInput), ButtonType.UP, true);
                    }
                    else if (state.IsPressed(Key.Right))
                    {
                        Invoke(new delegateUpdateDateInput(UpdateDateInput), ButtonType.RIGHT, true);
                    }
                    else if (state.IsPressed(Key.Left))
                    {
                        Invoke(new delegateUpdateDateInput(UpdateDateInput), ButtonType.LEFT, true);
                    }
                    else if (state.IsPressed(Key.NumberPadEnter))
                    {
                        Invoke(new delegateUpdateDateInput(UpdateDateInput), ButtonType.A, true);
                    }
                    else
                    { Invoke(new delegateUpdateDateInput(UpdateDateInput), ButtonType.DOWN, false);
                        Invoke(new delegateUpdateDateInput(UpdateDateInput), ButtonType.UP, false);
                        Invoke(new delegateUpdateDateInput(UpdateDateInput), ButtonType.LEFT, false);
                        Invoke(new delegateUpdateDateInput(UpdateDateInput), ButtonType.RIGHT, false);
                        Invoke(new delegateUpdateDateInput(UpdateDateInput), ButtonType.A, false);
                    }
*/

                    jState_before = jState;

                    await Task.Delay(100).ConfigureAwait(false);
                }
            }
            catch
            {
                InputInitialize();
                Loop();
            }
        }

        /// 
        /// 各種ボタンの種類の設定
        /// 
        enum ButtonType : byte
        {
            RIGHT = 0,
            LEFT,
            UP,
            DOWN,
            A,
            B,
            X,
            Y,
            R,
            L,
            ZR,
            ZL,
            RSTICK,
            LSTICK,
            RCLICK,
            LCLICK,
            HOME,
            CAPTURE,
            PLUS,
            MINUS
        }

        enum ButtonState : byte
        {
            PRESS = 0,
            RELEASE
        }

        enum Stick : byte
        {
            MIN = 0,
            CENTER = 128,
            MAX = 255
        }



        public PokemonSWSH_SeedSearchSupport()
        {
            InitializeComponent();
        }

        ///
        ///シリアルポートの取得
        ///
        private void getSerialPorts()
        {
            string[] ports = SerialPort.GetPortNames();
            comPortComboBox.Items.Clear();
            int count = -1;
            foreach (string port in ports)
            {
                comPortComboBox.Items.Add(port);
                count++;
            }
            if (comPortComboBox.Items.Count > 0)
            {
                comPortComboBox.SelectedIndex = count;
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            DateToday();
            getSerialPorts();
            ControllerChange();
            InputInitialize();
            Loop();
        }

        ///
        ///シリアルポートの変更時
        ///
        private void comPortComboBox_Click(object sender, EventArgs e)
        {
            getSerialPorts();
        }
        private void comPortComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            token_source = new CancellationTokenSource();
            cancel_token = token_source.Token;

            InputUnLock();
            this.token_source.Cancel();
            connectSerial();
        }

        ///
        ///シリアルポートへの接続確認
        ///
        private void connectSerial()
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                }
                serialPort.BaudRate = 115200;
                serialPort.Parity = Parity.None;
                serialPort.DataBits = 8;
                serialPort.StopBits = StopBits.One;
                serialPort.PortName = comPortComboBox.Text;
                serialPort.Open();
            }
            catch
            {
                MessageBox.Show("シリアルポートの接続に失敗しました");

            }
        }

        ///
        ///シリアルポートへの出力
        ///
        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {

                //int data_len = serialPort.BytesToRead;
                //Byte[] data = new Byte[data_len];
                //serialPort.Read(data, 0, data_len);

                String data = serialPort.ReadExisting();

                Invoke((MethodInvoker)(() =>	// 受信用スレッドから切り替えてデータを書き込む
                {
                    Console.Write(data);
                    Thread.Sleep(1);
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        ///
        ///コントローラの各ボタンの設定
        ///
        private void PressButton(ButtonType button)
        {
            if (serialPort.IsOpen)
            {
                Byte[] data = new byte[2];
                data[0] = (Byte)button;
                data[1] = (Byte)ButtonState.PRESS;

                serialPort.Write(data, 0, 2);
            }
        }

        private void ReleaseButton(ButtonType button)
        {
            if (serialPort.IsOpen)
            {
                Byte[] data = new byte[2];
                data[0] = (Byte)button;
                data[1] = (Byte)ButtonState.RELEASE;

                serialPort.Write(data, 0, 2);
            }
        }

        private void Left_MouseDown(object sender, MouseEventArgs e)
        {
            PressButton(ButtonType.LEFT);
        }

        private void Left_MouseUp(object sender, MouseEventArgs e)
        {
            ReleaseButton(ButtonType.LEFT);
        }

        private void Right_MouseDown(object sender, MouseEventArgs e)
        {
            PressButton(ButtonType.RIGHT);
        }

        private void Right_MouseUp(object sender, MouseEventArgs e)
        {
            ReleaseButton(ButtonType.RIGHT);
        }

        private void Up_MouseDown(object sender, MouseEventArgs e)
        {
            PressButton(ButtonType.UP);
        }

        private void Up_MouseUp(object sender, MouseEventArgs e)
        {
            ReleaseButton(ButtonType.UP);
        }

        private void Down_MouseDown(object sender, MouseEventArgs e)
        {
            PressButton(ButtonType.DOWN);
        }

        private void Down_MouseUp(object sender, MouseEventArgs e)
        {
            ReleaseButton(ButtonType.DOWN);
        }

        private void ButtonA_MouseDown(object sender, MouseEventArgs e)
        {
            PressButton(ButtonType.A);
        }

        private void ButtonA_MouseUp(object sender, MouseEventArgs e)
        {
            ReleaseButton(ButtonType.A);
        }

        private void ButtonB_MouseDown(object sender, MouseEventArgs e)
        {
            PressButton(ButtonType.B);
        }

        private void ButtonB_MouseUp(object sender, MouseEventArgs e)
        {
            ReleaseButton(ButtonType.B);
        }

        private void ButtonY_MouseDown(object sender, MouseEventArgs e)
        {
            PressButton(ButtonType.Y);
        }

        private void ButtonY_MouseUp(object sender, MouseEventArgs e)
        {
            ReleaseButton(ButtonType.Y);
        }

        private void ButtonX_MouseDown(object sender, MouseEventArgs e)
        {
            PressButton(ButtonType.X);
        }

        private void ButtonX_MouseUp(object sender, MouseEventArgs e)
        {
            ReleaseButton(ButtonType.X);
        }

        private void ButtonZR_MouseDown(object sender, MouseEventArgs e)
        {
            PressButton(ButtonType.ZR);
        }

        private void ButtonZR_MouseUp(object sender, MouseEventArgs e)
        {
            ReleaseButton(ButtonType.ZR);
        }

        private void ButtonZL_MouseDown(object sender, MouseEventArgs e)
        {
            PressButton(ButtonType.ZL);
        }

        private void ButtonZL_MouseUp(object sender, MouseEventArgs e)
        {
            ReleaseButton(ButtonType.ZL);
        }

        private void ButtonR_MouseDown(object sender, MouseEventArgs e)
        {
            PressButton(ButtonType.R);
        }

        private void ButtonR_MouseUp(object sender, MouseEventArgs e)
        {
            ReleaseButton(ButtonType.R);
        }

        private void ButtonL_MouseDown(object sender, MouseEventArgs e)
        {
            PressButton(ButtonType.L);
        }

        private void ButtonL_MouseUp(object sender, MouseEventArgs e)
        {
            ReleaseButton(ButtonType.L);
        }

        private void ButtonPlus_MouseDown(object sender, MouseEventArgs e)
        {
            PressButton(ButtonType.PLUS);
        }

        private void ButtonPlus_MouseUp(object sender, MouseEventArgs e)
        {
            ReleaseButton(ButtonType.PLUS);
        }

        private void ButtonMinus_MouseDown(object sender, MouseEventArgs e)
        {
            PressButton(ButtonType.MINUS);
        }

        private void ButtonMinus_MouseUp(object sender, MouseEventArgs e)
        {
            ReleaseButton(ButtonType.MINUS);
        }

        private void ButtonHome_MouseDown(object sender, MouseEventArgs e)
        {
            PressButton(ButtonType.HOME);
        }

        private void ButtonHome_MouseUp(object sender, MouseEventArgs e)
        {
            ReleaseButton(ButtonType.HOME);
        }

        private void ButtonCapture_MouseDown(object sender, MouseEventArgs e)
        {
            PressButton(ButtonType.CAPTURE);
        }

        private void ButtonCapture_MouseUp(object sender, MouseEventArgs e)
        {
            ReleaseButton(ButtonType.CAPTURE);
        }

        private void MoveStick(ButtonType button, Stick x_stick, Stick y_stick)
        {
            if (serialPort.IsOpen)
            {
                Byte[] data = new byte[3];
                data[0] = (Byte)button;
                data[1] = (Byte)x_stick;
                data[2] = (Byte)y_stick;

                serialPort.Write(data, 0, 3);
            }
        }
        private void MoveStick(ButtonType button, int x, int y)
        {
            if (serialPort.IsOpen)
            {
                Byte[] data = new byte[3];
                data[0] = (Byte)button;
                data[1] = (Byte)x;
                data[2] = (Byte)y;

                serialPort.Write(data, 0, 3);
            }
        }
        private void ReleaseStick(ButtonType button)
        {
            if (serialPort.IsOpen)
            {
                Byte[] data = new byte[3];
                data[0] = (Byte)button;
                data[1] = (Byte)Stick.CENTER;
                data[2] = (Byte)Stick.CENTER;

                serialPort.Write(data, 0, 3);
            }
        }

        private void LeftN_MouseDown(object sender, MouseEventArgs e)
        {
            MoveStick(ButtonType.LSTICK, Stick.CENTER, Stick.MIN);
        }

        private void LeftN_MouseUp(object sender, MouseEventArgs e)
        {
            ReleaseStick(ButtonType.LSTICK);
        }

        private void LeftS_MouseDown(object sender, MouseEventArgs e)
        {
            MoveStick(ButtonType.LSTICK, Stick.CENTER, Stick.MAX);
        }

        private void LeftS_MouseUp(object sender, MouseEventArgs e)
        {
            ReleaseStick(ButtonType.LSTICK);
        }

        private void LeftE_MouseDown(object sender, MouseEventArgs e)
        {
            MoveStick(ButtonType.LSTICK, Stick.MAX, Stick.CENTER);
        }

        private void LeftE_MouseUp(object sender, MouseEventArgs e)
        {
            ReleaseStick(ButtonType.LSTICK);
        }

        private void LeftW_MouseDown(object sender, MouseEventArgs e)
        {
            MoveStick(ButtonType.LSTICK, Stick.MIN, Stick.CENTER);
        }

        private void LeftW_MouseUp(object sender, MouseEventArgs e)
        {
            ReleaseStick(ButtonType.LSTICK);
        }

        private void LeftNE_MouseDown(object sender, MouseEventArgs e)
        {
            MoveStick(ButtonType.LSTICK, Stick.MAX, Stick.MIN);
        }

        private void LeftNE_MouseUp(object sender, MouseEventArgs e)
        {
            ReleaseStick(ButtonType.LSTICK);
        }

        private void LeftNW_MouseDown(object sender, MouseEventArgs e)
        {
            MoveStick(ButtonType.LSTICK, Stick.MIN, Stick.MIN);
        }

        private void LeftNW_MouseUp(object sender, MouseEventArgs e)
        {
            ReleaseStick(ButtonType.LSTICK);
        }

        private void LeftSE_MouseDown(object sender, MouseEventArgs e)
        {
            MoveStick(ButtonType.LSTICK, Stick.MAX, Stick.MAX);
        }

        private void LeftSE_MouseUp(object sender, MouseEventArgs e)
        {
            ReleaseStick(ButtonType.LSTICK);
        }

        private void LeftSW_MouseDown(object sender, MouseEventArgs e)
        {
            MoveStick(ButtonType.LSTICK, Stick.MIN, Stick.MAX);
        }

        private void LeftSW_MouseUp(object sender, MouseEventArgs e)
        {
            ReleaseStick(ButtonType.LSTICK);
        }

        private void RightN_MouseDown(object sender, MouseEventArgs e)
        {
            MoveStick(ButtonType.RSTICK, Stick.CENTER, Stick.MIN);
        }

        private void RightN_MouseUp(object sender, MouseEventArgs e)
        {
            ReleaseStick(ButtonType.RSTICK);
        }

        private void RightS_MouseDown(object sender, MouseEventArgs e)
        {
            MoveStick(ButtonType.RSTICK, Stick.CENTER, Stick.MAX);
        }

        private void RightS_MouseUp(object sender, MouseEventArgs e)
        {
            ReleaseStick(ButtonType.RSTICK);
        }

        private void RightE_MouseDown(object sender, MouseEventArgs e)
        {
            MoveStick(ButtonType.RSTICK, Stick.MAX, Stick.CENTER);
        }

        private void RightE_MouseUp(object sender, MouseEventArgs e)
        {
            ReleaseStick(ButtonType.RSTICK);
        }

        private void RightW_MouseDown(object sender, MouseEventArgs e)
        {
            MoveStick(ButtonType.RSTICK, Stick.MIN, Stick.CENTER);
        }

        private void RightW_MouseUp(object sender, MouseEventArgs e)
        {
            ReleaseStick(ButtonType.RSTICK);
        }

        private void RightNE_MouseDown(object sender, MouseEventArgs e)
        {
            MoveStick(ButtonType.RSTICK, Stick.MAX, Stick.MIN);
        }

        private void RightNE_MouseUp(object sender, MouseEventArgs e)
        {
            ReleaseStick(ButtonType.RSTICK);
        }

        private void RightSE_MouseDown(object sender, MouseEventArgs e)
        {
            MoveStick(ButtonType.RSTICK, Stick.MAX, Stick.MAX);
        }

        private void RightSE_MouseUp(object sender, MouseEventArgs e)
        {
            ReleaseStick(ButtonType.RSTICK);
        }

        private void RightSW_MouseDown(object sender, MouseEventArgs e)
        {
            MoveStick(ButtonType.RSTICK, Stick.MIN, Stick.MAX);
        }

        private void RightSW_MouseUp(object sender, MouseEventArgs e)
        {
            ReleaseStick(ButtonType.RSTICK);
        }

        private void RightNW_MouseDown(object sender, MouseEventArgs e)
        {
            MoveStick(ButtonType.RSTICK, Stick.MAX, Stick.MAX);
        }

        private void RightNW_MouseUp(object sender, MouseEventArgs e)
        {
            ReleaseStick(ButtonType.RSTICK);
        }

        //△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△

        ///
        ///表示更新メソッド
        ///


        delegate void UpdateCalendarDelegate();
        private void UpdateCalendar()
        {
            DateTimePicker1.Value = current_date;
        }


        delegate void UpdateCountDelegate(int i, int nday);
        private void UpdateCount(int i, int nday)
        {
            label_count.Text = "変更回数： " + (i + 1) + "/" + nday;
        }

        //△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△

        ///
        ///タスク実行中のボタンロックメソッド
        ///
        private void InputLock()
        {
            MoveStick(ButtonType.LSTICK, Stick.CENTER, Stick.CENTER);
            MoveStick(ButtonType.RSTICK, Stick.CENTER, Stick.CENTER);
            unUsedInput = true;
            Button_machineDateReset.Enabled = false;
            DateTimePicker1.Enabled = false;
            DayTextbox.Enabled = false;
            Button_machineDateReset.Enabled = false;
            Button_plus1Day.Enabled = false;
            Button_plus3Days.Enabled = false;
            Button_plusNDays.Enabled = false;
            Button_resetPlus3Days.Enabled = false;
            Button_resetPlus3Days_2.Enabled = false;
            Button_resetPlus4Days.Enabled = false;
            Button_resetPlus5Days.Enabled = false;
            Button_savePlus3days.Enabled = false;
            Button_machineDateToday.Enabled = false;
            Button_displayStatus.Enabled = false;
            Button_dmax1.Enabled = false;
            Button_dmax2.Enabled = false;
            Button_levelUp.Enabled = false;
            Button_repeatA.Enabled = false;
            Button_repeatB.Enabled = false;
            Button_startRaid.Enabled = false;
            Button_startRaidSelf.Enabled = false;
            Button_stop.Enabled = true;
            Button_AutoRaid.Enabled = false;
            Button_BattleTower.Enabled = false;
            Button_collectFeathers.Enabled = false;
            Button_Eggs.Enabled = false;
            Button_Horidashimono.Enabled = false;
            Button_IDkuji.Enabled = false;
            Button_Kinomi.Enabled = false;
            Button_Wget.Enabled = false;
            numericUpDown_Box.Enabled = false;
            numericUpDown_Cycle.Enabled = false;
        }
        private void InputUnLock()
        {
            Invoke(new UpdateCalendarDelegate(UpdateCalendar));
            MoveStick(ButtonType.LSTICK, Stick.CENTER, Stick.CENTER);
            MoveStick(ButtonType.RSTICK, Stick.CENTER, Stick.CENTER);
            unUsedInput = false;
            Button_machineDateReset.Enabled = true;
            DateTimePicker1.Enabled = true;
            DayTextbox.Enabled = true;
            Button_machineDateReset.Enabled = true;
            Button_plus1Day.Enabled = true;
            Button_plus3Days.Enabled = true;
            Button_plusNDays.Enabled = true;
            Button_resetPlus3Days.Enabled = true;
            Button_resetPlus3Days_2.Enabled = true;
            Button_resetPlus4Days.Enabled = true;
            Button_resetPlus5Days.Enabled = true;
            Button_savePlus3days.Enabled = true;
            Button_machineDateToday.Enabled = true;
            Button_displayStatus.Enabled = true;
            Button_dmax1.Enabled = true;
            Button_dmax2.Enabled = true;
            Button_levelUp.Enabled = true;
            Button_repeatA.Enabled = true;
            Button_repeatB.Enabled = true;
            Button_startRaid.Enabled = true;
            Button_startRaidSelf.Enabled = true;
            Button_stop.Enabled = true;
            Button_AutoRaid.Enabled = true;
            Button_BattleTower.Enabled = true;
            Button_collectFeathers.Enabled = true;
            Button_Eggs.Enabled = true;
            Button_Horidashimono.Enabled = true;
            Button_IDkuji.Enabled = true;
            Button_Kinomi.Enabled = true;
            Button_Wget.Enabled = true;
            numericUpDown_Box.Enabled = true;
            numericUpDown_Cycle.Enabled = true;
        }

        //△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△

        ///
        ///他メソッドから呼び出されるメソッド
        ///

        ///<summary>
        ///日付設定画面の表示
        ///</summary>
        private async Task DisplayDate()
        {
            try
            {
                PressButton(ButtonType.HOME);
                await Task.Delay(40);
                ReleaseButton(ButtonType.HOME);
                await Task.Delay(1000);
                PressButton(ButtonType.DOWN);
                await Task.Delay(50);
                for (int j = 0; j < 4; ++j)
                {
                    PressButton(ButtonType.RIGHT);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.RIGHT);
                    await Task.Delay(50);
                }
                PressButton(ButtonType.A);
                await Task.Delay(50);
                ReleaseButton(ButtonType.A);
                await Task.Delay(50);
                PressButton(ButtonType.DOWN);
                await Task.Delay(2200);
                ReleaseButton(ButtonType.DOWN);
                await Task.Delay(50);
                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }
                PressButton(ButtonType.A);
                await Task.Delay(50);
                ReleaseButton(ButtonType.A);
                await Task.Delay(50);

                for (int j = 0; j < 4; ++j)
                {
                    if (cancel_token.IsCancellationRequested)
                    {
                        throw new Exception();
                    }
                    PressButton(ButtonType.DOWN);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.DOWN);
                    await Task.Delay(50);
                }

                PressButton(ButtonType.A);
                await Task.Delay(50);
                ReleaseButton(ButtonType.A);
                await Task.Delay(300);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        ///<summary>
        ///本体の日付を1日進める（日付設定にフォーカスがあること）
        ///</summary>

        private async Task IncreaseDate(bool rade_hole_mode = false)
        {
            TimeSpan oneday = new TimeSpan(1, 0, 0, 0);
            DateTime tommorow = current_date + oneday;
            int year_diff = tommorow.Year - current_date.Year;
            int month_diff = tommorow.Month - current_date.Month;
            current_date = current_date + oneday;
            token_source = new CancellationTokenSource();
            cancel_token = token_source.Token;

            try
            {
                int update_num = 1;
                if (year_diff == 1) update_num = 3;
                if (month_diff == 1) update_num = 2;

                PressButton(ButtonType.A);
                await Task.Delay(40);
                ReleaseButton(ButtonType.A);
                await Task.Delay(300);

                if (rade_hole_mode)
                {
                    for (int i = 0; i < 2; ++i)
                    {
                        if (cancel_token.IsCancellationRequested)
                        {
                            return;
                        }
                        PressButton(ButtonType.RIGHT);
                        await Task.Delay(40);
                        ReleaseButton(ButtonType.RIGHT);
                        await Task.Delay(40);
                    }

                    for (int i = 0; i < update_num; ++i)
                    {
                        if (cancel_token.IsCancellationRequested)
                        {
                            return;
                        }
                        PressButton(ButtonType.UP);
                        await Task.Delay(40);
                        ReleaseButton(ButtonType.UP);
                        await Task.Delay(40);

                        if (i != update_num - 1)
                        {
                            if (cancel_token.IsCancellationRequested)
                            {
                                return;
                            }
                            PressButton(ButtonType.LEFT);
                            await Task.Delay(40);
                            ReleaseButton(ButtonType.LEFT);
                            await Task.Delay(40);
                        }
                    }

                    for (int i = 0; i < update_num + 3; ++i)
                    {
                        if (cancel_token.IsCancellationRequested)
                        {
                            return;
                        }
                        PressButton(ButtonType.A);
                        await Task.Delay(40);
                        ReleaseButton(ButtonType.A);
                        await Task.Delay(40);
                    }
                }
                else
                {
                    for (int i = 0; i < 2; ++i)
                    {
                        if (cancel_token.IsCancellationRequested)
                        {
                            return;
                        }
                        PressButton(ButtonType.LEFT);
                        await Task.Delay(40);
                        ReleaseButton(ButtonType.LEFT);
                        await Task.Delay(40);
                    }

                    for (int i = 0; i < update_num; ++i)
                    {
                        if (cancel_token.IsCancellationRequested)
                        {
                            return;
                        }
                        PressButton(ButtonType.LEFT);
                        await Task.Delay(40);
                        ReleaseButton(ButtonType.LEFT);
                        await Task.Delay(40);
                        PressButton(ButtonType.UP);
                        await Task.Delay(40);
                        ReleaseButton(ButtonType.UP);
                        await Task.Delay(40);
                    }

                    for (int i = 0; i < 3 + update_num; ++i)
                    {
                        if (cancel_token.IsCancellationRequested)
                        {
                            return;
                        }
                        PressButton(ButtonType.A);
                        await Task.Delay(40);
                        ReleaseButton(ButtonType.A);
                        await Task.Delay(40);
                    }
                    await Task.Delay(200);
                }
                Invoke(new UpdateCalendarDelegate(UpdateCalendar));
                //Invoke(new updateDateLabelDelegate(updateDateLabel));
            }
            catch(Exception e) {
                throw;
            }
        }

        ///<summary>
        ///レイドを使った時渡り（N日）
        ///</summary>
        private async Task IncreaseDateWithRaidHole()
        {
            token_source = new CancellationTokenSource();
            cancel_token = token_source.Token;
            try
            {
                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }

                PressButton(ButtonType.A);
                await Task.Delay(50);
                ReleaseButton(ButtonType.A);
                await Task.Delay(600);
                if (cancel_token.IsCancellationRequested)
                {
                    return;
                }
                PressButton(ButtonType.A);
                await Task.Delay(50);
                ReleaseButton(ButtonType.A);
                await Task.Delay(3200);
                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }
                await DisplayDate();

                for (int j = 0; j < 2; ++j)
                {
                    PressButton(ButtonType.DOWN);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.DOWN);
                    await Task.Delay(50);
                }

                //Increase date
                await IncreaseDate(true);
                if (cancel_token.IsCancellationRequested)
                {
                    return;
                }
                PressButton(ButtonType.HOME);
                await Task.Delay(50);
                ReleaseButton(ButtonType.HOME);
                await Task.Delay(1000);
                if (cancel_token.IsCancellationRequested)
                {
                    return;
                }
                PressButton(ButtonType.A);
                await Task.Delay(50);
                ReleaseButton(ButtonType.A);
                await Task.Delay(500);

                PressButton(ButtonType.B);
                await Task.Delay(100);
                ReleaseButton(ButtonType.B);
                await Task.Delay(1000);
                if (cancel_token.IsCancellationRequested)
                {
                    return;
                }
                PressButton(ButtonType.A);
                await Task.Delay(50);
                ReleaseButton(ButtonType.A);
                await Task.Delay(4000);

                for (int j = 0; j < 2; ++j)
                {
                    if (cancel_token.IsCancellationRequested)
                    {
                        throw new Exception();
                    }
                    PressButton(ButtonType.A);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.A);
                    await Task.Delay(500);
                }

                for (int j = 0; j < 3; ++j)
                {
                    if (cancel_token.IsCancellationRequested)
                    {
                        throw new Exception();
                    }
                    PressButton(ButtonType.A);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.A);
                    await Task.Delay(100);
                }
            }
            catch(Exception e) {
                throw;
            }
        }


        ///<summary>
        ///レイド使用日付変更メソッド
        ///</summary>
        private async Task PlusNDays(int n_days)
        {
            try
            {                
                token_source = new CancellationTokenSource();
                cancel_token = token_source.Token;

                await Task.Run(async () =>
                {
                    for (int i = 0; i < n_days; ++i)
                    {
                        if (cancel_token.IsCancellationRequested)
                        {
                            throw new Exception();
                        }

                        await IncreaseDateWithRaidHole();
                        Invoke(new UpdateCountDelegate(UpdateCount), i, n_days);
                    }
                }, cancel_token);
            }
            catch (Exception e)
            {
                throw;
            }
        }


        ///<summary>
        ///ランクマ後日付変更メソッド
        /// </summary>
        private async Task PlusNDaysFast(object sender, EventArgs e, int n_days)
        {
            try
            {
                token_source = new CancellationTokenSource();
                cancel_token = token_source.Token;

                DateTime dt = new DateTime(); 
                dt = current_date;

                int year = dt.Year;
                int month = dt.Month;
                int day = dt.Day;

                //ゲーム画面からスタート

                await DisplayDate();
                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }

                for (int i = 0; i < 2; i++)
                {
                    if (cancel_token.IsCancellationRequested)
                    {
                        return;
                    }
                    PressButton(ButtonType.A);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.A);
                    await Task.Delay(50);
                }//A2回

                for (int i = 0; i < 2; i++)
                {
                    PressButton(ButtonType.DOWN);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.DOWN);
                    await Task.Delay(300);
                }//Down2回


                PressButton(ButtonType.A);
                await Task.Delay(50);
                ReleaseButton(ButtonType.A);
                await Task.Delay(50);

                for (int i = 0; i < year - 1999; i++)
                {
                    if (cancel_token.IsCancellationRequested)
                    {
                        throw new Exception();
                    }
                    PressButton(ButtonType.DOWN);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.DOWN);
                    await Task.Delay(50);
                }
                PressButton(ButtonType.RIGHT);
                await Task.Delay(50);
                ReleaseButton(ButtonType.RIGHT);
                await Task.Delay(50);

                for (int i = 0; i < month - 1; i++)
                {
                    if (cancel_token.IsCancellationRequested)
                    {
                        throw new Exception();
                    }
                    PressButton(ButtonType.DOWN);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.DOWN);
                    await Task.Delay(50);
                }
                PressButton(ButtonType.RIGHT);
                await Task.Delay(50);
                ReleaseButton(ButtonType.RIGHT);
                await Task.Delay(50);
                for (int i = 0; i < day - 1; i++)
                {
                    if (cancel_token.IsCancellationRequested)
                    {
                        throw new Exception();
                    }
                    PressButton(ButtonType.DOWN);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.DOWN);
                    await Task.Delay(50);
                }
                for (int i = 0; i < 3; i++)
                {
                    if (cancel_token.IsCancellationRequested)
                    {
                        throw new Exception();
                    }
                    PressButton(ButtonType.A);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.A);
                    await Task.Delay(50);
                }
                current_date = new DateTime(2000, 1, 1, 0, 0, 0);
                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }
                PressButton(ButtonType.A);
                await Task.Delay(50);
                ReleaseButton(ButtonType.A);
                await Task.Delay(200);

                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }

                for (int i = 0; i < n_days; ++i)
                {
                    if (cancel_token.IsCancellationRequested)
                    {
                        throw new Exception();
                    }

                    await IncreaseDate();
                    Invoke(new UpdateCountDelegate(UpdateCount), i, n_days);
                    //Invoke(new updateDateLabelDelegate(updateDateLabel));
                }

                InputUnLock();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        //△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△


        ///<summary>
        ///レイドキャンセルしてセーブ　　
        ///</summary>
        private async Task ChanselAndSave()
        {
            try
            {

                token_source = new CancellationTokenSource();
                cancel_token = token_source.Token;
                Invoke(new UpdateCountDelegate(UpdateCount), -1, 3);
                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }
                PressButton(ButtonType.B);
                await Task.Delay(100);
                ReleaseButton(ButtonType.B);
                await Task.Delay(1500);
                PressButton(ButtonType.X);
                await Task.Delay(100);
                ReleaseButton(ButtonType.X);
                await Task.Delay(1050);
                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }
                PressButton(ButtonType.R);
                await Task.Delay(100);
                ReleaseButton(ButtonType.R);
                await Task.Delay(1500);
                PressButton(ButtonType.A);
                await Task.Delay(100);
                ReleaseButton(ButtonType.A);
                await Task.Delay(3000);
                PressButton(ButtonType.A);
                await Task.Delay(100);
                ReleaseButton(ButtonType.A);
                await Task.Delay(700);
                await PlusNDays(3);
            }
            catch (Exception exception)
            {
                throw;
            }

        }

        /// <summary>
        /// リセットしてN日後に進む
        /// </summary>
        private async Task ResetStartNdays(int n)
        {
            try
            {

                token_source = new CancellationTokenSource();
                cancel_token = token_source.Token;

                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }
                PressButton(ButtonType.HOME);
                await Task.Delay(100);
                ReleaseButton(ButtonType.HOME);
                await Task.Delay(1000);
                PressButton(ButtonType.X);
                await Task.Delay(80);
                ReleaseButton(ButtonType.X);
                await Task.Delay(1400);
                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }
                PressButton(ButtonType.A);
                await Task.Delay(100);
                ReleaseButton(ButtonType.A);//ソフト終了
                await Task.Delay(1000);
                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }
                PressButton(ButtonType.A);
                await Task.Delay(100);
                ReleaseButton(ButtonType.A);
                await Task.Delay(1500);
                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }
                PressButton(ButtonType.A);
                await Task.Delay(50);
                ReleaseButton(ButtonType.A);
                await Task.Delay(1000);
                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }
                PressButton(ButtonType.A);
                await Task.Delay(100);
                ReleaseButton(ButtonType.A);
                await Task.Delay(17600);
                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }
                PressButton(ButtonType.A);
                await Task.Delay(100);
                ReleaseButton(ButtonType.A);
                await Task.Delay(12000);
                await PlusNDays(n);


            }
            catch (Exception exception)
            {
                throw;
            }
        }

        //△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△


        /// <summary>
        ///ダイマックスして1つ目の技
        /// </summary>
        private async Task Battle_dmax1(object sender, EventArgs e)
        {
            try
            {
                PressButton(ButtonType.A);
                await Task.Delay(100);
                ReleaseButton(ButtonType.A);
                await Task.Delay(500);
                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }
                PressButton(ButtonType.LEFT);
                await Task.Delay(100);
                ReleaseButton(ButtonType.LEFT);
                await Task.Delay(1300);
                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }
                PressButton(ButtonType.A);
                await Task.Delay(100);
                ReleaseButton(ButtonType.A);
                await Task.Delay(1500);
                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }
                PressButton(ButtonType.A);
                await Task.Delay(100);
                ReleaseButton(ButtonType.A);
                await Task.Delay(500);
                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }
                PressButton(ButtonType.A);
                await Task.Delay(100);
                ReleaseButton(ButtonType.A);
                await Task.Delay(500);
            }
            catch (Exception exception)
            {
                throw;
            }
        }
        /// <summary>
        ///ダイマックスして2つ目の技
        /// </summary>
        private async Task Battle_dmax2(object sender, EventArgs e)
        {
            try
            {
                PressButton(ButtonType.A);
                await Task.Delay(100);
                ReleaseButton(ButtonType.A);
                await Task.Delay(500);
                PressButton(ButtonType.LEFT);
                await Task.Delay(100);
                ReleaseButton(ButtonType.LEFT);
                await Task.Delay(1300);
                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }
                PressButton(ButtonType.A);
                await Task.Delay(100);
                ReleaseButton(ButtonType.A);
                await Task.Delay(500);
                PressButton(ButtonType.DOWN);
                await Task.Delay(100);
                ReleaseButton(ButtonType.DOWN);
                await Task.Delay(500);
                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }
                PressButton(ButtonType.A);
                await Task.Delay(100);
                ReleaseButton(ButtonType.A);
                await Task.Delay(1800);
                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }
                PressButton(ButtonType.A);
                await Task.Delay(100);
                ReleaseButton(ButtonType.A);
                await Task.Delay(300);
                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }
                PressButton(ButtonType.A);
                await Task.Delay(100);
                ReleaseButton(ButtonType.A);
                await Task.Delay(500);
            }
            catch (Exception exception)
            {
                throw;
            }
        }
                       

        //△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△


        ///<summary>
        ///データを今日にする　　
        ///</summary>
        private async Task DateToday()
        {

            DateTime dt = DateTime.Now;
            current_date = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
            try
            {
                Invoke(new UpdateCalendarDelegate(UpdateCalendar));
            }
            catch (Exception exception)
            {
                throw;
            }
            // Invoke(new updateDateLabelDelegate(updateDateLabel));

        }

        /// <summary>
        /// 日付が変更された時データも変更する
        /// </summary>
        private async Task DayInputChange()
        {
            current_date = new DateTime(DateTimePicker1.Value.Year, DateTimePicker1.Value.Month, DateTimePicker1.Value.Day, 0, 0, 0);
            Invoke(new UpdateCalendarDelegate(UpdateCalendar));
            //   Invoke(new updateDateLabelDelegate(updateDateLabel));

        }

        /// <summary>
        /// 本体日付とデータを初期値にする
        /// </summary>
        private async Task DayReset()
        {
            InputLock();
            DateTime dt = new DateTime(); ;
            dt = current_date;

            int year = dt.Year;
            int month = dt.Month;
            int day = dt.Day;
            try
            {
                //ゲーム画面からスタート
                PressButton(ButtonType.HOME);
                await Task.Delay(50);
                ReleaseButton(ButtonType.HOME);
                await Task.Delay(1500);
                PressButton(ButtonType.DOWN);
                await Task.Delay(50);
                for (int j = 0; j < 4; ++j)
                {
                    PressButton(ButtonType.RIGHT);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.RIGHT);
                    await Task.Delay(50);
                }

                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }
                PressButton(ButtonType.A);
                await Task.Delay(50);
                ReleaseButton(ButtonType.A);
                await Task.Delay(50);
                PressButton(ButtonType.DOWN);
                await Task.Delay(2200);
                ReleaseButton(ButtonType.DOWN);
                await Task.Delay(50);
                PressButton(ButtonType.A);
                await Task.Delay(50);
                ReleaseButton(ButtonType.A);
                await Task.Delay(50);

                for (int j = 0; j < 4; ++j)
                {

                    if (cancel_token.IsCancellationRequested)
                    {
                        throw new Exception();
                    }
                    PressButton(ButtonType.DOWN);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.DOWN);
                    await Task.Delay(50);
                }

                PressButton(ButtonType.A);
                await Task.Delay(50);
                ReleaseButton(ButtonType.A);
                await Task.Delay(300);  //時間設定画面の表示


                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }

                for (int i = 0; i < 2; i++)
                {

                    if (cancel_token.IsCancellationRequested)
                    {
                        throw new Exception();
                    }
                    PressButton(ButtonType.A);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.A);
                    await Task.Delay(50);
                }//A2回
                for (int i = 0; i < 2; i++)
                {
                    PressButton(ButtonType.DOWN);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.DOWN);
                    await Task.Delay(300);
                }//Down2回

                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }

                PressButton(ButtonType.A);
                await Task.Delay(50);
                ReleaseButton(ButtonType.A);
                await Task.Delay(50);

                for (int i = 0; i < year - 1999; i++)
                {

                    if (cancel_token.IsCancellationRequested)
                    {
                        throw new Exception();
                    }
                    PressButton(ButtonType.DOWN);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.DOWN);
                    await Task.Delay(50);
                }
                PressButton(ButtonType.RIGHT);
                await Task.Delay(50);
                ReleaseButton(ButtonType.RIGHT);
                await Task.Delay(50);

                for (int i = 0; i < month - 1; i++)
                {

                    if (cancel_token.IsCancellationRequested)
                    {
                        throw new Exception();
                    }
                    PressButton(ButtonType.DOWN);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.DOWN);
                    await Task.Delay(50);
                }
                PressButton(ButtonType.RIGHT);
                await Task.Delay(50);
                ReleaseButton(ButtonType.RIGHT);
                await Task.Delay(50);
                //int x = DateTime.DaysInMonth(year, month);
                for (int i = 0; i < day - 1; i++)
                {

                    if (cancel_token.IsCancellationRequested)
                    {
                        throw new Exception();
                    }
                    PressButton(ButtonType.DOWN);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.DOWN);
                    await Task.Delay(50);
                }
                PressButton(ButtonType.RIGHT);
                await Task.Delay(50);
                ReleaseButton(ButtonType.RIGHT);
                await Task.Delay(50);
                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }
                PressButton(ButtonType.A);
                await Task.Delay(50);
                ReleaseButton(ButtonType.A);
                await Task.Delay(100);



                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }
                PressButton(ButtonType.HOME);
                await Task.Delay(50);
                ReleaseButton(ButtonType.HOME);
                await Task.Delay(1000);

                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }
                PressButton(ButtonType.A);
                await Task.Delay(50);
                ReleaseButton(ButtonType.A);
                await Task.Delay(1000);

                current_date = new DateTime(2000, 1, 1, 0, 0, 0);
            }
            catch (Exception exception)
            {
                throw;
            }
        }
               
        ///<summary>
        ///本体の日付を今日にする
        ///</summary>
        private async Task MachineDateChangeToday()
        {
            token_source = new CancellationTokenSource();
            cancel_token = token_source.Token;
            try
            {
                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }
                await DisplayDate();

                for (int i = 0; i < 2; i++)
                {
                    if (cancel_token.IsCancellationRequested)
                    {
                        throw new Exception();
                    }
                    PressButton(ButtonType.A);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.A);
                    await Task.Delay(50);
                }//A2回


                PressButton(ButtonType.HOME);
                await Task.Delay(50);
                ReleaseButton(ButtonType.HOME);
                await Task.Delay(1000);
                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }
                PressButton(ButtonType.A);
                await Task.Delay(50);
                ReleaseButton(ButtonType.A);
                await Task.Delay(500);
                await DateToday();
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        //△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△


        /// <summary>
        /// 2体目をLv100にする
        /// </summary>

        private async Task LevelUp()
        {
            try
            {
                token_source = new CancellationTokenSource();
                cancel_token = token_source.Token;

                PressButton(ButtonType.B);
                await Task.Delay(100);
                ReleaseButton(ButtonType.B);
                await Task.Delay(500);
                PressButton(ButtonType.B);
                await Task.Delay(100);
                ReleaseButton(ButtonType.B);
                await Task.Delay(500);
                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }
                PressButton(ButtonType.X);
                await Task.Delay(100);
                ReleaseButton(ButtonType.X);
                await Task.Delay(1050);
                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }
                PressButton(ButtonType.A);
                await Task.Delay(100);
                ReleaseButton(ButtonType.A);
                await Task.Delay(1800);
                for (int i = 0; i < 4; i++)
                {
                    PressButton(ButtonType.RIGHT);
                    await Task.Delay(100);
                    ReleaseButton(ButtonType.RIGHT);
                    await Task.Delay(100);
                }
                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }
                PressButton(ButtonType.A);
                await Task.Delay(100);
                ReleaseButton(ButtonType.A);
                await Task.Delay(500);
                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }
                PressButton(ButtonType.A);
                await Task.Delay(100);
                ReleaseButton(ButtonType.A);
                await Task.Delay(500);
                PressButton(ButtonType.DOWN);
                await Task.Delay(100);
                ReleaseButton(ButtonType.DOWN);
                await Task.Delay(500);
                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }
                PressButton(ButtonType.A);
                await Task.Delay(100);
                ReleaseButton(ButtonType.A);
                await Task.Delay(1000);
                PressButton(ButtonType.LEFT);
                await Task.Delay(100);
                ReleaseButton(ButtonType.LEFT);
                await Task.Delay(500);
                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }
                PressButton(ButtonType.A);
                await Task.Delay(100);
                ReleaseButton(ButtonType.A);
                await Task.Delay(1000);
                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }
                PressButton(ButtonType.A);
                await Task.Delay(100);
                ReleaseButton(ButtonType.A);
                await Task.Delay(500);
                PressButton(ButtonType.B);
                await Task.Delay(100);
                ReleaseButton(ButtonType.B);
                await Task.Delay(500);
                PressButton(ButtonType.B);
                await Task.Delay(100);
                ReleaseButton(ButtonType.B);
                await Task.Delay(500);
                PressButton(ButtonType.B);
                await Task.Delay(100);
                ReleaseButton(ButtonType.B);
                await Task.Delay(500);

            }
            catch (Exception exception)
            {
                throw;
            }
        }


        /// <summary>
        /// 2体目のステータスを表示する
        /// </summary>
        private async Task DisplayStatus()
        {
            try
            {
                InputLock();
                token_source = new CancellationTokenSource();
                cancel_token = token_source.Token;
                for (int i = 0; i < 3; i++)
                {
                    if (cancel_token.IsCancellationRequested)
                    {
                        throw new Exception();
                    }
                    PressButton(ButtonType.B);
                        await Task.Delay(100);
                        ReleaseButton(ButtonType.B);
                        await Task.Delay(1000);
                 }
                PressButton(ButtonType.X);
                await Task.Delay(100);
                ReleaseButton(ButtonType.X);
                await Task.Delay(1050);
                PressButton(ButtonType.RIGHT);
                await Task.Delay(100);
                ReleaseButton(ButtonType.RIGHT);
                await Task.Delay(800);
                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }
                PressButton(ButtonType.A);
                await Task.Delay(100);
                ReleaseButton(ButtonType.A);
                await Task.Delay(1300);
                PressButton(ButtonType.DOWN);
                await Task.Delay(100);
                ReleaseButton(ButtonType.DOWN);
                await Task.Delay(500);
                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }
                PressButton(ButtonType.A);
                await Task.Delay(100);
                ReleaseButton(ButtonType.A);
                await Task.Delay(500);
                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }
                PressButton(ButtonType.A);
                await Task.Delay(100);
                ReleaseButton(ButtonType.A);
                await Task.Delay(1400);
                for (int i = 0; i < 3; i++)
                {
                    PressButton(ButtonType.RIGHT);
                    await Task.Delay(100);
                    ReleaseButton(ButtonType.RIGHT);
                    await Task.Delay(300);
                }
            }
            catch (Exception exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Aボタン連打
        /// </summary>
        private async Task RepeatA()
        {
            try
            {
                token_source = new CancellationTokenSource();
                cancel_token = token_source.Token;
                while (true)
                {
                    if (cancel_token.IsCancellationRequested)
                    {
                        throw new Exception();
                    }
                    PressButton(ButtonType.A);
                    await Task.Delay(100);
                    ReleaseButton(ButtonType.A);
                    await Task.Delay(500);
                }
            }
            catch (Exception exception)
            {
                throw;
            }

        }
        /// <summary>
        /// Bボタン連打
        /// </summary>
        private async Task RepeatB()
        {
            try
            {
                token_source = new CancellationTokenSource();
                cancel_token = token_source.Token;
                while (true)
                {
                    if (cancel_token.IsCancellationRequested)
                    {
                        throw new Exception();
                    }
                    PressButton(ButtonType.B);
                    await Task.Delay(100);
                    ReleaseButton(ButtonType.B);
                    await Task.Delay(500);
                }
            }
            catch (Exception exception)
            {
                throw;
            }

        }

        /// <summary>
        /// ひとりでレイド開始
        /// </summary>
        private async Task StartRaidSelf_Click()
        {
            token_source = new CancellationTokenSource();
            cancel_token = token_source.Token;
            try
            {
                PressButton(ButtonType.DOWN);
                await Task.Delay(100);
                ReleaseButton(ButtonType.DOWN);
                await Task.Delay(200);
                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }
                PressButton(ButtonType.A);
                await Task.Delay(100);
                ReleaseButton(ButtonType.A);
                await Task.Delay(500);
                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }
                PressButton(ButtonType.A);
                await Task.Delay(100);
                ReleaseButton(ButtonType.A);
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        /// <summary>
        /// みんなでレイド開始
        /// </summary>
        private async Task StartRaid_Click()
        {
            try
            {
                token_source = new CancellationTokenSource();
                cancel_token = token_source.Token;
                PressButton(ButtonType.A);
                await Task.Delay(100);
                ReleaseButton(ButtonType.A);
                await Task.Delay(3500);
                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }
                PressButton(ButtonType.UP);
                await Task.Delay(100);
                ReleaseButton(ButtonType.UP);
                await Task.Delay(100);
                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }
                PressButton(ButtonType.A);
                await Task.Delay(100);
                ReleaseButton(ButtonType.A);
                }
            catch (Exception exception)
            {
                throw;
            }
        }

        //△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△

        /// 
        /// いろいろ周回
        /// 

        /// <summary>
        /// バトルタワー周回
        /// </summary>
        private async Task BattleTower()
        {
            token_source = new CancellationTokenSource();
            cancel_token = token_source.Token;
            try
            {
                while (true)
                {

                    PressButton(ButtonType.A);
                    await Task.Delay(25);
                    ReleaseButton(ButtonType.A);
                    await Task.Delay(500);//1
                    if (cancel_token.IsCancellationRequested)
                    {
                        throw new Exception();
                    }
                    PressButton(ButtonType.A);
                    await Task.Delay(25);
                    ReleaseButton(ButtonType.A);
                    await Task.Delay(2500);//2
                    if (cancel_token.IsCancellationRequested)
                    {
                        throw new Exception();
                    }
                    PressButton(ButtonType.A);
                    await Task.Delay(25);
                    ReleaseButton(ButtonType.A);
                    if (cancel_token.IsCancellationRequested)
                    {
                        throw new Exception();
                    }
                    await Task.Delay(500);//3
                    PressButton(ButtonType.A);
                    await Task.Delay(25);
                    ReleaseButton(ButtonType.A);
                    await Task.Delay(500);//4
                    PressButton(ButtonType.UP);
                    await Task.Delay(25);
                    ReleaseButton(ButtonType.UP);
                    await Task.Delay(500);//5
                    if (cancel_token.IsCancellationRequested)
                    {
                        throw new Exception();
                    }
                    PressButton(ButtonType.A);
                    await Task.Delay(25);
                    ReleaseButton(ButtonType.A);
                    await Task.Delay(2500);//6
                    PressButton(ButtonType.A);
                    if (cancel_token.IsCancellationRequested)
                    {
                        throw new Exception();
                    }
                    await Task.Delay(25);
                    ReleaseButton(ButtonType.A);
                    await Task.Delay(500);//7
                    PressButton(ButtonType.UP);
                    await Task.Delay(25);
                    ReleaseButton(ButtonType.UP);
                    await Task.Delay(500);//8
                    for (int i = 0; i < 8; i++)
                    {

                        if (cancel_token.IsCancellationRequested)
                        {
                            throw new Exception();
                        }
                        PressButton(ButtonType.A);
                        await Task.Delay(25);
                        ReleaseButton(ButtonType.A);
                        await Task.Delay(500);//9-16
                    }
                    for (int i = 0; i < 3; i++)
                    {

                        if (cancel_token.IsCancellationRequested)
                        {
                            throw new Exception();
                        }
                        PressButton(ButtonType.B);
                        await Task.Delay(25);
                        ReleaseButton(ButtonType.B);
                        await Task.Delay(500);//17-19
                    }
                    PressButton(ButtonType.UP);
                    await Task.Delay(25);
                    ReleaseButton(ButtonType.UP);
                    await Task.Delay(500);//8


                    /*
                                        for (int i = 0; i < 4; i++)
                                        {
                                            if (cancel_token.IsCancellationRequested)
                                            {
                                                return;
                                            }
                                            PressButton(ButtonType.A);
                                            await Task.Delay(50);
                                            ReleaseButton(ButtonType.A);
                                            await Task.Delay(1500);
                                        }
                                        PressButton(ButtonType.UP);
                                        await Task.Delay(50);
                                        ReleaseButton(ButtonType.UP);
                                        await Task.Delay(1500);
                                        for (int i = 0; i < 2; i++)
                                        {
                                            if (cancel_token.IsCancellationRequested)
                                            {
                                                return;
                                            }
                                            PressButton(ButtonType.A);
                                            await Task.Delay(50);
                                            ReleaseButton(ButtonType.A);
                                            await Task.Delay(1500);
                                        }
                                        PressButton(ButtonType.UP);
                                        await Task.Delay(50);
                                        ReleaseButton(ButtonType.UP);
                                        await Task.Delay(1500);
                                        for (int i = 0; i < 4; i++)
                                        {
                                            if (cancel_token.IsCancellationRequested)
                                            {
                                                return;
                                            }
                                            PressButton(ButtonType.A);
                                            await Task.Delay(50);
                                            ReleaseButton(ButtonType.A);
                                            await Task.Delay(1500);
                                        }
                                        PressButton(ButtonType.B);
                                        await Task.Delay(50);
                                        ReleaseButton(ButtonType.B);
                                        await Task.Delay(1500);
                                        PressButton(ButtonType.UP);
                                        await Task.Delay(50);
                                        ReleaseButton(ButtonType.UP);
                                        await Task.Delay(1500);
                                        PressButton(ButtonType.R);
                                        await Task.Delay(50);
                                        ReleaseButton(ButtonType.R);
                                        await Task.Delay(50);
                                        PressButton(ButtonType.R);
                                        await Task.Delay(50);
                                        ReleaseButton(ButtonType.R);
                                        await Task.Delay(1500);*/

                }
            }
            catch (Exception exception)
            {
                throw;
            }
        }
        /// <summary>
        /// ワイルドエリアの預け屋の前に移動
        /// </summary>
        private async Task EggFirstPoint()
        {
            token_source = new CancellationTokenSource();
            cancel_token = token_source.Token;
            try
            {
                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }
                PressButton(ButtonType.X);
                await Task.Delay(50);
                ReleaseButton(ButtonType.X);
                await Task.Delay(1100);
                PressButton(ButtonType.A);
                await Task.Delay(50);
                ReleaseButton(ButtonType.A);
                await Task.Delay(3850);

                if (cancel_token.IsCancellationRequested)
                {
                    throw new Exception();
                }
                PressButton(ButtonType.A);
                await Task.Delay(50);
                ReleaseButton(ButtonType.A);
                await Task.Delay(570);
                PressButton(ButtonType.A);
                await Task.Delay(50);
                ReleaseButton(ButtonType.A);
                await Task.Delay(2100);
            }
            catch (Exception exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 孵化周回
        /// </summary>
        private async Task Eggs(decimal cycle, decimal box, object sender, EventArgs e)
        {
            token_source = new CancellationTokenSource();
            cancel_token = token_source.Token;
            int boxCount = 0;
            int line = 0;
            int eggCount = 0;
            try
            {
                InputLock();
                label_Eggcount.Text = "孵化数：" + eggCount;
                while (boxCount < box)
                {
                    //手持ちの空き分繰り返す

                    for (int j = 0; j < 5; j++)
                    {
                        if (cancel_token.IsCancellationRequested)
                        {
                            throw new Exception();
                        }
                        await Task.Delay(50);
                        await EggFirstPoint();
                        if (cancel_token.IsCancellationRequested)
                        {
                            throw new Exception();
                        }
                        //預け屋に話しかける
                        MoveStick(ButtonType.LSTICK, Stick.CENTER, Stick.MAX);
                        await Task.Delay(400);
                        MoveStick(ButtonType.LSTICK, Stick.MAX, Stick.MAX);
                        await Task.Delay(500);
                        MoveStick(ButtonType.LSTICK, Stick.CENTER, Stick.CENTER);
                        PressButton(ButtonType.A);
                        for (int i = 0; i < 4; i++)
                        {
                            if (cancel_token.IsCancellationRequested)
                            {
                                throw new Exception();
                            }
                            PressButton(ButtonType.A);
                            await Task.Delay(50);
                            ReleaseButton(ButtonType.A);
                            await Task.Delay(500);
                        }
                        await Task.Delay(3000);
                        for (int i = 0; i < 7; ++i)
                        {

                            if (cancel_token.IsCancellationRequested)
                            {
                                throw new Exception();
                            }
                            PressButton(ButtonType.B);
                            await Task.Delay(100);
                            ReleaseButton(ButtonType.B);
                            await Task.Delay(500);
                        }
                        await EggFirstPoint();
                        if (cancel_token.IsCancellationRequested)
                        {
                            throw new Exception();
                        }
                        //走る前の位置調整
                        MoveStick(ButtonType.LSTICK, Stick.MAX, Stick.CENTER);
                        await Task.Delay(600);
                        MoveStick(ButtonType.LSTICK, Stick.MAX, Stick.MIN);
                        await Task.Delay(1500);                            
                        if (cancel_token.IsCancellationRequested)
                        {
                            throw new Exception();
                        }
                        //孵化まで走る
                        MoveStick(ButtonType.LSTICK, Stick.MIN, Stick.MIN);
                        MoveStick(ButtonType.RSTICK, Stick.MIN, Stick.MIN);
                        await Task.Delay((int)((int)cycle * 3900));
                        MoveStick(ButtonType.LSTICK, Stick.CENTER, Stick.CENTER);
                        MoveStick(ButtonType.RSTICK, Stick.CENTER, Stick.CENTER);
                        //孵化表示クリア
                        for (int i = 0; i < 100; ++i)
                        {
                            if (cancel_token.IsCancellationRequested)
                            {
                                throw new Exception();
                            }
                            PressButton(ButtonType.B);
                            await Task.Delay(50);
                            ReleaseButton(ButtonType.B);
                            await Task.Delay(100);
                        }
                        eggCount++;
                        label_Eggcount.Text = "孵化数：" + eggCount;
                        await Task.Delay(2000);
                    }
                    //手持ちがいっぱいになったらボックスへ
                    PressButton(ButtonType.X);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.X);
                    await Task.Delay(1050);
                    PressButton(ButtonType.UP);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.UP);
                    await Task.Delay(100);
                    PressButton(ButtonType.RIGHT);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.RIGHT);
                    await Task.Delay(100);
                    if (cancel_token.IsCancellationRequested)
                    {
                        throw new Exception();
                    }
                    PressButton(ButtonType.A);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.A);
                    await Task.Delay(1800);
                    PressButton(ButtonType.R);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.R);
                    await Task.Delay(2000);
                    if (cancel_token.IsCancellationRequested)
                    {
                        throw new Exception();
                    }
                    PressButton(ButtonType.LEFT);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.LEFT);
                    await Task.Delay(100);
                    PressButton(ButtonType.DOWN);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.DOWN);
                    for (int i = 0; i < 2; ++i)
                    {
                        if (cancel_token.IsCancellationRequested)
                        {
                            throw new Exception();
                        }
                        PressButton(ButtonType.Y);
                        await Task.Delay(50);
                        ReleaseButton(ButtonType.Y);
                        await Task.Delay(100);
                    }
                    await Task.Delay(100);
                    PressButton(ButtonType.A);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.A);
                    PressButton(ButtonType.DOWN);
                    await Task.Delay(1000);
                    ReleaseButton(ButtonType.DOWN);
                    if (cancel_token.IsCancellationRequested)
                    {
                        throw new Exception();
                    }
                    PressButton(ButtonType.A);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.A);
                    await Task.Delay(100);
                    PressButton(ButtonType.RIGHT);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.RIGHT);
                    await Task.Delay(100);
                    PressButton(ButtonType.UP);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.UP);
                    await Task.Delay(100);

                    if (line >= 6)
                    {
                        PressButton(ButtonType.R);
                        await Task.Delay(50);
                        ReleaseButton(ButtonType.R);
                        await Task.Delay(500);
                        line = 0;
                        boxCount++;
                    }
                    if (line < 6)
                    {
                        for (int i = 0; i < line; i++)
                        {
                            PressButton(ButtonType.RIGHT);
                            await Task.Delay(50);
                            ReleaseButton(ButtonType.RIGHT);
                            await Task.Delay(100);
                        }
                        line++;
                    }
                    if (cancel_token.IsCancellationRequested)
                    {
                        throw new Exception();
                    }
                    PressButton(ButtonType.A);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.A);
                    await Task.Delay(500);
                    for (int i = 0; i < 2; ++i)
                    {
                        if (cancel_token.IsCancellationRequested)
                        {
                            throw new Exception();
                        }
                        PressButton(ButtonType.B);
                        await Task.Delay(50);
                        ReleaseButton(ButtonType.B);
                        await Task.Delay(1500);
                    }
                    PressButton(ButtonType.LEFT);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.LEFT);
                    await Task.Delay(100);
                    PressButton(ButtonType.DOWN);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.DOWN);
                    await Task.Delay(100);
                    PressButton(ButtonType.B);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.B);
                    await Task.Delay(1500);
                }
                InputUnLock();
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 自動レイド周回
        /// </summary>
        private async Task AutoRaid()
        {
            token_source = new CancellationTokenSource();
            cancel_token = token_source.Token;
            try
            {
                InputLock();
                while (true)
                {
                    if (cancel_token.IsCancellationRequested)
                    {
                        throw new Exception();
                    }
                    MoveStick(ButtonType.RSTICK, Stick.CENTER, Stick.MIN);
                    PressButton(ButtonType.LEFT);
                    await Task.Delay(20);
                    ReleaseButton(ButtonType.LEFT);
                    PressButton(ButtonType.A);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.A);
                    await Task.Delay(1000);
                }
            }
            catch (Exception exception)
            {
                throw;
            }
        }


        /// <summary>
        /// 掘り出しもの周回
        /// </summary>
        private async Task Horidashimono()
        {
            token_source = new CancellationTokenSource();
            cancel_token = token_source.Token;
            InputLock();
            try
            {
                while (true)
                {
                    await DisplayDate();

                    if (cancel_token.IsCancellationRequested)
                    {
                        return;
                    }
                    for (int j = 0; j < 2; ++j)
                    {
                        PressButton(ButtonType.DOWN);
                        await Task.Delay(50);
                        ReleaseButton(ButtonType.DOWN);
                        await Task.Delay(50);
                        if (cancel_token.IsCancellationRequested)
                        {
                            throw new Exception();
                        }
                    }

                    //Increase date
                    await IncreaseDate(true);

                    PressButton(ButtonType.HOME);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.HOME);
                    await Task.Delay(1000);

                    if (cancel_token.IsCancellationRequested)
                    {
                        throw new Exception();
                    }
                    PressButton(ButtonType.A);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.A);
                    await Task.Delay(500);

                    //話しかける
                    for (int i = 0; i < 10; i++)
                    {

                        if (cancel_token.IsCancellationRequested)
                        {
                            throw new Exception();
                        }
                        PressButton(ButtonType.A);
                        await Task.Delay(50);
                        ReleaseButton(ButtonType.A);
                        await Task.Delay(500);
                    }

                    for (int i = 0; i < 10; i++)
                    {

                        if (cancel_token.IsCancellationRequested)
                        {
                            throw new Exception();
                        }
                        PressButton(ButtonType.B);
                        await Task.Delay(50);
                        ReleaseButton(ButtonType.B);
                        await Task.Delay(500);
                    }
                }
            }
            catch (Exception exception)
            {
                throw;
            }

        }

        /// <summary>
        /// きのみ周回
        /// </summary>
        private async Task Kinomi()
        {
            token_source = new CancellationTokenSource();
            cancel_token = token_source.Token;
            InputLock();
            try { 
                while (cancel_token.IsCancellationRequested==false)
                {
                    await DisplayDate();

                    if (cancel_token.IsCancellationRequested)
                    {
                        throw new Exception();
                    }
                    for (int j = 0; j < 2; ++j)
                    {
                        PressButton(ButtonType.DOWN);
                        await Task.Delay(50);
                        ReleaseButton(ButtonType.DOWN);
                        await Task.Delay(50);
                        if (cancel_token.IsCancellationRequested)
                        {
                            throw new Exception();
                        }
                    }

                    //Increase date
                    await IncreaseDate(true);

                    PressButton(ButtonType.HOME);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.HOME);
                    await Task.Delay(1000);
                    if (cancel_token.IsCancellationRequested)
                    {
                        throw new Exception();
                    }
                    PressButton(ButtonType.A);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.A);
                    await Task.Delay(500);

                    //木を揺らす
                    for (int i = 0; i < 3; i++)
                    {
                        if (cancel_token.IsCancellationRequested)
                        {
                            throw new Exception();
                        }
                        PressButton(ButtonType.A);
                        await Task.Delay(50);
                        ReleaseButton(ButtonType.A);
                        await Task.Delay(500);
                    }
                    await Task.Delay(3000);
                    for (int i = 0; i < 10; i++)
                    {
                        if (cancel_token.IsCancellationRequested)
                        {
                            throw new Exception();
                        }
                        PressButton(ButtonType.B);
                        await Task.Delay(50);
                        ReleaseButton(ButtonType.B);
                        await Task.Delay(500);
                    }
                }
            }

            catch (Exception exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 無限ワット
        /// </summary>
        private async Task Wget()
        {
            token_source = new CancellationTokenSource();
            cancel_token = token_source.Token;
            try
            {
                while (true)
                {
                    if (cancel_token.IsCancellationRequested)
                    {
                        throw new Exception();
                    }
                    await IncreaseDateWithRaidHole();
                }
            }
            catch (Exception exception)
            {
                throw;
            }

        }
        /// <summary>
        /// 羽回収
        /// </summary>
        private async Task CollectFeathers()
        {
            token_source = new CancellationTokenSource();
            cancel_token = token_source.Token;
            try
            {

                while (true)
                {

                    await DisplayDate();


                    if (cancel_token.IsCancellationRequested)
                    {
                        throw new Exception();
                    }
                    for (int j = 0; j < 2; ++j)
                    {
                        PressButton(ButtonType.DOWN);
                        await Task.Delay(50);
                        ReleaseButton(ButtonType.DOWN);
                        await Task.Delay(50);
                    }

                    //Increase date
                    await IncreaseDate(true);

                    PressButton(ButtonType.HOME);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.HOME);
                    await Task.Delay(1000);

                    if (cancel_token.IsCancellationRequested)
                    {
                        throw new Exception();
                    }
                    PressButton(ButtonType.A);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.A);
                    await Task.Delay(500);

                    PressButton(ButtonType.X);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.X);
                    await Task.Delay(1050);

                    if (cancel_token.IsCancellationRequested)
                    {
                        throw new Exception();
                    }
                    PressButton(ButtonType.A);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.A);
                    await Task.Delay(3850);

                    if (cancel_token.IsCancellationRequested)
                    {
                        throw new Exception();
                    }
                    //育て屋の前へ
                    MoveStick(ButtonType.LSTICK, Stick.MAX, Stick.MIN);
                    await Task.Delay(50);
                    MoveStick(ButtonType.LSTICK, Stick.CENTER, Stick.CENTER);
                    //await Task.Delay(100);

                    for (int i = 0; i < 20; i++)
                    {

                        if (cancel_token.IsCancellationRequested)
                        {
                            throw new Exception();
                        }
                        PressButton(ButtonType.A);
                        await Task.Delay(50);
                        ReleaseButton(ButtonType.A);
                        await Task.Delay(50);
                    }
                    await Task.Delay(1800);

                    //1
                    MoveStick(ButtonType.LSTICK, 254, 134);
                    await Task.Delay(3035);
                    MoveStick(ButtonType.LSTICK, Stick.CENTER, Stick.CENTER);
                    for (int i = 0; i < 15; i++)
                    {

                        if (cancel_token.IsCancellationRequested)
                        {
                            throw new Exception();
                        }
                        PressButton(ButtonType.A);
                        await Task.Delay(50);
                        ReleaseButton(ButtonType.A);
                        await Task.Delay(50);
                    }

                    //2
                    MoveStick(ButtonType.LSTICK, 255, 80);
                    await Task.Delay(1900);
                    MoveStick(ButtonType.LSTICK, Stick.CENTER, Stick.CENTER);
                    for (int i = 0; i < 15; i++)
                    {

                        if (cancel_token.IsCancellationRequested)
                        {
                            throw new Exception();
                        }
                        PressButton(ButtonType.A);
                        await Task.Delay(50);
                        ReleaseButton(ButtonType.A);
                        await Task.Delay(50);
                    }


                    //3

                    MoveStick(ButtonType.LSTICK, 254, 155);
                    await Task.Delay(1558);
                    MoveStick(ButtonType.LSTICK, Stick.CENTER, Stick.CENTER);
                    for (int i = 0; i < 15; i++)
                    {

                        if (cancel_token.IsCancellationRequested)
                        {
                            throw new Exception();
                        }
                        PressButton(ButtonType.A);
                        await Task.Delay(50);
                        ReleaseButton(ButtonType.A);
                        await Task.Delay(50);
                    }

                    //4
                    MoveStick(ButtonType.LSTICK, 255, 167);
                    await Task.Delay(1325);
                    MoveStick(ButtonType.LSTICK, Stick.CENTER, Stick.CENTER);
                    for (int i = 0; i < 15; i++)
                    {

                        if (cancel_token.IsCancellationRequested)
                        {
                            throw new Exception();
                        }
                        PressButton(ButtonType.A);
                        await Task.Delay(50);
                        ReleaseButton(ButtonType.A);
                        await Task.Delay(50);
                    }

                    //5
                    MoveStick(ButtonType.LSTICK, 255, 35);
                    await Task.Delay(822);
                    MoveStick(ButtonType.LSTICK, Stick.CENTER, Stick.CENTER);
                    for (int i = 0; i < 15; i++)
                    {

                        if (cancel_token.IsCancellationRequested)
                        {
                            throw new Exception();
                        }
                        PressButton(ButtonType.A);
                        await Task.Delay(50);
                        ReleaseButton(ButtonType.A);
                        await Task.Delay(50);
                    }

                    //6
                    MoveStick(ButtonType.LSTICK, 255, 152);
                    await Task.Delay(2651);
                    MoveStick(ButtonType.LSTICK, Stick.CENTER, Stick.CENTER);
                    for (int i = 0; i < 15; i++)
                    {

                        if (cancel_token.IsCancellationRequested)
                        {
                            throw new Exception();
                        }
                        PressButton(ButtonType.A);
                        await Task.Delay(50);
                        ReleaseButton(ButtonType.A);
                        await Task.Delay(50);
                    }

                    //7
                    MoveStick(ButtonType.LSTICK, 255, 60);
                    await Task.Delay(1792);
                    MoveStick(ButtonType.LSTICK, Stick.CENTER, Stick.CENTER);
                    for (int i = 0; i < 15; i++)
                    {

                        if (cancel_token.IsCancellationRequested)
                        {
                            throw new Exception();
                        }
                        PressButton(ButtonType.A);
                        await Task.Delay(50);
                        ReleaseButton(ButtonType.A);
                        await Task.Delay(50);
                    }


                    //8
                    MoveStick(ButtonType.LSTICK, 255, 205);
                    await Task.Delay(1900);
                    MoveStick(ButtonType.LSTICK, Stick.CENTER, Stick.CENTER);
                    for (int i = 0; i < 15; i++)
                    {

                        if (cancel_token.IsCancellationRequested)
                        {
                            throw new Exception();
                        }
                        PressButton(ButtonType.A);
                        await Task.Delay(50);
                        ReleaseButton(ButtonType.A);
                        await Task.Delay(50);
                    }

                    //9
                    MoveStick(ButtonType.LSTICK, 255, 80);
                    await Task.Delay(1885);
                    MoveStick(ButtonType.LSTICK, Stick.CENTER, Stick.CENTER);
                    for (int i = 0; i < 15; i++)
                    {

                        if (cancel_token.IsCancellationRequested)
                        {
                            throw new Exception();
                        }
                        PressButton(ButtonType.A);
                        await Task.Delay(50);
                        ReleaseButton(ButtonType.A);
                        await Task.Delay(50);
                    }

                    //10
                    MoveStick(ButtonType.LSTICK, 255, 205);
                    await Task.Delay(975);
                    MoveStick(ButtonType.LSTICK, Stick.CENTER, Stick.CENTER);
                    for (int i = 0; i < 15; i++)
                    {

                        if (cancel_token.IsCancellationRequested)
                        {
                            throw new Exception();
                        }
                        PressButton(ButtonType.A);
                        await Task.Delay(50);
                        ReleaseButton(ButtonType.A);
                        await Task.Delay(50);
                    }
                }
            }
            catch (Exception exception)
            {
                throw;
            }
        }
        /// <summary>
        /// IDくじ周回
        /// </summary>
        private async Task IDkuji()
        {
            token_source = new CancellationTokenSource();
            cancel_token = token_source.Token;
            InputLock();
            try
            {
                while (cancel_token.IsCancellationRequested == false)
                {
                    await DisplayDate();

                    if (cancel_token.IsCancellationRequested)
                    {
                        throw new Exception();
                    }
                    for (int j = 0; j < 2; ++j)
                    {
                        PressButton(ButtonType.DOWN);
                        await Task.Delay(50);
                        ReleaseButton(ButtonType.DOWN);
                        await Task.Delay(50);
                        if (cancel_token.IsCancellationRequested)
                        {
                            throw new Exception();
                        }
                    }

                    //Increase date
                    await IncreaseDate(true);

                    PressButton(ButtonType.HOME);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.HOME);
                    await Task.Delay(1000);
                    if (cancel_token.IsCancellationRequested)
                    {
                        throw new Exception();
                    }
                    PressButton(ButtonType.A);
                    await Task.Delay(50);
                    ReleaseButton(ButtonType.A);
                    await Task.Delay(500);

                    //ロトミに話しかける
                    for (int i = 0; i < 2; i++)
                    {
                        if (cancel_token.IsCancellationRequested)
                        {
                            throw new Exception();
                        }
                        PressButton(ButtonType.A);
                        await Task.Delay(100);
                        ReleaseButton(ButtonType.A);
                        await Task.Delay(1300);
                    }//A2回
                    PressButton(ButtonType.DOWN);
                    await Task.Delay(100);
                    ReleaseButton(ButtonType.DOWN);
                    await Task.Delay(500);

                    for (int i = 0; i < 20; i++)
                    {
                        if (cancel_token.IsCancellationRequested)
                        {
                            throw new Exception();
                        }
                        PressButton(ButtonType.A);
                        await Task.Delay(50);
                        ReleaseButton(ButtonType.A);
                        await Task.Delay(250);
                    }//A2回
                    for (int i = 0; i < 35; i++)
                    {
                        if (cancel_token.IsCancellationRequested)
                        {
                            throw new Exception();
                        }
                        PressButton(ButtonType.B);
                        await Task.Delay(50);
                        ReleaseButton(ButtonType.B);
                        await Task.Delay(250);
                    }//A2回


                }
            }
            catch (Exception exception)
            {
                throw;
            }

        }

        //△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△

        ///
        ///各種フォームボタンへのメソッド割り当て　
        ///


        private async void Button_machineDateToday_click(object sender, EventArgs e)
        {
            try
            {
                InputLock();
                await MachineDateChangeToday();
            }
            catch { }
            finally {
                InputUnLock();
            }
        }
        private async void Button_machineDateReset_Click(object sender, EventArgs e)
        {
            try
            {
                InputLock();
            await DayReset();
            }
            catch { }
            finally
            {
                InputUnLock();
            }
        }
        private async void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                InputLock();
                await DayInputChange();
            }
            catch { }
            finally
            {
                InputUnLock();
            }
        }
        private async void Button_chanselAndSave_Click(object sender, EventArgs e)
        {
            try
            {
                InputLock();
                await ChanselAndSave();
            }
            catch { }
            finally
            {
                InputUnLock();
            }
        }
        private async void Button_plusNDays_Click(object sender, EventArgs e)
        {
            try
            {
                InputLock();
                Invoke(new UpdateCountDelegate(UpdateCount), -1, int.Parse(DayTextbox.Text));
                await PlusNDaysFast(sender, e, int.Parse(DayTextbox.Text));
            }
            catch { }
            finally
            {
                InputUnLock();
            }
        }
        private async void Button_plus1Day_Click(object sender, EventArgs e)
        {
            try
            {
                InputLock();
                Invoke(new UpdateCountDelegate(UpdateCount), -1, 1);
                await PlusNDays(1);
            }
            catch { }
            finally
            {
                InputUnLock();
            }
        }
        private async void Button_plus3Days_Click(object sender, EventArgs e)
        {
            try
            {
                InputLock();
                Invoke(new UpdateCountDelegate(UpdateCount), -1, 3);
                await PlusNDays(3);
            }
            catch { }
            finally
            {
                InputUnLock();
            }
        }
        private async void Button_levelUp_Click(object sender, EventArgs e)
        {
            try
            {
                InputLock();
                await LevelUp();
            }
            catch { }
            finally
            {
                InputUnLock();
            }
        }
        private async void Button_repeatA_Click(object sender, EventArgs e)
        {
            try
            {
                InputLock();
                await RepeatA();
            }
            catch { }
            finally
            {
                InputUnLock();
            }            
        }
        private async void Button_repeatB_Click(object sender, EventArgs e)
        {
            try
            {
                InputLock();
                await RepeatB();
            }
            catch { }
            finally
            {
                InputUnLock();
            }
        }
        private async void Button_displayStatus_Click(object sender, EventArgs e)
        {
            try
            {
                InputLock();
             await DisplayStatus();
            }
            catch { }
            finally
            {
                InputUnLock();
            }
        }
        private async void Button_resetSlidPlus3Days_Click(object sender, EventArgs e)
        {
            try
            {
                InputLock();
                Invoke(new UpdateCountDelegate(UpdateCount), -1, 3);
                await ResetStartNdays(1);
                await ChanselAndSave();
                await PlusNDays(3);
            }
            catch { }
            finally
            {
                InputUnLock();
            }
        }
        private async void Button_resetPlus3Days_Click(object sender, EventArgs e)
        {
            try
            {
                InputLock();
                Invoke(new UpdateCountDelegate(UpdateCount), -1, 3);
                await ResetStartNdays(3);
            }
            catch { }
            finally
            {
                InputUnLock();
            }
        }
        private async void Button_resetPlus4Days_Click(object sender, EventArgs e)
        {
            try
            {
                InputLock();
                Invoke(new UpdateCountDelegate(UpdateCount), -1, 4);
                await ResetStartNdays(4);
            }
            catch { }
            finally
            {
                InputUnLock();
            }
        }
        private async void Button_resetPlus5Days_Click(object sender, EventArgs e)
        {
            try
            {
                InputLock();
                Invoke(new UpdateCountDelegate(UpdateCount), -1, 5);
                await ResetStartNdays(5);
            }
            catch { }
            finally
            {
                InputUnLock();
            }
        }
        private async void Button_startRaid_Click(object sender, EventArgs e)
        {
            try
            {
                InputLock();
                await StartRaid_Click();
            }
            catch { }
            finally
            {
                InputUnLock();
            }
        }
        private async void Button_startRaidSelf_Click(object sender, EventArgs e)
        {
            try
            {
                InputLock();
                await StartRaidSelf_Click();
            }
            catch { }
            finally
            {
                InputUnLock();
            }
            
        }
        private async void Button_dmax1_Click(object sender, EventArgs e)
        {
            try
            {
                InputLock();
                await Battle_dmax1(sender, e);
            }
            catch { }
            finally
            {
                InputUnLock();
            }
            
        }
        private async void Button_dmax2_Click(object sender, EventArgs e)
        {
            try
            {
                InputLock();
                await Battle_dmax2(sender, e);
            }
            catch { }
            finally
            {
                InputUnLock();
            }
        }
        private void Button_ControllerChange(object sender, EventArgs e)
        {
            try
            {
                InputLock();
                ControllerChange();
            }
            catch { }
            finally
            {
                InputUnLock();
            }
        }
        private async void Button_IDkuji_Click(object sender, EventArgs e)
        {
            try
            {
                InputLock();
                await IDkuji();
            }
            catch { }
            finally
            {
                InputUnLock();
            }
        }
        private async void Button_collectFeathers_Click(object sender, EventArgs e)
        {
            try
            {
                InputLock();
                await CollectFeathers();
            }
            catch { }
            finally
            {
                InputUnLock();
            }
        }
        private async void Button_Wget_Click(object sender, EventArgs e)
        {
            try
            {
                InputLock();
                await Wget();
            }
            catch { }
            finally
            {
                InputUnLock();
            }
        }
        private async void Button_Kinomi_Click(object sender, EventArgs e)
        {
            try
            {
                InputLock();
                await Kinomi();
            }
            catch { }
            finally
            {
                InputUnLock();
            }
        }
        private async void Button_BattleTower_Click(object sender, EventArgs e)
        {
            try
            {
                InputLock();
                await BattleTower();
            }
            catch { }
            finally
            {
                InputUnLock();
            }
        }
        private async void Button_AutoRaid_Click(object sender, EventArgs e)
        {
            try
            {
                InputLock();
                await AutoRaid();
            }
            catch { }
            finally
            {
                InputUnLock();
            }
        }
        private async void Button_Horidashimono_Click(object sender, EventArgs e)
        {
            try
            {
                InputLock();
                await Horidashimono();
            }
            catch { }
            finally
            {
                InputUnLock();
            }
        }
        private async void Button_Eggs_Click(object sender, EventArgs e)
        {
            try
            {
                InputLock();
                await Eggs(numericUpDown_Cycle.Value, numericUpDown_Box.Value, sender, e);
            }
            catch { }
            finally
            {
                InputUnLock();
            }
        }
        //△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△
                    
        /// <summary>
        /// マクロ停止メソッド
        /// </summary>
        private void Button_stop_Click(object sender, EventArgs e)
        {
            try
            {
                Invoke(new UpdateCalendarDelegate(UpdateCalendar));
                InputUnLock();
                this.token_source.Cancel();
                this.token_source.Cancel();
                this.token_source.Cancel();
                this.token_source.Cancel();
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// コントローラの選択が変わった時のメソッド
        /// </summary>
        private void ControllerChange()
        {
            if (button_xInput.Checked == true)
            {
                array[0] = ButtonType.B;
                array[1] = ButtonType.A;
                array[2] = ButtonType.Y;
                array[3] = ButtonType.X;
                array[4] = ButtonType.L;
                array[5] = ButtonType.R;
                array[6] = ButtonType.HOME;
                array[7] = ButtonType.PLUS;
                array[8] = ButtonType.LCLICK;
                array[9] = ButtonType.RCLICK;
                array[10] = ButtonType.B;
                array[11] = ButtonType.B;
                array[12] = ButtonType.B;
                array[13] = ButtonType.B;
                array[14] = ButtonType.CAPTURE;
            }
            else
            {
                array[0] = ButtonType.Y;
                array[1] = ButtonType.B;
                array[2] = ButtonType.A;
                array[3] = ButtonType.X;
                array[4] = ButtonType.L;
                array[5] = ButtonType.R;
                array[6] = ButtonType.ZL;
                array[7] = ButtonType.ZR;
                array[8] = ButtonType.HOME;
                array[9] = ButtonType.PLUS;
                array[10] = ButtonType.LCLICK;
                array[11] = ButtonType.RCLICK;
                array[12] = ButtonType.B;
                array[13] = ButtonType.B;
                array[14] = ButtonType.B;
            }
        }


        //△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△△


        private void Keypressed(object sender, KeyEventArgs e)
        {

            e.Handled = true;
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Down:
                case Keys.Right:
                    this.SelectNextControl(
                      this.ActiveControl, true, true, true, true);
                    break;
                case Keys.Up:
                case Keys.Left:
                    this.SelectNextControl(
                      this.ActiveControl, false, true, true, true);
                    break;
                default:
                    return base.ProcessDialogKey(keyData);
            }
            return true;
        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}