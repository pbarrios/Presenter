using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Presenter.Presentation
{
    public partial class Form1 : Form
    {
        string _htmlFilePath;
        int _screenNumber;
            
        public Form1(string htmlFilePath, int screenNumber)
        {
            InitializeComponent();

            _screenNumber = screenNumber;
            _htmlFilePath = htmlFilePath;
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            if (_screenNumber > Screen.AllScreens.Length) Close();

            var screen = Screen.AllScreens[_screenNumber - 1];
            this.Location = screen.WorkingArea.Location;

            WindowState = FormWindowState.Normal;
            FormBorderStyle = FormBorderStyle.None;
            Height = screen.WorkingArea.Height;
            Width = screen.WorkingArea.Width;

            this.webBrowser1.Navigate(_htmlFilePath);
        }

    }
}
