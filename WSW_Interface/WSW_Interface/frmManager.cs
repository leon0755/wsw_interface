using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Palgain.CommonModule;
using Palgain.MainProgram;
using Microsoft.Win32;
using System.IO.Ports;

namespace WSW_Interface
{
    public partial class frmManager : Form
    {
        Icon m_icon_ready = new Icon(".\\ICO\\running.ico");
        Icon m_icon_receive = new Icon(".\\ICO\\receive.ico");
        Icon m_icon_error = new Icon(".\\ICO\\closed.ico");

        MainProgram m_program =new MainProgram();
        protected ContextMenu m_menu = new ContextMenu();

        public frmManager()
        {
            InitializeComponent();

            m_menu.MenuItems.Add(new MenuItem("显示界面", new EventHandler(Menu_click)));
            m_menu.MenuItems.Add(new MenuItem("隐藏界面", new EventHandler(Menu_click)));
            m_menu.MenuItems.Add(new MenuItem("读取文件", new EventHandler(Menu_click)));
            m_menu.MenuItems.Add(new MenuItem("退出", new EventHandler(Menu_click)));

            notifyIcon1.ContextMenu = m_menu;
        }

        private void Menu_click(object sender, EventArgs e)
        {
            MenuItem t_menuitem = (MenuItem)sender;
            switch (t_menuitem.Text)
            {
                case "显示界面":
                    this.WindowState = FormWindowState.Normal;
                    this.Show();
                    
                    break;
                case "隐藏界面":
                    this.WindowState = FormWindowState.Minimized;
                    this.Hide();
                    break;
                case "退出":
                    CloseForm();
                    break;
            }
        }

        public void CloseForm()
        {
            notifyIcon1.Visible = false;
            m_icon_ready.Dispose();
            m_icon_receive.Dispose();
            m_icon_ready = null;
            m_icon_receive = null;
            this.Close();
            this.Dispose();
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            this.LoadComboxItem();
            this.LoadConfig();

            Thread t_thread = new Thread(new ThreadStart(SetState));
            t_thread.IsBackground = true;
            t_thread.Start();

            this.notifyIcon1.Icon = m_icon_ready;
            notifyIcon1.Visible = true;

            m_program.StartListenCOM();
        }

        private void LoadComboxItem()
        {
            //portname
            RegistryKey keyCom = Registry.LocalMachine.OpenSubKey("Hardware\\DeviceMap\\SerialComm");
            if (keyCom != null)
            {
                string[] sSubKeys = keyCom.GetValueNames();
                this.cb_portname.Items.Clear();
                foreach (string sName in sSubKeys)
                {
                    string sValue = (string)keyCom.GetValue(sName);
                    this.cb_portname.Items.Add(sValue);
                }
            }

            //baudrate
            int[] t_baudrates = new int[] { 300, 600, 1200, 2400, 4800, 9600, 19200, 38400, 43000, 56000 };
            foreach (int t_baudrate in t_baudrates)
            {
                this.cb_baudrate.Items.Add(t_baudrate);
            }

            //databits
            int[] t_databits = new int[] { 8, 7, 6 };
            foreach (int t_databit in t_databits)
            {
                this.cb_databits.Items.Add(t_databit);
            }

            //stopbits
            foreach (string t_stopbit in Enum.GetNames(typeof(StopBits)))
            {
                this.cb_stopbits.Items.Add(t_stopbit);
            }

            //Paritys
            foreach (string t_Parity in Enum.GetNames(typeof(Parity)))
            {
                this.cb_parity.Items.Add(t_Parity);
            }

            ////HandShakes
            //foreach (string t_handshake in Enum.GetNames(typeof(Handshake)))
            //{
            //    this.cb_HandShake.Items.Add(t_handshake);
            //}

        }

        private void LoadConfig()
        {
            cb_portname.SelectedItem = Consts.Config.Rs232ConfigInfo.PortName;
            cb_baudrate.SelectedItem = Consts.Config.Rs232ConfigInfo.BaudRate;
            cb_parity.SelectedItem = Consts.Config.Rs232ConfigInfo.Parity.ToString();
            cb_databits.SelectedItem = Consts.Config.Rs232ConfigInfo.Databits;
            cb_stopbits.SelectedItem = Consts.Config.Rs232ConfigInfo.Stopbits.ToString();
        }

        private void SetState()
        {
            while (true)
            {
                this.BeginInvoke(new Action<Enum_State>(SetIcon), m_program.State);
                Thread.Sleep(100);
            }
        }

        private void SetIcon(Enum_State i_state)
        {
            switch (i_state)
            {
                case Enum_State.Receive:
                    this.notifyIcon1.Icon = m_icon_receive;
                    break;
                case Enum_State.Error:
                    this.notifyIcon1.Icon = m_icon_error;
                    break;
                default:
                    this.notifyIcon1.Icon = m_icon_ready;
                    break;
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            ConfigInfo t_configinfo = new ConfigInfo();
            try
            {
                if (cb_portname.SelectedItem != null)
                {
                    t_configinfo.Rs232ConfigInfo.PortName = cb_portname.SelectedItem.ToString();
                }
                t_configinfo.Rs232ConfigInfo.BaudRate = int.Parse(cb_baudrate.SelectedItem.ToString());
                t_configinfo.Rs232ConfigInfo.Parity =
                    (Parity)Enum.Parse(typeof(Parity),cb_parity.SelectedItem.ToString());
                t_configinfo.Rs232ConfigInfo.Databits = int.Parse((string)cb_databits.SelectedItem.ToString());
                t_configinfo.Rs232ConfigInfo.Stopbits =
                    (StopBits)Enum.Parse(typeof(StopBits), (string)cb_stopbits.SelectedItem.ToString());
            }
            catch (Exception ex)
            {
                LogHelper.Error("保存设置时出错:" + ex.ToString());
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            m_program.SaveConfig(t_configinfo);
            this.LoadConfig();
            MessageBox.Show("保存成功!");
        }

        private void frmManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_program.ExitProgram();
        }

        bool m_is_first = true;
        private void frmManager_Activated(object sender, EventArgs e)
        {
            if (m_is_first)
            {
                this.Hide();
            }
            m_is_first = false;
        }

        private void frmManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}