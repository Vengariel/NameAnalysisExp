using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace NameAnalysis
{
    public class PersonComparer : IEqualityComparer<PersonName>
    {
        public bool Equals(PersonName x, PersonName y)
        {
            return string.Compare(x.CURP, y.CURP, CultureInfo.CurrentCulture,
                       CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace) == 0;
        }

        public int GetHashCode(PersonName obj)
        {
            return (string.IsNullOrEmpty(obj.CURP) ? obj.FullLastName.GetHashCode() : obj.CURP.GetHashCode());
        }
    }
}
