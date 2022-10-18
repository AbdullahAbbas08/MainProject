using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.ViewModels
{
    public class SelectItemDto<T> where T : class
    {
        public string Text { get; set; }
        public T value { get; set; }
    }
}
