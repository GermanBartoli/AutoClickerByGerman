using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AutoClickerByGerman
{
    public class KeyboardHook
    {
        //https://chatgpt.com/c/0189cbd4-82b5-4466-89ed-f8b65edc6954
        // Declaración de los delegados de eventos de teclado
        public delegate int KeyboardHookProc(int nCode, int wParam, IntPtr lParam);
        private static KeyboardHookProc hookProc;

        // Importación de las funciones de la librería user32.dll
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, KeyboardHookProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int CallNextHookEx(IntPtr hhk, int nCode, int wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        // Constantes para los códigos de tecla
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;

        // Delegado para el evento de tecla pulsada
        public delegate void KeyPressedEventHandler(Keys key);
        public event KeyPressedEventHandler KeyPressed;

        private IntPtr hookId = IntPtr.Zero;

        // Constructor
        public KeyboardHook()
        {
            hookProc = new KeyboardHookProc(HookCallback);
            hookId = SetHook(hookProc);
        }

        // Método para establecer el gancho del teclado
        private IntPtr SetHook(KeyboardHookProc hookProc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, hookProc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        // Método para liberar el gancho del teclado
        public void Unhook()
        {
            UnhookWindowsHookEx(hookId);
        }

        // Método que se llama cuando se detecta una pulsación de tecla
        private int HookCallback(int nCode, int wParam, IntPtr lParam)
        {
            if (nCode >= 0 && (wParam == WM_KEYDOWN || wParam == WM_KEYUP))
            {
                int vkCode = Marshal.ReadInt32(lParam);
                if (wParam == WM_KEYDOWN)
                {
                    KeyPressed?.Invoke((Keys)vkCode);
                }
            }
            return CallNextHookEx(hookId, nCode, wParam, lParam);
        }
    }
}
