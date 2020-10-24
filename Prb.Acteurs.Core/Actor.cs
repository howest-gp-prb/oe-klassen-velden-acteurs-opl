using System;
using System.Collections.Generic;
using System.Text;

namespace Prb.Acteurs.Core
{

    public class Actor
    {
        public enum Gender { Male, Female}

        public string firstName;
        public string lastName;
        public Gender gender;
        public int yearOfBirth;
        public string placeOfBirth;
        public string nationality;

        public Actor( string lastName, string firstName, Gender gender, int yearOfBirth, string placeOfBirth, string nationality)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.gender = gender;
            this.yearOfBirth = yearOfBirth;
            this.placeOfBirth = placeOfBirth;
            this.nationality = nationality;
        }

        public override string ToString()
        {
            return $"{lastName} {firstName}";
        }
    }
}
