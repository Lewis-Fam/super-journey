using Microsoft.VisualStudio.TestTools.UnitTesting;
using LewisFam.Html;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using LewisFam.Html.Strings;

namespace LewisFam.Html.Tests
{
    [TestClass()]
    public class HtmlUtilTests
    {

        string url1 = "https://www.cnbc.com/quotes/U";
        string url2 = "https://html-agility-pack.net/select-nodes";
        [TestMethod()]
        public async Task TestHtmlTest()
        {
            
        }

        [TestMethod()]
        public async Task LoadAsyncTest()
        {
            var loadTest = new HtmlUtil();
            await loadTest.LoadAsync(url1);
            Assert.IsTrue(loadTest.IsLoaded, "!loadTest.IsLoaded");
        }

        [TestMethod()]
        public async Task IsLoadedTest()
        {
            var loadTest = new HtmlUtil();
            await loadTest.LoadAsync(url1);
            var data = loadTest.GetDocumentNodeDescendants(HtmlDescendantName.Name);
            Console.WriteLine(data.Count);
            Assert.IsTrue(loadTest.IsLoaded, "loadTest.IsLoaded");
        }

        [TestMethod()]
        public async Task GetDocumentNodeDescendantsTest()
        {
            await HtmlUtil.Default.LoadAsync(url1);
            var data = HtmlUtil.Default.GetDocumentNodeDescendants(HtmlDescendantName.Name);
            Assert.IsTrue(data.Any(), "!data.Any()");
        }

        [TestMethod()]
        public async Task SelectNodesTest_IsNull()
        {
            await HtmlUtil.Default.LoadAsync(url1);
            var nodes = HtmlUtil.Default.SelectNodes("//aa");
            Assert.IsNull(nodes, "nodes != null");
        }

        [TestMethod()]
        public async Task SelectNodesTest()
        {
            await HtmlUtil.Default.LoadAsync(url1);
            var nodes = HtmlUtil.Default.SelectNodes("//html");
            Assert.IsNotNull(nodes, "nodes == null");
        }
    }
}