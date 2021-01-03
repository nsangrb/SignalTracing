using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace SignalTracing
{
    class SearcherParams
    {
        private static String WildCardToRegular(String value)
        {
            return Regex.Escape(value).Replace("\\?", "[\\w]").Replace("\\*", "[\\w]*");
        }
        // ----- Constructor -----
        public SearcherParams(List<FileInfo> list_paths, String containingText, Lucene_SearchParams.QueryMode mode, Boolean matchcase, Boolean matchwholeword)
        {
            List_Paths = list_paths.ToArray();
            if (!matchcase) containingText = containingText.ToLower();            
            if (mode == Lucene_SearchParams.QueryMode.WildCard)
            {
                ContainingText = WildCardToRegular(containingText);
            }
            else if(mode == Lucene_SearchParams.QueryMode.Text)
            {
                ContainingText = containingText;
            }
            else
            {
                ContainingText = containingText;
            }
            MatchCase = matchcase;
            MatchWholeWord = matchwholeword;
            Mode = mode;
        }

        public FileInfo[] List_Paths { get; }
        public String ContainingText { get; }
        public Boolean MatchCase { get; } = true;
        public Boolean MatchWholeWord { get; }
        public Lucene_SearchParams.QueryMode Mode { get; }
    }
}
