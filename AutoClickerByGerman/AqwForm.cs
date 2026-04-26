using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace AutoClickerByGerman
{
    public partial class AqwForm : Form
    {
        public bool VolverAlMenuSolicitado { get; private set; }

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

        private readonly byte[] secuenciaAutoatack = { VK_1, VK_2, VK_3, VK_4, VK_5 };
        private readonly int intervaloAutoatackEntreTeclas = 1500;
        private readonly byte[] secuenciaVhl = { VK_3, VK_1, VK_2, VK_4, VK_5 };
        private readonly int[] cooldownsVhlMs = { 4000, 2000, 3000, 3000, 6000 };
        private readonly byte[] secuenciaRevenant = { VK_2, VK_1, VK_3, VK_4, VK_5 };
        private readonly int[] cooldownsRevenantMs = { 4000, 2000, 4000, 5000, 6000 };
        private readonly byte[] secuenciaYami = { VK_1, VK_2, VK_3, VK_4, VK_5 };
        private readonly int[] cooldownsYamiMs = { 2000, 5000, 2000, 4000, 4000 };
        private readonly int intervaloMinimoGlobalMs = 1500;

        private Thread? hiloAutomatizacion;
        private bool automatizacionActiva;
        private IntPtr ventanaObjetivo = IntPtr.Zero;
        private readonly object syncProgramacion = new object();

        private bool autoatackHabilitado;
        private bool vhlHabilitado;
        private bool revenantHabilitado;
        private bool yamiHabilitado;
        private bool actualizandoChecksModo;
        private int indiceAutoatackActual;
        private int indiceVhlSiguiente;
        private int indiceRevenantSiguiente;
        private int indiceYamiSiguiente;
        private DateTime proximoAutoatackUtc = DateTime.MinValue;
        private DateTime proximoVhlUtc = DateTime.MinValue;
        private DateTime proximoRevenantUtc = DateTime.MinValue;
        private DateTime proximoYamiUtc = DateTime.MinValue;
        private DateTime proximoEnvioGlobalUtc = DateTime.MinValue;

        private enum ModoDisparo
        {
            Ninguno,
            Autoatack,
            Vhl,
            Revenant,
            Yami
        }

        public AqwForm()
        {
            InitializeComponent();
            autoatackHabilitado = chkAutoatack.Checked;
            vhlHabilitado = chkVhl.Checked;
            revenantHabilitado = chkRevenant.Checked;
            yamiHabilitado = chkYami.Checked;
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
            VolverAlMenuSolicitado = true;
            Close();
        }

        private void chkAutoatack_CheckedChanged(object sender, EventArgs e)
        {
            lock (syncProgramacion)
            {
                autoatackHabilitado = chkAutoatack.Checked;

                if (autoatackHabilitado)
                {
                    indiceAutoatackActual = 0;
                    proximoAutoatackUtc = DateTime.UtcNow;
                }
            }
        }

        private void chkVhl_CheckedChanged(object sender, EventArgs e)
        {
            if (!actualizandoChecksModo && chkVhl.Checked)
            {
                ActualizarChecksModoExclusivo(chkVhl);
            }

            lock (syncProgramacion)
            {
                vhlHabilitado = chkVhl.Checked;

                if (vhlHabilitado)
                {
                    indiceVhlSiguiente = 0;
                    proximoVhlUtc = DateTime.UtcNow;
                }
            }
        }

        private void chkRevenant_CheckedChanged(object sender, EventArgs e)
        {
            if (!actualizandoChecksModo && chkRevenant.Checked)
            {
                ActualizarChecksModoExclusivo(chkRevenant);
            }

            lock (syncProgramacion)
            {
                revenantHabilitado = chkRevenant.Checked;

                if (revenantHabilitado)
                {
                    indiceRevenantSiguiente = 0;
                    proximoRevenantUtc = DateTime.UtcNow;
                }
            }
        }

        private void chkYami_CheckedChanged(object sender, EventArgs e)
        {
            if (!actualizandoChecksModo && chkYami.Checked)
            {
                ActualizarChecksModoExclusivo(chkYami);
            }

            lock (syncProgramacion)
            {
                yamiHabilitado = chkYami.Checked;

                if (yamiHabilitado)
                {
                    indiceYamiSiguiente = 0;
                    proximoYamiUtc = DateTime.UtcNow;
                }
            }
        }

        private void ActualizarChecksModoExclusivo(CheckBox checkSeleccionado)
        {
            actualizandoChecksModo = true;

            try
            {
                chkVhl.Checked = checkSeleccionado == chkVhl;
                chkRevenant.Checked = checkSeleccionado == chkRevenant;
                chkYami.Checked = checkSeleccionado == chkYami;
            }
            finally
            {
                actualizandoChecksModo = false;
            }
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

            lock (syncProgramacion)
            {
                if (!autoatackHabilitado && !vhlHabilitado && !revenantHabilitado && !yamiHabilitado)
                {
                    MessageBox.Show(this, "Activa al menos un modo: VHL, Revenant o Yami.", "AQW", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                ReiniciarProgramacion();
            }

            automatizacionActiva = true;
            btnIniciarDetener.Text = "Detener";
            lblEstado.Text = "Estado: ejecutando";

            hiloAutomatizacion = new Thread(EjecutarAutomatizacion)
            {
                IsBackground = true
            };
            hiloAutomatizacion.Start();
        }

        private void ReiniciarProgramacion()
        {
            DateTime ahoraUtc = DateTime.UtcNow;
            indiceAutoatackActual = 0;
            indiceVhlSiguiente = 0;
            indiceRevenantSiguiente = 0;
            indiceYamiSiguiente = 0;
            proximoAutoatackUtc = ahoraUtc;
            proximoVhlUtc = ahoraUtc;
            proximoRevenantUtc = ahoraUtc;
            proximoYamiUtc = ahoraUtc;
            proximoEnvioGlobalUtc = ahoraUtc;
        }

        private void EjecutarAutomatizacion()
        {
            while (automatizacionActiva)
            {
                if (!IsWindow(ventanaObjetivo))
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

                if (!IntentarSeleccionarTecla(out byte tecla, out ModoDisparo modo, out int indiceModo, out int esperaMs))
                {
                    if (!EsperarConCancelacion(esperaMs))
                    {
                        return;
                    }

                    continue;
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

                DateTime enviadoUtc = DateTime.UtcNow;

                lock (syncProgramacion)
                {
                    proximoEnvioGlobalUtc = enviadoUtc.AddMilliseconds(intervaloMinimoGlobalMs);

                    if (modo == ModoDisparo.Autoatack && autoatackHabilitado)
                    {
                        AvanzarAutoatack(enviadoUtc);
                    }

                    if (modo == ModoDisparo.Vhl && indiceModo >= 0 && indiceModo < secuenciaVhl.Length && vhlHabilitado)
                    {
                        indiceVhlSiguiente = (indiceModo + 1) % secuenciaVhl.Length;
                        proximoVhlUtc = enviadoUtc.AddMilliseconds(intervaloMinimoGlobalMs);
                    }

                    if (modo == ModoDisparo.Revenant && indiceModo >= 0 && indiceModo < secuenciaRevenant.Length && revenantHabilitado)
                    {
                        indiceRevenantSiguiente = (indiceModo + 1) % secuenciaRevenant.Length;
                        proximoRevenantUtc = enviadoUtc.AddMilliseconds(intervaloMinimoGlobalMs);
                    }

                    if (modo == ModoDisparo.Yami && indiceModo >= 0 && indiceModo < secuenciaYami.Length && yamiHabilitado)
                    {
                        indiceYamiSiguiente = (indiceModo + 1) % secuenciaYami.Length;
                        proximoYamiUtc = enviadoUtc.AddMilliseconds(intervaloMinimoGlobalMs);
                    }
                }
            }
        }

        private bool IntentarSeleccionarTecla(out byte tecla, out ModoDisparo modo, out int indiceModo, out int esperaMs)
        {
            DateTime ahoraUtc = DateTime.UtcNow;
            DateTime proximaRevisionUtc = DateTime.MaxValue;
            DateTime mejorMomentoUtc = DateTime.MaxValue;

            tecla = 0;
            modo = ModoDisparo.Ninguno;
            indiceModo = -1;

            lock (syncProgramacion)
            {
                if (!autoatackHabilitado && !vhlHabilitado && !revenantHabilitado && !yamiHabilitado)
                {
                    esperaMs = 150;
                    return false;
                }

                if (proximoEnvioGlobalUtc > ahoraUtc)
                {
                    proximaRevisionUtc = proximoEnvioGlobalUtc;
                }

                if (autoatackHabilitado)
                {
                    EvaluarCandidato(ahoraUtc, proximoAutoatackUtc, secuenciaAutoatack[indiceAutoatackActual], ModoDisparo.Autoatack, indiceAutoatackActual, ref tecla, ref modo, ref indiceModo, ref mejorMomentoUtc, ref proximaRevisionUtc);
                }

                if (vhlHabilitado)
                {
                    EvaluarCandidato(ahoraUtc, proximoVhlUtc, secuenciaVhl[indiceVhlSiguiente], ModoDisparo.Vhl, indiceVhlSiguiente, ref tecla, ref modo, ref indiceModo, ref mejorMomentoUtc, ref proximaRevisionUtc);
                }

                if (revenantHabilitado)
                {
                    EvaluarCandidato(ahoraUtc, proximoRevenantUtc, secuenciaRevenant[indiceRevenantSiguiente], ModoDisparo.Revenant, indiceRevenantSiguiente, ref tecla, ref modo, ref indiceModo, ref mejorMomentoUtc, ref proximaRevisionUtc);
                }

                if (yamiHabilitado)
                {
                    EvaluarCandidato(ahoraUtc, proximoYamiUtc, secuenciaYami[indiceYamiSiguiente], ModoDisparo.Yami, indiceYamiSiguiente, ref tecla, ref modo, ref indiceModo, ref mejorMomentoUtc, ref proximaRevisionUtc);
                }

                if (modo != ModoDisparo.Ninguno && proximoEnvioGlobalUtc <= ahoraUtc)
                {
                    esperaMs = 20;
                    return true;
                }
            }

            if (proximaRevisionUtc == DateTime.MaxValue)
            {
                esperaMs = 100;
                return false;
            }

            esperaMs = (int)Math.Max(20, Math.Min(200, (proximaRevisionUtc - ahoraUtc).TotalMilliseconds));
            return false;
        }

        private static void EvaluarCandidato(
            DateTime ahoraUtc,
            DateTime disponibleUtc,
            byte teclaCandidata,
            ModoDisparo modoCandidato,
            int indiceCandidato,
            ref byte tecla,
            ref ModoDisparo modo,
            ref int indiceModo,
            ref DateTime mejorMomentoUtc,
            ref DateTime proximaRevisionUtc)
        {
            if (disponibleUtc <= ahoraUtc)
            {
                if (disponibleUtc <= mejorMomentoUtc)
                {
                    mejorMomentoUtc = disponibleUtc;
                    tecla = teclaCandidata;
                    modo = modoCandidato;
                    indiceModo = indiceCandidato;
                }

                return;
            }

            if (disponibleUtc < proximaRevisionUtc)
            {
                proximaRevisionUtc = disponibleUtc;
            }
        }

        private void AvanzarAutoatack(DateTime enviadoUtc)
        {
            indiceAutoatackActual = (indiceAutoatackActual + 1) % secuenciaAutoatack.Length;
            proximoAutoatackUtc = enviadoUtc.AddMilliseconds(intervaloAutoatackEntreTeclas);
        }

        private bool EsperarConCancelacion(int esperaMs)
        {
            int restante = Math.Max(20, esperaMs);

            while (automatizacionActiva && restante > 0)
            {
                int tramo = Math.Min(100, restante);
                Thread.Sleep(tramo);
                restante -= tramo;
            }

            return automatizacionActiva;
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
