using Firebase.Auth;
using Firebase.Database.Query;
using LandsiteMobile.Domain;
using LandsiteMobile.Models;
using LandsiteMobile.Resources.Languages;
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
                        Age = SelectAge == null ? -1 : Ages.IndexOf(SelectAge),
                        Gender = SelectGender == null ? -1 : Genders.IndexOf(SelectGender),
                        Occupation = SelectOccupation == null ? -1 : Occupations.IndexOf(SelectOccupation),
                    };

                    await _firebaseDatabase.Child("UsersLandslide")
                            .PostAsync(model);
                    await MaterialDialog.Instance.SnackbarAsync(message: LanguageResource.messageSuccess,
                                             msDuration: MaterialSnackbar.DurationLong);
                    await Shell.Current.Navigation.PopToRootAsync();
                }
            }
            catch (FirebaseAuthException ex)
            {
                if (ex.ResponseData == "N/A")
                    await MaterialDialog.Instance.SnackbarAsync(message: LanguageResource.messageInternet,
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
            Ages = new ObservableCollection<string> { _empty };
            Genders = new ObservableCollection<string> { _empty, LanguageResource.genderMale, LanguageResource.genderFemale, LanguageResource.genderOther };
            Occupations = new ObservableCollection<string> { _empty, LanguageResource.occupStudent, LanguageResource.occupEmployee, LanguageResource.occupFreelancer, LanguageResource.occupUnemployed, LanguageResource.occupRetiree };

            Ages.Add(LanguageResource.age15);
            Ages.Add(LanguageResource.age16);
            Ages.Add(LanguageResource.age21);
            Ages.Add(LanguageResource.age26);
            Ages.Add(LanguageResource.age31);
            Ages.Add(LanguageResource.age36);
            Ages.Add(LanguageResource.age41);
            Ages.Add(LanguageResource.age46);
            Ages.Add(LanguageResource.age51);
            Ages.Add(LanguageResource.age56);
            Ages.Add(LanguageResource.age61);
            Ages.Add(LanguageResource.age66);
            Ages.Add(LanguageResource.age70);
        }

        async Task<bool> CheckCondition()
        {
            if (string.IsNullOrEmpty(Email))
            {
                await MaterialDialog.Instance.SnackbarAsync(message: LanguageResource.errorEmail,
                                 msDuration: MaterialSnackbar.DurationLong);
                return false;
            }
            if (string.IsNullOrEmpty(Password))
            {
                await MaterialDialog.Instance.SnackbarAsync(message: LanguageResource.errorPass,
                                 msDuration: MaterialSnackbar.DurationLong);
                return false;
            }
            if (Password.Length < 8)
            {
                await MaterialDialog.Instance.SnackbarAsync(message: LanguageResource.error8,
                                 msDuration: MaterialSnackbar.DurationLong);
                return false;
            }
            if (string.IsNullOrEmpty(Confirm))
            {
                await MaterialDialog.Instance.SnackbarAsync(message: LanguageResource.errorConfirm,
                                 msDuration: MaterialSnackbar.DurationLong);
                return false;
            }
            if(Password != Confirm)
            {
                await MaterialDialog.Instance.SnackbarAsync(message: LanguageResource.errordiff,
                                 msDuration: MaterialSnackbar.DurationLong);
                return false;
            }
            if (string.IsNullOrEmpty(SelectAge))
            {
                await MaterialDialog.Instance.SnackbarAsync(message: LanguageResource.errorAge,
                                 msDuration: MaterialSnackbar.DurationLong);
                return false;
            }
            if (string.IsNullOrEmpty(SelectGender))
            {
                await MaterialDialog.Instance.SnackbarAsync(message: LanguageResource.errorGender,
                                 msDuration: MaterialSnackbar.DurationLong);
                return false;
            }
            if (string.IsNullOrEmpty(SelectOccupation))
            {
                await MaterialDialog.Instance.SnackbarAsync(message: LanguageResource.errorOccup,
                                 msDuration: MaterialSnackbar.DurationLong);
                return false;
            }
            return true;
        }
    }
}
