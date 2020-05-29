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
    public partial class frmBrgyClearance : Form, IFormAction
    {
        private int origWidth;
        private int origHeight;
        private int printWidth;
        private int printHeight;

        public frmBrgyClearance()
        {
            InitializeComponent();
            printWidth = LocalUtils.WindowPrintWidth;
            printHeight = LocalUtils.WindowPrintHeight;
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
                    brgyClearanceID = int.Parse(gridMain.SelectedRows[0].Cells[0].Value.ToString());
                    if (brgyClearanceID != 0)
                    {
                        DialogResult result = MetroMessageBox.Show(this, "Are you sure you want to delete this record?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            var obj = uow.BrgyClearances.Get(brgyClearanceID);
                            if (obj != null)
                            {
                                uow.BrgyClearances.Remove(obj);
                                uow.Complete();
                                LoadMainGrid();
                                brgyClearanceID = 0;
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
                var ls = uow.BrgyClearances.GetAllBy(txtSearch.Text);
                gridMain.Rows.Clear();
                int count = 0;
                foreach (var item in ls)
                {
                    count++;
                    gridMain.Rows.Add(new string[] {item.BrgyClearanceID.ToString(),
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
                    brgyClearanceID = int.Parse(gridMain.SelectedRows[0].Cells[0].Value.ToString());
                    if (brgyClearanceID != 0)
                    {
                        var obj = uow.BrgyClearances.Get(brgyClearanceID);
                        cboResidents.SelectedItem = LocalUtils.GetSelectedItemX(cboResidents.Items, obj.ResidentID.ToString()); cboResidents.SelectedItem = LocalUtils.GetSelectedItemX(cboResidents.Items, obj.ResidentID.ToString());
                        txtTaxCert.Text = obj.CommTaxtCert;
                        dtDateIssued.Value= obj.DateIssued;
                        txtPlaceIssued.Text = obj.PlaceIssued;
                        txtPurpose.Text = obj.PurposeOfRequest;
                        txtTinNum.Text = obj.TinNo;
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
                if (brgyClearanceID == 0)
                {
                    //add
                    var obj = new BrgyClearance();
                    obj.CommTaxtCert = txtTaxCert.Text;
                    obj.ResidentID = int.Parse(((ItemX)cboResidents.SelectedItem).Value);
                    obj.DateIssued = dtDateIssued.Value;
                    obj.PlaceIssued = txtPlaceIssued.Text;
                    obj.PurposeOfRequest = txtPurpose.Text;
                    obj.TinNo = txtTinNum.Text;
                    uow.BrgyClearances.Add(obj);
                    uow.Complete();
                    brgyClearanceID = obj.BrgyClearanceID;

                }
                else
                {
                    //edit
                    var obj = uow.BrgyClearances.Get(brgyClearanceID);
                    obj.CommTaxtCert = txtTaxCert.Text;
                    obj.ResidentID = int.Parse(((ItemX)cboResidents.SelectedItem).Value);
                    obj.DateIssued = dtDateIssued.Value;
                    obj.PlaceIssued = txtPlaceIssued.Text;
                    obj.PurposeOfRequest = txtPurpose.Text;
                    obj.TinNo = txtTinNum.Text;
                    uow.BrgyClearances.Edit(obj);
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
            brgyClearanceID = 0;
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
                foreach (var item in objs)
                {
                    cboResidents.Items.Add(new ItemX(item.FirstName + " " + item.LastName, item.ResidentID.ToString()));
                }
            }
        }

        public int brgyClearanceID { get; set; }

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

        private void frmBrgyClearance_Load(object sender, EventArgs e)
        {
            origWidth = this.Width;
            origHeight = this.Height;

            SetButtonImageVisible();
            LoadMainGrid();
            LoadResidents();
            this.rpt.RefreshReport();
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
           
           
            PrintPreview();
        }

        private void SetFormSize(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        private void PrintPreview()
        {

            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {


                    var brgyClearance = uow.BrgyClearances.GetBy(brgyClearanceID);
                    if (brgyClearance != null)
                    {


                        var currentDate = DateTime.Now;
                        rpt.LocalReport.DataSources.Clear();
                        var p_date = new ReportParameter("Date", currentDate.ToShortDateString());
                        var p_FullName = new ReportParameter("FullName", brgyClearance.Resident.FirstName + " " + brgyClearance.Resident.MiddleName + " " + brgyClearance.Resident.LastName);
                        var p_Age = new ReportParameter("Age", Utils.CalculateAge(brgyClearance.Resident.Birthday).ToString());
                        var p_Reason = new ReportParameter("Reason", brgyClearance.PurposeOfRequest);
                        var p_taxNum = new ReportParameter("TaxNum", brgyClearance.CommTaxtCert);
                        var p_dateIssued = new ReportParameter("DateIssued", brgyClearance.DateIssued.ToShortDateString());
                        var p_placeIssued = new ReportParameter("PlaceIssued", brgyClearance.PlaceIssued);
                        var p_tinNum = new ReportParameter("TinNum", brgyClearance.TinNo);


                        this.rpt.LocalReport.SetParameters(new ReportParameter[] {
                        p_date,
                        p_FullName, 
                        p_Age,
                        p_Reason,
                        p_taxNum,
                        p_dateIssued,
                        p_placeIssued,
                        p_tinNum});

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

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            pages.SetPage(pageDetail);
            
            SetFormSize(origWidth, origHeight);
            this.CenterToScreen();
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

        private void pageDetail_Click(object sender, EventArgs e)
        {

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenRecord();
        }

    }
}
