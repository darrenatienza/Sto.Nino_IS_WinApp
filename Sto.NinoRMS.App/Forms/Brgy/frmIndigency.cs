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
    public partial class frmIndigency : Form, IFormAction
    {
        private int indigencyID;
        private int origWidth;
        private int origHeight;

        public frmIndigency()
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
                    indigencyID = int.Parse(gridMain.SelectedRows[0].Cells[0].Value.ToString());
                    if (indigencyID != 0)
                    {
                        DialogResult result = MetroMessageBox.Show(this, "Are you sure you want to delete this record?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            var obj = uow.Indigencies.Get(indigencyID);
                            if (obj != null)
                            {
                                uow.Indigencies.Remove(obj);
                                uow.Complete();
                                LoadMainGrid();
                                indigencyID = 0;
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
                var ls = uow.Indigencies.GetAllBy(txtSearch.Text);
                gridMain.Rows.Clear();
                int count = 0;
                foreach (var item in ls)
                {
                    count++;
                    gridMain.Rows.Add(new string[] {item.IndigencyID.ToString(),
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
                    indigencyID = int.Parse(gridMain.SelectedRows[0].Cells[0].Value.ToString());
                    if (indigencyID != 0)
                    {
                        var obj = uow.Indigencies.GetBy(indigencyID);
                        cboResidents.SelectedItem = LocalUtils.GetSelectedItemX(cboResidents.Items, obj.ResidentID.ToString()); cboResidents.SelectedItem = LocalUtils.GetSelectedItemX(cboResidents.Items, obj.ResidentID.ToString());
                        txtOccupation.Text = obj.Resident.Occupation;
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
                if (indigencyID == 0)
                {
                    //add
                    var obj = new Indigency();
                   
                    obj.ResidentID = int.Parse(((ItemX)cboResidents.SelectedItem).Value);
                   
                    uow.Indigencies.Add(obj);
                    uow.Complete();
                    indigencyID = obj.IndigencyID;

                }
                else
                {
                    //edit
                    var obj = uow.Indigencies.Get(indigencyID);              
                    obj.ResidentID = int.Parse(((ItemX)cboResidents.SelectedItem).Value);
                    uow.Indigencies.Edit(obj);
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
            indigencyID = 0;
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

        private void txtSearch_OnIconRightClick(object sender, EventArgs e)
        {
            LoadMainGrid();
        }

        private void gridMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SetData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Add();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void frmIndigency_Load(object sender, EventArgs e)
        {
            origWidth = this.Width;
            origHeight = this.Height;
            SetButtonImageVisible();
            LoadMainGrid();
            LoadResidents();
        }
        private void PrintPreview()
        {

            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var indigency = uow.Indigencies.GetBy(indigencyID);
                    if (indigency != null)
                    {


                        var currentDate = DateTime.Now;
                        rpt.LocalReport.DataSources.Clear();

                        var p_FullName = new ReportParameter("FullName", indigency.Resident.FirstName + " " + indigency.Resident.MiddleName + " " + indigency.Resident.LastName);
                        var age = Utils.CalculateAge(indigency.Resident.Birthday);

                        var p_age = new ReportParameter("Age", age.ToString());
                        var p_occupation = new ReportParameter("Occupation", indigency.Resident.Occupation);
                        var day = currentDate.Date.Day;
                        var p_day = new ReportParameter("Day", day.ToString() + " " + Utils.GetDaySuffix(day));
                        var p_month = new ReportParameter("Month", currentDate.ToString("MMMM"));
                        var p_year = new ReportParameter("Year", currentDate.Year.ToString());



                        this.rpt.LocalReport.SetParameters(new ReportParameter[] {
                        p_age,
                        p_FullName, 
                        p_occupation,
                        p_day,
                        p_month,
                        p_year
                        });

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
        private void cboResidents_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetOtherData();
        }

        private void SetOtherData()
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                if (cboResidents.SelectedIndex >= 0)
                {
                    var residentID = int.Parse(((ItemX)cboResidents.SelectedItem).Value);
                    if (residentID != 0)
                    {
                        var obj = uow.Residents.Get(residentID);
                        txtAge.Text = Utils.CalculateAge(obj.Birthday).ToString();
                        txtOccupation.Text = obj.Occupation;

                    }
                    else
                    {
                        LocalUtils.ShowNoRecordFoundMessage(this);
                    }
                }
                

            }
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

        private void btnBack_Click(object sender, EventArgs e)
        {
            pages.SetPage(pageDetail);

            SetFormSize(origWidth, origHeight);
            this.CenterToScreen();
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
                frm.OnRecordSave += frm_OnRecordSave;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {

                LocalUtils.ShowErrorMessage(this, "Error while opening resident record!");
            }
        }

        void frm_OnRecordSave(object sender, EventArgs e)
        {
            SetData();
        }


        
    }
}
