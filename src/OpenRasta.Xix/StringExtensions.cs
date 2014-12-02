using System.Text;

namespace OpenRasta.Xix
{
  public static class StringExtensions
  {
    public static string ConvertUndescores(this string name)
    {
      var sb = new StringBuilder();
      var lastWasUnderscore = false;
      foreach (var cur in name)
      {
          if (cur == '_' && lastWasUnderscore) {sb.Append('_'); lastWasUnderscore = false;}
          else if (cur == '_' && !lastWasUnderscore) lastWasUnderscore = true;
          else if (lastWasUnderscore) { lastWasUnderscore = false; sb.Append('-').Append(cur); }
          else sb.Append(cur);
      }
      return sb.ToString();
    }
  }
}