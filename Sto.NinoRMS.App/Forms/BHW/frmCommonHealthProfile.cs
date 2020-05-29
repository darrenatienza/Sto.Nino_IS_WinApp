using Bunifu.Framework.UI;
using Bunifu.UI.WinForms.BunifuTextbox;
using Helpers;
using MetroFramework;
using Microsoft.Reporting.WinForms;
using Sto.NinoRMS.App.Helpers.Themes;
using Sto.NinoRMS.App.Properties;
using Sto.NinoRMS.Queries.Core.Domain;
using Sto.NinoRMS.Queries.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sto.NinoRMS.App
{
    public partial class frmCommonHealthProfile : Form, IFormAction
    {
        private int origWidth;
        private int origHeight;

        public frmCommonHealthProfile()
        {
            InitializeComponent();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            
        }
       

       

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimized_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void btnAbout_Click(object sender, EventArgs e)
        {
         

        }

    

        private void btnDashboard_Click(object sender, EventArgs e)
        {
   
        }

        
        /// <summary>
        ///fix for unexpected hiding of bunifu flat button image
        /// </summary>
        private void SetButtonImageVisible()
        {
            var buttons = LocalUtils.GetAll(this,typeof(BunifuFlatButton));
            foreach (BunifuFlatButton item in buttons)
            {
                item.IconVisible = true;
            }
        }

        
        public void Delete()
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {


                if (gridMain.SelectedRows.Count > 0)
                {
                    commonHealthProfileID = int.Parse(gridMain.SelectedRows[0].Cells[0].Value.ToString());
                    if (commonHealthProfileID != 0)
                    {
                        DialogResult result = MetroMessageBox.Show(this, "Are you sure you want to delete this record?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            var obj = uow.CommonHealthProfiles.Get(commonHealthProfileID);
                            if (obj != null)
                            {
                                uow.CommonHealthProfiles.Remove(obj);
                                uow.Complete();
                                LoadMainGrid();
                                commonHealthProfileID = 0;
                                LocalUtils.ShowDeleteSuccessMessage(this);
                                ResetInputs();
                            }

                        }
                    }
                    else
                    {
                        LocalUtils.ShowNoRecordFoundMessage(this);
                    }

                }
                else
                {

                    LocalUtils.ShowNoRecordSelectedMessage(this);
                }


            }
        }

        public void LoadMainGrid()
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var ls = uow.CommonHealthProfiles.GetAllBy(txtSearch.Text);
                gridMain.Rows.Clear();
                int count = 0;
                foreach (var item in ls)
                {
                    count++;
                    gridMain.Rows.Add(new string[] {item.CommonHealthProfileID.ToString(),
                        count.ToString(),
                        item.Title
                });
                }

            }
        }

        public void SetData()
        {

            using (var uow = new UnitOfWork(new DataContext()))
            {
                if (gridMain.SelectedRows.Count > 0)
                {
                    commonHealthProfileID = int.Parse(gridMain.SelectedRows[0].Cells[0].Value.ToString());
                    if (commonHealthProfileID != 0)
                    {
                        var obj = uow.CommonHealthProfiles.Get(commonHealthProfileID);
                        txtTitle.Text = obj.Title;
                        
                    }
                    else
                    {
                        LocalUtils.ShowNoRecordFoundMessage(this);
                    }
                }
                else
                {

                    LocalUtils.ShowNoRecordSelectedMessage(this);
                }

            }
        }

        public void Save()
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                if (!ValidatedFields())
                {
                    return;
                }
                if (commonHealthProfileID == 0)
                {
                    //add
                    var obj = new CommonHealthProfile();
                    obj.Title = txtTitle.Text;
                    uow.CommonHealthProfiles.Add(obj);
                    uow.Complete();
                    commonHealthProfileID = obj.CommonHealthProfileID;

                }
                else
                {
                    //edit
                    var obj = uow.CommonHealthProfiles.Get(commonHealthProfileID);
                    obj.Title = txtTitle.Text;
                    uow.CommonHealthProfiles.Edit(obj);
                    uow.Complete();
                }
                LoadMainGrid();
                LocalUtils.ShowSaveMessage(this);
                ResetInputs();
            }
        }

        public void Add()
        {
            ResetInputs();
            commonHealthProfileID = 0;
            LocalUtils.ShowAddNewMessage(this);

        }

        public void ResetInputs()
        {
            var textboxes = LocalUtils.GetAll(pageDetail, typeof(BunifuTextBox));
            foreach (BunifuTextBox item in textboxes)
            {
                item.Text = string.Empty;
            }
            var comboboxes = LocalUtils.GetAll(pageDetail, typeof(Bunifu.UI.WinForms.BunifuDropdown));
            foreach (Bunifu.UI.WinForms.BunifuDropdown item in comboboxes)
            {
                item.SelectedIndex = -1;
                item.Enabled = true;
            }
            var dates = LocalUtils.GetAll(pageDetail, typeof(BunifuDatepicker));
            foreach (BunifuDatepicker item in dates)
            {
                item.Value = DateTime.UtcNow;
            }
        }


        public bool ValidatedFields()
        {

            var ctrlTextBoxes2 = LocalUtils.GetAll(pageDetail, typeof(BunifuTextBox))
                       .Where(c => c.CausesValidation == true).OrderBy(c => c.TabIndex);
            foreach (BunifuTextBox item in ctrlTextBoxes2)
            {
                if (item.Text == string.Empty)
                {
                    item.Focus();
                    LocalUtils.ShowValidationFailedMessage(this);
                    return false;
                }
            }

            var ctrlDropdown = LocalUtils.GetAll(pageDetail, typeof(Bunifu.UI.WinForms.BunifuDropdown))
                        .Where(c => c.CausesValidation == true).OrderBy(c => c.TabIndex);
            foreach (Bunifu.UI.WinForms.BunifuDropdown item in ctrlDropdown)
            {
                if (item.SelectedIndex < 0)
                {
                    item.Focus();
                    MetroMessageBox.Show(this, string.Format("Empty field {0 }", item.Tag), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }

        

        public int commonHealthProfileID { get; set; }

        private void txtSearch_OnIconRightClick(object sender, EventArgs e)
        {
            LoadMainGrid();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Add();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void gridMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SetData();
        }

        private void frmCommonHealthProfile_Load(object sender, EventArgs e)
        {
            origWidth = this.Width;
            origHeight = this.Height;
            SetButtonImageVisible();
            LoadMainGrid();
            this.rpt.RefreshReport();
        }

        

        private void SetFormSize(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        

        private void btnBack_Click(object sender, EventArgs e)
        {
            pages.SetPage(pageDetail);
            
            SetFormSize(origWidth, origHeight);
            this.CenterToScreen();
        }
    }
}
