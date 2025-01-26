using HtmlSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Html_Serializer
{
    internal class Selector
    {
        public string TagName { get; set; }
        public string Id { get; set; }
        public List<string> Classes { get; set; }
        public Selector Parent { get; set; }
        public Selector Child { get; set; }

        public static Selector CreateByQuery(string query)
        {
            var qryLevels = query.Split(' ');
            string[] classes;
            Selector root = new Selector(), cur = root;
            foreach (var level in qryLevels)
            {
                cur.Child = new Selector();
                cur.Child.Parent = cur;
                cur = cur.Child;
                string str= level;
                int x = level.IndexOf('#');
                if (x != 0 && !level.StartsWith('.'))
                {
                    int y;
                    for (y = 0;y<level.Length&& level[y] != '.' && level[y] != '#'; y++) ;
                    str = level.Substring(0, y );
                    if (HtmlHelper.Instance.HtmlTags.Contains(str))
                    {
                        cur.TagName = str;
                    }
                    str= level.Substring(y);
                    x = str.IndexOf("#");
                }
                if (x > -1)
                {
                    int y;
                    
                    if ((y = str.IndexOf('.', x)) > -1)
                    {
                        cur.Id = str.Substring(x + 1, y - x - 1);
                        str = str.Substring(0, x)+str.Substring(y);
                    }
                    else { 
                        cur.Id = str.Substring(x + 1); 
                        str= str.Substring(0, x);
                    }
                }
                cur.Classes = str.Split('.').ToList();
                cur.Classes.Remove("");
               
            }
            cur = root;
            root = root.Child;
            root.Parent = null;
            cur.Child = null;
            return root;
        }
    }
}
