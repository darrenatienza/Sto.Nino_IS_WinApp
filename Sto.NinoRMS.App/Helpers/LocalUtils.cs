using MetroFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sto.NinoRMS.Queries.Persistence
{
    public class LocalUtils
    {
        public  const int WindowPrintHeight = 768;
        public  const int WindowPrintWidth = 1024;
        #region Centering Things
        /// <summary>
        /// Center control horizontally
        /// </summary>
        /// <param name="parent">Parent Container</param>
        /// <param name="child">Child Container to center</param>
        /// <param name="paddingTop">Space from top of child container</param>
        public static void CenterControlHorizontal(Control parent, Control child, int paddingTop)
        {
            child.Location = new Point((parent.Width - child.Width) / 2, 20);
        }
        /// <summary>
        /// Center control horizontally and vertically
        /// </summary>
        /// <param name="parent">Parent Container</param>
        /// <param name="child">Child Container</param>
        public static void CenterControlXY(Control parent, Control child)
        {
            child.Location = new Point((parent.Width - child.Width) / 2, (parent.Height - child.Height) / 2);
        }
        #endregion

        #region Metro Messages
        internal static DialogResult ShowErrorMessage(Form parent, string message)
        {
            return MetroMessageBox.Show(parent, "An error occured \n" + message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        internal static DialogResult ShowDeleteMessage(Form parent)
        {
            return MetroMessageBox.Show(parent, "Are you want to delete this record?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }
        internal static DialogResult ShowDeleteSuccessMessage(Form parent)
        {
            return MetroMessageBox.Show(parent, "Record deleted successfuly!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        internal static DialogResult ShowValidationFailedMessage(Form parent)
        {
            return MetroMessageBox.Show(parent, "Validation Failed for one or more fields!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        internal static DialogResult ShowAddNewMessage(Form parent)
        {
            return MetroMessageBox.Show(parent, "Ready for adding new record!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        internal static DialogResult ShowSaveMessage(Form parent)
        {
            return MetroMessageBox.Show(parent, "Record succesfully save!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        internal static DialogResult ShowNoRecordFoundMessage(Form parent)
        {
            return MetroMessageBox.Show(parent, "No Record found for selected ID!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        internal static DialogResult ShowNoRecordSelectedMessage(Form parent)
        {
            return MetroMessageBox.Show(parent, "No record selected!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion


        #region Bunifu Dropdown Item Selector
        internal static ItemX GetSelectedItemX(ComboBox.ObjectCollection objectCollection, string id)
        {
            try
            {
                return objectCollection.OfType<ItemX>().FirstOrDefault(r => r.Value == id.ToString());
                
            }
            catch (Exception)
            {

                return new ItemX("", "");
            }
            
           
        }
        #endregion

        #region Get All Controls In form
        public static IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
        }
        #endregion

        internal static ItemX GetSelectedItemXString(ComboBox.ObjectCollection objectCollection, string str)
        {
            return objectCollection.OfType<ItemX>().FirstOrDefault(r => r.Name == str);
        }

        internal static DialogResult ShowInfo(Control parent, string message)
        {
            return MetroMessageBox.Show(parent, message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    public class ItemX
    {
        public string Name;
        public string Value;
        public ItemX(string name, string value)
        {
            Name = name; Value = value;
        }
        public override string ToString()
        {
            // Generates the text shown in the combo box
            return Name;
        }
    }
}
