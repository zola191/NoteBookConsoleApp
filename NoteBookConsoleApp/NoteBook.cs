using NoteBookConsoleApp.Attributes;
using System;

namespace NoteBookConsoleApp
{
    public class NoteBook
    {
        [String(IsEmpty = false, MinLength = 2)]
        public string FirstName { get; set; }
        [String(IsEmpty = false, MinLength = 2)]
        public string MiddleName { get; set; } //(поле не является обязательным);
        [String(IsEmpty = false, MinLength = 2)]
        public string LastName { get; set; }
        [String(IsEmpty = false, MinLength = 11, MaxLength = 11, StartWith = '8')]
        public string PhoneNumber { get; set; }
        [String(IsEmpty = false)]
        public string Country { get; set; }
        [Newtonsoft.Json.JsonConverter(typeof(DataTimeCustomConverter))]
        public DateTime BirthDay { get; set; } //(поле не является обязательным);
        public string Organization { get; set; } //(поле не является обязательным);
        public string Position { get; set; } //(поле не является обязательным);
        public string Other { get; set; } //(поле не является обязательным);

        public NoteBook(string firstName, string middleName, string lastName, string phoneNumber, string country, DateTime birthDay, string organization, string position, string other)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Country = country;
            BirthDay = birthDay;
            Organization = organization;
            Position = position;
            Other = other;
        }

        public override string ToString()
        {
            return $"{FirstName}_{MiddleName}_{LastName}_{PhoneNumber}_{Country}_{BirthDay}_{Organization}_{Position}_{Other}";
        }
    }
}
