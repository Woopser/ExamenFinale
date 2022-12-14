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
using WinRT;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Covoiturage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DemanderArret : Page
    {
        string ville;
        int heure;
        DateTime date;
        bool valide = true;

        Client c = GestionBD.getInstance().UtilCli;
        public DemanderArret()
        {
            this.InitializeComponent();

            cbVille.ItemsSource = GestionBD.getInstance().GetVilles();
            
        }

        private void cbVille_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ville = cbVille.SelectedItem.ToString();
        }

        

        private void btnConfirmer_Click(object sender, RoutedEventArgs e)
        {
            spTrajets.Visibility = Visibility.Visible;

            if (cbVille.SelectedIndex == -1)
                lvTrajets.ItemsSource = GestionBD.getInstance().getTrajetsD(date);

            else if (date == DateTime.MinValue)
                lvTrajets.ItemsSource = GestionBD.getInstance().getTrajetsV(ville);

            else
            { 
                lvTrajets.ItemsSource = GestionBD.getInstance().getTrajetsVD(ville, date);
            }
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            if (ville == null)
            {
                error.Text = "Vous devez choisir une ville";
                valide = false;
            }

            if (valide)
            {
                Trajet t = lvTrajets.SelectedItem.As<Trajet>();

                GestionBD.getInstance().updatePlace(t.Id_trajet);
                GestionBD.getInstance().AjoutArret(t.Id_trajet, c.Id_client, ville);

                int id = GestionBD.getInstance().getIdfacture(t.Id_trajet);

                double montant = 0;

                if (GestionBD.getInstance().getVoiture(t.Id_chauffeur) == "VUS")
                {
                    montant = 15.00;
                }

                if (GestionBD.getInstance().getVoiture(t.Id_chauffeur) == "Berline")
                {
                    montant = 10.00;
                }


                GestionBD.getInstance().updateFacture(id, montant);

                Frame.Navigate(typeof(MainAffiche));
            }
        }

        private void calendar_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            date = calendar.Date.Value.Date;
        }
    }
}
