using Humanizer;
using System;
using System.ComponentModel.DataAnnotations;

public class PublicationDateValidation : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value is string dateString)
        {
            if (DateTime.TryParse(dateString, out DateTime publicationDate))
            {
                DateTime today = DateTime.Today;
                DateTime weekFromToday = today.AddDays(7);

                if (publicationDate < today || publicationDate > weekFromToday)
                {
                    return false;
                }
            }
        }

        return true;
    }
}

