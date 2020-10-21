using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// interface版
/// </summary>
namespace PasswordCreator {
    public interface ILetterFactory {
        Letter Create(Random random, int c);
    }
}
