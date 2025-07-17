using System.Collections.Generic;
using BarberSalonPrototype.Models;

namespace BarberSalonPrototype.Models
{
    public class ServicesViewModel
    {
        public List<Service> Services { get; set; } = new List<Service>();
        public List<ServiceCategory> Categories { get; set; } = new List<ServiceCategory>();
        public ServiceCategory? SelectedCategory { get; set; }
    }
}