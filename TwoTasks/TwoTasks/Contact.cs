using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoTasks
{
    public class Contact : ICloneable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string[] Phones { get; set; }
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        public char? FirstLetter
        {
            get
            {
                int number;
                if (!string.IsNullOrEmpty(FullName))
                {
                    return char.ToUpper(FullName[0]);
                }
                else if (Phones != null && int.TryParse(Phones[0], out number))
                {
                    return number.ToString()[0];
                }

                return null;
            }
        }

        public object Clone()
        {
            return new Contact { FirstName = FirstName, LastName = LastName };
        }

        public override string ToString()
        {
            return $"{FullName}: {Phones[0]}";
        }
    }
}
