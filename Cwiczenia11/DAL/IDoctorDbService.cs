using Cwiczenia11.Models;
using System.Collections.Generic;

namespace Cwiczenia11.DAL
{
    public interface IDoctorDbService
    {
        IEnumerable<Doctor> GetDoctors();

        string AddDoctor(Doctor doctor);

        Doctor ModifyDoctor(Doctor doctor);

        string DeleteDoctor(int id);
    }
}
