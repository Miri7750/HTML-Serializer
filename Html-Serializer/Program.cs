
using Html_Serializer;
using HtmlSerializer;
using System.Text.RegularExpressions;

var url = "https://learn.malkabruk.co.il/practicode/projects/pract-2";
var html = await Load(url);
await Console.Out.WriteLineAsync(html);

//var cleanHtml = new Regex("\\s").Replace(html, "");
//var htmlLines = new Regex("<.*?>").Split(cleanHtml).Where(s => s.Length > 0);

var attributes = new Regex("([^\\s]*?)=\"(.*?)\"");
var onlySpaces = new Regex(@"^\s*$");
var cleanHtml = new Regex("\\n").Replace(html, string.Empty);
var htmlLines = new Regex("<(.*?)>").Split(cleanHtml).Where(s => !onlySpaces.IsMatch(s) && s.Length > 0).ToList();

HtmlElement root = new HtmlElement(), cur = root;

var first = htmlLines[1];
int spaceIndex = first.IndexOf(' ');
string firstWord = (spaceIndex == -1) ? first : first.Substring(0, spaceIndex);
root.InnerHtml = firstWord;
cur=CreatHtmlElement(root, first);

//static HtmlElement BuildTree(HtmlElement cur,List<string> htmlLines) {
    int i =2;
var line = htmlLines[i];
while (line!="/html")
    {

        i++;


    if (line.StartsWith("!--"))
    {
        while (!line.EndsWith("--"))
            line = htmlLines[i++];
        line = htmlLines[i];
    }
    else
    {
        
    
    spaceIndex = line.IndexOf(' ');
        firstWord = (spaceIndex == -1) ? line : line.Substring(0, spaceIndex);
        if (firstWord.StartsWith("/"))
        {
            cur = cur.Parent;
        }
        else
        {
            if (HtmlHelper.Instance.HtmlTags.Contains(firstWord))
            {
                var element = new HtmlElement();
                element.Name = firstWord;
                element.Parent = cur;
                cur.Children.Add(element);
                /*element =*/ CreatHtmlElement(element, line);
                if (!(line.EndsWith("/") || HtmlHelper.Instance.HtmlVoidTags.Contains(firstWord)))
                {
                    cur = element;
                }
            }
            else
            {
                cur.InnerHtml += line;

            }
        }
        line = htmlLines[i];
    }
    


}
//}
Console.WriteLine("aastg");
var query=Console.ReadLine();
//Console.WriteLine("sss");

var selector = Selector.CreateByQuery(query);
//Console.WriteLine("sss");
var result=root.Search(selector);
foreach (var item in result)
    Console.WriteLine(item);




Console.WriteLine();
HtmlElement CreatHtmlElement(HtmlElement element,string line)
{

    //int spaceIndex = line.IndexOf(' ');
    //string firstWord = (spaceIndex == -1) ? line : line.Substring(0, spaceIndex);
    //element.Name = firstWord;
    element.Attributes = new Regex("([^\\s]*?)=\"(.*?)\"").Matches(line).Select(a=>a.Value).ToList();
    //if (line.Contains("class"))

    //    element.Classes=element.Attributes.Find(a => a.StartsWith("class"))?.Substring(6).Split(' ').ToList();
    if (line.Contains("class"))
    {
        element.Classes = element.Attributes.Find(a => a.StartsWith("class"))?.Substring(6).Split(' ').ToList() ?? new List<string>();
        element.Attributes.Remove(element.Attributes.Find(a=>a.StartsWith("class")));
    }

    if (line.Contains(" id="))
    {
        element.Id = element.Attributes.Find(a => a.StartsWith("id")).Split('=').Last();
        element.Attributes.Remove($"id={element.Id}");
    }
        element.InnerHtml = "";
    return element;

}
async Task<string> Load(string url)
{
    HttpClient client = new HttpClient();
    var response = await client.GetAsync(url);
    var html = await response.Content.ReadAsStringAsync();
    return html;
}
