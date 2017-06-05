using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using SQLitePCL;

namespace OurChat {
    class UserItemViewModel {
        private ObservableCollection<UserItem> allItems = new ObservableCollection<UserItem>();
        public ObservableCollection<UserItem> AllItems { get { return this.allItems; } }

        private UserItem selectedItem = default(UserItem);
        public UserItem SelectedItem { get { return selectedItem; } set { this.selectedItem = value; } }

        private static UserItemViewModel instance;

        private UserItemViewModel() {

            var db = App.conn;
            using (var statement = db.Prepare("SELECT * FROM UserItem")) {
                while (SQLiteResult.ROW == statement.Step()) {
                    this.allItems.Add(new UserItem((string)statement[0], (string)statement[1]));
                }
            }

        }

        public static UserItemViewModel getInstance() {
            if (instance == null) {
                instance = new UserItemViewModel();
            }
            return instance;
        }

        public bool Add(string username, string password) {

            foreach (UserItem item in allItems) {
                if (item.username.Equals(username)) {
                    return false;
                }
            }

            UserItem temp = new UserItem(username, MD5(password));

            this.allItems.Add(temp);
            var db = App.conn;
            using (var userItem = db.Prepare("INSERT INTO UserItem(Username, Password) VALUES (?, ?)")) {
                userItem.Bind(1, temp.username);
                userItem.Bind(2, temp.password);
                userItem.Step();
            }
            return true;
        }

        public bool Authorization(string username, string password)
        {
            string encrtpt_password = MD5(password);
            foreach (UserItem item in allItems) {
                if (item.username.Equals(username) && item.password.Equals(encrtpt_password)) {
                    return true;
                }
            }
            return false;
        }

        private string MD5(string password)
        {
            string encrtpt_password;
            CryptographicHash objHash = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5).CreateHash();
            objHash.Append(CryptographicBuffer.ConvertStringToBinary(password, BinaryStringEncoding.Utf16BE));
            encrtpt_password = CryptographicBuffer.EncodeToBase64String(objHash.GetValueAndReset());
            return encrtpt_password;
        }

    }
}
