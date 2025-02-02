﻿using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EnemyCharsFinder
{
    public class Decompressor
    {
        public HtmlWeb web = new HtmlWeb();
        public List<string>? NameList = new List<string>();
        public List<string>? ServersList = new List<string>();

        private bool DelegatedMethod(HttpWebRequest request)
        {
            request.AutomaticDecompression = DecompressionMethods.All;
            return true;
        }

        public void Decompress()
        {
            web.PreRequest += new HtmlWeb.PreRequestHandler(this.DelegatedMethod);
        }
        public void Names(string url, StringBuilder builder)
        {
            var document = web.Load(url);
            var items = document.QuerySelectorAll(".Odd [href], .Even [href]");
            foreach (var item in items)
            {
                builder.AppendLine($"{item.InnerText} ");
            }
        }
        public void Servers(string url)
        {
            var document = web.Load(url);
            var tables = document.QuerySelectorAll(".TableContent");
            var items = tables[2].QuerySelectorAll(".Odd, .Even");
            foreach (var item in items)
            {
                var a = item.QuerySelectorAll("a");
                var text = a[0].InnerText;
                ServersList.Add(text);
            }
        }


    }
}
