using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrisonerMS.Models
{
    public class AllergyCRUD
    {
        public static Allergy GetAllergy(int id)
        {
            Allergy a = new Allergy();
            a.Desc = "ddddddddddddddda";
            a.EntryDate = "11/11/2020";
            //

            return a;
        }

        public static List<Allergy> GetAllMedicalAllergys(int id)
        {
            List<Allergy> AllergysList = new List<Allergy>();

            AllergysList.Add(GetAllergy(1));

            return AllergysList;
        }
    }
}