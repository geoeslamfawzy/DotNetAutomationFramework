using NUnit.Framework;
using ServoFramework.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServoTestProject.Data
{
    public  class TestDataMethods
    {
        public static IEnumerable<TestCaseData> LoginAdminOperator()
        {
            yield return new TestCaseData(JsonHelper.getDataParser().ExtractData("admin_username"), JsonHelper.getDataParser().ExtractData("admin_password"));
            yield return new TestCaseData(JsonHelper.getDataParser().ExtractData("operator_username"), JsonHelper.getDataParser().ExtractData("operator_password"));
        }
        public static IEnumerable<TestCaseData> LoginAdmin()
        {
            yield return new TestCaseData(JsonHelper.getDataParser().ExtractData("admin_username"), JsonHelper.getDataParser().ExtractData("admin_password"));
        }
        public static IEnumerable<TestCaseData> LoginOperator()
        {
            yield return new TestCaseData(JsonHelper.getDataParser().ExtractData("operator_username"), JsonHelper.getDataParser().ExtractData("operator_password"));
        }
    }
}
