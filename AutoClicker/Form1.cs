using System.Runtime.InteropServices;
using System.Threading;


namespace API
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Opacity = 0;
        }

        [DllImport("user32.dll")]
        private static extern bool ClientToScreen(IntPtr hWnd, ref Point point);

        [StructLayout(LayoutKind.Sequential)]
        private struct Point
        {
            public int X;
            public int Y;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            MoveMouse(SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height);
           // ClickMouse();
        }

        #region Mouse Move
        
        private void MoveMouse(int screenWidth, int screenHeigt)
        {
            Point p = new Point();
            Random r = new Random();
            int c = 0;

            while (true)
            {
                p.X = Convert.ToInt16(r.Next(screenWidth));
                p.Y = Convert.ToInt16(r.Next(screenHeigt));

                ClientToScreen(Handle, ref p);
                SetCursorPos(p.X, p.Y);

                c++;
                Thread.Sleep(15000);
              
            }

        }

        [DllImport("user32.dll")]
        private static extern long SetCursorPos(int x, int y);

        #endregion

        #region Mouse Click
        private void ClickMouse()
        {
            int c = 0; 
            Point p =  new Point();

            while (true)
            {
                GetCursorPos(ref p);
                ClientToScreen(Handle, ref p);
                DoMouseLeftClick(p.X, p.Y);

                c++;

                Thread.Sleep(1000);

                if (c == 50) 
                    break;
            }
        }

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dsFlags, int dx, int dy, int cButtond, int dsExstraInfro);

        public  const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;

        public const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        public const int MOUSEEVENTF_RIGHTUP = 0x010;

        private void DoMouseLeftClick(int x, int y)
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, x, y, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, x, y, 0, 0);
        }

        private void DoMouseRightClick(int x, int y)
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN, x, y, 0, 0);
            mouse_event(MOUSEEVENTF_RIGHTUP, x, y, 0, 0);
        }

        private void DoMouseDoubleLeftClick(int x, int y)
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, x, y, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, x, y, 0, 0);

            mouse_event(MOUSEEVENTF_LEFTDOWN, x, y, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, x, y, 0, 0);
        }


        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(ref Point lpPoint);
        #endregion
    
    }
}