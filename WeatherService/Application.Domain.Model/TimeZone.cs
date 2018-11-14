using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.Model
{
    public class TimeZone
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string GmtOffset { get; set; }
        public bool IsDaylightSaving { get; set; }
        public string NextOffsetChange { get; set; }
    }
}
