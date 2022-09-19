using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Utils.Exceptions;

namespace Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IBaseService<Doctor> _baseService;
        public DoctorService(IBaseService<Doctor> baseService) => _baseService = baseService;

        public async Task AddDoctorAsync(Doctor doctor)
        {
            var getDoctor= await _baseService.GetAsync(d=>d.Id==doctor.Id);
            if (getDoctor!=null) throw new AlreadyExistException("Already exist doctor");

            await _baseService.CreateAsync(doctor);
            await _baseService.SaveAsync();
        }

        public async Task DeleteDoctorAsync(int id)
        {
            var deleteDoctor = await _baseService.GetAsync(d => d.Id == id);
            if (deleteDoctor == null) throw new NotFoundException("Not found doctor");

            _baseService.Delete(deleteDoctor);
            await _baseService.SaveAsync();
        }

        public async Task<ICollection<Doctor>> GetAllDoctorsAsync() => await _baseService.GetAllAsync();

        public async Task<ICollection<Doctor>> GetDoctorByIdAsync(int id)
        {
            var getAllDoctors = await _baseService.GetAllAsync();
            return getAllDoctors;
        }  

        public async Task UpdateDoctorAsync(int id)
        {
            await _baseService.GetAsync(d => d.Id == id);
            await _baseService.SaveAsync();
        }
    }
}
