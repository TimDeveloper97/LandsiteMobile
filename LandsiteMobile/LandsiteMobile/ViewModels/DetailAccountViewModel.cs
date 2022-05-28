using Firebase.Auth;
using Firebase.Database.Query;
using LandsiteMobile.Domain;
using LandsiteMobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace LandsiteMobile.ViewModels
{
    class DetailAccountViewModel : BaseViewModel
    {
        #region Properties
        const string _empty = "--";
        private ObservableCollection<string> ages, genders, occupations;
        private string selectAge, selectGender, selectOccupation;

        public ObservableCollection<string> Ages { get => ages; set => SetProperty(ref ages, value); }
        public ObservableCollection<string> Genders { get => genders; set => SetProperty(ref genders, value); }
        public ObservableCollection<string> Occupations { get => occupations; set => SetProperty(ref occupations, value); }
        public string SelectAge { get => selectAge; set { SetProperty(ref selectAge, value); if (selectAge == _empty) SelectAge = null; } }
        public string SelectGender { get => selectGender; set { SetProperty(ref selectGender, value); if (selectGender == _empty) SelectGender = null; } }
        public string SelectOccupation { get => selectOccupation; set { SetProperty(ref selectOccupation, value); if (selectOccupation == _empty) SelectOccupation = null; } }
        #endregion

        #region Command 

        public ICommand PageAppearingCommand => new Command(async () =>
        {
            SelectAge = _usermodel.Age;
            SelectGender = _usermodel.Gender;
            SelectOccupation = _usermodel.Occupation;
        });

        public ICommand UploadProfileUserCommand => new Command(async () => await ExecuteUploadProfileUserCommand());


        #endregion

        public DetailAccountViewModel()
        {
            Title = "Edit Profile";

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

        #region Method
        async Task ExecuteUploadProfileUserCommand()
        {
            IsBusy = true;
            try
            {
                var user = (await _firebaseDatabase.Child("Users").OnceAsync<UserModel>()).Where(a => a.Object.LocalId == _usermodel.LocalId).FirstOrDefault();

                _usermodel.Age = user.Object.Age = SelectAge;
                _usermodel.Gender = user.Object.Gender = SelectGender;
                _usermodel.Occupation = user.Object.Occupation = SelectOccupation;

                await _firebaseDatabase.Child("Users").Child(user.Key)
                        .PutAsync(user.Object);



                await MaterialDialog.Instance.SnackbarAsync(message: "Update profile success",
                                         msDuration: MaterialSnackbar.DurationLong);
                await Shell.Current.GoToAsync("..");
            }
            catch (FirebaseAuthException ex)
            {
                if (ex.ResponseData == "N/A")
                    await MaterialDialog.Instance.SnackbarAsync(message: "Internet connection error",
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
