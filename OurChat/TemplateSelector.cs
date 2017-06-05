using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace OurChat {
    class TemplateSelector : DataTemplateSelector {

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container) {
            if ((item as Content).name == "right") {
                return App.Current.Resources["RightDataTemplate"] as DataTemplate;
            } else if ((item as Content).name == "left") {
                if ((item as Content).code == "100000") {
                    return App.Current.Resources["LeftDataTemplate"] as DataTemplate;
                } else if ((item as Content).code == "200000") {
                    return App.Current.Resources["urls"] as DataTemplate;
                } else if ((item as Content).code == "302000") {
                    return App.Current.Resources["news"] as DataTemplate;
                } else if ((item as Content).code == "308000") {
                    return App.Current.Resources["cookbook"] as DataTemplate;
                }
            }

            return base.SelectTemplateCore(item, container);
        }

    }
}
