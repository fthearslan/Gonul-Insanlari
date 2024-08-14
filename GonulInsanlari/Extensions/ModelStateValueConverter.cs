using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GonulInsanlari.Extensions
{
    public static class ModelStateValueConverter
    {

        public static List<string>? GetModelErrors(this ModelStateDictionary values)
        {
            if (values.ErrorCount == 0)
                return null;

            List<string> errors = new();

            foreach (var value in values.Values)
            {
                foreach (var error in value.Errors)
                    errors.Add(error.ErrorMessage);

            }

            return errors;

        }


    }
}
