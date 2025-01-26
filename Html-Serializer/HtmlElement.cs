using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlSerializer
{
    internal class HtmlElement
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public List<string>? Attributes { get; set; }//= new List<string>();
        public List<string>? Classes { get; set; }//=new List<string>();
        public string? InnerHtml { get; set; }
        public HtmlElement? Parent { get; set; }
        public List<HtmlElement>? Children { get; set; }//= new List<HtmlElement>();
        public IEnumerable<HtmlElement>? Descendants()
        {
            Queue<HtmlElement> queue = new Queue<HtmlElement>();
            queue.Enqueue(this);
            while (queue.Count > 0)
            {
                HtmlElement child = queue.Dequeue();
                yield return child;
                foreach (var item in child.Children)
                {
                    queue.Enqueue(item);
                }
            }
        }
        public HtmlElement()
        {
            Attributes = new List<string>();
            Classes = new List<string>();
            Children = new List<HtmlElement>();
        }
        public IEnumerable<HtmlElement>? Ancestors()
        {
            HtmlElement element = Parent;
            while (element != null)
            {
                yield return element;
                element = element.Parent;
            }
        }


    }
}
