using Firebase.Auth;
using Firebase.Database.Query;
using LandsiteMobile.Domain;
using LandsiteMobile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace LandsiteMobile.ViewModels
{
    class SignUpViewModel : BaseViewModel
    {
        #region Properties
        const string _empty = "--";
        private ObservableCollection<string> ages, genders, occupations;
        private string selectAge, selectGender, selectOccupation;
        private string email, password, confirm;

        public ObservableCollection<string> Ages { get => ages; set => SetProperty(ref ages, value); }
        public ObservableCollection<string> Genders { get => genders; set => SetProperty(ref genders, value); }
        public ObservableCollection<string> Occupations { get => occupations; set => SetProperty(ref occupations, value); }
        public string SelectAge { get => selectAge; set { SetProperty(ref selectAge, value); if (selectAge == _empty) SelectAge = null; } }
        public string SelectGender { get => selectGender; set { SetProperty(ref selectGender, value); if (selectGender == _empty) SelectGender = null; } }
        public string SelectOccupation { get => selectOccupation; set { SetProperty(ref selectOccupation, value); if (selectOccupation == _empty) SelectOccupation = null; } }
        public string Email { get => email; set => SetProperty(ref email, value); }
        public string Password { get => password; set => SetProperty(ref password, value); }
        public string Confirm { get => confirm; set => SetProperty(ref confirm, value); }
        #endregion

        #region Command 
        public ICommand LoginCommand => new Command(async () =>
        {
            await Shell.Current.Navigation.PopToRootAsync();
        });
        public ICommand SignUpCommand => new Command(async () =>
        {
            IsBusy = true;
            try
            {
                if(await CheckCondition())
                {
                    var auth = await _firebaseAuthProvider.CreateUserWithEmailAndPasswordAsync(Email, Password, Email, true);
                    var content = await auth.GetFreshAuthAsync();
                     
                    var model = new UserModel
                    {
                        Email = Email,
                        DisplayName = Email,
                        IsEmailVerified = false,
                        LocalId = content.User.LocalId,
                        Age = SelectAge,
                        Gender = SelectGender, 
                        Occupation = SelectOccupation,
                    };

                    await _firebaseDatabase.Child("Users")
                            .PostAsync(model);
                    await MaterialDialog.Instance.SnackbarAsync(message: "Account successfully created!\nPlease verify your email to use the account",
                                             msDuration: MaterialSnackbar.DurationLong);
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
            finally
            {
                IsBusy = false;
            }
        });
        #endregion

        public SignUpViewModel()
        {
            Ages = new ObservableCollection<string> { _empty, "Less than 15" };
            Genders = new ObservableCollection<string> { _empty, "Male", "Female", "Other" };
            Occupations = new ObservableCollection<string> { _empty, "Student", "Employee", "Freelancer", "Unemployed", "Retiree" };

            for (int i = 16; i <= 70; i += 5)
            {
                string item = "From " + i + " to " + (i + 4);
                Ages.Add(item);
            }
            Ages.Add("More than 70");
        }

        async Task<bool> CheckCondition()
        {
            if (string.IsNullOrEmpty(Email))
            {
                await MaterialDialog.Instance.SnackbarAsync(message: "Must enter email",
                                 msDuration: MaterialSnackbar.DurationLong);
                return false;
            }
            if (string.IsNullOrEmpty(Password))
            {
                await MaterialDialog.Instance.SnackbarAsync(message: "Must enter password",
                                 msDuration: MaterialSnackbar.DurationLong);
                return false;
            }
            if (Password.Length < 8)
            {
                await MaterialDialog.Instance.SnackbarAsync(message: "Password at least 8 characters",
                                 msDuration: MaterialSnackbar.DurationLong);
                return false;
            }
            if (string.IsNullOrEmpty(Confirm))
            {
                await MaterialDialog.Instance.SnackbarAsync(message: "Must enter confirm",
                                 msDuration: MaterialSnackbar.DurationLong);
                return false;
            }
            if(Password != Confirm)
            {
                await MaterialDialog.Instance.SnackbarAsync(message: "Confirm password must like password",
                                 msDuration: MaterialSnackbar.DurationLong);
                return false;
            }
            if (string.IsNullOrEmpty(SelectAge))
            {
                await MaterialDialog.Instance.SnackbarAsync(message: "Must enter age",
                                 msDuration: MaterialSnackbar.DurationLong);
                return false;
            }
            if (string.IsNullOrEmpty(SelectGender))
            {
                await MaterialDialog.Instance.SnackbarAsync(message: "Must enter gender",
                                 msDuration: MaterialSnackbar.DurationLong);
                return false;
            }
            if (string.IsNullOrEmpty(SelectOccupation))
            {
                await MaterialDialog.Instance.SnackbarAsync(message: "Must enter occupation",
                                 msDuration: MaterialSnackbar.DurationLong);
                return false;
            }
            return true;
        }
    }
}
