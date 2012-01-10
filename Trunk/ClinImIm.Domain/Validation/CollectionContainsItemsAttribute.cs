using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Collections;

namespace ClinImIm.Domain.Validation
{
    public class CollectionContainsItemsAttribute : ValidationAttribute
    {
        public CollectionContainsItemsAttribute()
            : base("There must be at least one item")
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IEnumerable)
            {
                var item = value as IEnumerable;
                if (item.GetEnumerator().MoveNext())
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(ErrorMessageString);
                }
            }
            throw new InvalidOperationException("This attribute can only be used on IEnumerable properties");
        }
    }
}
