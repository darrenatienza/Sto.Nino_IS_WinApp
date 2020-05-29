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
    public partial class frmHealthDataBoard : Form, IFormAction
    {
        private int origWidth;
        private int origHeight;

        public frmHealthDataBoard()
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
                    healthDataBoardID = int.Parse(gridMain.SelectedRows[0].Cells[0].Value.ToString());
                    if (healthDataBoardID != 0)
                    {
                        DialogResult result = MetroMessageBox.Show(this, "Are you sure you want to delete this record?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            var obj = uow.HealthDataBoard.Get(healthDataBoardID);
                            if (obj != null)
                            {
                                uow.HealthDataBoard.Remove(obj);
                                uow.Complete();
                                LoadMainGrid();
                                healthDataBoardID = 0;
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
                var ls = uow.HealthDataBoard.GetAllBy(txtSearch.Text,year);
                gridMain.Rows.Clear();
                int count = 0;
                foreach (var item in ls)
                {
                    count++;
                    gridMain.Rows.Add(new string[] {item.HealthDataBoardID.ToString(),
                        count.ToString(),
                        item.User.FirstName + " " + item.User.LastName,
                        item.CommonHealthProfile.Title,
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
                    healthDataBoardID = int.Parse(gridMain.SelectedRows[0].Cells[0].Value.ToString());
                    if (healthDataBoardID != 0)
                    {
                        var obj = uow.HealthDataBoard.Get(healthDataBoardID);
                        cboSurveyee.SelectedItem = LocalUtils.GetSelectedItemX(cboSurveyee.Items, obj.UserID.ToString());
                        cboCommonHealthProfile.SelectedItem = LocalUtils.GetSelectedItemX(cboCommonHealthProfile.Items, obj.CommonHealthProfileID.ToString());
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
                if (healthDataBoardID == 0)
                {
                    //add
                    var obj = new HealthDataBoard();
                    obj.CommonHealthProfileID = int.Parse(((ItemX)cboCommonHealthProfile.SelectedItem).Value);
                    obj.UserID = int.Parse(((ItemX)cboSurveyee.SelectedItem).Value);
                    obj.Count = Utils.ConvertToInteger(txtTally.Text);
                    obj.Year = Utils.ConvertToInteger(txtYear.Text);
                    uow.HealthDataBoard.Add(obj);
                    uow.Complete();
                    healthDataBoardID = obj.HealthDataBoardID;

                }
                else
                {
                    //edit
                    var obj = uow.HealthDataBoard.Get(healthDataBoardID);
                    obj.CommonHealthProfileID = int.Parse(((ItemX)cboCommonHealthProfile.SelectedItem).Value);
                    obj.UserID = int.Parse(((ItemX)cboSurveyee.SelectedItem).Value);
                    obj.Count = Utils.ConvertToInteger(txtTally.Text);
                    obj.Year = Utils.ConvertToInteger(txtYear.Text);
                    uow.HealthDataBoard.Edit(obj);
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
            healthDataBoardID = 0;
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
        private void LoadCommonHealthProfile()
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var objs = uow.CommonHealthProfiles.GetAll();
                foreach (var item in objs)
                {
                    cboCommonHealthProfile.Items.Add(new ItemX(item.Title, item.CommonHealthProfileID.ToString()));
                }
            }
        }

        public int healthDataBoardID { get; set; }

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
            LoadCommonHealthProfile();
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


                   var healthDataboard = uow.HealthDataBoard.GetAllBy(txtSearch.Text,dtSearchYear.Value.Year);
                    
                    var ds = new dsReport();
                    DataTable t = ds.Tables["HealthDataboard"];
                    DataRow r;
                    int count = 0;
                    foreach (var item in healthDataboard)
                    {
                        count++;
                        r = t.NewRow();
                        r["i"] = count.ToString();
                        r["Surveyee"] = item.User.FirstName + " " + item.User.LastName;
                        r["CommonHealthProfile"] = item.CommonHealthProfile.Title;
                        r["Tally"] = item.Count;
                        t.Rows.Add(r);
                    }
                    rpt.LocalReport.DataSources.Clear();
                    var p_year = new ReportParameter("Year",dtSearchYear.Value.Year.ToString());
                    this.rpt.LocalReport.SetParameters(new ReportParameter[] {
                        p_year
                       });
                    ReportDataSource rds = new ReportDataSource("HealthDataboard", t);
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
