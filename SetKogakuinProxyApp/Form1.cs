using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace SetSystemProxyApp
{
    public partial class Form1 : Form
    {

        private static string regKeyName = @"Software\Microsoft\Windows\CurrentVersion\Internet Settings";
        private static string hachiouji = "cache.ns.kogakuin.ac.jp:8080";
        private static string shinzyuku = "cache.ccs.kogakuin.ac.jp:8080";
        private RegistryKey rKey = Registry.CurrentUser.OpenSubKey(regKeyName, true);

        public Form1()
        {
            InitializeComponent();

            object defVal = rKey.GetValue("ProxyEnable");
            if (double.Parse(defVal.ToString()) == 1)
            {
                button1.Text = "Off";
                label1.BackColor = Color.Lime;
            }
            else
            {
                button1.Text = "On";
                label1.BackColor = Color.Red;
            }

            object defVal2 = rKey.GetValue("ProxyServer");
            if (defVal2.ToString() == hachiouji)
            {
                //comboBox1.SelectedIndex == 1; SelectedItem == "八王子";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string setValue = "";
            int enabled;

            if (comboBox1.SelectedItem == null)
            {
                setValue = "";
            }
            else if (comboBox1.SelectedItem.Equals("八王子"))
            {
                setValue = hachiouji;
            }
            else if (comboBox1.SelectedItem.Equals("新宿"))
            {
                setValue = shinzyuku;
            }

            if (button1.Text == "On")
            {
                enabled = 1;
                button1.Text = "Off";
                label1.BackColor = Color.Lime;
            }
            else
            {
                enabled = 0;
                button1.Text = "On";
                label1.BackColor = Color.Red;
            }

            // レジストリの設定と削除
            try
            {
                RegistryKey rKey = Registry.CurrentUser.OpenSubKey(regKeyName, true);
                rKey.SetValue("ProxyServer", setValue, RegistryValueKind.String);
                rKey.SetValue("ProxyEnable", enabled, RegistryValueKind.DWord);
                rKey.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occured." + ex.ToString());
            }
        }
    }
}
