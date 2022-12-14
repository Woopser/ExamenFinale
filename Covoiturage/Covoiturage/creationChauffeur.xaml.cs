using Microsoft.UI.Xaml;
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
    public sealed partial class creationChauffeur : Page
    {

        string voitu;
        bool valide = true;
        public creationChauffeur()
        {
            this.InitializeComponent();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {



            if (nomU.Text == "")
            {
                error.Text += " Veuillez entrer un nom d'utilisateur. ";
                valide = false;
            }
            if (Mdp.Text == "")
            {
                error.Text += " Veuillez entrer un mot de passe. ";
                valide = false;
            }

            if (prenom.Text == "" )
            {
                error.Text += " Veuillez entrer un prenom. ";
                valide = false;
            }
            if (nom.Text == "")
            {
                error.Text += " Veuillez entrer un nom. ";
                valide = false;
            }
            if (adresse.Text == "")
            {
                error.Text += " Veuillez entrer une adresse. ";
                valide = false;
            }
            if (tele.Text == "")
            {
                error.Text += " Veuillez entrer un numero de telephone. ";
                valide = false;
            }
            if (voiture.SelectedIndex == -1)
            {
                error.Text += " Veuillez selectionnez votre type de voiture. ";
                valide = false;
            }


            if (valide)
            {
                GestionBD.getInstance().AjoutCompte(nomU.Text, Mdp.Text, "Chauffeur");
                int b = GestionBD.getInstance().GetIdCompte(nomU.Text);
                GestionBD.getInstance().AjoutChauf(b, prenom.Text, nom.Text, adresse.Text, tele.Text, email.Text, voitu);
                Frame.Navigate(typeof(login));
            }
        }

        private void voiture_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            voitu = voiture.SelectedItem.ToString();
        }
    }
}
