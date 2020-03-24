using System.ComponentModel;
using System.Runtime.CompilerServices;
using Presentation.App.Code;

namespace Presentation.App.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        //Notify Property Changed Interface
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged( [CallerMemberName] string property = null )
        {
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( property ) );
        }

        //Application Context
        public ApplicationContext Context => ApplicationContext.GetApplicationContext();
    }
}
