using Bunifu.Framework.UI;
using Bunifu.UI.WinForms;
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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sto.NinoRMS.App
{
    public partial class frmResidents : Form, IFormAction
    {
        private int residentID;
        private string destinationPath = "";
        private string imageFileName = "";
        private string imageSourcePath = "";
        private int origWidth;
        private int origHeight;
        private int childID;
        private int educationID;
        public event EventHandler OnRecordSave;
        public frmResidents()
        {
            InitializeComponent();
        }

        public frmResidents(int residentID)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.residentID = residentID;
            btnDelete.Enabled = false;
            btnAdd.Enabled = false;
            gridMain.Enabled = false;
            txtSearch.Enabled = false;
            txtFirstName.ReadOnly = true;
            txtMiddleName.ReadOnly = true;
            txtLastName.ReadOnly = true;
            toolTip1.SetToolTip(txtFirstName, "To edit, please open Resident's Record on Dashboard!");
            toolTip1.SetToolTip(txtMiddleName, "To edit, please open Resident's Record on Dashboard!");
            toolTip1.SetToolTip(txtLastName, "To edit, please open Resident's Record on Dashboard!");
        }
        void RecordSave()
        {
            var handler = this.OnRecordSave;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
        private void btnMenu_Click(object sender, EventArgs e)
        {
            
        }
      
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void LoadChildrensGridList()
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var ls = uow.Childrens.GetAll(residentID);
                gridChildren.Rows.Clear();
                int count = 0;
                foreach (var item in ls)
                {
                    count++;
                    gridChildren.Rows.Add(new string[] {item.ChildrenID.ToString(),
                        count.ToString(),
                        item.FullName,
                        item.Age.ToString(),
                        item.Occupation

                });
                }
               

            }
        }
        void LoadEducationGridList()
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var ls = uow.Educations.GetAll(residentID);
                var grid = gridEducation;
                grid.Rows.Clear();
                int count = 0;
                foreach (var item in ls)
                {
                    count++;
                    grid.Rows.Add(new string[] {item.EducationID.ToString(),
                        count.ToString(),
                        item.Level,
                        item.SchoolName,
                        item.InclusiveDate,
                        item.Course

                });
                }
                

            }
        }

        void LoadEducationLevel()
        {
            string[] levels = { "Primary", "Secondary", "Tertiary", "Vocational" };
            foreach (var item in levels)
            {
                cboEducationLevel.Items.Add(new ItemX(item, item));
            }
        }
        void LoadGender()
        {
            string[] levels = { "Male", "Female"};
            foreach (var item in levels)
            {
                cboGender.Items.Add(new ItemX(item, item));
            }
        }
        void LoadCivilStatus()
        {
            string[] levels = { "Single", "Married", "Widowed", "Separated" };
            foreach (var item in levels)
            {
                cboCivilStatus.Items.Add(new ItemX(item, item));
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void frmResidents_Load(object sender, EventArgs e)
        {
            
            destinationPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "Sto.NinoIS");
            destinationPath = Resources.ImageStorage;
            SetButtonImageVisible();
            origHeight = this.Height;
            origWidth = this.Width;
            LoadMainGrid();
            LoadEducationLevel();
            LoadGender();
            LoadCivilStatus();
            // open from other form
            if (residentID > 0)
            {
                SetData();
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
        private void txtSearch_OnIconRightClick(object sender, EventArgs e)
        {
            LoadMainGrid();
        }

        private void gridMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            Edit();

        }

        private void Edit()
        {
            try
            {

                if (gridMain.SelectedRows.Count > 0)
                {
                    // get id
                    residentID = int.Parse(gridMain.SelectedRows[0].Cells[0].Value.ToString());
                    if (residentID != 0)
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

        private void dtBday_ValueChanged(object sender, EventArgs e)
        {
            txtAge.Text = Utils.CalculateAge(dtBday.Value).ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Add();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }



        public void Delete()
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {


                if (gridMain.SelectedRows.Count > 0)
                {
                    residentID = int.Parse(gridMain.SelectedRows[0].Cells[0].Value.ToString());
                    if (residentID != 0)
                    {
                        DialogResult result = MetroMessageBox.Show(this, "Are you sure you want to delete this record?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            var obj = uow.Residents.Get(residentID);
                            if (obj != null)
                            {
                                uow.Residents.Remove(obj);
                                uow.Complete();
                                LoadMainGrid();
                                residentID = 0;
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
                var ls = uow.Residents.GetAllBy(txtSearch.Text);
                gridMain.Rows.Clear();
                int count = 0;
                foreach (var item in ls)
                {
                    count++;
                    gridMain.Rows.Add(new string[] {item.ResidentID.ToString(),
                        count.ToString(),
                        item.FirstName + " " + item.MiddleName + " " + item.LastName

                });
                }

            }
        }

        public void SetData()
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {
                ResetInputs(pageChildren);
                ResetInputs(pageEducation);
                ResetInputs(pnlDetail);
                var obj = uow.Residents.Get(residentID);
                txtFirstName.Text = obj.FirstName;
                txtMiddleName.Text = obj.MiddleName;
                txtLastName.Text = obj.LastName;
                dtBday.Value = obj.Birthday;
                txtAge.Text = Utils.CalculateAge(obj.Birthday).ToString();
                txtContactNum.Text = obj.ContactNumber;
                txtOccupation.Text = obj.Occupation;
                txtPurok.Text = obj.Purok;
                txtSitio.Text = obj.Sitio;
                imageFileName = obj.ImageName;

                cboGender.SelectedItem = LocalUtils.GetSelectedItemX(cboGender.Items, obj.Gender);
                cboCivilStatus.SelectedItem = LocalUtils.GetSelectedItemX(cboCivilStatus.Items, obj.CivilStatus);
                txtFathersName.Text = obj.FathersFullName;
                txtFathersOccupation.Text = obj.FathersOccupation;
                txtMothersName.Text = obj.MothersFullName;
                txtMothersOccupation.Text = obj.MothersOccupation;
                txtSpouseName.Text = obj.SpouseFullName;
                txtSpouseOccupation.Text = obj.SpouseOccupation;

                SetImage(obj.ImageName);
                LoadChildrensGridList();
                LoadEducationGridList();
               

            }
        }
        private void SetImage(string imageName)
        {
            if (imageName != "" && CheckImageExists(imageFileName))
            {
                string imgPath = Path.Combine(destinationPath, imageName);
                using (var img = new Bitmap(imgPath))
                {
                    pic.Image = new Bitmap(img);
                }

            }
            else
            {
                pic.Image = null;
            }
        }
        public void Save()
        {
           using (var uow = new UnitOfWork(new DataContext()))
            {
                var isValidMobileNumber = Utils.IsValidMobileNumber(txtContactNum.Text);
                if (!ValidatedFields(pnlDetail) || !isValidMobileNumber)
                {
                    
                    return; 
                }
                if (residentID == 0)
                {                    
                    //add
                    var obj = new Resident();
                    obj.FirstName = txtFirstName.Text;
                    obj.MiddleName = txtMiddleName.Text;
                    obj.LastName = txtLastName.Text;
                    obj.Birthday = dtBday.Value;
                    obj.ContactNumber = txtContactNum.Text;
                    obj.FullName = txtFirstName.Text;
                    obj.Occupation = txtOccupation.Text;
                    obj.Purok = txtPurok.Text;
                    obj.Sitio = txtSitio.Text;
                    obj.ImageName = imageFileName;
                    obj.Gender = cboGender.Text;
                    obj.CivilStatus = cboCivilStatus.Text;
                    obj.FathersFullName = txtFathersName.Text;
                    obj.FathersOccupation = txtFathersOccupation.Text;
                    obj.MothersFullName = txtMothersName.Text;
                    obj.MothersOccupation = txtMothersOccupation.Text;
                    obj.SpouseFullName = txtSpouseName.Text;
                    obj.SpouseOccupation = txtSpouseOccupation.Text;
                    uow.Residents.Add(obj);
                    uow.Complete();
                    residentID = obj.ResidentID;

                }
                else
                {
                    //edit
                    var obj = uow.Residents.Get(residentID);
                    obj.FirstName = txtFirstName.Text;
                    obj.MiddleName = txtMiddleName.Text;
                    obj.LastName = txtLastName.Text;
                    obj.Birthday = dtBday.Value;
                    obj.ContactNumber = txtContactNum.Text;
                    obj.FullName = txtFirstName.Text;
                    obj.Occupation = txtOccupation.Text;
                    obj.Purok = txtPurok.Text;
                    obj.Sitio = txtSitio.Text;
                    obj.ImageName = imageFileName;
                    obj.Gender = cboGender.Text;
                    obj.CivilStatus = cboCivilStatus.Text;
                    obj.FathersFullName = txtFathersName.Text;
                    obj.FathersOccupation = txtFathersOccupation.Text;
                    obj.MothersFullName = txtMothersName.Text;
                    obj.MothersOccupation = txtMothersOccupation.Text;
                    obj.SpouseFullName = txtSpouseName.Text;
                    obj.SpouseOccupation = txtSpouseOccupation.Text;
                    uow.Residents.Edit(obj);
                    uow.Complete();
                }
                RecordSave();
                CopyImage();
                LoadMainGrid();
                LocalUtils.ShowSaveMessage(this);
            }
        }

        public void SaveChild()
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    if (!ValidatedFields(pageChildren))
                    {
                        
                        return;
                    }
                    if (childID == 0)
                    {
                        //add
                        var obj = new Children();
                        obj.ResidentID = residentID;
                        obj.Age = Utils.ConvertToInteger(txtChildAge.Text);
                        obj.FullName = txtChildFullName.Text;
                        obj.Occupation = txtChildOccupation.Text;
                        uow.Childrens.Add(obj);
                        uow.Complete();
                        childID = obj.ChildrenID;

                    }
                    else
                    {
                        //edit
                        var obj = uow.Childrens.Get(childID);
                        obj.Age = Utils.ConvertToInteger(txtChildAge.Text);
                        obj.FullName = txtChildFullName.Text;
                        obj.Occupation = txtChildOccupation.Text;
                        uow.Childrens.Edit(obj);
                        uow.Complete();
                    }

                    LoadChildrensGridList();
                    LocalUtils.ShowSaveMessage(this);
                }
            }
            catch (Exception ex)
            {

                LocalUtils.ShowErrorMessage(this, ex.ToString());
            }
            
        }
        private void CopyImage()
        {
            if (imageSourcePath != "")
            {
                if (!CheckImageExists(imageFileName))
                {
                    File.Copy(imageSourcePath, String.Format("{0}\\{1}", destinationPath, imageFileName), true);
                    imageSourcePath = "";
                }
            }
        }
        private bool CheckImageExists(string fileName)
        {
            return Directory.GetFiles(destinationPath)
                .Where(f => f.Contains(fileName))
                .Count() > 0 ? true : false;

        }
        public void Add()
        {
            ResetInputs(pnlDetail);
            ResetInputs(pagesOtherDetail);
            pic.Image = null;
            residentID = 0;
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

        public bool ValidatedFields(Control parent)
        {

            var ctrlTextBoxes2 = LocalUtils.GetAll(parent, typeof(BunifuTextBox))
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

            var ctrlDropdown = LocalUtils.GetAll(parent, typeof(Bunifu.UI.WinForms.BunifuDropdown))
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

        private void pageDetail_Click(object sender, EventArgs e)
        {

        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            
                ChangeImage();
           
           
        }
        private void ChangeImage()
        {
            Directory.CreateDirectory(destinationPath);
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Multiselect = false;
            openFileDialog1.Title = "Picture";
            openFileDialog1.Filter = "Image files|*.jpg;*.gif;*.png;*.jpg|JPG files|*.jpg|GIF files|*.gif|PNG files|*.PNG|BMP files|*.BMP";

            DialogResult dialogResult = openFileDialog1.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {

                try
                {
                    pic.Image = null;
                    //if (imageFileName != "")
                    //{
                    //    File.Delete(Path.Combine(destinationPath, imageFileName));
                    //}
                    imageSourcePath = openFileDialog1.FileName;
                    imageFileName = openFileDialog1.SafeFileName;


                    using (var img = new Bitmap(imageSourcePath))
                    {
                        pic.Image = new Bitmap(img);
                    }

                }
                catch (Exception ex)
                {

                    imageSourcePath = "";
                    //isImageChange = false;
                    //picImage.Image = picImage.InitialImage;
                }
            }
        }

        private void txtContactNum_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtContactNum_Validating(object sender, CancelEventArgs e)
        {
            if (!Utils.IsValidMobileNumber(txtContactNum.Text))
            {
                LocalUtils.ShowErrorMessage(this, "Invalid Mobile Number!");
                e.Cancel = true;
            }
        }

        private void pic_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuCustomLabel8_Click(object sender, EventArgs e)
        {

        }

        private void txtOccupation_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFullName_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel7_Click(object sender, EventArgs e)
        {

        }

        private void txtContactNum_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel6_Click(object sender, EventArgs e)
        {

        }

        private void txtSitio_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel5_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel4_Click(object sender, EventArgs e)
        {

        }

        private void txtAge_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel3_Click(object sender, EventArgs e)
        {

        }

        private void txtPurok_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
             
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            pages.SetPage(pagePrintPreview);
            SetFormSize(LocalUtils.WindowPrintWidth, LocalUtils.WindowPrintHeight);
            this.CenterToScreen();
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

                    
                    var residents = uow.Residents.GetAll();

                    var ds = new dsReport();
                    DataTable t = ds.Tables["Residents"];
                    DataRow r;
                    int count = 0;
                    foreach (var item in residents)
                    {
                        count++;
                        r = t.NewRow();
                        r["i"] = count.ToString();
                        r["FullName"] = item.FirstName + " " + item.MiddleName + " " + item.LastName;
                        r["Address"] = item.Sitio + " " + item.Purok + " " + "Sto. Niño, Taysan Batangas";
                        r["ContactNumber"] = item.ContactNumber;
                       
                        t.Rows.Add(r);
                    }
                    rpt.LocalReport.DataSources.Clear();
                    //var p_year = new ReportParameter("Year", dtSearchYear.Value.Year.ToString());
                    //var p_quarter = new ReportParameter("Quarter", quarter.ToString());
                    //this.rpt.LocalReport.SetParameters(new ReportParameter[] {
                    //    p_year,
                    //    p_quarter
                    //   });
                    ReportDataSource rds = new ReportDataSource("Residents", t);
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
            pages.SetPage(pageDetail);

            SetFormSize(origWidth, origHeight);
            this.CenterToScreen();
        }

        private void btnChildren_Click(object sender, EventArgs e)
        {
            pagesOtherDetail.SetPage(pageChildren);
        }

        private void btnEducation_Click(object sender, EventArgs e)
        {
            pagesOtherDetail.SetPage(pageEducation);
        }

        private void btnAddChildren_Click(object sender, EventArgs e)
        {
            AddChild();
        }

        private void AddChild()
        {
            if (residentID != 0)
            {
                ResetInputs(pageChildren);
                childID = 0;
                LocalUtils.ShowAddNewMessage(this);

            }
            else
            {
                LocalUtils.ShowErrorMessage(this, "No Resident selected!");
            }
        }
        private void EditChild()
        {
            try
            {
                var grid = gridChildren;
                if (grid.SelectedRows.Count > 0)
                {
                    // get id
                    childID = int.Parse(grid.SelectedRows[0].Cells[0].Value.ToString());
                    if (residentID != 0)
                    {
                        SetChildData();
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
        private void EditEducation()
        {
            try
            {
                var grid = gridEducation;
                if (grid.SelectedRows.Count > 0)
                {
                    // get id
                    educationID = int.Parse(grid.SelectedRows[0].Cells[0].Value.ToString());
                    if (educationID != 0)
                    {
                        SetEducationData();
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
        private void SetChildData()
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var obj = uow.Childrens.Get(childID);
                  txtChildAge.Text = obj.Age.ToString();
                  txtChildFullName.Text = obj.FullName;
                  txtChildOccupation.Text = obj.Occupation;
                    

                }
            }
            catch (Exception ex)
            {
                
                LocalUtils.ShowErrorMessage(this, ex.ToString());
            }
          
        }

        private void ResetInputs(Control parent)
        {
            var textboxes = LocalUtils.GetAll(parent, typeof(BunifuTextBox));
            foreach (BunifuTextBox item in textboxes)
            {
                item.Text = string.Empty;
            }
            var comboboxes = LocalUtils.GetAll(parent, typeof(Bunifu.UI.WinForms.BunifuDropdown));
            foreach (Bunifu.UI.WinForms.BunifuDropdown item in comboboxes)
            {
                item.SelectedIndex = -1;
                item.Enabled = true;
            }
            var dates = LocalUtils.GetAll(parent, typeof(BunifuDatepicker));
            foreach (BunifuDatepicker item in dates)
            {
                item.Value = DateTime.UtcNow;
            }
            var grids = LocalUtils.GetAll(parent, typeof(BunifuDataGridView));
            foreach (BunifuDataGridView item in grids)
            {
                item.Rows.Clear();
            }
        }

        private void btnDeleteChildren_Click(object sender, EventArgs e)
        {
            DeleteChild();
        }

        private void DeleteChild()
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {

                var grid = gridChildren;
                if (grid.SelectedRows.Count > 0)
                {
                    childID = int.Parse(grid.SelectedRows[0].Cells[0].Value.ToString());
                    if (childID != 0)
                    {
                        DialogResult result = MetroMessageBox.Show(this, "Are you sure you want to delete this record?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            var obj = uow.Childrens.Get(childID);
                            if (obj != null)
                            {
                                uow.Childrens.Remove(obj);
                                uow.Complete();
                                LoadChildrensGridList();
                                childID = 0;
                                LocalUtils.ShowDeleteSuccessMessage(this);
                                ResetInputs(pageChildren);
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

        private void btnEditChildren_Click(object sender, EventArgs e)
        {
            EditChild();
        }

        private void btnSaveChildren_Click(object sender, EventArgs e)
        {
            SaveChild();
            
        }

        private void gridChildren_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            EditChild();
        }

        private void txtChildAge_KeyPress(object sender, KeyPressEventArgs e)
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
        }

        private void txtContactNum_KeyPress(object sender, KeyPressEventArgs e)
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
        }

        private void gridEducation_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            EditEducation();
        }

        private void SetEducationData()
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var obj = uow.Educations.Get(educationID);
                   txtCourse.Text = obj.Course;
                   txtInclusiveDate.Text = obj.InclusiveDate;
                   cboEducationLevel.SelectedItem = LocalUtils.GetSelectedItemX(cboEducationLevel.Items, obj.Level);
                   txtSchoolName.Text = obj.SchoolName;


                }
            }
            catch (Exception ex)
            {

                LocalUtils.ShowErrorMessage(this, ex.ToString());
            }
        }

        private void btnSaveEducation_Click(object sender, EventArgs e)
        {
            SaveEducation();
        }

        private void SaveEducation()
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    if (!ValidatedFields(pageEducation))
                    {
                        
                        return;
                    }
                    if (educationID == 0)
                    {
                        //add
                        var obj = new Education();
                        obj.ResidentID = residentID;
                        obj.Level = cboEducationLevel.Text;
                        obj.Course = txtCourse.Text;
                        obj.SchoolName = txtSchoolName.Text;
                        obj.InclusiveDate = txtInclusiveDate.Text;
                        uow.Educations.Add(obj);
                        uow.Complete();
                        childID = obj.EducationID;

                    }
                    else
                    {
                        //edit
                        var obj = uow.Educations.Get(educationID);
                        obj.ResidentID = residentID;
                        obj.Level = cboEducationLevel.Text;
                        obj.Course = txtCourse.Text;
                        obj.SchoolName = txtSchoolName.Text;
                        obj.InclusiveDate = txtInclusiveDate.Text;
                        uow.Educations.Edit(obj);
                        uow.Complete();
                    }

                    LoadEducationGridList();
                    LocalUtils.ShowSaveMessage(this);
                }
            }
            catch (Exception ex)
            {

                LocalUtils.ShowErrorMessage(this, ex.ToString());
            }
        }

        private void btnDeleteEducation_Click(object sender, EventArgs e)
        {
            DeleteEducation();
        }

        private void DeleteEducation()
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {

                    var grid = gridEducation;
                    if (grid.SelectedRows.Count > 0)
                    {
                        educationID = int.Parse(grid.SelectedRows[0].Cells[0].Value.ToString());
                        if (educationID != 0)
                        {
                            DialogResult result = MetroMessageBox.Show(this, "Are you sure you want to delete this record?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (result == DialogResult.Yes)
                            {
                                var obj = uow.Educations.Get(educationID);
                                if (obj != null)
                                {
                                    uow.Educations.Remove(obj);
                                    uow.Complete();
                                    LoadEducationGridList();
                                    educationID = 0;
                                    LocalUtils.ShowDeleteSuccessMessage(this);
                                    ResetInputs(pageEducation);
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
            catch (Exception ex)
            {

                LocalUtils.ShowErrorMessage(this, ex.ToString());
            }
        }

        private void btnAddEducation_Click(object sender, EventArgs e)
        {
            AddEducation();
        }

        private void AddEducation()
        {
            if (residentID != 0)
            {
                ResetInputs(pageEducation);
                educationID = 0;
                LocalUtils.ShowAddNewMessage(this);

            }
            else
            {
                LocalUtils.ShowErrorMessage(this, "No Resident selected!");
            }
        }

        private void txtChildOccupation_TextChanged(object sender, EventArgs e)
        {
             
        }

        private void btnBack2_Click(object sender, EventArgs e)
        {
            pages.SetPage(pageDetail);
        }

        private void btnPrintResidentDetail_Click(object sender, EventArgs e)
        {
            PrintDetail();
            
        }

        private void PrintDetail()
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {


                    var resident = uow.Residents.GetBy(residentID);
                    if (resident != null)
                    {
                        pages.SetPage(pagePrintDetail);

                        var ds = new dsReport();
                        var childrenTable = ds.Tables["Children"];
                        DataRow childrenRow;
                        int c_count = 0;
                        foreach (var item in resident.Childrens)
                        {
                            c_count++;
                            childrenRow = childrenTable.NewRow();
                            childrenRow["i"] = c_count.ToString();
                            childrenRow["FullName"] = item.FullName;
                            childrenRow["Age"] = item.Age;
                            childrenRow["Occupation"] = item.Occupation;
                            childrenTable.Rows.Add(childrenRow);
                        }

                        int e_count = 0;
                        var educationTable = ds.Tables["Education"];
                        DataRow educationRow;

                        foreach (var item in resident.Educations)
                        {
                            e_count++;
                            educationRow = educationTable.NewRow();
                            educationRow["i"] = c_count.ToString();
                            educationRow["Level"] = item.Level;
                            educationRow["SchoolName"] = item.SchoolName;
                            educationRow["InclusiveDate"] = item.InclusiveDate;
                            educationRow["Course"] = item.Course;
                            educationTable.Rows.Add(educationRow);
                        }

                        var p_FullName = new ReportParameter("FullName", resident.FirstName + " " + resident.MiddleName + " " + resident.LastName);
                        var p_Address = new ReportParameter("Address", resident.Purok + " " + resident.Sitio + " Sto. Niño, Taysan Batangas");
                        var p_Mobile = new ReportParameter("Mobile", resident.ContactNumber);
                        var p_BirthDate = new ReportParameter("Birthdate", resident.Birthday.ToShortDateString());
                        var p_Age = new ReportParameter("Age", Utils.CalculateAge(resident.Birthday).ToString());
                        var p_Gender = new ReportParameter("Gender", resident.Gender);
                        var p_CivilStatus = new ReportParameter("CivilStatus", resident.CivilStatus);
                        var p_Occupation = new ReportParameter("Occupation", resident.Occupation);
                        var p_FathersName = new ReportParameter("FathersName", resident.FathersFullName);
                        var p_FathersOccupation = new ReportParameter("FathersOccupation", resident.FathersOccupation);
                        var p_MothersName = new ReportParameter("MothersName", resident.MothersFullName);
                        var p_MothersOccupation = new ReportParameter("MothersOccupation", resident.MothersOccupation);
                        var p_SpouseName = new ReportParameter("SpouseName", resident.SpouseFullName);
                        var p_SpouseOccupation = new ReportParameter("SpouseOccupation", resident.SpouseOccupation);
                        var imageUrl = "";
                        if (CheckImageExists(resident.ImageName))
                        {
                            imageUrl = Path.Combine(destinationPath, resident.ImageName);
                        }
                        rptDetail.LocalReport.EnableExternalImages = true;
                        var p_ImageUrl = new ReportParameter("ImageUrl", new Uri(imageUrl).AbsoluteUri);
                        this.rptDetail.LocalReport.SetParameters(new ReportParameter[] {
                        p_FullName, 
                        p_Address,
                        p_Mobile,
                        p_BirthDate,
                        p_Age,
                        p_Gender,
                        p_CivilStatus, 
                        p_Occupation,
                        p_FathersName,
                        p_FathersOccupation,
                        p_MothersName,
                        p_MothersOccupation,
                        p_SpouseName,
                        p_SpouseOccupation,
                        p_ImageUrl
                    });
                        var childrenRDS = new ReportDataSource("Children", childrenTable);
                        var educationRDS = new ReportDataSource("Education", educationTable);
                        rptDetail.LocalReport.DataSources.Clear();
                        rptDetail.LocalReport.DataSources.Add(childrenRDS);
                        rptDetail.LocalReport.DataSources.Add(educationRDS);

                        rptDetail.SetDisplayMode(DisplayMode.PrintLayout);
                        rptDetail.ZoomMode = ZoomMode.PageWidth;
                        this.rptDetail.RefreshReport();
                        
                    }
                    else
                    {
                        LocalUtils.ShowErrorMessage(this, "No resident selected!");
                    }
                }
            }
            catch (Exception ex)
            {

                LocalUtils.ShowErrorMessage(this, ex.ToString());
            }
        }
       
    }
}
