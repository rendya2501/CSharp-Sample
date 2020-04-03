using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryPattern.Factory
{
    public abstract class Tray : Item
    {
        protected List<Item> tray = new List<Item>();

        //コンストラクタ
        public Tray(String caption) : base(caption) { }
        
        public void Add(Item item) => tray.Add(item);
    }
}
