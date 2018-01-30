using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToSql.Model
{
    [Table(Name = "AccessTab")]
    public class AccessTab
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int intTabID { get; set; }

        [Column(Name = "strTabName")]
        public string strTabName { get; set; }

        [Column(Name = "strDescription")]
        public string strDescription { get; set; }

        [Column(Name = "strTabUrl")]
        public string strTabUrl { get; set; }

        [Column(Name = "strTabGroupName")]
        public string strTabGroupName { get; set; }


        [Association(ThisKey = "intTabID", OtherKey = "intTabID")]
        public EntitySet<AccessUser> AccessUser { get; set; }

    }
}