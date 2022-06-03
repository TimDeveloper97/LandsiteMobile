using System;
using System.Collections.Generic;
using System.Text;

namespace LandsiteMobile.Models
{
    public class TypeModel
    {
        public string Type { get; set; }
        public string Status { get; set; }
        public bool HasOneItem { get; set; } = false;
        public bool HasTwoItem { get; set; } = false;
    }

    public class ResponceType
    {
        public string Option { get; set; }
        public List<TypeModel> Types { get; set; }
    }

    public class ResponcePin : ResponcePosition
    {
        public string Landslide { get; set; }
        public string Material { get; set; }
        public string Hill { get; set; }
        public string Water { get; set; }
        public string Vegetation { get; set; }
        public string Damages { get; set; }
        public string Notes { get; set; }
        public string Photo { get; set; }
        public ResponceType Measure { get; set; }
        public ResponceType System { get; set; }

    }

    public class ResponcePosition
    {
        public string Title { get; set; }
        public string Tag { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
