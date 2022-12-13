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

        private void cbHeure_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cbHeure.SelectedItem.ToString())
            {
                case "12:00":
                    heure = 12;
                    break;
                case "13:00":
                    heure = 13;
                    break;
                case "14:00":
                    heure = 14;
                    break;
                case "15:00":
                    heure = 15;
                    break;
                case "16:00":
                    heure = 16;
                    break;
                default:
                    break;
            }
        }

        private void cbVille_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ville = cbVille.SelectedItem.ToString();
        }

        

        private void btnConfirmer_Click(object sender, RoutedEventArgs e)
        {
            spTrajets.Visibility = Visibility.Visible;

            if (cbVille.SelectedIndex == -1 && cbHeure.SelectedIndex == -1 && date == DateTime.MinValue)
                lvTrajets.ItemsSource = GestionBD.getInstance().getTrajets();

            else if (cbVille.SelectedIndex == -1 && cbHeure.SelectedIndex == -1)
                lvTrajets.ItemsSource = GestionBD.getInstance().getTrajetsD(date);

            else if (cbVille.SelectedIndex == -1 && date == DateTime.MinValue)
                lvTrajets.ItemsSource = GestionBD.getInstance().getTrajetsH(heure);

            else if (date == DateTime.MinValue && cbHeure.SelectedIndex == -1)
                lvTrajets.ItemsSource = GestionBD.getInstance().getTrajetsV(ville);

            else if (cbVille.SelectedIndex == -1)
                lvTrajets.ItemsSource = GestionBD.getInstance().getTrajetsHD(heure, date);

            else if (cbHeure.SelectedIndex == -1)
                lvTrajets.ItemsSource = GestionBD.getInstance().getTrajetsVD(ville, date);

            else if (date == DateTime.MinValue)
                lvTrajets.ItemsSource = GestionBD.getInstance().getTrajetsVH(ville, heure);

            else
            {
                
                lvTrajets.ItemsSource = GestionBD.getInstance().getTrajetsVHD(ville, heure, date);
            }
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            if(ville == null)
            {
                error.Text = "Vous devez choisir une ville";
                valide = false;
            }

            if (valide)
            {
                Trajet t = lvTrajets.SelectedItem.As<Trajet>();
                GestionBD.getInstance().updatePlace(t.Id_trajet);//A changer comme l'autre d'en dessous, pour aller chercher l'id du trajet
                GestionBD.getInstance().AjoutArret(t.Id_trajet, c.Id_client, ville);
                Frame.Navigate(typeof(MainAffiche));
            }
        }

        private void calendar_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            date = calendar.Date.Value.Date;
        }
    }
}
