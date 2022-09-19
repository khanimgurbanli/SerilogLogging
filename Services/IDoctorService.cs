using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IDoctorService
    {
        Task AddDoctorAsync(Doctor doctor);
        Task DeleteDoctorAsync(int id);
        Task UpdateDoctorAsync(int id);
        Task<ICollection<Doctor>> GetDoctorByIdAsync(int id);
        Task<ICollection<Doctor>> GetAllDoctorsAsync();
    }
}
