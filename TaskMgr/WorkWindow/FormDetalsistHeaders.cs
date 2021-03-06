﻿using System;
using System.Windows.Forms;

namespace PCMgr.WorkWindow
{
    public partial class FormDetalsistHeaders : Form
    {
        public FormDetalsistHeaders(FormMain m)
        {
            InitializeComponent();
            formMain = m;
        }

        private FormMain formMain = null;
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormDetalsistHeaders_Load(object sender, EventArgs e)
        {
            FormSettings.LoadFontSettingForUI(this);
            NativeMethods.MAppWorkCall3(182, listItems.Handle, IntPtr.Zero);
            foreach (string s in formMain.MainPageProcessDetails.allCols)
            {
                ListViewItem item = new ListViewItem();
                item.Name = s;
                item.Text = Lanuages.LanuageMgr.GetStr(s);
                if (formMain.MainPageProcessDetails.ProcessListDetailsGetListIndex(s) != -1)
                {
                    item.Tag = "OldShow";
                    item.Checked = true;
                }
                else item.Tag = "NewShow";
                listItems.Items.Add(item);
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            int checkedCount = 0;
            foreach (ListViewItem item in listItems.Items)
                if (item.Checked)
                    checkedCount++;
            if (checkedCount > 16)
            {
                MessageBox.Show(Lanuages.LanuageMgr.GetStr("MaxShow16Item"));
                return;
            }

            foreach (ListViewItem item in listItems.Items)
            {
                if (item.Tag.ToString() == "OldShow" && item.Checked == false)
                    formMain.MainPageProcessDetails.ProcessListDetailsRemoveHeader(item.Name);
                if (item.Tag.ToString() == "NewShow" && item.Checked)
                    formMain.MainPageProcessDetails.ProcessListDetailsAddHeader(item.Name);
            }
            formMain.MainPageProcessDetails.ProcessListDetailsGetColumnsIndex();
            formMain.MainPageProcessDetails.nextUpdateStaticVals = true;
            Close();
        }
    }
}
