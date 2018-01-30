using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToSql.Model
{
    public class MCSModel : DataContext
    {
        public MCSModel() : base("Data source=192.168.111.107; Initial Catalog=MCS; User ID=af; Password=Af123456")
        { 
     
        }
        public Table<AccessTab> AccessTabs { get; set; }
        public Table<AccessUser> AccessUsers { get; set; }
    }
}