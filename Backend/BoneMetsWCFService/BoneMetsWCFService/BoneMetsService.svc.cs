using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Windows;
using BoneMetsLibrary;

namespace BoneMetsWCFService
{
    [MessageContract]
    public class BoneMetsService : IMetsService
    {
        [MessageHeader]
        public string header = "Access-Control-Allow-Origin: *";
        public string GetData(string value)
        {
            BoneMetsDataModel dm = new BoneMetsDataModel();

            Patient pat = (Patient)dm.GetPatient(value);

            return pat.Serialize();
        }

        public string SetData(Patient value)
        {
            BoneMetsDataModel dm = new BoneMetsDataModel();

            string retVal = dm.SetPatient(value);

            return retVal;
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            
            return composite;
        }
    }
}
