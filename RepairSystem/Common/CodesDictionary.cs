using System;
using Common.Enums;

namespace Common
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
                case ( ActivityStatus.Cancelled ): return "CAN";
                case ( ActivityStatus.Finished ): return "FIN";
                    
                default: throw new Exception();
            }
        }

        public static AccountStatus AccountStatus( string status )
        {
            switch ( status )
            {
                case ( "T" ): return Enums.AccountStatus.Active;
                case ( "F" ): return Enums.AccountStatus.Inactive;

                default: throw new Exception();
            }
        }

        public static string AccountStatus( AccountStatus status )
        {
            switch ( status )
            {
                case ( Enums.AccountStatus.Active ): return "T";
                case ( Enums.AccountStatus.Inactive ): return "F";

                default: throw new Exception();
            }
        }
    }
}
