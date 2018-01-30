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
   public class AccessUser
    {
 
        [Column(IsPrimaryKey = true)]
        public int intAccessID { get; set; }
        [Column]
        public int intUserId { get; set; }
        [Column]
        public DateTime dCreated { get; set; }
        [Column]
        public int intTabID { get; set; }

        public string strTabGrpName
         {

            get { return strTabGrpName; }
            set { strTabGrpName = value; }
         }

        [Association(ThisKey = "intTabID", OtherKey = "intTabID")]
        public EntitySet<AccessTab> AccessTabs { get; set; }
    }
}
