using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Ambulance
{
    internal class Patient
    {
        private static int _counter = 0;
        public int Id { get; private set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Location { get; set; }
         
        public Patient (string fullName  , string phoneNumber, string location)
        {
            _counter++; 
            Id = ++_counter;
            FullName = Helper.CheckFullName(fullName);
            PhoneNumber = Helper.CheckPhoneNumber(phoneNumber);
            Location = Helper.CheckLocation(location);
        }




    }
}
