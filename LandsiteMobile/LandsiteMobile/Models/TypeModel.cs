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
}
