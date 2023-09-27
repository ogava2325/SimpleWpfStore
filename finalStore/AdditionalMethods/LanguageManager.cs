using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace finalStore
{
    public static class LanguageManager
    {
        public static void SwitchLanguage(Window window, string languageCode)
        {
            ResourceDictionary dictionary = GetResourceDictionary(languageCode);
            window.Resources.MergedDictionaries.Clear();
            window.Resources.MergedDictionaries.Add(dictionary);
        }
        public static void SwitchLanguage(Page page, string languageCode)
        {
            ResourceDictionary dictionary = GetResourceDictionary(languageCode);
            page.Resources.MergedDictionaries.Clear();
            page.Resources.MergedDictionaries.Add(dictionary);
        }

        public static ResourceDictionary GetResourceDictionary(string languageCode)
        {
            string resourcePath = $"..\\LanguageResources\\StringResources.{languageCode}.xaml";
            ResourceDictionary dictionary = new ResourceDictionary();
            dictionary.Source = new Uri(resourcePath, UriKind.Relative);
            return dictionary;
        }
    }

}
