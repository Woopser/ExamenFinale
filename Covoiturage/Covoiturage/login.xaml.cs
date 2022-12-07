﻿using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Covoiturage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class login : Page
    {
        public login()
        {
            this.InitializeComponent();
            lvLogin.ItemsSource = GestionBD.getInstance().getComptes();
        }

        private void Hyperlink_Click(Microsoft.UI.Xaml.Documents.Hyperlink sender, Microsoft.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            Frame.Navigate(typeof(CreationCompte));
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string login = tbMdp.Text + tbMdp.Text;

            foreach (var item in lvLogin.Items)
            {
                if (Convert.ToString(item) == login)
                {
                    tbErreur.Visibility = Visibility.Collapsed;

                    Frame.Navigate(typeof(MainAffiche));

                    break;
                }
                else
                    tbErreur.Visibility = Visibility.Visible;
            }
        }
    }
}
