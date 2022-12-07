using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covoiturage
{
    internal class GestionBD
    {
        MySqlConnection con;
        static GestionBD gestionBD = null;

        public GestionBD()
        {
            this.con = new MySqlConnection("Server=cours.cegep3r.info;Database=a2022_420326ri_eq4;Uid=2046711;Pwd=2046711");
        }

        //Pour aller chercher l'instance (a utilisé avant chaque autre fonction de gestionBD)
        public static GestionBD getInstance()
        {
            if(gestionBD == null)
                gestionBD = new GestionBD();
            return gestionBD;
        }

        //Pour aller chercher la liste des trajets
        public ObservableCollection<Trajet> getTrajet()
        {
            ObservableCollection<Trajet> liste = new ObservableCollection<Trajet>();
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from trajet";

            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            while (r.Read())
            {
                Trajet t = new Trajet()
                {
                    Id_trajet = r.GetInt32("id_trajet"),
                    Id_chauffeur = r.GetInt32("id_chauffeur"),
                    PlaceDisp = r.GetInt32("place_disp"),
                    VilleDep = r.GetString("ville_dep"),
                    VilleArr = r.GetString("ville_arr"),
                    HeureDep = r.GetInt32("heureDep"),
                    HeureArr = r.GetInt32("heureArr")
                };
                liste.Add(t);
            }
            r.Close();
            con.Close();
            return liste;
        }

        //Pour aller chercher la liste des clients
        public ObservableCollection<Client> getClients()
        {
            ObservableCollection<Client> liste = new ObservableCollection<Client>();
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from client";

            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            while (r.Read())
            {
                Client c = new Client()
                {
                    Id_client = r.GetInt32("id_client"),
                    Id_compte = r.GetInt32("id_compte"),
                    Prenom = r.GetString("prenom"),
                    Adresse = r.GetString("adresse"),
                    Numero = r.GetString("no_telephone"),
                    VilleDep = r.GetString("ville_dep"),
                    VilleArr = r.GetString("ville_arr")
                };
                liste.Add(c);
            }
            r.Close();
            con.Close();
            return liste;
        }

        // Rechercher les comptes pour le login
        public ObservableCollection<Comptes> getComptes()
        {
            ObservableCollection<Comptes> liste = new ObservableCollection<Comptes>();
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from compte";

            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            while (r.Read())
            {
                Comptes c = new Comptes()
                {
                    Id_compte = r.GetInt32("id_compte"),
                    Type_compte = r.GetString("type_compte"),
                    Nom_utilisateur = r.GetString("nom_utilisateur"),
                    Password = r.GetString("password")
                };
                liste.Add(c);
            }
            r.Close();
            con.Close();
            return liste;
        }

        //Autre fonction 
    }
}
