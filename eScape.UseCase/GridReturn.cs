using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eScape.UseCase
{
    public class GridReturn<T>
    {
        public List<T>? Items { get; set; }
        public int Total {  get; set; }
    }
}
