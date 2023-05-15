using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels
{
    public class HomeViewModel
    {

        public List<Slider> Sliders { get; set; }
        public List<WorkProcess> WorkProcesses { get; set; }
        public List<CompanyImage> CompanyImages { get; set; }
        public List<Feature> Features { get; set; }
        public List<Service> Services { get; set; }
        public List<Setting> Settings { get; set; }
        public SupportImage SupportImage { get; set; }
    }
}
