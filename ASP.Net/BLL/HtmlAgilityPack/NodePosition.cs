using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.HtmlAgilityPack
{
    /**Usage:**/
    /*
    // build a list of nodes ordered by stream position
            var pos = new NodePositions(htmlDoc);

            // browse all tags detected as not opened
            foreach (HtmlParseError error in htmlDoc.ParseErrors.Where(e => e.Code == HtmlParseErrorCode.TagNotOpened))
            {
                // find the text node just before this error
                HtmlTextNode last = pos.Nodes.OfType<HtmlTextNode>().LastOrDefault(n => n.StreamPosition < error.StreamPosition);
                if (last != null)
                {
                    // fix the text; reintroduce the broken tag
                    last.Text = error.SourceText.Replace("/", "") + last.Text + error.SourceText;
                }
            }
            // browse all tags detected as not opened
            foreach (HtmlParseError error in htmlDoc.ParseErrors.Where(e => e.Code == HtmlParseErrorCode.TagNotClosed))
            {
                // find the text node just before this error
                HtmlTextNode first = pos.Nodes.OfType<HtmlTextNode>().FirstOrDefault(n => n.StreamPosition > error.StreamPosition);
                if (first != null)
                {
                    // fix the text; reintroduce the broken tag
                    //var tag = !String.IsNullOrEmpty(error.Reason) && error.Reason.Contains("<") && error.Reason.Contains(">")
                      //  ? error.Reason.Substring(error.Reason.IndexOf('<'), error.Reason.IndexOf('>')) : "";
                    //var tag = !String.IsNullOrEmpty(error.Reason) && error.Reason.Contains("<") && error.Reason.Contains(">")
                      //  ? Regex.Replace(error.Reason, "<.*?>", string.Empty) : "";

                    var tag = "";
                    if (!String.IsNullOrEmpty(error.Reason) && error.Reason.Contains("<") && error.Reason.Contains(">"))
                    {
                        Regex pRegex = new Regex("<.*?>", RegexOptions.IgnoreCase);
                        // if text is not single line use this regex
                        // Regex pRegex = new Regex("<p.*?(?=</p>), RegexOptions.SingleLine"); 

                        tag = pRegex.Match(error.Reason).Value;
                    }
                    first.Text = first.Text + tag;
                }
            }

            //doc.Save(Console.Out);
     */


    public class NodePositions //http://stackoverflow.com/questions/22661640/how-to-fix-ill-formed-html-with-html-agility-pack
    {
        public NodePositions(HtmlDocument doc)
        {
            AddNode(doc.DocumentNode);
            Nodes.Sort(new NodePositionComparer());
        }

        private void AddNode(HtmlNode node)
        {
            Nodes.Add(node);
            foreach (HtmlNode child in node.ChildNodes)
            {
                AddNode(child);
            }
        }

        private class NodePositionComparer : IComparer<HtmlNode>
        {
            public int Compare(HtmlNode x, HtmlNode y)
            {
                return x.StreamPosition.CompareTo(y.StreamPosition);
            }
        }

        public List<HtmlNode> Nodes = new List<HtmlNode>();
    }
}
