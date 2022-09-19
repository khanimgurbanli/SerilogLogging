using Entities;
using Microsoft.AspNetCore.Mvc;
using SerilogLogging.Controllers;
using Services;
using System.Numerics;
using Utils.Exceptions;

namespace Logging_Serilog.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly ILogger<DoctorController> _logger;
        public DoctorController(IDoctorService doctorService, ILogger<DoctorController> logger)
        {
            _doctorService = doctorService;
            _logger = logger;
        }

        public IActionResult NotFound() => View();

        public IActionResult Opps() => View();
        public async Task<IActionResult> Index()
        {
            var getAllDoctors = await _doctorService.GetAllDoctorsAsync();
            var exMessage = new NotFoundException("Not found any doctor");
            if (getAllDoctors.Count == 0)
            {
                _logger.LogInformation(exMessage.Message);
                return RedirectToAction("NotFound");
            }
            else
            {
                _logger.LogInformation("All doctors list...");
                ViewBag.Doctors = getAllDoctors;
                return View();
            }
        }

        public IActionResult Add() => View();
        [HttpPost]
        public async Task<IActionResult> Add(Doctor doctor)
        {
            try
            {
                await _doctorService.AddDoctorAsync(doctor);
                _logger.LogInformation("Added succesfully new doctor");
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Don't add a new doctor");
                _logger.LogInformation($"Details for fail insert operation{ex}");
                return RedirectToAction("Opps");
            }
        }

        public IActionResult Remove() => View();
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                await _doctorService.DeleteDoctorAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Don't remove doctor");
                _logger.LogInformation($"Details for fail delete operation{ex}");
            }
            return View();
        }

        public IActionResult Update() => View();
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                await _doctorService.UpdateDoctorAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Don't edit doctor infos");
                _logger.LogInformation($"Details for fail update operation{ex}");
            }
            return View();
        }

        public IActionResult GetById() => View();
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                await _doctorService.UpdateDoctorAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Don't edit doctor infos");
                _logger.LogInformation($"Details for fail update operation{ex}");
            }
            return View();
        }
    }
}
