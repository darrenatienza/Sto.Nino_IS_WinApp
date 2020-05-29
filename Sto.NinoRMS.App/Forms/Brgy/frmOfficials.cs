using Bunifu.Framework.UI;
using Bunifu.UI.WinForms.BunifuTextbox;
using MetroFramework;
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
    public partial class frmOfficials : Form, IFormAction
    {
        private int officialID;

        public frmOfficials()
        {
            InitializeComponent();
            toolTip1.SetToolTip(cboResidents, Resources.TooltipBarangayFormFullName);
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

        private void btnSetting_Click(object sender, EventArgs e)
        {
            
        }

        private void bunifuCustomLabel3_Click(object sender, EventArgs e)
        {

        }

        public void Delete()
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {


                if (gridMain.SelectedRows.Count > 0)
                {
                    officialID = int.Parse(gridMain.SelectedRows[0].Cells[0].Value.ToString());
                    if (officialID != 0)
                    {
                        DialogResult result = MetroMessageBox.Show(this, "Are you sure you want to delete this record?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            var obj = uow.Officials.Get(officialID);
                            if (obj != null)
                            {
                                uow.Officials.Remove(obj);
                                uow.Complete();
                                LoadMainGrid();
                                officialID = 0;
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
                var ls = uow.Officials.GetAllBy(txtSearch.Text);
                gridMain.Rows.Clear();
                int count = 0;
                foreach (var item in ls)
                {
                    count++;
                    gridMain.Rows.Add(new string[] {item.OfficialID.ToString(),
                        count.ToString(),
                        item.Resident.FirstName + " " + item.Resident.LastName,
                        item.OfficialPosition.Title

                });
                }

            }
        }

        public void SetData()
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var obj = uow.Officials.Get(officialID);
                cboResidents.SelectedItem = LocalUtils.GetSelectedItemX(cboResidents.Items, obj.ResidentID.ToString());cboResidents.SelectedItem = LocalUtils.GetSelectedItemX(cboResidents.Items, obj.ResidentID.ToString());
                cboPosition.SelectedItem = LocalUtils.GetSelectedItemX(cboPosition.Items, obj.OfficialPositionID.ToString());
                txtDesignation.Text = obj.Designation;

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
                if (officialID == 0)
                {
                    //add
                    var obj = new Official();
                    obj.Designation = txtDesignation.Text;
                    obj.ResidentID = int.Parse(((ItemX)cboResidents.SelectedItem).Value);
                    obj.OfficialPositionID = int.Parse(((ItemX)cboPosition.SelectedItem).Value);
                    uow.Officials.Add(obj);
                    uow.Complete();
                    officialID = obj.OfficialID;

                }
                else
                {
                    //edit
                    var obj = uow.Officials.Get(officialID);
                    obj.Designation = txtDesignation.Text;
                    obj.ResidentID = int.Parse(((ItemX)cboResidents.SelectedItem).Value);
                    obj.OfficialPositionID = int.Parse(((ItemX)cboPosition.SelectedItem).Value);
                    uow.Officials.Edit(obj);
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
            officialID = 0;
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

        private void LoadResidents()
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var objs = uow.Residents.GetAll();
                foreach (var  item in objs)
                {
                    cboResidents.Items.Add(new ItemX(item.FirstName + " " + item.LastName, item.ResidentID.ToString()));
                }
            }
        }
        private void LoadPositions()
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var objs = uow.OfficialPositions.GetAll();
                foreach (var item in objs)
                {
                    cboPosition.Items.Add(new ItemX(item.Title, item.OfficialPositionID.ToString()));
                }
            }
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

        private void txtSearch_OnIconRightClick(object sender, EventArgs e)
        {
            LoadMainGrid();
        }

        private void gridMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (gridMain.SelectedRows.Count > 0)
                {
                    // get id
                    officialID = int.Parse(gridMain.SelectedRows[0].Cells[0].Value.ToString());
                    if (officialID != 0)
                    {
                        SetData();
                    }
                    else
                    {
                        //LocalUtils.ShowNoRecordFoundMessage(this);

                    }

                }
                else
                {
                    LocalUtils.ShowNoRecordSelectedMessage(this);
                }
            }
            catch (Exception ex)
            {

                LocalUtils.ShowErrorMessage(this, ex.ToString());
            }
        }

        private void frmOfficials_Load(object sender, EventArgs e)
        {
            LoadMainGrid();
            LoadResidents();
            LoadPositions();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenRecord();
            
            
        }

        private void OpenRecord()
        {
            try
            {
                var residentID = int.Parse(((ItemX)cboResidents.SelectedItem).Value);
                var frm = new frmResidents(residentID);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {

                LocalUtils.ShowErrorMessage(this, "Error while opening resident record!");
            }
        }
    }
}
