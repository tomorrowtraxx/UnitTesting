using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.UnitTest.Builders
{
    public class UserBuilder
    {
        private string _name;
        private string _accountNumber;
        private DateTime _dateOfBirth;

        public UserBuilder()
        {
            _name = "Jasper";
            _accountNumber = "0000000000000";
            _dateOfBirth = new DateTime(1900, 1, 1);
        }

        public UserBuilder WithAccountNumber(string accountNumber)
        {
            _accountNumber = accountNumber;
            return this;
        }

        public UserBuilder WithDateOfBirth(DateTime dateOfBirth)
        {
            _dateOfBirth = dateOfBirth;
            return this;
        }

        public UserBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public User Build()
        {
            return new User(_name, _dateOfBirth, _accountNumber);
        }
    }
}
