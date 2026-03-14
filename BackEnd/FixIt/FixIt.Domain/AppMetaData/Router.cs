using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Domain.AppMetaData
{
    public static class Router
    {
        public const string Root = "Api";
        public const string Versoin = "V01";
        public const string Rule = Root + "/" + Versoin + "/";

        public static class WorkerRouting
        {

            public const string Prefix = Rule + "Worker";
            public const string List = Prefix + "/List";
            public const string Create = Prefix + "/Create";

        }



    }
}
