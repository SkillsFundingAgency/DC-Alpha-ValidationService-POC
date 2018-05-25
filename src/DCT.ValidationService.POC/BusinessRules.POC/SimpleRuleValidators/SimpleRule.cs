using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.POC.SimpleRuleValidators
{
    public class SimpleRule
    {
        public SimpleRule ()
        {

        }
        public SimpleRule(string left, string operatr, string right)
        {
            Left = left;
            Right = right;
            Operator = operatr;
        }


        public string Left { get; set; }
        public string Operator { get; set; }
        public string Right { get; set; }

    }
}
