using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUp.Models.Entity
{
    /// <summary>
    /// The Idea is to 'force' entities to have some DateTime attributes
    /// so we can compare them later!
    /// </summary>
    public interface IBasicEntity
    {
        DateTime AddAt { get; set; }
        DateTime? EditAt { get; set; }
        Nullable<DateTime> DeleteAt { get; set; }
        bool? Deleted { get; set; }

    }
}
