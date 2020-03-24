using Common.Enums;

namespace Presentation.App.Code
{
    public class ApplicationContext
    {
        //Fields
        public string Username { get; set; }
        public long UserId { get; set; }
        public UserRole UserRole { get; set; }

        //Singleton
        private ApplicationContext() { }

        private static ApplicationContext _context;
        public static ApplicationContext GetApplicationContext()
        {
            if ( _context == null )
                _context = new ApplicationContext();

            return _context;
        }

    }
}
