using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Utility
{
    public static class AgeCalculator
    {
        public static int Calculate(DateTime dateOfBirth, DateTime reference)
        {
            if (reference < dateOfBirth)
            {
                throw new ArgumentException("Reference date can not be less than dateOfBirth", "reference");
            }
            int age = reference.Year - dateOfBirth.Year;
            if (reference.Month < dateOfBirth.Month)
            {
                age--;
            }
            else if (reference.Month == dateOfBirth.Month && reference.Day < dateOfBirth.Day)
            {
                age--;
            }
            return age;
        }
    }
}
