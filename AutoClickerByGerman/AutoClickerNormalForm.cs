using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AutoClickerByGerman
{
    public partial class AutoClickerNormalForm : Form
    {
        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);

        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;

        private readonly System.Windows.Forms.Timer timerClicks;
        private readonly KeyboardHook keyboardHook;
        private bool autoClickActivo;

        public AutoClickerNormalForm()
        {
            InitializeComponent();

            timerClicks = new System.Windows.Forms.Timer();
            timerClicks.Interval = (int)numIntervaloMs.Value;
            timerClicks.Tick += TimerClicks_Tick;

            keyboardHook = new KeyboardHook();
            keyboardHook.KeyPressed += KeyboardHook_KeyPressed;
        }

        private void KeyboardHook_KeyPressed(Keys key)
        {
            if (key != Keys.D0 && key != Keys.NumPad0)
            {
                return;
            }

            if (IsDisposed || !IsHandleCreated)
            {
                return;
            }

            BeginInvoke((MethodInvoker)(() =>
            {
                if (autoClickActivo)
                {
                    DetenerAutoClick();
                }
                else
                {
                    IniciarAutoClick();
                }
            }));
        }

        private void TimerClicks_Tick(object? sender, EventArgs e)
        {
            RealizarClickIzquierdo();
        }

        private static void RealizarClickIzquierdo()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, UIntPtr.Zero);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, UIntPtr.Zero);
        }

        private void btnIniciarDetener_Click(object sender, EventArgs e)
        {
            if (autoClickActivo)
            {
                DetenerAutoClick();
                return;
            }

            IniciarAutoClick();
        }

        private void IniciarAutoClick()
        {
            timerClicks.Interval = (int)numIntervaloMs.Value;
            timerClicks.Start();
            autoClickActivo = true;
            btnIniciarDetener.Text = "Detener";
            lblEstado.Text = "Estado: activo";
            numIntervaloMs.Enabled = false;
        }

        private void DetenerAutoClick()
        {
            timerClicks.Stop();
            autoClickActivo = false;
            btnIniciarDetener.Text = "Iniciar";
            lblEstado.Text = "Estado: detenido";
            numIntervaloMs.Enabled = true;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void numIntervaloMs_ValueChanged(object sender, EventArgs e)
        {
            if (autoClickActivo)
            {
                timerClicks.Interval = (int)numIntervaloMs.Value;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            keyboardHook.KeyPressed -= KeyboardHook_KeyPressed;
            keyboardHook.Unhook();
            timerClicks.Stop();
            timerClicks.Dispose();
            base.OnFormClosing(e);
        }
    }
}
