using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordCreator {
    class MarkLetter : Letter{
        public MarkLetter(Random random) : base(random) {  }
        public override string GetLetter() {
            string[] marks = { "!", "#", "$", "%", "&", "@" };
            int n = random.Next(marks.Length);
            return marks[n];
        }
    }
}
