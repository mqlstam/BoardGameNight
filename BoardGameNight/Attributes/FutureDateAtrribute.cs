namespace BoardGameNight.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

public class FutureDateAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value is DateTime)
        {
            DateTime compareDate = (DateTime)value;
            if (compareDate > DateTime.Now)
            {
                return true;
            }
        }
        
        return false;
    }

    public override string FormatErrorMessage(string name)
    {
        return $"De {name} moet in de toekomst liggen.";
    }
}