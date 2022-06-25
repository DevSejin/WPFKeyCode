using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace WPFKeyCode
{
    static class Hook
    {
        private static WinAPI.HookProc hookDelegate;
        private static IntPtr hHook = IntPtr.Zero;
        //private static IntPtr mHook = IntPtr.Zero;

        public static void SetHook()
        {
            //Keyboard Hook
            /*
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                WinAPI.SetWindowsHookEx(WinAPI.WH_KEYBOARD_LL, HookCallback, WinAPI.GetModuleHandle(curModule.ModuleName), 0);
            }
            */
            //Mouse Hook
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                hookDelegate = new WinAPI.HookProc(MouseHookCallback);
                hHook = WinAPI.SetWindowsHookEx(WinAPI.WH_MOUSE_LL, hookDelegate, WinAPI.GetModuleHandle(curModule.ModuleName), 0);
            }
            //hHook = WinAPI.SetWindowsHookEx(WinAPI.WH_MOUSE_LL, HookCallback, WinAPI.LoadLibrary("User32"), 0);
        }

        public static void UnHook()
        {
            WinAPI.UnhookWindowsHookEx(hHook);
            Console.WriteLine(" UnHooked ");
        }

        static short wData;
        static IntPtr interceptMsg = (IntPtr)1;
        public static IntPtr MouseHookCallback(int code, int wParam, IntPtr lParam)
        {
            if ((code >= 0) && (WinAPI.GetAsyncKeyState(WinAPI.VK_F6) < 0))
            {

                if (wParam == WinAPI.WM_MOUSEWHEEL)
                {
                    wData = Marshal.ReadInt16(lParam + 10);
                    if (wData > 0)
                    {
                        VolumeUp();
                    }
                    else if (wData < 0)
                    {
                        VolumeDown();
                    }
                    return interceptMsg;
                }
                else if (wParam == WinAPI.WM_MBUTTONDOWN)
                {
                    Mute();
                    return interceptMsg;
                }

            }
            return WinAPI.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
        }

        private static IntPtr HookCallback(int nCode, int wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == WinAPI.WM_KEYDOWN || wParam == WinAPI.WM_SYSKEYDOWN)
            {
                Console.WriteLine(WinAPI.GetAsyncKeyState(0x12));
                int vkCode = Marshal.ReadInt32(lParam);

                Console.WriteLine(Convert.ToString(vkCode, 16));
            }
            return WinAPI.CallNextHookEx(hHook, nCode, wParam, lParam);
        }

        // send funtions
        public static void VolumeUp()
        {
            SendKey(WinAPI.VK_VOLUME_UP);
        }

        public static void VolumeDown()
        {
            SendKey(WinAPI.VK_VOLUME_DOWN);
        }

        public static void Mute()
        {
            SendKey(WinAPI.VK_VOLUME_MUTE);
        }

        static void SendKey(byte key)
        {
            WinAPI.keybd_event(key, 0x45, 1, IntPtr.Zero);
            WinAPI.keybd_event(key, 0x45, 1 | 2, IntPtr.Zero);
        }
    }
}
