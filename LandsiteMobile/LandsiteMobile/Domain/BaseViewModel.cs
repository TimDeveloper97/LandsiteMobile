
using Firebase.Auth;
using Firebase.Database;
using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace LandsiteMobile.Domain
{
    public class BaseViewModel : BaseBinding
    {
        private readonly string _realTimeData = "https://firecast-app-7a261.firebaseio.com/";
        private readonly string _auth = "AIzaSyAGMVT0Qttpl_5XS_HcB1y06kIZ4wzHKZI";
        private readonly string _storage = "firecast-app-7a261.appspot.com";

        protected readonly FirebaseClient _firebaseDatabase;
        protected readonly FirebaseAuthProvider _firebaseAuthProvider;
        protected readonly FirebaseStorage _firebaseStorage;
        protected static User _user;

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
