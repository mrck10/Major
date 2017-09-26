using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Mail;
using System.Threading;
using System.Net;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using Microsoft.CSharp;
using System.CodeDom.Compiler;

namespace Major
{
    public class FormRelatedFunctions
    {
        //  Gui related stuff

        [DllImport("User32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int cmdShow); // Show Window Function

        [DllImport("Kernel32.dll")]
        public static extern IntPtr GetConsoleWindow(); // GetConsoleWindow Function

        public void HideConsoleWindow()
        {
            IntPtr hWnd = GetConsoleWindow(); // Gets the Console Window
            if (hWnd != IntPtr.Zero) // if the console window exsists
            {
                ShowWindow(hWnd, 0); // Hide the window ( 0 = null )
            }
        }

        public void ShowConsoleWindow()
        {
            IntPtr hWnd = GetConsoleWindow(); // Gets the Console Window
            if (hWnd != IntPtr.Zero) // if the console window exsists
            {
                ShowWindow(hWnd, 1); // Show the window
            }
        }

        public Color WindowColorBeforeTransparent; // The color before the GUI becomes transparent.

    }
}
