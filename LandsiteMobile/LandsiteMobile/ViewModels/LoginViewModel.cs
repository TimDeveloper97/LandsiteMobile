using Firebase.Auth;
using Firebase.Database;
using LandsiteMobile.Domain;
using LandsiteMobile.Models;
using LandsiteMobile.Resources.Languages;
using LandsiteMobile.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace LandsiteMobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Properties
        private string email, password;
        private string errorEmail, errorPassword;

        public string Email { get => email; set => SetProperty(ref email, value); }
        public string Password { get => password; set => SetProperty(ref password, value); }
        public string ErrorEmail { get => errorEmail; set => SetProperty(ref errorEmail, value); }
        public string ErrorPassword { get => errorPassword; set => SetProperty(ref errorPassword, value); }
        #endregion

        #region Command 

        public ICommand PageAppearingCommand => new Command(() =>
        {
            Init();
        });
        public ICommand LoginCommand => new Command(async () =>
        {
            if (CheckLogin())
            {
                Reset();
                await ExecuteLoadLoginCommand();
            }
        });
        public ICommand ForgotPasswordCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        });
        public ICommand SignUpCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync(nameof(PolicyPage));
        });
        #endregion


        public LoginViewModel()
        {
            Title = "Login";
            Email = "timdeveloper97@gmail.com";
            Password = "12345678";
        }

        #region Method
        void Init()
        {
            Reset();
            ChangeLanguage(Preferences.Get("language", ""));
        }

        void ChangeLanguage(string lang)
        {
            LocalizationResourceManager.Current.SetCulture(lang == null ? CultureInfo.CurrentCulture : new CultureInfo(lang));
        }

        void Reset()
        {
            ErrorEmail = null;
            ErrorPassword = null;
        }

        bool CheckLogin()
        {
            if (string.IsNullOrEmpty(Email))
            {
                ErrorEmail = LanguageResource.loginErrorEmail;
                return false;
            }

            if (string.IsNullOrEmpty(Password))
            {
                ErrorPassword = LanguageResource.loginErrorPassword;
                return false;
            }

            return true;
        }

        async Task ExecuteLoadLoginCommand()
        {
            IsBusy = true;
            Preferences.Set("email", Email);

            try
            {
                var auth = await _firebaseAuthProvider.SignInWithEmailAndPasswordAsync(Email, Password);
                var content = await auth.GetFreshAuthAsync();
                //if (!content.User.IsEmailVerified)
                //{
                //    var json = JsonConvert.SerializeObject(new BaseResponse { Error = new Error { Message = "Email is not verified" } });
                //    throw new FirebaseAuthException(null, null, json, null);
                //}

                var user = (await _firebaseDatabase.Child("UsersLandslide")
                                    .OnceAsync<UserModel>()).FirstOrDefault(x => x.Object.LocalId == content.User.LocalId);
                if (user != null)
                {
                    _usermodel = user.Object;

                    await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
                    await Shell.Current.Navigation.PopToRootAsync();
                }
            } 
            catch (FirebaseAuthException ex)
            {
                if (ex.ResponseData == "N/A")
                    await MaterialDialog.Instance.SnackbarAsync(message: "Internet connection error",
                                     msDuration: MaterialSnackbar.DurationLong);
                else
                {
                    var response = JsonConvert.DeserializeObject<BaseResponse>(ex.ResponseData);

                    await MaterialDialog.Instance.SnackbarAsync(message: response.Error.Message,
                                         msDuration: MaterialSnackbar.DurationLong);
                }
            }
            catch (FirebaseException fe)
            {
                if (fe.ResponseData == "")
                    await MaterialDialog.Instance.SnackbarAsync(message: "Timeout responce",
                                     msDuration: MaterialSnackbar.DurationLong);
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion
    }
}

