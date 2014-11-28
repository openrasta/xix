using System.Text;

namespace OpenRasta.Xix
{
  public static class StringExtensions
  {
    public static string ConvertUndescores(this string name)
    {
      var sb = new StringBuilder();
      bool lastWas_ = false;
      for (int i = 0; i < name.Length; i++)
      {
        char cur = name[i];
        if (cur == '_' && lastWas_) {sb.Append('_'); lastWas_ = false;}
        else if (cur == '_' && !lastWas_) lastWas_ = true;
        else if (lastWas_) { lastWas_ = false; sb.Append('-').Append(cur); }
        else sb.Append(cur);
      }
      return sb.ToString();
    }
  }
}