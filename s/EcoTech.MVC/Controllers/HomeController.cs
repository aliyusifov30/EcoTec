using Business.Repositories;
using Business.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EcoTech.MVC.Controllers
{
    public class HomeController : Controller
    {

        ISliderRepository _sliderRepository;
        IWorkProcessRepository _workProcessRepositor;
        ICompanyImageRepository _companyImageRepository;
        IFeatureRepository _featureRepository;
        IServiceRepository _serviceRepository;
        ISupportImageRepository _supportImageRepository;
        ISettingService _settingService;
        public HomeController(ISliderRepository sliderRepository, IWorkProcessRepository workProcessRepositor, ICompanyImageRepository companyImageRepository, IFeatureRepository featureRepository, IServiceRepository serviceRepository, ISupportImageRepository supportImageRepository, ISettingService settingService)
        {
            _sliderRepository = sliderRepository;
            _workProcessRepositor = workProcessRepositor;
            _companyImageRepository = companyImageRepository;
            _featureRepository = featureRepository;
            _serviceRepository = serviceRepository;
            _supportImageRepository = supportImageRepository;
            _settingService = settingService;
        }

        public IActionResult Index()
        {

            var sliders = _sliderRepository.GetAll(false);
            var workProcess = _workProcessRepositor.GetAll(false);
            var companyImages = _companyImageRepository.GetAll(false);
            var feautures = _featureRepository.GetAll(false);
            var services = _serviceRepository.GetAll(false);
            var supportImage = _supportImageRepository.GetSingleAsync((e) => true).Result;
            var setttings = _settingService.GetAll(false);
            HomeViewModel model = new HomeViewModel
            {
                Sliders = sliders.ToList(),
                WorkProcesses = workProcess.ToList(),
                CompanyImages = companyImages.ToList(),
                Features = feautures.ToList(),
                Services = services.ToList(),
                SupportImage=supportImage,
                Settings = setttings.ToList()
            };

            return View(model);
        }


    }
}