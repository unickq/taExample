using NUnit.Framework;
using OpenQA.Selenium;

namespace NUnitExample.Core
{
    public abstract class SearchEngine : Base
    {
        protected abstract string QueryUrl { get; }
        protected abstract By ResultsBy { get; }


        public SearchEngine Open(string query)
        {
            WrapInStep(() => { Driver.Navigate().GoToUrl($"{QueryUrl}{query}"); }, $"Searching for {query}");
            return this;
        }

        public void ValidateResult(int index, string expectedResult)
        {
            WrapInStep(
                () =>
                {
                    StringAssert.AreEqualIgnoringCase(expectedResult, Driver.FindElements(ResultsBy)[index].Text);
                }, $"Validating that result #{index} has {expectedResult} valued");
        }
    }
}