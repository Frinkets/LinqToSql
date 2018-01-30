using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using LinqToSql.Model;
using System.Data.Linq.Mapping;

namespace LinqToSql
{
    class Program
    {
        static MCSModel db = new MCSModel();
        static void Main(string[] args)
        {

           
            Example8();
            
        }
        static void Example1() 
        {
            Table<AccessTab> accessTables =
            db.GetTable<AccessTab>();

            foreach (AccessTab tab in accessTables)
            {
                Console.WriteLine("Tab Name:" + tab.strTabName);

            }
        }
        static void Example2()
        {
            try
            {
                Table<AccessTab> accessTables =
                db.GetTable<AccessTab>();
                AccessTab tab = accessTables.FirstOrDefault(f => f.intTabID == 56);
                tab.strDescription = "***some descr";
                db.SubmitChanges(ConflictMode.FailOnFirstConflict);
            }
            catch(ChangeConflictException ex)
            {

                Console.WriteLine(ex.Message);
                foreach (ObjectChangeConflict item in db.ChangeConflicts)
                {
                    MetaTable metatable = db.Mapping.GetTable(item.Object.GetType());

                    Model.AccessTab en = (Model.AccessTab)item.Object;
                    Console.WriteLine("Table Name {0}:", metatable.TableName);

                }

            }
            //foreach (AccessTab tab in accessTables)
            //{
            //    Console.WriteLine("Tab Name:" + tab.strTabName);

            //}
        }

        static void Example3()
        {
            Table<AccessUser> users = db.GetTable<AccessUser>();

            var query = from u in users
                        where u.intUserId == 1
                        select
                            from t in u.AccessTabs
                            select new { u.intUserId, t.strTabName };

           

            Console.WriteLine("--------------------------------");
            db.Log = Console.Out;
            Console.WriteLine("--------------------------------");
            MetaModel mm = db.Mapping;
            Console.WriteLine("--------------------------------");

            foreach (var item in query)
            {
                foreach (var item2 in item)
                {
                    Console.WriteLine(item2.intUserId + " - " + item2.strTabName);
                }
            }
            //Console.WriteLine("------------------------------------------------------------------------");
            //foreach (var user in users)
            //{
            //    foreach (var tab in user.AccessTabs)
            //    {
            //        Console.WriteLine(user.intUserId + " : " + user.strTabGrpName);
            //    }
            //}

        }
        static void Example4()
        {
            Console.WriteLine("connection {0}", db.Connection);
            Console.WriteLine("connection {0}", db.Connection.ConnectionString);
        
        }
        static void Example5(MCSModel dataContext)
        {
            Table<AccessTab> tabs = dataContext.GetTable<AccessTab>();
            Table<AccessUser> users = dataContext.GetTable<AccessUser>();

            AccessTab a = tabs.OrderBy(o => o.strTabName).First(f => f.intTabID == 56);





        }


        static void Example8()
        {
            Table<AccessTab> accessTabs = db.GetTable<AccessTab>();
            AccessTab aTab = accessTabs.FirstOrDefault(f => f.intTabID == 1);

            aTab.strDescription = "Test 005";

            Table<AccessUser> users = db.GetTable<AccessUser>();
            AccessUser aUser = users.FirstOrDefault(f => f.intAccessID == 6822);

            aUser.dCreated = DateTime.Now;
            aUser.intTabID = 108;
            try 
            {
                using (System.Transactions.TransactionScope scope =
                new System.Transactions.TransactionScope()) 
                {
                    db.SubmitChanges();
                    scope.Complete();
                }
            }
            catch(Exception ex)
             {
                Console.WriteLine(ex.Message);
              }

              finally
              {
                db.Refresh(RefreshMode.OverwriteCurrentValues, accessTabs);
                Console.WriteLine( "StrDescription : {0}", aTab.strDescription);

                db.Refresh(RefreshMode.OverwriteCurrentValues, accessTabs);
                Console.WriteLine("dCreated : {0}", aUser.dCreated);

            }







        }
    }
}