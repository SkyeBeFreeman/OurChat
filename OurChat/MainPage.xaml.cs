using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace OurChat {
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page {

        private UserItemViewModel userItemViewModel;

        public MainPage() {
            this.InitializeComponent();
            userItemViewModel = UserItemViewModel.getInstance();

        }

        private void Regist_OnClick(object sender, RoutedEventArgs e) {
            Frame.Navigate(typeof(Register));
        }

        private async void Login_OnClick(object sender, RoutedEventArgs e) {
            if (userItemViewModel.Authorization(username.Text, password.Password)) {
                Frame.Navigate(typeof(ChatPage), username.Text);
            } else {
                var dialog = new ContentDialog() {
                    Title = "错误提示",
                    Content = "用户名或者密码错误",
                    PrimaryButtonText = "确定",
                };
                dialog.PrimaryButtonClick += (_s, _e) => {
                    password.Password = "";
                };
                await dialog.ShowAsync();
            }
        }
    }
}
