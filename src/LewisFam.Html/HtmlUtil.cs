using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

using HtmlAgilityPack;

using LewisFam.Html.Strings;
using LewisFam.Utils;
using LewisFam.Well_Known;

namespace LewisFam.Html
{
    public class AttributeSome
    {
        #region Properties

        public string Name { get; set; }

        public string Value { get; set; }

        #endregion Properties
    }

    /// <summary>The Html Util.</summary>
    /// <remarks>https://html-agility-pack.net/</remarks>
    public class HtmlUtil
    {
        #region Fields

        public static HtmlUtil Default = new HtmlUtil();

        private HttpClient _http;

        #endregion Fields

        #region Constructors

        public HtmlUtil()
        {
            Debug.WriteLine($"{nameof(HtmlUtil)} : ctor");
        }

        #endregion Constructors

        #region Properties

        public HtmlDocument HtmlDocument
        {
            get;
            protected set;
        }

        public bool IsLoaded { get; private set; }

        #endregion Properties

        #region Methods

        /// <summary>GetDocumentNodeDescendantsList</summary>
        /// <param name="htmlDescendantName"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public List<string> GetDocumentNodeDescendantsList(string htmlDescendantName = HtmlDescendantName.Name)
        {
            Debug.WriteLine($"{nameof(GetDocumentNodeDescendantsList)} : {nameof(htmlDescendantName)}={htmlDescendantName}");

            if (HtmlDocument == null)
                throw new ArgumentNullException("The HtmlDocument is not loaded.");

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

        public void LoadFile(string path)
        {
            Debug.WriteLine($"{nameof(LoadFile)} : {nameof(path)}={path}");
            IsLoaded = false;
            HtmlDocument = new HtmlDocument();
            HtmlDocument.Load(path);
            IsLoaded = true;
        }

        public void LoadHtml(string html)
        {
            Debug.WriteLine($"{nameof(LoadHtml)} : {nameof(html)}={html}");
            IsLoaded = false;
            HtmlDocument = new HtmlDocument();
            HtmlDocument.LoadHtml(html);
            IsLoaded = true;
        }

        public void LoadWeb(string uri)
        {
            Debug.WriteLine($"{nameof(LoadWeb)} : {nameof(uri)}={uri}");
            IsLoaded = false;
            var web = new HtmlWeb();
            HtmlDocument = web.Load(uri);
            IsLoaded = true;
        }

        /// <summary>Saves the HtmlDocument.</summary>
        /// <param name="path">    The path.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <remarks><seealso cref="HtmlAgilityPack"/></remarks>
        public void Save(string path, FileMode fileMode = FileMode.OpenOrCreate)
        {
            if (HtmlDocument == null)
                throw new ArgumentNullException("The HtmlDocument is not loaded.");

            FileUtil.Stream.Save(path, HtmlDocument.Text, fileMode);
        }

        /// <summary>Saves the HtmlDocument Async.</summary>
        /// <param name="path">    The path.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <remarks><seealso cref="HtmlAgilityPack"/></remarks>
        public async Task SaveAsync(string path, FileMode fileMode = FileMode.OpenOrCreate)
        {
            if (HtmlDocument == null)
                throw new ArgumentNullException("The HtmlDocument is not loaded.");

            await FileUtil.Stream.SaveAsync(path, HtmlDocument.Text, fileMode);
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

        private async Task<HtmlDocument> GetHtml(string uri)
        {
            Debug.WriteLine($"{nameof(GetHtml)} : {nameof(uri)}={uri}");

            if (string.IsNullOrEmpty(uri))
                return null;

            IsLoaded = false;
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

            return null;
        }

        #endregion Methods

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
        #region Properties

        public string company { get; set; }

        public string Name { get; set; }

        public string street { get; set; }

        #endregion Properties
    }
}