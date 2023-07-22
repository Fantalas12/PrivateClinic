using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using PrivateClinic.Desktop.Model;
using PrivateClinic.Desktop.ViewModel;


namespace PrivateClinic.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private PrivateClinicApiService _service;
        private MainViewModel _mainViewModel;
        private LoginViewModel _loginViewModel;
        private MainWindow _mainView;
        private LoginWindow _loginView;
        private MedicalRecordWindow _medicalRecordWindow;


        public App()
        {
            Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            _service = new PrivateClinicApiService(ConfigurationManager.AppSettings["baseAddress"]);



            _loginViewModel = new LoginViewModel(_service);

            _loginViewModel.LoginSucceeded += ViewModel_LoginSucceeded;
            _loginViewModel.LoginFailed += ViewModel_LoginFailed;
            _loginViewModel.MessageApplication += ViewModel_MessageApplication;

            _loginView = new LoginWindow
            {
                DataContext = _loginViewModel
            };


            _mainViewModel = new MainViewModel(_service);
            _mainViewModel.MessageApplication += ViewModel_MessageApplication;
            _mainViewModel.LogoutSucceeded += ViewModel_LogoutSucceeded;
            _mainViewModel.MedicalRecordEditStarted += ViewModel_MedicalRecordEditStarted;
            _mainViewModel.MedicalRecordEditEnded += ViewModel_MedicalRecordEditEnded;

            _mainView = new MainWindow
            {
                DataContext = _mainViewModel
            };

            _medicalRecordWindow = new MedicalRecordWindow
            {
                DataContext = _mainViewModel
            };


        // Alkalmazás leállítása a főablak bezárásakor
        // (alapértelmezetten az összes ablak bezárásakor történik, de a login ablakot csak elrejteni fogjuk)
        MainWindow = _mainView;
            ShutdownMode = ShutdownMode.OnMainWindowClose;
            _loginView.Closed += LoginView_Closed; // bejelentkezési ablak bezárásakor is leállítás


            //_mainView.Show();
            _loginView.Show();
        }

        private void LoginView_Closed(object sender, EventArgs e)
        {
            Shutdown();
        }

        private void ViewModel_LoginSucceeded(object sender, EventArgs e)
        {
            _loginView.Hide();
            _mainView.Show();
        }

        private void ViewModel_LoginFailed(object sender, EventArgs e)
        {
            MessageBox.Show("Login unsuccessful!", "PrivateClinic", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private void ViewModel_LogoutSucceeded(object sender, EventArgs e)
        {
            _mainView.Hide();
            _loginView.Show();
        }

        private void ViewModel_MessageApplication(object sender, MessageEventArgs e)
        {
            MessageBox.Show(e.Message, "PrivateClinic", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private void ViewModel_MedicalRecordEditStarted(object sender, EventArgs e)
        {
            _mainView.Hide();
            _medicalRecordWindow.Show();
        }

        private void ViewModel_MedicalRecordEditEnded(object sender, EventArgs e)
        {
            _medicalRecordWindow.Hide();
            _mainView.Show();
        }



    }
}
