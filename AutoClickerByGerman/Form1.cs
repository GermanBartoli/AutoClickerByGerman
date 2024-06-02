using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace AutoClickerByGerman
{
    public partial class Form1 : Form
    {
        // https://chatgpt.com/c/77c98c37-aa45-43a0-97a2-5c3ff0514bea
        // Declaración de un objeto KeyboardHook
        private KeyboardHook keyboardHook;

        // Key teclado
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, IntPtr dwExtraInfo);
        private const int KEYEVENTF_KEYDOWN = 0x0001;
        private const int KEYEVENTF_KEYUP = 0x0002;
        private const byte VK_O = 0x4F;

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
        private const byte VK_SHIFT = 0x10;

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;

        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;



        private Thread autoClickerThread;
        private bool isAutoClickerRunning = false;
        private int clickInterval;

        public Form1()
        {
            InitializeComponent();
            keyboardHook = new KeyboardHook();
            // Asociación del evento KeyPressed al método Form1_KeyPressed
            keyboardHook.KeyPressed += Form1_KeyPressed;
        }

        private void DoMouseClick()
        {
            //mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0); // Presionar el botón izquierdo
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0); // Presionar el botón izquierdo
            //mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
            //mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0); // Presionar el botón izquierdo
            
            // Simular presionar la tecla Shift
            keybd_event((byte)Keys.ShiftKey, 0, 0, UIntPtr.Zero);

            // Simular clic derecho del mouse
            keybd_event((byte)Keys.ShiftKey, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);


            //mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0); // Presionar el botón izquierdo
            //mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0); // Presionar el botón izquierdo

            //mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
            //keybd_event(VK_O, 0, KEYEVENTF_KEYDOWN, IntPtr.Zero);
            //keybd_event(VK_O, 0, KEYEVENTF_KEYUP, IntPtr.Zero);

        }

        // Método que se llama cuando se detecta una pulsación de tecla
        private void Form1_KeyPressed(Keys key)
        {
            // Verifica si la tecla pulsada es la tecla '0'
            if (key == Keys.D0)
            {
                ToggleAutoClicker();
            }
        }

        private void StartAutoClicker()
        {
            while (isAutoClickerRunning)
            {
                DoMouseClick();
                Thread.Sleep(clickInterval);
            }
        }

        private void buttonStartStop_Click(object sender, EventArgs e)
        {
            ToggleAutoClicker(); // Alternar el estado del autoclicker cuando se hace clic en el botón
        }

        private void ToggleAutoClicker()
        {
            if (isAutoClickerRunning)
            {
                isAutoClickerRunning = false;
                autoClickerThread.Join();
                btnStartStop.Text = "Start";
                labelStatus.Text = "AutoClicker stopped.";
            }
            else
            {
                if (int.TryParse(txtIntervaloClick.Text, out clickInterval))
                {
                    isAutoClickerRunning = true;
                    autoClickerThread = new Thread(StartAutoClicker);
                    autoClickerThread.IsBackground = true;
                    autoClickerThread.Start();
                    btnStartStop.Text = "Stop";
                    labelStatus.Text = "AutoClicker running...";
                }
                else
                {
                    MessageBox.Show("Please enter a valid interval.");
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
