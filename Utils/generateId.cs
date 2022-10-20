using System.Text;
namespace BTL.NET2.Utils;

class GenerateID
{
  StringBuilder builder = new StringBuilder();

  public string createID()
  {
    Enumerable
    .Range(65, 26)
    .Select(e => ((char)e).ToString())
    .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
    .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
    .OrderBy(e => Guid.NewGuid())
    .Take(11)
    .ToList().ForEach(e => builder.Append(e));
    string id = builder.ToString();

    return id;
  }
}