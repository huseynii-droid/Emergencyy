using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Ambulance
{
    internal class Ambulance
    {
        private static int _counter = 0;
        public int Id { get; private set; }
        public string PlateNumber { get; set; }
        public string DriverName { get; set; }
        public bool IsAvailable { get; set; } = true;

        public Ambulance(string plateNumber , string driverName)
        {
            _counter++;
            Id=_counter;

            PlateNumber = plateNumber;
            DriverName= driverName;

        }
        public void SetAvailability(bool value )
        {
            IsAvailable = value;
        }



    }
}
