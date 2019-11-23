using OpenQA.Selenium;

namespace NUnitExample.Core.Search
{
    public class GoogleSearch : SearchEngine
    {
        protected override string QueryUrl => "https://www.google.com/search?q=";
        protected override By ResultsBy => By.CssSelector(".S3Uucc");
    }
}