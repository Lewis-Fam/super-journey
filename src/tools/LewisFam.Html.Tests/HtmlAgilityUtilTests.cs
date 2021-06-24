using LewisFam.Html;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LewisFam.Html.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LewisFam.Html.Tests
{
    [TestClass()]
    public class HtmlAgilityUtilTests
    {
        #region Fields

        private const string htmlStrg = "<div><img src=\"\" style=\"width: 420px; height: 420px\" height=\"42\" width=\"42\" /><img style=\" width: 42px; height: 42px; \" /></div>";

        private const string url1 = "https://www.cnbc.com/quotes/U";

        private const string url2 = "https://html-agility-pack.net/select-nodes";

        #endregion Fields

        #region Methods

        [TestMethod()]
        public async Task GetDocumentNodeDescendantsTest()
        {
            HtmlAgilityUtil.Default.LoadWeb(url1);
            var data = HtmlAgilityUtil.Default.GetDocumentNodeDescendantsList();
            Assert.IsTrue(data.Any(), "!data.Any()");
        }

        [TestMethod()]
        public void IsLoadedTest()
        {
            var loadTest = new HtmlAgilityUtil();
            loadTest.LoadWeb(url1);
            var data = loadTest.GetDocumentNodeDescendantsList();
            Console.WriteLine(data.Count);
            Assert.IsTrue(loadTest.IsLoaded, "loadTest.IsLoaded");
        }

        [TestMethod()]
        public void LoadHtmlTest()
        {
            var html = new HtmlAgilityUtil();
            html.LoadHtml(htmlStrg);
            Assert.IsTrue(!string.IsNullOrEmpty(html.HtmlDocument.Text));
            Console.WriteLine(html.HtmlDocument.Text);
        }

        [TestMethod()]
        public async Task SaveAsyncTest()
        {
            //HtmlUtil.Default.LoadWeb(url1);
            //await HtmlUtil.Default.SaveAsync(@"w:\tmp\test1.html");
            //Assert.IsTrue();
            Assert.Inconclusive("Uncomment above to test.");
        }

        [TestMethod()]
        public void SaveTest()
        {
            HtmlAgilityUtil.Default.LoadWeb(url1);
            HtmlAgilityUtil.Default.Save("test.html");
        }

        [TestMethod()]
        public void SelectNodesTest()
        {
            HtmlAgilityUtil.Default.LoadWeb(url1);
            var nodes = HtmlAgilityUtil.Default.SelectNodes("//a[@href]");

            Assert.IsNotNull(nodes, "nodes == null");
            foreach (var node in nodes)
            {
                var a = node.Attributes["href"];
                Console.WriteLine(a.Value);
            }
        }

        [TestMethod()]
        public void SelectNodesTest_Custom()
        {
            HtmlAgilityUtil.Default.LoadWeb(url1);
            var nodes = HtmlAgilityUtil.Default.SelectNodes("//a[@href]");

            Assert.IsNotNull(nodes, "nodes == null");

            foreach (var node in nodes)
            {
                var a = node.Attributes["href"];
                Console.WriteLine(a.Value);
            }

            //Assert.IsTrue();
            Assert.Inconclusive("Uncomment above to test.");
        }

        [TestMethod()]
        public void SelectNodesTest_IsNull()
        {
            HtmlAgilityUtil.Default.LoadWeb(url1);
            var nodes = HtmlAgilityUtil.Default.SelectNodes("//aa");
            Assert.IsNull(nodes, "nodes != null");
        }

        #endregion Methods

        [TestMethod()]
        public void LoadWebTest()
        {
            var htmlUtil = new HtmlAgilityUtil();
            htmlUtil.LoadWeb(url1);
            Console.WriteLine(htmlUtil.HtmlDocument.Text);
        }

        [TestMethod()]
        public async Task LoadWebAsyncTest()
        {
            var htmlUtil = new HtmlAgilityUtil();
            await htmlUtil.LoadWebAsync(url1);
            Assert.IsTrue(htmlUtil.IsLoaded, "!loadTest.IsLoaded");
        }

        [TestMethod()]
        public void TestParsedTextTest()
        {
        }
    }
}