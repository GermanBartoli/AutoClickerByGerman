using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace AutoClickerByGerman
{
    public partial class AqwForm : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool PostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern bool IsWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("kernel32.dll")]
        private static extern uint GetCurrentThreadId();

        [DllImport("user32.dll")]
        private static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

        [DllImport("user32.dll")]
        private static extern IntPtr GetFocus();

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowText(IntPtr hWnd, System.Text.StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern uint MapVirtualKey(uint uCode, uint uMapType);

        private const uint WM_KEYDOWN = 0x0100;
        private const uint WM_KEYUP = 0x0101;
        private const uint WM_ACTIVATE = 0x0006;
        private const uint WM_SETFOCUS = 0x0007;
        private const uint WA_ACTIVE = 1;
        private const int SW_RESTORE = 9;
        private const byte VK_1 = 0x31;
        private const byte VK_2 = 0x32;
        private const byte VK_3 = 0x33;
        private const byte VK_4 = 0x34;
        private const byte VK_5 = 0x35;

        private readonly byte[] secuenciaTeclas = { VK_1, VK_2, VK_3, VK_4, VK_5 };
        private readonly int intervaloEntreTeclas = 1500;
        private readonly int intervaloEntreCiclos = 1500;

        private Thread? hiloAutomatizacion;
        private bool automatizacionActiva;
        private IntPtr ventanaObjetivo = IntPtr.Zero;

        public AqwForm()
        {
            InitializeComponent();
        }

        private void btnCapturarVentana_Click(object sender, EventArgs e)
        {
            lblEstado.Text = "Estado: cambia a AQW (3s)...";
            btnCapturarVentana.Enabled = false;
            WindowState = FormWindowState.Minimized;

            Thread hiloCaptura = new Thread(() =>
            {
                Thread.Sleep(3000);
                IntPtr ventanaActiva = GetForegroundWindow();

                BeginInvoke((MethodInvoker)delegate
                {
                    if (WindowState == FormWindowState.Minimized)
                    {
                        WindowState = FormWindowState.Normal;
                    }

                    Show();
                    Activate();

                    btnCapturarVentana.Enabled = true;

                    if (ventanaActiva == IntPtr.Zero || ventanaActiva == Handle)
                    {
                        lblEstado.Text = "Estado: detenido";
                        MessageBox.Show(this, "No se capturó una ventana válida. Intenta de nuevo y cambia a AQW antes de que termine la cuenta.", "AQW", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    ventanaObjetivo = ventanaActiva;
                    string titulo = ObtenerTituloVentana(ventanaObjetivo);

                    if (string.IsNullOrWhiteSpace(titulo))
                    {
                        titulo = "(sin título)";
                    }

                    lblVentanaObjetivo.Text = $"Ventana objetivo: {titulo}";
                    lblEstado.Text = "Estado: detenido";
                });
            })
            {
                IsBackground = true
            };

            hiloCaptura.Start();
        }

        private void btnIniciarDetener_Click(object sender, EventArgs e)
        {
            AlternarAutomatizacion();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            automatizacionActiva = false;
            Close();
        }

        private void AlternarAutomatizacion()
        {
            if (automatizacionActiva)
            {
                automatizacionActiva = false;
                btnIniciarDetener.Text = "Iniciar";
                lblEstado.Text = "Estado: detenido";
                return;
            }

            if (ventanaObjetivo == IntPtr.Zero || !IsWindow(ventanaObjetivo))
            {
                MessageBox.Show(this, "Primero enfoca la ventana de AQW y pulsa 'Capturar ventana activa'.", "AQW", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            automatizacionActiva = true;
            btnIniciarDetener.Text = "Detener";
            lblEstado.Text = "Estado: ejecutando";

            hiloAutomatizacion = new Thread(EjecutarSecuenciaTeclas)
            {
                IsBackground = true
            };
            hiloAutomatizacion.Start();
        }

        private void EjecutarSecuenciaTeclas()
        {
            while (automatizacionActiva)
            {
                for (int indice = 0; indice < secuenciaTeclas.Length; indice++)
                {
                    byte tecla = secuenciaTeclas[indice];

                    if (!automatizacionActiva)
                    {
                        return;
                    }

                    if (!PresionarTeclaEnVentana(ventanaObjetivo, tecla))
                    {
                        BeginInvoke((MethodInvoker)delegate
                        {
                            automatizacionActiva = false;
                            btnIniciarDetener.Text = "Iniciar";
                            lblEstado.Text = "Estado: detenido";
                            MessageBox.Show(this, "La ventana objetivo ya no está disponible.", "AQW", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        });
                        return;
                    }

                    bool esUltimaTeclaDelCiclo = indice == secuenciaTeclas.Length - 1;
                    int pausa = esUltimaTeclaDelCiclo ? intervaloEntreCiclos : intervaloEntreTeclas;

                    for (int espera = 0; espera < pausa / 100; espera++)
                    {
                        if (!automatizacionActiva)
                        {
                            return;
                        }

                        Thread.Sleep(100);
                    }
                }
            }
        }

        private static bool PresionarTeclaEnVentana(IntPtr ventana, byte tecla)
        {
            if (!IsWindow(ventana))
            {
                return false;
            }

            if (IsIconic(ventana))
            {
                ShowWindowAsync(ventana, SW_RESTORE);
                Thread.Sleep(60);
            }

            uint scanCode = MapVirtualKey(tecla, 0);
            IntPtr lParamDown = (IntPtr)(1 | (scanCode << 16));
            IntPtr lParamUp = (IntPtr)(1 | (scanCode << 16) | (1u << 30) | (1u << 31));

            IntPtr destino = ObtenerDestinoEntrada(ventana);

            SendMessage(destino, WM_ACTIVATE, (IntPtr)WA_ACTIVE, IntPtr.Zero);
            SendMessage(destino, WM_SETFOCUS, IntPtr.Zero, IntPtr.Zero);

            bool keyDownOk = PostMessage(destino, WM_KEYDOWN, (IntPtr)tecla, lParamDown);
            bool keyUpOk = PostMessage(destino, WM_KEYUP, (IntPtr)tecla, lParamUp);

            if (keyDownOk && keyUpOk)
            {
                return true;
            }

            if (destino != ventana)
            {
                SendMessage(ventana, WM_ACTIVATE, (IntPtr)WA_ACTIVE, IntPtr.Zero);
                SendMessage(ventana, WM_SETFOCUS, IntPtr.Zero, IntPtr.Zero);

                bool keyDownFallbackOk = PostMessage(ventana, WM_KEYDOWN, (IntPtr)tecla, lParamDown);
                bool keyUpFallbackOk = PostMessage(ventana, WM_KEYUP, (IntPtr)tecla, lParamUp);
                return keyDownFallbackOk && keyUpFallbackOk;
            }

            return false;
        }

        private static IntPtr ObtenerDestinoEntrada(IntPtr ventanaPrincipal)
        {
            uint hiloObjetivo = GetWindowThreadProcessId(ventanaPrincipal, out _);
            uint hiloActual = GetCurrentThreadId();
            bool hilosAdjuntos = false;

            try
            {
                hilosAdjuntos = AttachThreadInput(hiloActual, hiloObjetivo, true);
                IntPtr focoObjetivo = GetFocus();

                if (focoObjetivo != IntPtr.Zero && IsWindow(focoObjetivo))
                {
                    return focoObjetivo;
                }
            }
            finally
            {
                if (hilosAdjuntos)
                {
                    AttachThreadInput(hiloActual, hiloObjetivo, false);
                }
            }

            return ventanaPrincipal;
        }

        private static string ObtenerTituloVentana(IntPtr ventana)
        {
            int longitud = GetWindowTextLength(ventana);
            if (longitud <= 0)
            {
                return string.Empty;
            }

            System.Text.StringBuilder titulo = new System.Text.StringBuilder(longitud + 1);
            GetWindowText(ventana, titulo, titulo.Capacity);
            return titulo.ToString();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            automatizacionActiva = false;
            base.OnFormClosing(e);
        }
    }
}
