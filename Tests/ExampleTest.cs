using System.Collections;
using System.IO;
using Allure.Commons;
using CsvHelper;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using NUnitExample.Core;
using NUnitExample.Core.Search;
using NUnitExample.Model;

namespace NUnitExample.Tests
{
    [AllureSuite("Search engine test")]
    [AllureSeverity(SeverityLevel.minor)]
    public class ExampleTest : BaseTest
    {
        private static IEnumerable TestCases
        {
            get
            {
                var csvFile = TestContext.Parameters.Get("testData", "testData\\prod.csv");
                using var reader = new StreamReader(csvFile);
                using var csv = new CsvReader(reader);
                foreach (var record in csv.GetRecords<TestData>())
                    yield return new TestCaseData(record.Query, record.Index, record.Text);
            }
        }

        private SearchEngine _engine;

        [SetUp]
        public void SetUp()
        {
            var se = TestContext.Parameters.Get("searchEngine", "google");
            _engine = se.ToLowerInvariant() switch
            {
                "ddg" => (SearchEngine) new DuckDuckGoSearch(),
                "bing" => new BingSearch(),
                _ => new GoogleSearch()
            };
        }


        [Test]
        [TestCaseSource(typeof(ExampleTest), nameof(TestCases))]
        public void Test(string query, int index, string text)
        {
            _engine.Open(query).ValidateResult(index, text);
        }
    }
}