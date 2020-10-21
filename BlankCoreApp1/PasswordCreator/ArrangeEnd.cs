using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 末尾を記号にする
/// </summary>
namespace PasswordCreator {
    public class ArrangeEnd {
        public static string Execute(string pw){
            string[] marks = { "!", "#", "$", "%", "&", "@" };
            int count = pw.Where(x => marks.Contains(x.ToString())).Count();
            if (count == 0)  return pw;     //記号が含まれていない
            bool IsMarkEnd = marks.Contains(pw[pw.Length - 1].ToString());
            if (IsMarkEnd)   return pw;     //末尾が記号である
            var ans = pw.Select((Value, Index) => new { Value, Index })
                                .Where(x => marks.Contains(x.Value.ToString()))
                                .FirstOrDefault();
            var builder = new StringBuilder(pw);
            builder.Remove(ans.Index, 1);
            builder.Append(ans.Value);
            return builder.ToString();
        }
    }
}
