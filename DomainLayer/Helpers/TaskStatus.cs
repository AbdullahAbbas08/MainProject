using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Helpers
{
    public enum TaskStatus
    {
        [Display(Name = "New Task")]
        New,
        [Display(Name = "InProgress Task")]
        InProgress,
        [Display(Name = "Finished Task")]
        Finished
    }
}
