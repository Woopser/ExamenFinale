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
    public sealed partial class login : Page
    {
        public login()
        {
            this.InitializeComponent();
        }

        private void Hyperlink_Click(Microsoft.UI.Xaml.Documents.Hyperlink sender, Microsoft.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            Frame.Navigate(typeof(CreationCompte));
        }

        //Vérifie la connexion avec le singleton
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Compte B = GestionBD.getInstance().verifConnect(tbMdp.Text, tbUser.Text);
            if (B != null)
            {
                string type = GestionBD.getInstance().GetTypeCompte(tbUser.Text);

                if(type == "Administateur")
                {
                    //Frame.Navigate(typeof(/*ENTRER PAGE ICI*/));
                }
                else if(type == "Chauffeur")
                {
                    //Faut trouver le moyen de rafraichir la page MainWindow en ce login comme sa les choses vont se barrer si pas connecter
                }
                else if(type== "Client")
                {
                    //Trouver comment envoyer un objet de type client du client qui est login pis garder sa dans MainWindow + creer un objet "LoggedUser" pour garder les pistes du user qui est connecter IMPORTANT
                    //DEVRAIT ETRE LA PROCHAINE ÉTAPE
                    
                    Frame.Navigate(typeof(MainAffiche));
                }
            }
            else
            {
                Logi.Text = "Échec de connexion";
            };
           
        }

        private void CreerLog_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ChoixCompte));
        }
    }
}
