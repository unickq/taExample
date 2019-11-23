using OpenQA.Selenium;

namespace NUnitExample.Core.Search
{
    public class BingSearch : SearchEngine
    {
        protected override string QueryUrl => "https://www.bing.com/search?q=";
        protected override By ResultsBy => By.CssSelector(".b_algo > h2 > a");
    }
}