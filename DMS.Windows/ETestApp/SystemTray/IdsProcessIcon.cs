using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ETestApp.SystemTray
{
    class IdsProcessIcon : IDisposable
    {
        /// <summary>
        /// The NotifyIcon object.
        /// </summary>
        NotifyIcon nIcon;
        Form1 frmMain = null;

        public static int callCount = 0;

        public IdsProcessIcon()
        {
            // Instantiate the NotifyIcon object.
            nIcon = new NotifyIcon();
        }

        /// <summary>
        /// Displays the icon in the system tray.
        /// </summary>
        public void Display()
        {
            // Put the icon in the system tray and allow it react to mouse clicks.	
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            nIcon.MouseClick += new MouseEventHandler(ni_MouseClick);
            nIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            nIcon.Text = "DMS Application.";
            nIcon.Visible = true;

            // Attach a context menu.
            nIcon.ContextMenuStrip = new ContextMenu().Create();
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        public void Dispose()
        {
            // When the application closes, this will remove the icon from the system tray immediately.
            nIcon.Dispose();
        }

        /// <summary>
        /// Handles the MouseClick event of the ni control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>      

        void ni_MouseClick(object sender, MouseEventArgs e)
        {
            frmMain = Application.OpenForms["Main_IDSUtilitySuite"] as Form1;

            if (e.Button == MouseButtons.Left)
            {
                if (callCount == 0)
                {
                    callCount = 1;
                    frmMain = new Form1();
                    frmMain.Closed += frmMain_closed;
                    frmMain.Show();
                }
                else if (frmMain != null)
                {
                    frmMain.WindowState = FormWindowState.Minimized;
                    frmMain.WindowState = FormWindowState.Normal;
                    frmMain.BringToFront();
                }
            }
        }

        public void frmMain_closed(object sender, EventArgs e)
        {
            callCount = 0;
            frmMain = null;
        }
    }
}
