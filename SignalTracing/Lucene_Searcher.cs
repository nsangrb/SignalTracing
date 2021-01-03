using Lucene.Net.Analysis.Standard;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using Version = Lucene.Net.Util.Version;
using Contrib.Regex;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using System.Text.RegularExpressions;

namespace SignalTracing
{
    class Lucene_Searcher
    {

        public Lucene_Searcher(Lucene_SearchParams pars)
        {
            Lucene_SearchParams = pars;
        }

        public void Start()
        {
            FSDirectory directory = FSDirectory.Open(Lucene_SearchParams.DirectoryInfo_Indexed);
            var searcher = new IndexSearcher(directory);
            TopScoreDocCollector collector = TopScoreDocCollector.Create(100000, true);
            string querystring = Lucene_SearchParams.Query_string;
            BooleanQuery bool_query = new BooleanQuery();

            bool _is_all_exts = false;
            string query_exts = "";
            foreach (string ext in Lucene_SearchParams.Exts)
            {
                if (ext == "*.*")
                {
                    _is_all_exts = true;
                }
                if (query_exts != "")
                {
                    query_exts += "|";
                }
                query_exts += ext.Substring(2);
            }
            if (!_is_all_exts)
            {
                query_exts = "\\.(" + query_exts + ")$";
                bool_query.Add(new RegexQuery(new Term(Lucene_Constants.FILE_NAME, query_exts)), Occur.MUST);
            }


            switch (Lucene_SearchParams.Query_Mode)
            {
                case Lucene_SearchParams.QueryMode.RegEx:
                    if (Lucene_SearchParams.Query_Lststring.Count > 1)
                    {
                        for (int i = 0; i < Lucene_SearchParams.Query_Lststring.Count; i++)
                        {
                            bool_query.Add(new RegexQuery(new Term(Lucene_Constants.CONTENTS, Lucene_SearchParams.Query_Lststring[i] )), Occur.MUST);                           
                        }
                    }
                    else
                    {
                        bool_query.Add(new RegexQuery(new Term(Lucene_Constants.CONTENTS, Lucene_SearchParams.Query_Lststring[0])), Occur.MUST);
                    }
                    break;
                case Lucene_SearchParams.QueryMode.Text:
                    if (!Lucene_SearchParams.MatchWholeWord)
                    {
                        if (Lucene_SearchParams.Query_Lststring.Count > 1)
                        {
                            bool_query.Add(new WildcardQuery(new Term(Lucene_Constants.CONTENTS, "*" + Lucene_SearchParams.Query_Lststring[0] + "*")), Occur.MUST);                            
                            for (int i = 1; i < Lucene_SearchParams.Query_Lststring.Count - 1; i++)
                            {
                                bool_query.Add(new WildcardQuery(new Term(Lucene_Constants.CONTENTS, "*" + Lucene_SearchParams.Query_Lststring[i] + "*")), Occur.MUST);                                
                            }
                            bool_query.Add(new WildcardQuery(new Term(Lucene_Constants.CONTENTS, "*" + Lucene_SearchParams.Query_Lststring[Lucene_SearchParams.Query_Lststring.Count - 1] + "*")), Occur.MUST);                           
                        }
                        else
                        {
                            bool_query.Add(new WildcardQuery(new Term(Lucene_Constants.CONTENTS, "*" + Lucene_SearchParams.Query_Lststring[0] + "*")), Occur.MUST);
                        }
                    }
                    else
                    {
                        if (Lucene_SearchParams.Query_Lststring.Count > 1)
                        {
                            bool_query.Add(new RegexQuery(new Term(Lucene_Constants.CONTENTS, "(^|[^\\w]+)" + Regex.Escape(Lucene_SearchParams.Query_Lststring[0]))), Occur.MUST);                            
                            for (int i = 1; i < Lucene_SearchParams.Query_Lststring.Count - 1; i++)
                            {
                                bool_query.Add(new RegexQuery(new Term(Lucene_Constants.CONTENTS, Regex.Escape(Lucene_SearchParams.Query_Lststring[i]))), Occur.MUST);                                
                            }
                            bool_query.Add(new RegexQuery(new Term(Lucene_Constants.CONTENTS, Regex.Escape(Lucene_SearchParams.Query_Lststring[Lucene_SearchParams.Query_Lststring.Count - 1]) + "([^\\w]+|$)")), Occur.MUST);                            
                        }
                        else
                        {
                            bool_query.Add(new RegexQuery(new Term(Lucene_Constants.CONTENTS, "(^|[^\\w]+)" + Regex.Escape(Lucene_SearchParams.Query_Lststring[0]) + "([^\\w]+|$)")), Occur.MUST);
                        }

                    }
                    
                    //bool_query.Add(multi_query, Occur.MUST);
                    break;
                case Lucene_SearchParams.QueryMode.WildCard:
                    if (Lucene_SearchParams.Query_Lststring[0].IndexOf('*') != 0) Lucene_SearchParams.Query_Lststring[0] = "*" + Lucene_SearchParams.Query_Lststring[0];
                    if (Lucene_SearchParams.Query_Lststring[0].LastIndexOf('*') != Lucene_SearchParams.Query_Lststring[0].Length) Lucene_SearchParams.Query_Lststring[0] += "*";
                    bool_query.Add(new WildcardQuery(new Term(Lucene_Constants.CONTENTS, Lucene_SearchParams.Query_Lststring[0])), Occur.MUST);
                    break;
            }
            searcher.Search(bool_query, collector);
            ScoreDoc[] hits = collector.TopDocs().ScoreDocs;
            for (int i = 0; i < hits.Length; i++)
            {
                int docId = hits[i].Doc;
                var doc = searcher.Doc(docId);
                if (doc.Get(Lucene_Constants.FILE_NAME).ToLower().Contains(Lucene_SearchParams.Search_Directory.ToLower()))
                {
                    Results.Add(doc.Get(Lucene_Constants.FILE_NAME));
                }
            }
            searcher.Dispose();
            searcher = null;
        }

        public Lucene_SearchParams Lucene_SearchParams { get; }
        public List<String> Results { get; } = new List<String>();
    }
}
