using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enums;

namespace Data.DAL
{
    public static class CodesDictionary
    {
        public static UserRoles RoleType( string role )
        {
            switch ( role )
            {
                case ( "WRK" ): return UserRoles.Worker;
                case ( "MAN" ): return UserRoles.Manager;
                case ( "ADM" ): return UserRoles.Administrator;

                default: throw new Exception();
            }
        }

        public static string RoleType( UserRoles role )
        {
            switch ( role )
            {
                case ( UserRoles.Administrator ): return "ADM";
                case ( UserRoles.Manager ): return "MAN";
                case ( UserRoles.Worker ): return "WRK";

                default: throw new Exception();
            }
        }

        public static ActivityStatus ActivityType( string status )
        {
            switch ( status )
            {
                case ( "OPN" ): return ActivityStatus.Open;
                case ( "PRO" ): return ActivityStatus.InProgress;
                case ( "CAN" ): return ActivityStatus.Cancelled;
                case ( "FIN" ): return ActivityStatus.Finished;

                default: throw new Exception();
            }
        }

        public static string ActivityType( ActivityStatus status )
        {
            switch ( status )
            {
                case ( ActivityStatus.Open ): return "OPN";  
                case ( ActivityStatus.InProgress ): return "PRO";  
                case ( ActivityStatus.Cancelled ): return "CAN";
                case ( ActivityStatus.Finished ): return "FIN";
                    
                default: throw new Exception();
            }
        }
    }
}
