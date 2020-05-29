using Helpers;
using Sto.NinoRMS.App.Helpers.CustomAttributes;
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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            bunifuCards1.BackColor = Color.FromArgb(200, Color.Gainsboro);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }
        async void Login()
        {
            try
            {
                if (txtUserName.Text == string.Empty && txtPassword.Text == string.Empty)
                {
                    LocalUtils.ShowErrorMessage(this, "User Name or Password is empty");
                }
                else
                {
                    var userName = txtUserName.Text;
                    var password = txtPassword.Text;
                    var crypto = new SimpleCrypto.PBKDF2();
                    bunifuCircleProgressbar1.Visible = true;
                    timer1.Enabled = true;
                    bunifuCircleProgressbar1.Value = 0;
                    using (var uow = new UnitOfWork(new DataContext()))
                    {
                        User user = await Task.Run(() =>uow.Users.GetUser(userName));
                        if (user != null)
                        {
                            if (user.Password != await Task.Run(() => crypto.Compute(password, user.PasswordSalt)))
                            {
                                LocalUtils.ShowErrorMessage(this, "Invalid password");
                            }
                            else
                            {
                                
                                CurrentUser.UserID = user.UserID;
                                CurrentUser.UserName = user.UserName;
                                CurrentUser.RoleID = user.RoleID;
                                CurrentUser.RoleName = user.Role.Name;
                                isLogin = true;
                                LoginSuccess();
                                this.Close();
                            }
                        }
                        else
                        {
                            LocalUtils.ShowErrorMessage(this, "Invalid User Name");
                        }
                    }
                    bunifuCircleProgressbar1.Visible = false;
                    
                    
                }
            }
            catch (Exception ex)
            {

                LocalUtils.ShowErrorMessage(this, ex.Message);
            }

        }

        private void LoginSuccess()
        {
            var handler = this.OnLoginSuccess;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public bool isLogin { get; set; }
        public event EventHandler OnLoginSuccess;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (bunifuCircleProgressbar1.Value == 100)
            {
                bunifuCircleProgressbar1.Value = 0;
            }
            else
            {
                bunifuCircleProgressbar1.Value += 1;
            }
           
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isLogin)
            {
                Application.Exit();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
