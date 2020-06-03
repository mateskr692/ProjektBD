using System.ComponentModel;
using System.Runtime.CompilerServices;
using Presentation.App.Commands;
using Presentation.App.Security;

namespace Presentation.App.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public BaseViewModel()
        {
            this.SignOutCommand = new RelayCommand( this.RaiseSignOut );
        }

        //Notify Property Changed Interface
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged( [CallerMemberName] string property = null )
        {
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( property ) );
        }

        private string _ErrorMessage;
        public string ErrorMessage
        {
            get => this._ErrorMessage;
            set { this._ErrorMessage = value; this.OnPropertyChanged(); }
        }

        public delegate void WindowDelegate();

        public event WindowDelegate CloseWindow;
        protected void RaiseCloseEvent( object parameter = null ) => this.CloseWindow();

        public event WindowDelegate SignOut;
        protected void RaiseSignOut( object parameter = null ) => this.SignOut();

        public string CurrentUser => AuthenticationManager.UserName();
        public RelayCommand SignOutCommand { get; set; }
    }
}
