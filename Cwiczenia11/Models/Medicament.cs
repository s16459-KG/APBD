using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia11.Models
{
    public class Medicament
    {
        public Medicament()
        {
            Prescriptions_Medicaments = new HashSet<Prescription_Medicament>();
        }
        public int IdMedicament { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String Type { get; set; }

        public virtual ICollection<Prescription_Medicament> Prescriptions_Medicaments { get; set; }
    }
}
