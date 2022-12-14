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
    public sealed partial class creationAdministrateur : Page
    {

        bool valide = true;
        public creationAdministrateur()
        {
            this.InitializeComponent();
        }

        private void btAjout_Click(object sender, RoutedEventArgs e)
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



            if (valide)
            {
                GestionBD.getInstance().AjoutCompte(nomU.Text, Mdp.Text, "Administrateur");
                Frame.Navigate(typeof(login));
            }
        }
    }
}
