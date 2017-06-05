using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace OurChat {
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Register : Page {

        private UserItemViewModel userItemViewModel;

        public Register() {
            this.InitializeComponent();
            userItemViewModel = UserItemViewModel.getInstance();
        }

        private async void Regist_OnClick(object sender, RoutedEventArgs e) {

            // 初始化tips文本
            username_tips.Text = "";
            password_tips.Text = "";
            password_check_tips.Text = "";

            // 获取文本框数据
            string username = regist_username.Text;
            string password = regist_password.Password;
            string password_check = regist_password_check.Password;

            if (username != "" && password != "" && password_check != "") {
                if (password == password_check) {
                    if (userItemViewModel.Add(username, password))
                    {
                        Frame.Navigate(typeof(MainPage), username);
                    }
                    else
                    {
                        var dialog = new ContentDialog() {
                            Title = "错误提示",
                            Content = "用户名已存在",
                            PrimaryButtonText = "确定",
                        };
                        dialog.PrimaryButtonClick += (_s, _e) =>
                        {
                            regist_username.Text = "";
                            regist_password.Password = "";
                            regist_password_check.Password = "";
                        };
                        await dialog.ShowAsync();
                    }
                    
                } else {

                    var dialog = new ContentDialog() {
                        Title = "错误提示",
                        Content = "两次密码输入不一致",
                        PrimaryButtonText = "确定",
                    };
                    dialog.PrimaryButtonClick += (_s, _e) => {
                        regist_password.Password = "";
                        regist_password_check.Password = "";
                    };
                    await dialog.ShowAsync();
                }
            } else {
                if (username.Equals("")) {
                    username_tips.Text = "* 请输入用户名";
                }
                if (password.Equals("")) {
                    password_tips.Text = "* 请输入密码";
                }
                if (password_check.Equals("")) {
                    password_check_tips.Text = "* 请再次输入密码";
                }
            }
        }

        private void Cacel_OnClick(object sender, RoutedEventArgs e) {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
