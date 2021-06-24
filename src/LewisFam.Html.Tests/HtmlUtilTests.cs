using LewisFam.Html;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LewisFam.Html.Tests
{
    [TestClass()]
    public class HtmlUtilTests
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
            HtmlUtil.Default.LoadWeb(url1);
            var data = HtmlUtil.Default.GetDocumentNodeDescendantsList();
            Assert.IsTrue(data.Any(), "!data.Any()");
        }

        [TestMethod()]
        public async Task IsLoadedTest()
        {
            var loadTest = new HtmlUtil();
            loadTest.LoadWeb(url1);
            var data = loadTest.GetDocumentNodeDescendantsList();
            Console.WriteLine(data.Count);
            Assert.IsTrue(loadTest.IsLoaded, "loadTest.IsLoaded");
        }

        [TestMethod()]
        public async Task LoadAsyncTest()
        {
            var loadTest = new HtmlUtil();
            loadTest.LoadWeb(url1);
            Assert.IsTrue(loadTest.IsLoaded, "!loadTest.IsLoaded");
        }

        [TestMethod()]
        public void LoadHtmlTest()
        {
            var html = new HtmlUtil();
            html.LoadHtml(htmlStrg);
            Assert.IsTrue(!string.IsNullOrEmpty(html.HtmlDocument.Text));
            Console.WriteLine(html.HtmlDocument.Text);
        }

        [TestMethod()]
        public async Task SaveAsyncTest()
        {
            HtmlUtil.Default.LoadWeb(url1);
            await HtmlUtil.Default.SaveAsync(@"w:\tmp\test1.html");
            Assert.Inconclusive("Work in progress.");
        }

        [TestMethod()]
        public async Task SaveTest()
        {
            HtmlUtil.Default.LoadWeb(url1);
            HtmlUtil.Default.Save("test.html");
        }

        [TestMethod()]
        public async Task SelectNodesTest()
        {
            HtmlUtil.Default.LoadWeb(url1);
            var nodes = HtmlUtil.Default.SelectNodes("//a[@href]");

            Assert.IsNotNull(nodes, "nodes == null");
            foreach (var node in nodes)
            {
                var a = node.Attributes["href"];
                Console.WriteLine(a.Value);
            }
        }

        [TestMethod()]
        public async Task SelectNodesTest_Custom()
        {
            HtmlUtil.Default.LoadWeb(url1);
            var nodes = HtmlUtil.Default.SelectNodes("//a[@href]");

            Assert.IsNotNull(nodes, "nodes == null");

            foreach (var node in nodes)
            {
                var a = node.Attributes["href"];
                Console.WriteLine(a.Value);
            }

            Assert.Inconclusive("Work in progress.");
        }

        [TestMethod()]
        public async Task SelectNodesTest_IsNull()
        {
            HtmlUtil.Default.LoadWeb(url1);
            var nodes = HtmlUtil.Default.SelectNodes("//aa");
            Assert.IsNull(nodes, "nodes != null");
        }

        #endregion Methods

        [TestMethod()]
        public void LoadWebTest()
        {
            var htmlUtil = new HtmlUtil();
            htmlUtil.LoadWeb(url1);
            Console.WriteLine(htmlUtil.HtmlDocument.Text);
        }
    }
}