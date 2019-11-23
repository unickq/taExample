using OpenQA.Selenium;

namespace NUnitExample.Core.Search
{
    public class DuckDuckGoSearch : SearchEngine
    {
        protected override string QueryUrl => "https://duckduckgo.com/?q=";
        protected override By ResultsBy => By.CssSelector(".result__title");
    }
}