using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    class CheckValueAttribute : ValidationAttribute
    {
        private string checkValueString;
        public CheckValueAttribute(string checkvalue)
        {
            this.checkValueString = checkvalue;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ("体温" == checkValueString)
            {
                if (null == value)
                    return ValidationResult.Success;
                string str = value.ToString();
                float f;
                if (float.TryParse(str,out f))
                {
                    if (f<45 && f>35)
                        return ValidationResult.Success;
                }
                var errorMsg = string.Format("体温需填大于35小于45的数字");
                return new ValidationResult(errorMsg);
            }
            if ("性别" == checkValueString)
            {
                var errorMsg = string.Format("性别需填写\"男\"或\"女\"");
                if (null == value)
                    return new ValidationResult(errorMsg);
                List<string> x = new List<string>{"男","女"};
                if (x.Contains(value.ToString()))
                    return ValidationResult.Success;
                else
                    return new ValidationResult(errorMsg);
            }
            return ValidationResult.Success;
        }
    }
}
