using System;
using System.Collections.Generic;
using System.Text;

namespace Ambulance
{
    internal class Helper
    {
                public static string CheckFullName(string fullName)
                {
                    if (string.IsNullOrWhiteSpace(fullName))
                        throw new ArgumentException("Full name cannot be empty.");
                  return fullName;
                }
        public static string CheckPhoneNumber(string phoneNumber)
        {
            if (phoneNumber.Length < 10 || phoneNumber.Length > 15)
                throw new ArgumentException("Phone number must be between 10 and 15 digits.");
            foreach (char c in phoneNumber)
            {
                if (!char.IsDigit(c))
                    throw new Exception("phone number sadece reqem ola biler");
            }
            return phoneNumber;
        }
        public static string CheckLocation(string location)
        {
            if (string.IsNullOrWhiteSpace(location))
                throw new Exception("Location bosh ola bilmez");
            return location;
        }
        public static string GenerateCaseNo(int number)
        {
            return $"EMG{number}";
        }

        




    }
}
