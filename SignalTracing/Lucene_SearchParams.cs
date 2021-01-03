using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace SignalTracing
{
    class Lucene_SearchParams
    {
        public enum QueryMode
        {
            Text,
            RegEx,
            WildCard
        }
        // ----- Constructor -----
        public Lucene_SearchParams(DirectoryInfo indexed_directory, string search_dir, string query, QueryMode mode, List<String> exts, Boolean matchwholeword)
        {
            DirectoryInfo_Indexed = indexed_directory;
            Search_Directory = search_dir;
            Query_string = query;
            Query_Mode = mode;
            Exts = exts;
            MatchWholeWord = matchwholeword;

            Query_Lststring = GetlistString(query);

        }

        private List<String> GetlistString(string querystring)
        {
            List<String> list;
            MatchCollection matches = Regex.Matches(querystring, "[A-Z]");
            string querystring_lowcase = querystring;
            foreach (Match match in matches)
            {
                querystring_lowcase = Regex.Replace(querystring_lowcase, match.Value, match.Value.ToLower());
            }

            if (Query_Mode == QueryMode.RegEx)
            {
                querystring_lowcase = querystring_lowcase.Replace("[\\s]+", "\r");
                querystring_lowcase = querystring_lowcase.Replace("[/s]+", "\r");
                querystring_lowcase = querystring_lowcase.Replace("[\\s]*", "\r");
                querystring_lowcase = querystring_lowcase.Replace("[/s]*", "\r");
                querystring_lowcase = querystring_lowcase.Replace("[\\s]", "\r");
                querystring_lowcase = querystring_lowcase.Replace("[/s]", "\r");
                querystring_lowcase = querystring_lowcase.Replace("\\s+", "\r");
                querystring_lowcase = querystring_lowcase.Replace("/s+", "\r");
                querystring_lowcase = querystring_lowcase.Replace("\\s*", "\r");
                querystring_lowcase = querystring_lowcase.Replace("/s*", "\r");
                querystring_lowcase = querystring_lowcase.Replace("\\s", "\r");
                querystring_lowcase = querystring_lowcase.Replace("/s", "\r");
                querystring_lowcase = Regex.Replace(querystring_lowcase, "\\[[\\s]+\\]", "\r");
                querystring_lowcase = Regex.Replace(querystring_lowcase, "//[[//s]+//]", "\r");
            }
            list = Regex.Replace(querystring_lowcase, "[\\s]+", "\r").Split('\r').ToList();
            return list;
        }
        // ----- Public Properties -----
        public DirectoryInfo DirectoryInfo_Indexed { get; }

        public string Search_Directory { get; }
        public String Query_string { get; }

        public List<String> Query_Lststring { get; }

        public QueryMode Query_Mode { get; }

        public List<String> Exts { get; }

        public Boolean MatchWholeWord { get; }

    }
}
