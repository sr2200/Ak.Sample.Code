﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ak.Sample.Core.Aspect.Test.SampleClass
{
    public interface IMyClass
    {
        int Sum(int a, int b);
        int Quotient(int a, int b);
    }

    public class MyClass : IMyClass
    {
        public int Sum(int a ,int b)
        {
            return a + b;
        }

        public int Quotient(int a, int b)
        {
            return a / b;
        }
    }
}
