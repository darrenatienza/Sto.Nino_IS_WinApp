using Bunifu.Framework.UI;
using Bunifu.UI.WinForms.BunifuTextbox;
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
    public partial class frmBusinessClearance : Form, IFormAction
    {
        private int bizClearanceID;
        private int origWidth;
        private int origHeight;

        public frmBusinessClearance()
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

        public void Delete()
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {


                if (gridMain.SelectedRows.Count > 0)
                {
                    bizClearanceID = int.Parse(gridMain.SelectedRows[0].Cells[0].Value.ToString());
                    if (bizClearanceID != 0)
                    {
                        DialogResult result = MetroMessageBox.Show(this, "Are you sure you want to delete this record?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            var obj = uow.BrgyBizClearances.Get(bizClearanceID);
                            if (obj != null)
                            {
                                uow.BrgyBizClearances.Remove(obj);
                                uow.Complete();
                                LoadMainGrid();
                                bizClearanceID = 0;
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
                var ls = uow.BrgyBizClearances.GetAllBy(txtSearch.Text);
                gridMain.Rows.Clear();
                int count = 0;
                foreach (var item in ls)
                {
                    count++;
                    gridMain.Rows.Add(new string[] {item.BrgyBizClearanceID.ToString(),
                        count.ToString(),
                        item.Resident.FirstName + " " + item.Resident.LastName


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
                    bizClearanceID = int.Parse(gridMain.SelectedRows[0].Cells[0].Value.ToString());
                    if (bizClearanceID != 0)
                    {
                        var obj = uow.BrgyBizClearances.Get(bizClearanceID);
                        cboResidents.SelectedItem = LocalUtils.GetSelectedItemX(cboResidents.Items, obj.ResidentID.ToString()); cboResidents.SelectedItem = LocalUtils.GetSelectedItemX(cboResidents.Items, obj.ResidentID.ToString());
                        txtBizName.Text = obj.BizName;
                        radioIsFollowing1.Checked = false;
                        radioIsFollowing2.Checked = false;
                        radioIsHinder1.Checked = false;
                        radioIsHinder2.Checked = false;
                        if (obj.isFollowingAllProvision)
                        {
                            radioIsFollowing1.Checked = true;
                        }
                        else
                        {
                            radioIsFollowing2.Checked = true;
                        }
                        if (obj.isHinderPermitApplication)
                        {
                            radioIsHinder1.Checked= true;
                        }
                        else
                        {
                            radioIsHinder2.Checked = true;
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

        public void Save()
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                if (!ValidatedFields())
                {
                    return;
                }
                if (bizClearanceID == 0)
                {
                    //add
                    var obj = new BrgyBizClearance();
                    obj.BizName = txtBizName.Text;
                    obj.ResidentID = int.Parse(((ItemX)cboResidents.SelectedItem).Value);
                    if (radioIsFollowing1.Checked)
                    {
                        obj.isFollowingAllProvision = true;
                    }
                    else
                    {
                        obj.isFollowingAllProvision = false;
                    }
                    if (radioIsHinder1.Checked)
                    {
                        obj.isHinderPermitApplication = true;
                    }
                    else
                    {
                        obj.isHinderPermitApplication = false;
                    }
                    
                    uow.BrgyBizClearances.Add(obj);
                    uow.Complete();
                    bizClearanceID = obj.BrgyBizClearanceID;

                }
                else
                {
                    //edit
                    var obj = uow.BrgyBizClearances.Get(bizClearanceID);
                    obj.BizName = txtBizName.Text;
                    obj.ResidentID = int.Parse(((ItemX)cboResidents.SelectedItem).Value);
                    if (radioIsFollowing1.Checked)
                    {
                        obj.isFollowingAllProvision = true;
                    }
                    else
                    {
                        obj.isFollowingAllProvision = false;
                    }
                    if (radioIsHinder1.Checked)
                    {
                        obj.isHinderPermitApplication = true;
                    }
                    else
                    {
                        obj.isHinderPermitApplication = false;
                    }
                    uow.BrgyBizClearances.Edit(obj);
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
            bizClearanceID = 0;
            LocalUtils.ShowAddNewMessage(this);

        }

        public void ResetInputs()
        {
            var textboxes = LocalUtils.GetAll(pages, typeof(BunifuTextBox));
            foreach (BunifuTextBox item in textboxes)
            {
                item.Text = string.Empty;
            }
            var comboboxes = LocalUtils.GetAll(pages, typeof(Bunifu.UI.WinForms.BunifuDropdown));
            foreach (Bunifu.UI.WinForms.BunifuDropdown item in comboboxes)
            {
                item.SelectedIndex = -1;
                item.Enabled = true;
            }
            var dates = LocalUtils.GetAll(pages, typeof(BunifuDatepicker));
            foreach (BunifuDatepicker item in dates)
            {
                item.Value = DateTime.UtcNow;
            }
        }


        public bool ValidatedFields()
        {

            var ctrlTextBoxes2 = LocalUtils.GetAll(pages, typeof(BunifuTextBox))
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

            var ctrlDropdown = LocalUtils.GetAll(pages, typeof(Bunifu.UI.WinForms.BunifuDropdown))
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
                foreach (var item in objs)
                {
                    cboResidents.Items.Add(new ItemX(item.FirstName + " " + item.LastName, item.ResidentID.ToString()));
                }
            }
        }

        private void frmBusinessClearance_Load(object sender, EventArgs e)
        {
            origWidth = this.Width;
            origHeight = this.Height;

            SetButtonImageVisible();
            LoadResidents();
            LoadMainGrid();
        }

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

        private void gridMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SetData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void SetFormSize(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }
        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            
            PrintPreview();
        }
        private void PrintPreview()
        {

            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {


                    var brgyBizClearance = uow.BrgyBizClearances.GetBy(bizClearanceID);
                    if (brgyBizClearance != null)
                    {


                        var currentDate = DateTime.Now;
                        rpt.LocalReport.DataSources.Clear();
                        var p_date = new ReportParameter("Date", currentDate.ToShortDateString());
                        var p_bizName = new ReportParameter("BizName", brgyBizClearance.BizName);
                        var p_operator = new ReportParameter("Operator", brgyBizClearance.Resident.FirstName + " " + brgyBizClearance.Resident.MiddleName + " " + brgyBizClearance.Resident.LastName);
                        ReportParameter p_select1 = new ReportParameter("Select1");
                        ReportParameter p_select2 = new ReportParameter("Select2");
                        ReportParameter p_select3 = new ReportParameter("Select3");
                        ReportParameter p_select4 = new ReportParameter("Select4");
                        if (brgyBizClearance.isHinderPermitApplication)
                        {
                            p_select3.Values.Add("X");
                            p_select4.Values.Add("_");
                        }
                        else
                        {
                            p_select4.Values.Add("X");
                            p_select3.Values.Add("_");
                        }
                        if (brgyBizClearance.isFollowingAllProvision)
                        {
                            p_select1.Values.Add("X");
                            p_select2.Values.Add("_");
                        }
                        else
                        {
                            p_select2.Values.Add("X");
                            p_select1.Values.Add("_");
                        }



                        this.rpt.LocalReport.SetParameters(new ReportParameter[] {
                        p_date,
                        p_bizName,
                        p_operator, 
                        p_select1,
                        p_select2,
                        p_select3,
                        p_select4});

                        rpt.SetDisplayMode(DisplayMode.PrintLayout);
                        rpt.ZoomMode = ZoomMode.PageWidth;
                        this.rpt.RefreshReport();

                        SetFormSize(LocalUtils.WindowPrintWidth, LocalUtils.WindowPrintHeight);
                        pages.SetPage(pagePrintPreview);
                        this.CenterToScreen();
                    }
                    else
                    {
                        LocalUtils.ShowNoRecordSelectedMessage(this);
                    }
                }
            }
            catch (Exception ex)
            {

                LocalUtils.ShowErrorMessage(this, ex.ToString());
            }

        }
        /// <summary>
        ///fix for unexpected hiding of bunifu flat button image
        /// </summary>
        private void SetButtonImageVisible()
        {
            var buttons = LocalUtils.GetAll(this, typeof(BunifuFlatButton));
            foreach (BunifuFlatButton item in buttons)
            {
                item.IconVisible = true;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            pages.SetPage(pageDetail);

            SetFormSize(origWidth, origHeight);
            this.CenterToScreen();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

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
