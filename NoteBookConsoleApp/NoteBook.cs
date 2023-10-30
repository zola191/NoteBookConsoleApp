using NoteBookConsoleApp.Attributes;
using System;

namespace NoteBookConsoleApp
{
    [Cap]
    public class NoteBook
    {
        [NotNull(false)]
        [StringMinLength(2)]
        public string FirstName { get; set; }
        [NotNull(false)]
        [StringMinLength(2)]
        public string MiddleName { get; set; } //(поле не является обязательным);
        [NotNull(false)]
        [StringMinLength(2)]
        public string LastName { get; set; }
        [NotNull(false)]
        [StringMinLength(11)]
        [StringMaxLength(11)]
        public string PhoneNumber { get; set; }
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

        public NoteBook()
        {

        }

        public override string ToString()
        {
            return $"{FirstName}_{MiddleName}_{LastName}_{PhoneNumber}_{Country}_{BirthDay}_{Organization}_{Position}_{Other}";
        }
    }
}
