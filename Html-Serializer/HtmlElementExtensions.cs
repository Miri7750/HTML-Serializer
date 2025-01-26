using HtmlSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Html_Serializer
{
    internal static class HtmlElementExtensions
    {
        public static HashSet<HtmlElement> Search(this HtmlElement root, Selector selector)
        {
            HashSet<HtmlElement> match = new HashSet<HtmlElement>();
            Search(selector, root, match);
            return match;
        }
        private static void Search(Selector selector, HtmlElement htmlElement, HashSet<HtmlElement> matches)
        {
            var descendants = htmlElement.Descendants();
            if (selector.Child == null)
            {
                foreach (var descendant in descendants)
                {
                    Console.WriteLine($"Checking descendant: {descendant.Name}");
                    if (selector.Id == null || (selector.Id != null && selector.Id.Equals(descendant.Id)))
                        if (selector.Classes == null || (selector.Classes != null && selector.Classes.All(c => descendant.Classes.Contains(c))))
                            if (selector.TagName == null || (selector.TagName != null && descendant.Name != null && descendant.Name.Equals(selector.TagName)))
                            {
                                matches.Add(descendant);
                                Console.WriteLine($"Match found: {descendant.Name}\n\n");
                            }
                }
            }
            else
            {
                foreach (var descendant in descendants)
                {
                    Console.WriteLine($"Checking descendant: {descendant.Name}");
                    if (selector.Id == null || (selector.Id != null && selector.Id.Equals(descendant.Id)))
                        if (selector.Classes == null || (selector.Classes != null && selector.Classes.All(c => descendant.Classes.Contains(c))))
                            if (selector.TagName == null || (selector.TagName != null && descendant.Name != null && descendant.Name.Equals(selector.TagName)))
                            {
                                Search(selector.Child, descendant, matches);
                            }
                }
            }
        }
    }
}
