using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryPattern.Factory
{
    public abstract class Link : Item
    {
        protected string url;
        public Link(string caption,string url) : base(caption)
        {
            this.url = url;
        }
    }
}
