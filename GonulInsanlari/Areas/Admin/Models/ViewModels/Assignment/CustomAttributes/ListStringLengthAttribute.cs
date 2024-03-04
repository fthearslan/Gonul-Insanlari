using DataAccessLayer.Migrations;
using JetBrains.Annotations;
using System.ComponentModel.DataAnnotations;

namespace GonulInsanlari.Areas.Admin.Models.ViewModels.Assignment.CustomAttributes
{
    public class ListStringLengthAttribute : StringLengthAttribute
    {

        public ListStringLengthAttribute(int maxLenght):base(maxLenght)
        {
        }

        public override bool IsValid(object? value)
        {
            if(value is not  List<string>)
                return false;
            foreach(var item in value as List<string>) 
            {
            if(item.Length>MaximumLength || item.Length<MinimumLength)
                    return false;
            }
            return true;
        }

    }
}
