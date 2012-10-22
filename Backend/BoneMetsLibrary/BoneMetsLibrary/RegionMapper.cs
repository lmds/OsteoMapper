using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoneMetsLibrary
{
    public class RegionMapper
    {
        private static Dictionary<string, string> mapping = new Dictionary<string, string>() 
        { 
            { "T1", "t-spine" }, 
            { "T2", "t-spine" }, 
            { "T3", "t-spine" }, 
            { "T4", "t-spine" }, 
            { "T5", "t-spine" }, 
            { "T6", "t-spine" }, 
            { "T7", "t-spine" }, 
            { "T8", "t-spine" }, 
            { "T9", "t-spine" }, 
            { "T10", "t-spine" }, 
            { "T11", "t-spine" }, 
            { "T12", "t-spine" }, 
            { "C1", "c-spine" }, 
            { "C2", "c-spine" }, 
            { "C3", "c-spine" }, 
            { "C4", "c-spine" }, 
            { "C5", "c-spine" }, 
            { "C6", "c-spine" }, 
            { "C7", "c-spine" }, 
            { "L1", "l-spine" }, 
            { "L2", "l-spine" }, 
            { "L3", "l-spine" }, 
            { "L4", "l-spine" }, 
            { "L5", "l-spine" }, 
            { "S1", "s-spine" }, 
            { "S2", "s-spine" }, 
            { "S3", "s-spine" }, 
            { "S4", "s-spine" }, 
            { "S5", "s-spine" }, 
            { "coccyx", "coccyx" }
        };

        public static string MapDoseHistory(string location)
        {
            if (mapping.Keys.Contains(location))
            {
                return mapping[location];
            }

            // Wasn't found return empty
            return "";
        }
    }
}
