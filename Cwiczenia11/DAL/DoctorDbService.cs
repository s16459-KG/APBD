using Cwiczenia11.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Cwiczenia11.DAL
{
    public class DoctorDbService : IDoctorDbService
    {
        private readonly CodeFirstContext _context;

        public DoctorDbService(CodeFirstContext context)
        {
            _context = context;
        }

        public IEnumerable<Doctor> GetDoctors()
        {
            return _context.Doctor.ToList();
        }

        public string AddDoctor(Doctor doctor)
        {
            _context.Entry(doctor).State = EntityState.Added;
            _context.SaveChanges();
            return "Poprawnie dodano lekarza do bazy";
        }

        public string DeleteDoctor(int id)
        {
            var doctor = new Doctor { IdDoctor = id };
            _context.Entry(doctor).State = EntityState.Deleted;
            _context.SaveChanges();
            return "Usunięto lekarza o Id: " + id;
        }

        public Doctor ModifyDoctor(Doctor doctor)
        {
            _context.Entry(doctor).State = EntityState.Modified;
            _context.SaveChanges();
            return doctor;
        }
    }
}