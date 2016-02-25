using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ETestApp.SystemTray
{
    class ContextMenu
    {
        #region Declarations
        Form1 frmMain = null;
        #endregion
        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>ContextMenuStrip</returns>
        public ContextMenuStrip Create()
        {
            // Add the default menu options.
            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem item;

            // Configuration.
            item = new ToolStripMenuItem();
            item.Text = "DMS Application";
            item.Click += new EventHandler(IdsUtility_Click);
            menu.Items.Add(item);

            // Exit.
            item = new ToolStripMenuItem();
            item.Text = "Exit";
            item.Click += new EventHandler(Exit_Click);
            menu.Items.Add(item);

            return menu;
        }

        /// <summary>
        /// Handles the Click event of the About control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void IdsUtility_Click(object sender, EventArgs e)
        {
            frmMain = Application.OpenForms["Main_IDSUtilitySuite"] as Form1;

            if (frmMain == null && IdsProcessIcon.callCount == 0)
            {
                IdsProcessIcon.callCount = 1;
                frmMain = new Form1();
                frmMain.Closed += frmMain_closed;
                frmMain.Show();
            }
            else if (frmMain != null)
            {
                frmMain.WindowState = FormWindowState.Normal;
                frmMain.BringToFront();
            }
        }

        void Exit_Click(object sender, EventArgs e)
        {
            foreach (var process in Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName))
            {
                process.Kill();
            }
            Application.Exit();
        }

        void frmMain_closed(object sender, EventArgs e)
        {
            IdsProcessIcon.callCount = 0;
            frmMain = null;
        }
    }
}
