using System;

namespace NoteBookConsoleApp
{
    public class NoteBook
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; } //(поле не является обязательным);
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string BirthDay { get; set; } //(поле не является обязательным);
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
            BirthDay = birthDay.ToShortDateString();
            Organization = organization;
            Position = position;
            Other = other;
        }

        public override string ToString()
        {
            return $"{FirstName} {MiddleName} {LastName} {PhoneNumber} {Country} {BirthDay} {Organization} {Position} {Other}";
        }
    }
}
