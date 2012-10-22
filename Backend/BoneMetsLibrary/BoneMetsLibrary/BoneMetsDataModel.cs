using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows;
using System.IO;

namespace BoneMetsLibrary
{
    public class BoneMetsDataModel
    {
        SqlConnection dbCon = new SqlConnection(@"Data Source=WinHacker\SQLExpress;Initial Catalog=BoneMets;Integrated Security=True");
    
        public Patient GetPatient(string MRN)
        {
            Patient retVal = new Patient();

            List<TreatmentField> fields = new List<TreatmentField>();

            dbCon.Open();

            SqlCommand command = new SqlCommand(@"Select * From MasterList
                                                  Where MRN = @mrn
                                                  Order by TreatmentField, TreatmentDate",
                dbCon);
            command.Parameters.Add(new SqlParameter("@mrn", MRN));

            string curFieldName = "";
            DateTime curDate = DateTime.MinValue;
            TreatmentField curTreatmentField = new TreatmentField();
            using (SqlDataReader dbReader = command.ExecuteReader())
            {
                while (dbReader.Read())
                {
                    if (string.IsNullOrEmpty(retVal.MRN))
                    {
                        retVal.MRN = (string)dbReader["MRN"];
                        retVal.Firstname = (string)dbReader["Firstname"];
                        retVal.Lastname = (string)dbReader["Lastname"];
                    }
                    if ((string)dbReader["TreatmentField"] != curFieldName || (DateTime)dbReader["TreatmentDate"] != curDate)
                    {
                        if (curTreatmentField.AffectedRegions.Count > 0)
                        {
                            fields.Add(curTreatmentField);
                        }
                        curFieldName = (string)dbReader["TreatmentField"];
                        curDate = ((DateTime)dbReader["TreatmentDate"]);
                        curTreatmentField = new TreatmentField()
                        {
                            FieldName = curFieldName,
                            TreatmentDate = curDate,
                            Machine = (string)dbReader["Machine"],
                            TreatmentFacility = (string)dbReader["Facility"]
                        };
                    }
                    string site = (string)dbReader["Site"];
                    curTreatmentField.AffectedRegions.Add(new DoseHistory(RegionMapper.MapDoseHistory(site), site, 
                        (double)dbReader["Dose"]));
                }
            }

            if (curTreatmentField != null)
            {
                fields.Add(curTreatmentField);
            }

            retVal.Fields = fields;

            dbCon.Close();

            TextWriter writer = new StreamWriter("C:/Users/mike/Desktop/coolfile.txt");
            writer.WriteLine(retVal.Serialize());
            writer.Close();
            return retVal;
        }

        public string SetPatient(Patient data)
        {
            string retVal = "";

            Patient patient = data;

            dbCon.Open();

            SqlCommand ClearCommand = new SqlCommand(@"Delete From MasterList
                                                Where MRN=@MRN",
                                                dbCon);
            ClearCommand.Parameters.Add(new SqlParameter("@MRN", patient.MRN));
            ClearCommand.ExecuteNonQuery();

            SqlCommand AddCommand = new SqlCommand(@"Insert Into MasterList (MRN,, Firstname, Lastname, TreatmentDate, TreatmentField, Dose,
                                                        Fractions, Site, Facility, Machine)
                                                     Values(@MRN, @Firstname, @Lastname, @TreatmentDate, @TreatmentField, @Dose,
                                                        @Fractions, @Site, @Facility, @Machine)",
                                                    dbCon);
            AddCommand.Parameters.Add(new SqlParameter("@MRN", patient.MRN));
            AddCommand.Parameters.Add(new SqlParameter("@Firstname", patient.Firstname));
            AddCommand.Parameters.Add(new SqlParameter("@Lastname", patient.Lastname));
            foreach (TreatmentField field in patient.Fields)
            {
                AddCommand.Parameters.Add(new SqlParameter("@TreatmentField", field.FieldName));
                AddCommand.Parameters.Add(new SqlParameter("@Machine", field.Machine));
                AddCommand.Parameters.Add(new SqlParameter("@Facility", field.TreatmentFacility));
                foreach(DoseHistory history in field.AffectedRegions)
                {
                    AddCommand.Parameters.Add(new SqlParameter("@Site", history.Location));
                    AddCommand.Parameters.Add(new SqlParameter("@Dose", history.dose));
                }
            }

            return retVal;
        }
    }
}
