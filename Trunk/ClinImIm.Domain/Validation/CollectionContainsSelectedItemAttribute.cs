using System;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using ClinImIm.Domain.Core;

namespace ClinImIm.Domain.Validation
{
    /// <summary>
    /// At least one item in the collection must be of type "ClinImIm.Domain.Core.ISelectable" and must have a "IsSelected" value of "true" in order to pass validation
    /// </summary>
    public class CollectionContainsSelectedItemAttribute : ValidationAttribute
    {
        public CollectionContainsSelectedItemAttribute()
            : base("There must be at least one selected item")
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IEnumerable)
            {
                var item = value as IEnumerable;
                var itemLooper = item.GetEnumerator();
                while (itemLooper.MoveNext())
                {
                    if (itemLooper.Current is ISelectable)
                    {
                        var currItem = itemLooper.Current as ISelectable;
                        if (currItem.IsSelected)
                        {
                            return ValidationResult.Success;
                        }
                    }
                }
                return new ValidationResult(ErrorMessageString);
            }
            throw new InvalidOperationException("This attribute can only be used on IEnumerable properties");
        }
    }
}
