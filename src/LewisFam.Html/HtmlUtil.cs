using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

using HtmlAgilityPack;

using LewisFam.Html.Strings;
using LewisFam.Well_Known;

namespace LewisFam.Html
{
    public class AttributeSome
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }

    public class HtmlUtil
    {
        public static HtmlUtil Default = new HtmlUtil();

        private HttpClient _http;

        public HtmlUtil()
        {
            Debug.WriteLine($"{nameof(HtmlUtil)} : ctor");
        }

        public HtmlDocument HtmlDocument
        {
            get;
            protected set;
        }

        public bool IsLoaded { get; private set; }

        /// <summary>GetDocumentNodeDescendants</summary>
        /// <param name="htmlDescendantName"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public List<string> GetDocumentNodeDescendants(string htmlDescendantName = HtmlDescendantName.Name)
        {
            Debug.WriteLine($"{nameof(GetDocumentNodeDescendants)} : {nameof(htmlDescendantName)}={htmlDescendantName}");

            if (HtmlDocument == null)
            {
                throw new ArgumentNullException("The HtmlDocument is not loaded.");
            }

            var rtn = new List<string>();

            foreach (var descendant in HtmlDocument.DocumentNode.Descendants())
            {
                switch (htmlDescendantName)
                {
                    case HtmlDescendantName.Name:
                        rtn.Add(descendant.Name);
                        Debug.WriteLine(descendant.Name);
                        break;

                    case HtmlDescendantName.InnerHtml:
                        rtn.Add(descendant.InnerHtml);
                        Debug.WriteLine(descendant.InnerHtml);
                        break;

                    case HtmlDescendantName.InnerText:
                        rtn.Add(descendant.InnerText);
                        Debug.WriteLine(descendant.InnerText);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(htmlDescendantName);
                }
            }

            return rtn;
        }

        public async Task LoadAsync(string uri)
        {
            Debug.WriteLine($"{nameof(LoadAsync)} : {nameof(uri)}={uri}");
            if (!string.IsNullOrEmpty(uri) && !IsLoaded)
            {
                HtmlDocument = await LoadHtmlDocumentAsync(uri);
            }
            await Task.CompletedTask;
        }

        /// <inheritdoc cref="HtmlNode.SelectNodes"/>
        /// <remarks>If you use //, it searches from the document begin. Use .// to search all from the current node</remarks>
        public HtmlNodeCollection SelectNodes(string xpath = HtmlNodeXPath.Html)
        {
            Debug.WriteLine($"{nameof(SelectNodes)} : {nameof(xpath)}={xpath}");
            if (HtmlDocument == null)
            {
                throw new ArgumentNullException("The HtmlDocument is not loaded.");
            }

            return HtmlDocument?.DocumentNode?.SelectNodes(xpath);
        }

        protected async Task<HtmlDocument> LoadHtmlDocumentAsync(string uri)
        {
            Debug.WriteLine($"{nameof(LoadHtmlDocumentAsync)} : {nameof(uri)}={uri}");

            if (string.IsNullOrEmpty(uri))
                return null;

            _http = new HttpClient();
            using var request = await _http.GetAsync(uri);
            if (request.IsSuccessStatusCode)
            {
                var mediaType = request.Content.Headers.ContentType.MediaType;
                switch (mediaType)
                {
                    case MIME.Text.Html:
                        var html = await request.Content.ReadAsStringAsync();
                        HtmlDocument = new HtmlDocument();
                        HtmlDocument.LoadHtml(html);
                        IsLoaded = true;
                        return HtmlDocument;

                    default:
                        break;
                }
            }
            IsLoaded = false;
            return null;
        }

        private void SampleRecordParse()
        {
            //https://html-agility-pack.net/knowledge-base/15003409/htmlagilitypack-and-selecting-nodes-and-subnodes
            List<Record> lstRecords = new List<Record>();

            foreach (HtmlNode node in HtmlDocument.DocumentNode.SelectNodes("//div[@class='search_hit']"))
            {
                Record record = new Record();

                foreach (HtmlNode node2 in node.SelectNodes(".//span[@prop]"))
                {
                    string attributeValue = node2.GetAttributeValue("prop", "");
                    if (attributeValue == "name")
                    {
                        record.Name = node2.InnerText;
                    }
                    else if (attributeValue == "company")
                    {
                        record.company = node2.InnerText;
                    }
                    else if (attributeValue == "street")
                    {
                        record.street = node2.InnerText;
                    }
                }
                lstRecords.Add(record);
            }
        }
    }

    public class Record
    {
        public string company { get; set; }

        public string Name { get; set; }

        public string street { get; set; }
    }
}