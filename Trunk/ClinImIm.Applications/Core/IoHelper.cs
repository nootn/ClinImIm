using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClinImIm.Applications.Core
{
    public static class IoHelper
    {
        public static bool IsValidImageFile(string fileName)
        {
            return fileName.EndsWith(".png", StringComparison.CurrentCultureIgnoreCase) ||
                   fileName.EndsWith(".jpg", StringComparison.CurrentCultureIgnoreCase) ||
                   fileName.EndsWith(".tif", StringComparison.CurrentCultureIgnoreCase) ||
                   fileName.EndsWith(".bmp", StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
