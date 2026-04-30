using System;

namespace Ambulance
{
    internal class Patient
    {
        private static int _counter = 0;
        public int Id { get; private set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Location { get; set; }

        public Patient(string fullName, string phoneNumber, string location)
        {
            _counter++;
            Id = _counter;
            FullName = fullName;
            PhoneNumber = phoneNumber;
            Location = location;
        }
    }
}
