using Bunifu.Framework.UI;
using Bunifu.UI.WinForms.BunifuTextbox;
using Helpers;
using MetroFramework;
using Microsoft.Reporting.WinForms;
using Sto.NinoRMS.App.Helpers.Themes;
using Sto.NinoRMS.App.Properties;
using Sto.NinoRMS.App.Reports;
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
    public partial class frmQuarterlyReport : Form, IFormAction
    {
        private int origWidth;
        private int origHeight;

        public frmQuarterlyReport()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
                    quarterlyReportID = int.Parse(gridMain.SelectedRows[0].Cells[0].Value.ToString());
                    if (quarterlyReportID != 0)
                    {
                        DialogResult result = MetroMessageBox.Show(this, "Are you sure you want to delete this record?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            var obj = uow.QuarterlyReports.Get(quarterlyReportID);
                            if (obj != null)
                            {
                                uow.QuarterlyReports.Remove(obj);
                                uow.Complete();
                                LoadMainGrid();
                                quarterlyReportID = 0;
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
                string criteria = txtSearch.Text;
                int year = dtSearchYear.Value.Year;
                int quarter = Utils.ConvertToInteger(cboSearchQuarter.Text);
                var ls = uow.QuarterlyReports.GetAllBy(txtSearch.Text,year,quarter);
                gridMain.Rows.Clear();
                int count = 0;
                foreach (var item in ls)
                {
                    count++;
                    gridMain.Rows.Add(new string[] {item.QuarterlyReportID.ToString(),
                        count.ToString(),
                        item.User.FirstName + " " + item.User.LastName,
                        item.Accomplishment.Title,
                        item.Gender,
                        item.Year.ToString(),
                        item.Count.ToString()
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
                    quarterlyReportID = int.Parse(gridMain.SelectedRows[0].Cells[0].Value.ToString());
                    if (quarterlyReportID != 0)
                    {
                        var obj = uow.QuarterlyReports.Get(quarterlyReportID);
                        cboSurveyee.SelectedItem = LocalUtils.GetSelectedItemX(cboSurveyee.Items, obj.UserID.ToString());
                        cboAccomplishment.SelectedItem = LocalUtils.GetSelectedItemX(cboAccomplishment.Items, obj.AccomplishmentID.ToString());
                        cboGender.SelectedItem = LocalUtils.GetSelectedItemXString(cboGender.Items, obj.Gender);
                        txtYear.Text = obj.Year.ToString();
                        txtTally.Text = obj.Count.ToString();
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
                if (quarterlyReportID == 0)
                {
                    //add
                    var obj = new QuarterlyReport();
                    obj.AccomplishmentID = int.Parse(((ItemX)cboAccomplishment.SelectedItem).Value);
                    obj.UserID = int.Parse(((ItemX)cboSurveyee.SelectedItem).Value);
                    obj.Count = Utils.ConvertToInteger(txtTally.Text);
                    obj.Year = Utils.ConvertToInteger(txtYear.Text);
                    obj.Gender = cboGender.Text;
                    uow.QuarterlyReports.Add(obj);
                    uow.Complete();
                    quarterlyReportID = obj.QuarterlyReportID;

                }
                else
                {
                    //edit
                    var obj = uow.QuarterlyReports.Get(quarterlyReportID);
                    obj.AccomplishmentID = int.Parse(((ItemX)cboAccomplishment.SelectedItem).Value);
                    obj.UserID = int.Parse(((ItemX)cboSurveyee.SelectedItem).Value);
                    obj.Count = Utils.ConvertToInteger(txtTally.Text);
                    obj.Year = Utils.ConvertToInteger(txtYear.Text);
                    obj.Gender = cboGender.Text;
                    uow.QuarterlyReports.Edit(obj);
                    uow.Complete();
                }
                pages.SetPage(pageList);
                LoadMainGrid();
                LocalUtils.ShowSaveMessage(this);
                ResetInputs();
            }
        }

        public void Add()
        {
            ResetInputs();
            quarterlyReportID = 0;
            txtYear.Text = DateTime.Now.Year.ToString();
            LocalUtils.ShowAddNewMessage(this);
            pages.SetPage(pageDetail);

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

        private void LoadSurveyee()
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var objs = uow.Users.GetAll();
                foreach (var item in objs)
                {
                    cboSurveyee.Items.Add(new ItemX(item.FirstName + " " + item.LastName, item.UserID.ToString()));
                }
            }
        }
        private void LoadAccomplishment()
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var objs = uow.Accomplishments.GetAll();
                foreach (var item in objs)
                {
                    cboAccomplishment.Items.Add(new ItemX(item.Title, item.AccomplishmentID.ToString()));
                }
            }
        }
        private void LoadGender()
        {
      
            cboGender.Items.Add(new ItemX("Male", "1"));
            cboGender.Items.Add(new ItemX("Female", "2"));
            
        }

        public int quarterlyReportID { get; set; }

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

        private void frmHealthDataBoard_Load(object sender, EventArgs e)
        {
            origWidth = this.Width;
            origHeight = this.Height;
            pages.SetPage(pageList);
            SetButtonImageVisible();
            LoadMainGrid();
            LoadSurveyee();
            LoadAccomplishment();
            LoadGender();
            this.rpt.RefreshReport();
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            pages.SetPage(pagePrintPreview);
          
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

                var quarter = Utils.ConvertToInteger(cboSearchQuarter.Text);
                   var healthDataboard = uow.QuarterlyReports.GetAllBy(txtSearch.Text,dtSearchYear.Value.Year,quarter);
                    
                    var ds = new dsReport();
                    DataTable t = ds.Tables["QuarterlyReport"];
                    DataRow r;
                    int count = 0;
                    foreach (var item in healthDataboard)
                    {
                        count++;
                        r = t.NewRow();
                        r["i"] = count.ToString();
                        r["Surveyee"] = item.User.FirstName + " " + item.User.LastName;
                        r["Accomplishment"] = item.Accomplishment.Title;
                        r["Gender"] = item.Gender;
                        r["Tally"] = item.Count;
                        t.Rows.Add(r);
                    }
                    rpt.LocalReport.DataSources.Clear();
                    var p_year = new ReportParameter("Year",dtSearchYear.Value.Year.ToString());
                    var p_quarter = new ReportParameter("Quarter", quarter.ToString());
                    this.rpt.LocalReport.SetParameters(new ReportParameter[] {
                        p_year,
                        p_quarter
                       });
                    ReportDataSource rds = new ReportDataSource("QuarterlyReport", t);
                    rpt.LocalReport.DataSources.Add(rds);
                    rpt.SetDisplayMode(DisplayMode.PrintLayout);
                    rpt.ZoomMode = ZoomMode.PageWidth;
                    this.rpt.RefreshReport();
                }
            }
            catch (Exception ex)
            {

                LocalUtils.ShowErrorMessage(this, ex.ToString());
            }
            
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            pages.SetPage(pageList);
            
           
        }

        private void btnBack2_Click(object sender, EventArgs e)
        {
            pages.SetPage(pageList);

            SetFormSize(origWidth, origHeight);
            this.CenterToScreen();
        }

      
        private void gridMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            pages.SetPage(pageDetail);
            SetData();
        }

        private void cboCommonHealthProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void txtTally_OnIconRightClick(object sender, EventArgs e)
        {
            int tally = Utils.ConvertToInteger(txtTally.Text);
            tally++;
            txtTally.Text = tally.ToString();
            txtTally.SelectionStart = txtTally.TextLength;
        }

        private void txtTally_OnIconLeftClick(object sender, EventArgs e)
        {
            int tally = Utils.ConvertToInteger(txtTally.Text);
            tally--;
            txtTally.Text = tally.ToString();
            txtTally.SelectionStart = txtTally.TextLength;
        }

        private void txtTally_TextChanged(object sender, EventArgs e)
        {
           
            
           
        }

        private void txtTally_TextChange(object sender, EventArgs e)
        {
            
        }

        private void txtTally_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
               
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
            txtTally.SelectionStart = txtTally.TextLength;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            pages.SetPage(pagePrintPreview);
            PrintPreview();
        }

        private void gridMain_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        
    }
}
