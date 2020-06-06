using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia11.Models
{
    public class Prescription
    {
        public Prescription()
        {
            Prescriptions_Medicaments = new HashSet<Prescription_Medicament>();
        }

        public int idPrescription { get; set; }
        public String DueDate { get; set; }
        public DateTime Date { get; set; }
        public int IdPatient { get; set; }
        public int IdDoctor { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual ICollection<Prescription_Medicament> Prescriptions_Medicaments { get; set; }
    }
}
