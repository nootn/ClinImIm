using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Waf.Foundation;

namespace ClinImIm.Domain
{
    public class ValidationModel : Model, IDataErrorInfo
    {
        private readonly DataErrorInfoSupport _dataErrorInfoSupport;

        public ValidationModel()
        {
            _dataErrorInfoSupport = new DataErrorInfoSupport(this);
        }

        public string Error { get { return _dataErrorInfoSupport.Error; } }

        string IDataErrorInfo.this[string columnName] { get { return _dataErrorInfoSupport[columnName]; } }
    }
}
