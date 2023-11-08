using Newtonsoft.Json;
using NoteBookApp.Attributes;
using System.ComponentModel.DataAnnotations;

namespace NoteBookApp
{
    [Cap]
    public class Notebook
    {
        public int Id { get; set; }
        [StringMinLength(2)]
        [StringContainsNumber(false)]
        [Required]
        public string FirstName { get; set; }

        [StringMinLength(2, true)]
        [StringContainsNumber(false)]
        public string MiddleName { get; set; } //(поле не является обязательным);

        [StringMinLength(2)]
        [StringContainsNumber(false)]
        [Required]
        public string LastName { get; set; }

        [StringMinLength(11)]
        [StringMaxLength(11)]
        [StringOfDigit(true)]
        [StringStartWith("8")]
        [Required]
        public string PhoneNumber { get; set; }

        [StringMinLength(2)]
        [StringContainsNumber(false)]
        [Required]
        public string Country { get; set; }

        [DateCorrectBirthDate]
        public DateTime BirthDay { get; set; } //(поле не является обязательным);

        [StringMinLength(2, true)]
        [StringContainsNumber(true)]
        public string Organization { get; set; } //(поле не является обязательным);

        [StringMinLength(2, true)]
        [StringContainsNumber(true)]
        public string Position { get; set; } //(поле не является обязательным);

        [StringMinLength(2, true)]
        [StringContainsNumber(true)]
        public string Other { get; set; } //(поле не является обязательным);

        [JsonConstructor]
        public Notebook(string firstName, string middleName, string lastName,
                        string phoneNumber, string country, DateTime birthDay,
                        string organization, string position, string other)
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

        public Notebook()
        {

        }

        public override string ToString()
        {
            return $"{FirstName}_{MiddleName}_{LastName}_{PhoneNumber}_{Country}_{BirthDay}_{Organization}_{Position}_{Other}";
        }
    }
}
