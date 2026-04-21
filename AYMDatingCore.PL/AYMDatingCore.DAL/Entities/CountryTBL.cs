using AYMDatingCore.DAL.BaseEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AYMDatingCore.DAL.Entities
{
    public class CountryTBL : BaseEntity<int>
    {
        public string? Name { get; set; } = null!;
    }
}
