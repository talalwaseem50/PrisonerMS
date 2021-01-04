using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrisonerMS.Models
{
    public class MedicationCRUD
    {
        public static Medication GetMedication(int id)
        {
            Medication m = new Medication();
            m.Desc = "dddddddddddddddm";
            m.EntryDate = "11/11/2020";
            //

            return m;
        }

        public static List<Medication> GetAllMedicalMedications(int id)
        {
            List<Medication> MedicationsList = new List<Medication>();

            MedicationsList.Add(GetMedication(1));

            return MedicationsList;
        }
    }
}