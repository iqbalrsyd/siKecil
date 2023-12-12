using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace siKecil.Model
{
    public class LocationData
    {
        public string Province { get; set; }
        public string Regency { get; set; }
        public string District { get; set; }
        public string Village { get; set; }
    }

    public class Province
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public override string ToString() { return Name; }
    }

    public class Regency
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public override string ToString() { return Name; }
    }

    public class District
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class Village
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public override string ToString() { return Name; }
    }
}