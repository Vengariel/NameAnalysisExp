using System;
using System.Collections.Generic;
using System.Text;

namespace NameAnalysis
{
   public class PersonName 
    {
        public string FirstName { get; set; }
        public string FirstLastName { get; set; }

        public string SecondLastName { get; set; }

        public string CURP { get; set; }

        public string FullLastName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(FirstLastName) || string.IsNullOrWhiteSpace(SecondLastName))
                {
                    return !string.IsNullOrWhiteSpace(FirstLastName) ? FirstName : SecondLastName;
                }
                else
                {
                     return $"{FirstLastName} {SecondLastName}";
                } 
            }
        }

        public override string ToString()
        {
            return  $"FirstName: {FirstName} FirstLastName: {FirstLastName} SecondLastName: {SecondLastName}";
        }
    }
}
