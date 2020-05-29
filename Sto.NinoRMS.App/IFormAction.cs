using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sto.NinoRMS.App
{
    public interface IFormAction
    {
        void Save();
        void Delete();

        void LoadMainGrid();
        void SetData();
        void Add();
        void ResetInputs();

        bool ValidatedFields();
    }
}
