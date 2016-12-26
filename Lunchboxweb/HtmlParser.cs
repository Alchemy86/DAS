﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.Web;
using Fizzler.Systems.HtmlAgilityPack;

namespace Lunchboxweb
{
    public abstract class HtmlParser
    {
        public string UrlEncode(string s)
        {
            return HttpUtility.UrlEncode(s);
        }

        protected IEnumerable<HtmlNode> QuerySelectorAll(HtmlNode node, string query)
        {
            return node.QuerySelectorAll(query);
        }

        protected bool NodeExists(HtmlNode node)
        {
            return node != null;
        }

        protected HtmlNode QuerySelector(HtmlNode node, string query)
        {
            return node.QuerySelector(query);
        }

        protected string UrlDecode(string s)
        {
            return HttpUtility.UrlDecode(s);
        }

        protected string HtmlDecode(string m)
        {
            return HttpUtility.HtmlDecode(m);
        }

        protected string HtmlEncode(string m)
        {
            return HttpUtility.HtmlEncode(m);
        }

        protected string UrlPathEncode(string m)
        {
            return HttpUtility.UrlPathEncode(m);
        }

        protected string HtmlTrimDecode(string nonFriendly)
        {
            return HtmlDecode(nonFriendly).Trim();
        }

        protected string UrlTrimDecode(string nonFriendly)
        {
            return UrlDecode(nonFriendly).Trim();
        }

        protected IEnumerable<string> GetSubStrings(string input, string start, string end)
        {
            var r = new Regex(Regex.Escape(start) + "(.*?)" + Regex.Escape(end));
            var matches = r.Matches(input);
            return from Match match in matches select match.Groups[1].Value;
        }

        protected string GetSubString(string input, string start, string end)
        {
            var r = new Regex(Regex.Escape(start) + "(.*?)" + Regex.Escape(end));
            var matches = r.Matches(input);
            foreach (Match match in matches)
                return match.Groups[1].Value;

            return "";
        }

        protected Uri CompleteUri(string url, string siteBaseUrl)
        {
            var baseUri = new Uri(siteBaseUrl, UriKind.Absolute);
            var page = new Uri(baseUri, url);

            return page;
        }

        public void TurnOffOption()
        {
            HtmlNode.ElementsFlags.Remove("option");
        }

        public void TurnOffCustom(string op)
        {
            HtmlNode.ElementsFlags.Remove(op);
        }

        public string ExtractViewStateSearch(string viewstatestring)
        {
            const string obj1 = "__VIEWSTATE";
            const string obj2 = "value=\"";

            var viewStateNamePosition = viewstatestring.IndexOf(obj1, StringComparison.Ordinal);
            var viewStateValuePosition = viewstatestring.IndexOf(obj2, viewStateNamePosition, StringComparison.Ordinal);

            var viewStateStartPosition = viewStateValuePosition + obj2.Length;
            var viewStateEndPosition = viewstatestring.IndexOf("\"", viewStateStartPosition, StringComparison.Ordinal);

            viewstatestring = viewstatestring.Substring(
                        viewStateStartPosition,
                        viewStateEndPosition - viewStateStartPosition);

            return viewstatestring;
        }

        public static List<string> ExtractEmailAddresses(string input)
        {
            const string matchEmailPattern =
           @"(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
           + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
             + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
           + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})";
            var rx = new Regex(matchEmailPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            // Find matches.
            var matches = rx.Matches(input);
            // Report on each match.
            return (from Match match in matches select match.Value).ToList();
        }

        protected static List<string> ExtractContactPageLinks(string html)
        {
            var document = new HtmlDocument();
            document.LoadHtml(html);
            var mPageLinks = new HashSet<string>();
            try
            {
                for (var index = 0; index < document.DocumentNode.SelectNodes("//*[@href]").Count; index++)
                {
                    var link = document.DocumentNode.SelectNodes("//*[@href]")[index];
                    if (link.Attributes["href"].Value.Contains("contact") ||
                        link.Attributes["href"].Value.Contains("about") ||
                        link.Attributes["href"].Value.Contains("us"))
                    {
                        mPageLinks.Add(link.Attributes["href"].Value);
                    }
                }
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch (Exception)
            { }
            return mPageLinks.ToList();
        }
    }
}
