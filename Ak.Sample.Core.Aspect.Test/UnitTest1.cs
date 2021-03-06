using Ak.Sample.Core.Aspect.Test.SampleClass;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;

namespace Ak.Sample.Core.Aspect.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            IMyClass cls = AkProxy<IMyClass>.Create(new MyClass());
            Console.WriteLine(cls.Sum(1,2));
        }

        [TestMethod]
        public void TestMethod2()
        {
            try
            {
                IMyClass cls = AkProxy<IMyClass>.Create(new MyClass());
                Console.WriteLine(cls.Quotient(2, 0));
                Assert.Fail();
            }
            catch (Exception)
            {
            }
        }
    }
}
