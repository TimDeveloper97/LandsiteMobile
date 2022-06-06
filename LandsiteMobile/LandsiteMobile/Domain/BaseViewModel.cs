
using Firebase.Auth;
using Firebase.Database;
using Firebase.Storage;
using LandsiteMobile.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace LandsiteMobile.Domain
{
    public class BaseViewModel : BaseBinding
    {
        private readonly string _realTimeData = "https://supervisor-cloud-default-rtdb.firebaseio.com/";
        private readonly string _auth = "AIzaSyApIY-Eo7vVosS24J_sJMRdx9oo42wt16g";
        private readonly string _storage = "supervisor-cloud.appspot.com";

        protected readonly FirebaseClient _firebaseDatabase;
        protected readonly FirebaseAuthProvider _firebaseAuthProvider;
        protected readonly FirebaseStorage _firebaseStorage;
        protected static UserModel _usermodel;

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public BaseViewModel()
        {
            _firebaseDatabase = new FirebaseClient(_realTimeData);
            _firebaseStorage = new FirebaseStorage(_storage);
            _firebaseAuthProvider = new FirebaseAuthProvider(new FirebaseConfig(_auth));
        }
    }
}
