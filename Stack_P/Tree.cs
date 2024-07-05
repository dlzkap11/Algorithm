using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack_P
{
    class Tree<T>
    {
        public T Data { get; set; }
        public List<Tree<T>> Children { get; set; } = new List<Tree<T>>();
    }
    


}
