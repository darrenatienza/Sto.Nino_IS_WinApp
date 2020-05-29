using Bunifu.Framework.UI;
using Helpers;
using Sto.NinoRMS.App.Helpers.Themes;
using Sto.NinoRMS.App.Properties;
using Sto.NinoRMS.App.UserControls;
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
    public partial class frmDashboard : Form
    {
        private string destinationPath;
        DateTime expDate;
        public frmDashboard()
        {
            InitializeComponent();
            expDate = DateTime.Parse("2020-05-05");
        }
       

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            switch (this.WindowState)
            {
                case FormWindowState.Maximized:
                    // restore the default windows state
                    this.WindowState = FormWindowState.Normal;
                    // change image to maximized
                    btnMaximize.Image = Resources.maximize_window_48;
                    break;
                default:
                    // restore the maximized windows state
                    this.WindowState = FormWindowState.Maximized;
                    // change image to restore
                    btnMaximize.Image = Resources.restore_window_48;
                    break;
            }  
             
        }
        private void CheckTrialVersion()
        {
            if (expDate.Date <= DateTime.Now.Date)
            {
                LocalUtils.ShowErrorMessage(this, "Your trial version has been ended! Please contact developer.");
                Application.Exit();
            }
            else
            {
                LocalUtils.ShowInfo(this, "This application is a trial version and will be expired on " + expDate.ToString("MMMM dd, yyyy"));
            }
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
            page.SetPage(pageAbout);

        }

    

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            page.SetPage(pageDashboard);
         
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            //CheckTrialVersion();
            destinationPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures), "Sto.NinoIS");
            destinationPath = Resources.ImageStorage;
            Directory.CreateDirectory(destinationPath);
            ShowLogin();
            SetResidenceCount();
            SetCurrentUser();
            SetDashboardDate();
            
            SetTransparentCard(pageBrgyForms);
            SetTransparentCard(pageDashboard);
            SetTransparentCard(pageBHWForms);
            SetAboutTransparency();
            LoadBrgyOfficialAbout();

            LoadAboutPDF();
            
        }

        private void SetCurrentUser()
        {
          
            lblUserName.Text = CurrentUser.UserName + " \n" + CurrentUser.RoleName;
        }

        private void SetAboutTransparency()
        {
            pnlOfficials.BackColor = Color.FromArgb(170, Color.Gainsboro);
        }

        private void LoadBrgyOfficialAbout()
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var officials = uow.Officials.GetAllBy("");
                    flowLayoutPanel1.Controls.Clear();
                    foreach (var item in officials)
                    {
                        var brgyOfficalCard = new BrgyOfficialCard();
                        brgyOfficalCard.FullName = item.Resident.FullName;
                        brgyOfficalCard.Position = item.OfficialPosition.Title;
                        brgyOfficalCard.Designation = item.Designation;
                        brgyOfficalCard.Icon = GetImage(item.Resident.ImageName);
                        flowLayoutPanel1.Controls.Add(brgyOfficalCard);
                    }
                }
            }
            catch (Exception ex)
            {

                LocalUtils.ShowErrorMessage(this, ex.ToString());
            }
        }
        private Image GetImage(string imageName)
        {
            if (imageName != "" && CheckImageExists(imageName))
            {
                string imgPath = Path.Combine(destinationPath, imageName);
                using (var img = new Bitmap(imgPath))
                {
                    return new Bitmap(img);
                }

            }
            else
            {
                return Resources.logo1;
            }
        }
        private bool CheckImageExists(string fileName)
        {
            return Directory.GetFiles(destinationPath)
                .Where(f => f.Contains(fileName))
                .Count() > 0 ? true : false;

        }
        private void ShowLogin()
        {
            var frm = new frmLogin();
            frm.OnLoginSuccess += frm_OnLoginSuccess;
            frm.ShowDialog();
        }

        void frm_OnLoginSuccess(object sender, EventArgs e)
        {
            this.Visible = true;
        }

        private void SetResidenceCount()
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var rCount = uow.Residents.GetAll().Count();
                    lblResidenceCount.Text = rCount.ToString();
                }
            }
            catch (Exception ex)
            {

                LocalUtils.ShowErrorMessage(this, ex.ToString());
            }
        }

        private void SetDashboardDate()
        {
            lblDashboardYear.Text = DateTime.Now.Year.ToString();
            lblDashboardDayOfMonth.Text = DateTime.Now.Day.ToString();
            lblDashboardMonth.Text = DateTime.Now.ToString("MMM");
        }

        private void SetTransparentCard(Control parent)
        {
            var cards = LocalUtils.GetAll(parent, typeof(BunifuCards));
            foreach (BunifuCards item in cards)
            {
                item.BackColor = Color.FromArgb(220, Color.Gainsboro);
            }
           
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            if (CurrentUser.HasRole())
            {
                var frm = new frmAccount();
                frm.ShowDialog();
            }
            else
            {
                LocalUtils.ShowErrorMessage(this, "Current user " + CurrentUser.UserName + " is not allowed for this action!");
            }
        }

       

        private void bunifuCustomLabel3_Click(object sender, EventArgs e)
        {

        }

        private void btnBrgyForms_Click(object sender, EventArgs e)
        {
            if (CurrentUser.HasRole("Barangay Official"))
            {
                page.SetPage(pageBrgyForms);
            }
            else
            {
                LocalUtils.ShowErrorMessage(this, "Current user " + CurrentUser.UserName + " is not allowed for this action!");
            }
           
        }

        private void btnResidents_Click(object sender, EventArgs e)
        {
            if (CurrentUser.HasRole("Barangay Official"))
            {
                var form = new frmResidents();
                form.ShowDialog();
            }
            else
            {
                LocalUtils.ShowErrorMessage(this, "Current user " + CurrentUser.UserName + " is not allowed for this action!");
            }
           
        }

        private void btnBrgyClearance_Click(object sender, EventArgs e)
        {
            ShowBrgyForm("BrgyClearance");
           
        }

        private void ShowBrgyForm(string formToShow)
        {
            if (CurrentUser.HasRole("Barangay Official"))
            {
                
               
                switch (formToShow)
                {
                    case "BrgyClearance":
                         var frm = new frmBrgyClearance();
                        frm.Show();
                        break;
                    case "BizClearance":
                        new frmBusinessClearance()
                      .ShowDialog();
                        break;
                    case "Indigency":
                        new frmIndigency()
                        .ShowDialog();
                        break;
                    case "Residency": ;
                        new frmResidency()
                       .ShowDialog();
                        break;
                   
                }
            }

        }

        private void btnBrgyBizClearance_Click(object sender, EventArgs e)
        {
            ShowBrgyForm("BizClearance");
        }

        private void btnIndigency_Click(object sender, EventArgs e)
        {
            ShowBrgyForm("Indigency");
        }

        private void btnResidency_Click(object sender, EventArgs e)
        {
            ShowBrgyForm("Residency");
        }

        private void btnOfficials_Click(object sender, EventArgs e)
        {
            var form = new frmOfficials();
            form.ShowDialog();
        }

        private void btnDashboard_MouseHover(object sender, EventArgs e)
        {
          
            lblTitle.Text +=   " - Dashboard";
        }

        private void btnResidents_MouseHover(object sender, EventArgs e)
        {
            lblTitle.Text += " - Residence Records";
        }

        private void pnlDashboard_Resize(object sender, EventArgs e)
        {
           
        }
        

        private void btnBrgyOfficial_Click(object sender, EventArgs e)
        {
            if (CurrentUser.HasRole("Barangay Official"))
            {
                var form = new frmOfficials();
                form.ShowDialog();
            }
            else
            {
                LocalUtils.ShowErrorMessage(this, "Current user " + CurrentUser.UserName + " is not allowed for this action!");
            }
            
        }

        private void btnBhw_Click(object sender, EventArgs e)
        {
            if (CurrentUser.HasRole("Barangay Health Worker"))
            {
                page.SetPage(pageBHWForms);
            }
            else
            {
                LocalUtils.ShowErrorMessage(this, "Current user " + CurrentUser.UserName + " is not allowed for this action!");
            }
        }

        private void btnHealtDataBoard_Click(object sender, EventArgs e)
        {
            var frm = new frmHealthDataBoard();
            frm.ShowDialog();
        }

        private void btnQuarterlyReport_Click(object sender, EventArgs e)
        {
            var frm = new frmQuarterlyReport();
            frm.ShowDialog();
        }
        private void btnCommonHealthProfile_Click(object sender, EventArgs e)
        {
            var frm = new frmCommonHealthProfile();
            frm.ShowDialog();
        }

        private void btnAccomplishment_Click(object sender, EventArgs e)
        {
            var frm = new frmAccomplishment();
            frm.ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            ShowLogin();
        }
       

        

        private void frmDashboard_Shown(object sender, EventArgs e)
        {
            
        }

        private void bunifuCustomLabel19_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel8_Click(object sender, EventArgs e)
        {

        }

        private void LoadAboutPDF()
        {
            // get path to pdf file
            string path = string.Format(@"{0}\pdf\about.pdf", Application.StartupPath);
            axAcroPDF1.LoadFile(path);
        }
       

       
       
    }
}
