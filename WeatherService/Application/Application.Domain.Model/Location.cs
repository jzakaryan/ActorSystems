using System;

namespace Application.Domain.Model
{
    public class Location
    {
        public int Key { get; set; }
        public int Version { get; set; }

        public string EnglishName { get; set; }
        public string LocalizedName { get; set; }

        public string Type { get; set; }

        public Region Region { get; set; }
        public TimeZone TimeZone { get; set; }
        public Country Country { get; set; }
        
    }
}
