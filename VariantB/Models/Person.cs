using System;
using System.Text.RegularExpressions;

namespace VariantB.Models
{
    abstract class Person : ICloneable
    {
        public string Name { get; protected set; }
        public string Surname { get; protected set; }
        public DateTime Birthdate { get; protected set; }

        protected Person(string name, string surname)
        {
            name = Regex.Replace(name.Trim(), "\\s+", " ");
            surname = Regex.Replace(surname.Trim(), "\\s+", " ");

            if (IsNameValid(name) && IsNameValid(surname))
            {
                Name = name;
                Surname = surname;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public override string ToString()
        {
            return $"Name: {Name} \nSurname: {Surname} \nBirthdate: {Birthdate} \n";
        }

        // классы наследники сами определяют логику валидации фио
        protected abstract bool IsNameValid(string name);

        public virtual object Clone() => MemberwiseClone();
    }
}
