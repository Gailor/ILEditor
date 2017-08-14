﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ILEditor.Classes;

namespace ILEditor.Forms
{
    public partial class HostSelect : Form
    {
        public Boolean SystemSelected;
        public HostSelect()
        {
            InitializeComponent();
            SystemSelected = false;
            LoadListView();
        }

        private void LoadListView()
        {
            systemlist.Clear();

            foreach(string System in GetSystemsList())
            {
                systemlist.Items.Add(new ListViewItem(System, 0));
            }
        }

        public static string[] GetSystemsList()
        {
            if (!Directory.Exists(Program.SYSTEMSDIR))
                Directory.CreateDirectory(Program.SYSTEMSDIR);

            string[] systems = Directory.GetFiles(Program.SYSTEMSDIR);

            for (int i = 0; i < systems.Length; i++)
            {
                systems[i] = Path.GetFileName(systems[i]);
            }

            return systems;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void newhost_Click(object sender, EventArgs e)
        {
            new NewSystem().ShowDialog();
            LoadListView();
        }

        private void systemlist_DoubleClick(object sender, EventArgs e)
        {
            string ConfigPath = "";
            if (systemlist.SelectedItems.Count == 1)
            {
                ConfigPath = Program.SYSTEMSDIR + @"\" + systemlist.SelectedItems[0].Text;
                IBMi.CurrentSystem = new Config(ConfigPath);
                SystemSelected = true;
                this.Close();
            }
        }
    }
}