using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sto.NinoRMS.App.Properties;

namespace Sto.NinoRMS.App.UserControls
{
    public partial class BrgyOfficialCard : UserControl
    {
       
        private string _fullName;
        private string _position;
        private string _designation;
        private Image _icon;
        public BrgyOfficialCard()
        {
            InitializeComponent();
            
        }
        [Category("Custom Props")]
        public string FullName
        {
            get
            {
                return _fullName;
            }
            set
            {
                if (value == "" || value == null)
                {
                    value = "Full Name";
                }
                _fullName = value;
                lblFullName.Text = value;
              
            }
        }
        [Category("Custom Props")]
        public string Position
        {
            get
            {
                return _position;
            }
            set
            {
                if (value == "" || value == null)
                {
                    value = "Position";
                }
                _position = value;
                lblPosition.Text = _position;
                
            }
        }
        [Category("Custom Props")]
        public string Designation
        {
            get
            {
                return _designation;
            }
            set
            {
                if (value == "" || value == null)
                {
                    value = "Designation";
                }
                _designation = value;
                lblDesignation.Text = _designation;
                
            }
        }

        [Category("Custom Props")]
        public Image Icon
        {
            get
            {
                return _icon;
            }
            set {
                if (value == null)
                {
                    value = Resources.logo1;
                }
                _icon = value;
                imgPic.Image = value; }
        }
    }
}
