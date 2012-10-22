using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows;

namespace BoneMetsLibrary
{
    public class Patient
    {
        public string MRN;
        public string Firstname;
        public string Lastname;
        public List<TreatmentField> Fields = new List<TreatmentField>();

        public Patient()
        {

        }

        public Patient(string MRN, string Firstname = "", string Lastname = "")
        {
            this.MRN = MRN;
            this.Firstname = Firstname;
            this.Lastname = Lastname;
        }

        public override string ToString()
        {
            string retVal = string.Format("MRN: {0}\r\nFirstname: {1}\r\nLastname: {2}", MRN, Firstname, Lastname);

            foreach (TreatmentField field in Fields)
            {
                retVal += "\r\n" + Fields.ToString();
            }
            return retVal;
        }

        public string Serialize()
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            return serial.Serialize(this);
        }

        public static Patient Deserialize(string json)
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();

            return serial.Deserialize<Patient>(json);
        }
    }
}
