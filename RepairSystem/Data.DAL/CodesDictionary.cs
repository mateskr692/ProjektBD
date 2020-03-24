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
        public static UserRole RoleType( string role )
        {
            switch ( role )
            {
                case ( "WRK" ): return UserRole.Worker;
                case ( "MAN" ): return UserRole.Manager;
                case ( "ADM" ): return UserRole.Administrator;

                default: throw new Exception();
            }
        }

        public static string RoleType( UserRole role )
        {
            switch ( role )
            {
                case ( UserRole.Administrator ): return "ADM";
                case ( UserRole.Manager ): return "MAN";
                case ( UserRole.Worker ): return "WRK";

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
