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
    public sealed partial class creationClient : Page
    {
        public creationClient()
        {
            this.InitializeComponent();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            GestionBD.getInstance().AjoutCompte(nomU.Text, Mdp.Text, "Chauffeur");


            int b = GestionBD.getInstance().GetIdCompte(nomU.Text);
            
            GestionBD.getInstance().AjoutCli(b, prenom.Text, nom.Text, adresse.Text, tele.Text, email.Text, villeDep.Text, villeArr.Text);
            Frame.Navigate(typeof(login));

        }
    }
}