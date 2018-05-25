﻿using System;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.POC.Interfaces
{
    public interface IRule<T> where T : class
    {
        void Validate(T objectToValidate);
    }
}
