using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>

    public sealed partial class MainWindow : Window
    {

        MySqlConnection con = new MySqlConnection("Server=cours.cegep3r.info;Database=a2022_420326ri_eq4;Uid=2046711;Pwd=2046711");
        ObservableCollection<Trajet> liste = new ObservableCollection<Trajet>();
        ObservableCollection<string> ville = new ObservableCollection<string>(); //Ajouter une liste des villes prise en charge



        public MainWindow()
        {
            this.InitializeComponent();
            GestionBD.getInstance().NavVueAdmin = navVueAdmin;
            GestionBD.getInstance().Histo = histo;
            GestionBD.getInstance().Ajtraj = ajTraj;
            GestionBD.getInstance().AjVille = ajVille;
            GestionBD.getInstance().AjArr = ajArr;

            mainFrame.Navigate(typeof(MainAffiche));

            Compte c = GestionBD.getInstance().Utilisateur;
            /*if (c != null)
            {
                switch (c.Type_compte)
                {
                    case "Client":
                        Arret.Visibility = Visibility.Visible;
                        ComTraj.Visibility = Visibility.Collapsed;
                        break;
                    case "Chauffeur":
                        Arret.Visibility = Visibility.Collapsed;
                        ComTraj.Visibility = Visibility.Visible;
                        break;
                    case "Administrateur":
                        Arret.Visibility = Visibility.Visible;
                        ComTraj.Visibility = Visibility.Visible;
                        break;
                    default:
                        Arret.Visibility = Visibility.Collapsed;
                        ComTraj.Visibility = Visibility.Collapsed;
                        break;
                }
            }
            else if(c == null)
            {
                Arret.Visibility = Visibility.Collapsed;
                ComTraj.Visibility = Visibility.Collapsed;
            }*/
        }

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var item = (NavigationViewItem)args.SelectedItem;

            switch (item.Content)
            {
                case "Trajet":
                    mainFrame.Navigate(typeof(MainAffiche));
                    break;
                case "Demander un arrêt":
                    mainFrame.Navigate(typeof(DemanderArret));
                    break;
                case "Login":
                    mainFrame.Navigate(typeof(login));
                    break;
                case "Ville":
                    mainFrame.Navigate(typeof(AjoutArret));
                    break;
                case "Vue Administrateur":
                    mainFrame.Navigate(typeof(AdminAffiche));
                    break;
                case "Commencer un trajet":
                    mainFrame.Navigate(typeof(AjoutTraj));
                    break;
                case "Historique des trajets":
                    mainFrame.Navigate(typeof(Historique));
                    break;
                case "Se déconnecter":
                    GestionBD.getInstance().Utilisateur = null;
                    GestionBD.getInstance().UtilCli = null;
                    GestionBD.getInstance().UtilChauf = null;
                    mainFrame.Navigate(typeof(login));
                    navVueAdmin.Visibility = Visibility.Collapsed;
                    histo.Visibility = Visibility.Collapsed;
                    ajTraj.Visibility = Visibility.Collapsed;
                    ajVille.Visibility = Visibility.Collapsed;
                    ajArr.Visibility = Visibility.Collapsed;
                    break;
                       
                default:
                    break;
            }
            //Pour Ajouter une proceduire stockés
            //MySqlCommand commande = new MySqlCommand("p_ajout_employe");
            //commande.Connection = con;
            //commande.CommandType = System.Data.CommandType.StoredProcedure;
            //commande.Parameters.AddWithValue("matricule", "15854");
            //commande.Parameters.AddWithValue("nom", "Lavoie");
            //commande.Parameters.AddWithValue("prenom", "Nicolas");
            //commande.Parameters.AddWithValue("departement", "Finances");
            //con.Open();
            //commande.Prepare();
            //int i = commande.ExecuteNonQuery();

            //con.Close(); A mettre dans le singleton 

        }

    }
}
