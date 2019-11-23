using Allure.Commons;
using NUnit.Allure.Core;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace NUnitExample.Core
{
    [AllureNUnit]
    [Parallelizable(ParallelScope.Children)]
    public class BaseTest : Base
    {
        [OneTimeSetUp]
        public virtual void ClearAllure()
        {
            AllureLifecycle.Instance.CleanupResultDirectory();
        }

        [SetUp]
        public void StartBrowser()
        {
            AllureExtensions.WrapSetUpTearDownParams(
                () => InitWebDriver(new ChromeDriver()), "Launching browser");

            Driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void After()
        {
            TestContext.Progress.WriteLine(
                $"!!! - {TestContext.CurrentContext.Test.FullName} - {TestContext.CurrentContext.Result.Outcome.Status}");
            if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Passed)
                AllureLifecycle.Instance.AddAttachment("ScreenShot", "image/png",
                    ((ITakesScreenshot) Driver).GetScreenshot().AsByteArray, ".png");

            AllureExtensions.WrapSetUpTearDownParams(KillWebDriver, "Killing browser");
        }
    }
}