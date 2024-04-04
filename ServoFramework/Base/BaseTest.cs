using NUnit.Framework;
using ServoFramework.Helpers;
using ServoFramework.Driver;

namespace ServoFramework.Base
{
    public class BaseTest : DriverActions
    {
        [OneTimeSetUp]
        public void Setup()
        {
            LogHelper.CreateLogFile();
            ExtentReporterHelper.setupReport();
        }

        [SetUp]
        public void StartBrowser()
        {
            InitDriver();
            ExtentReporterHelper.MakeReportOnMethod();
        }

        [TearDown]
        public void AfterTest()
        {
            
            ScreenshotHelper.TakeScreenshot();
            LogHelper.Write(TestContext.CurrentContext.Result.StackTrace);
            ExtentReporterHelper.ReportFaliureResults();
            //DriverAdministration.getDriver().Quit();
        }
        [OneTimeTearDown]
        public void BeforeClass()
        {
            
        }
        //public static IEnumerable<TestCaseData> AddTestConfig()
        //{
        //    yield return new TestCaseData(JsonHelper.getDataParser().ExtractData("username"), JsonHelper.getDataParser().ExtractData("password"));
        //    yield return new TestCaseData(JsonHelper.getDataParser().ExtractDataArray("url"), JsonHelper.getDataParser().ExtractDataArray("username"));
        //    //getDataParser().ExtractDataArray("url"),
        //}
    }
}