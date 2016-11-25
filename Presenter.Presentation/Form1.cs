using System.Windows.Forms;

namespace Presenter.Presentation
{
    public partial class Form1 : Form
    {
        string _htmlFilePath;
            
        public Form1(string htmlFilePath)
        {
            InitializeComponent();
            //WindowState = FormWindowState.Normal;
            //FormBorderStyle = FormBorderStyle.None;
            //WindowState = FormWindowState.Maximized;

            _htmlFilePath = htmlFilePath;
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
             this.webBrowser1.Navigate(_htmlFilePath);
        }
    }
}
