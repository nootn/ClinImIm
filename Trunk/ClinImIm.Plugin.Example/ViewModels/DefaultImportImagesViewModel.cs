using System;
using System.Collections.Generic;
using System.IO;
using ClinImIm.Domain;
using ClinImIm.Plugin.Example.Domain;
using log4net;

namespace ClinImIm.Plugin.Example.ViewModels
{
    public class DefaultImportImagesViewModel
    {
        private ImportDetails _model;
        private readonly ILog _log = LogManager.GetLogger(typeof(DefaultImportImagesViewModel));

        public DefaultImportImagesViewModel(ImportDetails model)
        {
            _model = model;
        }

        public ImportDetails Model
        {
            get { return _model; }
        }

        public IEnumerable<string> TryImport(ImagesToImport modelFromClinImIm)
        {
            var errorMessages = new List<string>();

            if (string.IsNullOrWhiteSpace(_model.Destination))
            {
                errorMessages.Add("You must supply a destination directory.");
            }
            else if (!Directory.Exists(_model.Destination))
            {
                errorMessages.Add("The destination directory must exist.");
            }

            try
            {
                var path = Path.Combine(_model.Destination ?? string.Empty,
                                        string.Concat(modelFromClinImIm.PatientImagesAreOf.Identifier, "_",
                                                      DateTime.Now.Ticks));
                var dir = Directory.CreateDirectory(path);
                foreach (var currImage in modelFromClinImIm.Images)
                {
                    var currFile = new FileInfo(currImage);
                    if (!currFile.Exists)
                    {
                        errorMessages.Add(string.Format("Unable to copy file '{0}' because it does not exist", currImage));        
                    }
                    else
                    {
                        var dest = Path.Combine(dir.FullName, currFile.Name);
                        if (_model.DeleteImages)
                        {
                            currFile.MoveTo(dest);
                        }
                        else
                        {
                            currFile.CopyTo(dest);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                errorMessages.Add(ex.Message);
            }

            return errorMessages;
        }
    }
}
