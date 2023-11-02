﻿using NoteBookConsoleApp.Attributes;
using System;

namespace NoteBookConsoleApp
{
    [Cap]
    public class NoteBook
    {
        [StringMinLength(2)]
        [StringContainsNumber(false)]
        public string FirstName { get; set; }

        [StringMinLength(2, true)]
        [StringContainsNumber(false)]
        public string MiddleName { get; set; } // вопрос если пользователь ввел строку, то должен отрабатывать атрибут

        [StringMinLength(2)]
        [StringContainsNumber(false)]
        public string LastName { get; set; }

        [StringMinLength(11)]
        [StringMaxLength(11)]
        [StringOfDigit(true)]
        public string PhoneNumber { get; set; }

        [StringMinLength(2)]
        [StringContainsNumber(false)]
        public string Country { get; set; }

        [DateCorrectBirthDate]
        public DateTime BirthDay { get; set; } //(поле не является обязательным); // вопрос по валидации

        [StringMinLength(2, true)]
        [StringContainsNumber(false)]
        public string Organization { get; set; } //(поле не является обязательным);

        [StringMinLength(2, true)]
        [StringContainsNumber(false)]
        public string Position { get; set; } //(поле не является обязательным);

        [StringMinLength(2, true)]
        [StringContainsNumber(false)]
        public string Other { get; set; } //(поле не является обязательным);

        public NoteBook(string firstName, string middleName, string lastName,
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

        public NoteBook()
        {

        }

        public override string ToString()
        {
            return $"{FirstName}_{MiddleName}_{LastName}_{PhoneNumber}_{Country}_{BirthDay}_{Organization}_{Position}_{Other}";
        }
    }
}
