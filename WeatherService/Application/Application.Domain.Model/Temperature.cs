using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.Model
{
    public class Temperature
    {
        public Measurement Metric { get; set; }

        public Measurement Imperial { get; set; }

        public int RelativeHumidity { get; set; }
    }

    public class Measurement
    {
        public double Value { get; set; }

        public string Unit { get; set; }

        public int UnitType { get; set; }
    }
}
