using System;
using System.Collections.Generic;
using System.Threading;
using Allure.Commons;
using NLog;
using NLog.Config;
using NLog.Targets;
using NUnit.Allure.Core;
using NUnit.Framework;
using NUnitExample.Model;
using OpenQA.Selenium;
using LogLevel = NLog.LogLevel;

namespace NUnitExample.Core
{
    public abstract class Base
    {
        static Base()
        {
            var config = new LoggingConfiguration();
            var consoleTarget = new ColoredConsoleTarget
            {
                Layout = @"${date:format=| yyyy-MM-dd HH\:mm\:ss} | ${pad:padding=5:inner=${level}} | ${message}"
            };
            config.AddTarget("console", consoleTarget);
            config.AddRule(LogLevel.Trace, LogLevel.Fatal, consoleTarget);
            LogManager.Configuration = config;
        }


        protected static Logger Logger => LogManager.GetCurrentClassLogger();

        protected Base WrapInStep(Action action, string message, LogLevel logLevel = null)
        {
            if (logLevel == null) logLevel = LogLevel.Info;
            Logger.Log(logLevel, message);
            try
            {
                AllureLifecycle.Instance.WrapInStep(action, message);
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                throw;
            }

            return this;
        }

        protected static IWebDriver Driver => ThreadLocalContext.Value;

        private static readonly ThreadLocal<IWebDriver> ThreadLocalContext = new ThreadLocal<IWebDriver>();

        protected void InitWebDriver(IWebDriver driver)
        {
            ThreadLocalContext.Value = driver;
        }

        private IEnumerable<TestData> TestDataBuilder()
        {
            var testData = new List<TestData>();
            var fileName = TestContext.Parameters.Get("testData");
            var url = TestContext.Parameters.Get("url", null);
            if (!string.IsNullOrEmpty(url))
            {
                Logger.Trace($"Got {url} from NUnit run settings"); ;
            }

            
           

            return null;
        }

        protected void KillWebDriver()
        {
            ThreadLocalContext.Value.Quit();
            ThreadLocalContext.Value = null;
        }
    }
}