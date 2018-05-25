﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessRules.POC.Settings;
using System.Collections.Concurrent;

namespace BusinessRules.POC.ReferenceData
{
   /// <summary>
   /// This class will fetch values from the settings file from settings project and return the value for the passed in key
   /// </summary>
   /// <typeparam name="TResult">This is the data type of the result</typeparam>
    public class ReferenceDataFromSettingsFile : IReferenceData<string, string>
    {
        private readonly RuleReferenceValue _RuleReferenceValueSettings;

        private readonly ConcurrentDictionary<string, string> _referenceDataDictionary = new ConcurrentDictionary<string, string>();

        public ReferenceDataFromSettingsFile()
        {
            _RuleReferenceValueSettings = RuleReferenceValue.Default;                     
        }
        
        public string Get(string lookupKey)
        {
            return _referenceDataDictionary.GetOrAdd(lookupKey, (lookup) => _RuleReferenceValueSettings[lookupKey].ToString());            
        }
    }
}
