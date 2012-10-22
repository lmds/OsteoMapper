using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoneMetsLibrary
{
    public struct DoseHistory
    {
        public string GrossAnatomy;
        public string Location;
        public double dose;

        public DoseHistory(string ParentLocation, string Location, double dose)
        {
            GrossAnatomy = ParentLocation;
            this.Location = Location;
            this.dose = dose;
        }

        public override string ToString()
        {
            return string.Format("Gross Anatomy: {1}\r\n, Location: {1}\r\ndose: {2}", GrossAnatomy, Location, dose);
        }
    }

    public class TreatmentField
    {
        public string FieldName;
        public List<DoseHistory> AffectedRegions = new List<DoseHistory>();
        public int Fractions;
        public DateTime TreatmentDate;
        public string TreatmentFacility;
        public string Machine;

        public TreatmentField()
        {

        }

        public TreatmentField(string FieldName, List<DoseHistory> AffectedRegions, int fractions, DateTime TreatmentDate, string Facility = "", string Machine ="")
        {
            this.FieldName = FieldName;
            this.AffectedRegions = AffectedRegions;
            this.Fractions = fractions;
            this.TreatmentDate = TreatmentDate;
            this.TreatmentFacility = Facility;
            this.Machine = Machine;
        }

        public override string ToString()
        {
            string retVal = string.Format("Field name: {0}\r\nTreatment Date: {1}\r\nFacility: {2}\r\nMachine: {3}", 
                FieldName, TreatmentDate, TreatmentFacility, Machine);
            foreach (DoseHistory history in AffectedRegions)
            {
                retVal += "\r\n" + history.ToString();
            }
            return retVal;
        }
    }
}
