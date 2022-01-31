using MVVMTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTest
{
    public static class Settings
    {
        public static int id_User { get; set; }

        public static int? id_executor { get; set; } = null;
        
        public static string FullName { get; set; }
        
        public static string UserLogin { get; set; }

        public static bool TypeAuthLDAP { get; set; }

        public static bool UserLock { get; set; }

        public readonly static string stringDomain = "LDAP://upr.en.lg.ua:389";

        public readonly static string connectSqlString = "server=RSK234;uid=student;pwd=123;database=EIS_Proza";

        public static List<DICT_STATUSES> statuses;
    }
}
