using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.Intrinsics.Arm;
using Microsoft.UI.Xaml.Controls;
using System.Runtime.Intrinsics.X86;
using MySqlX.XDevAPI;
using Org.BouncyCastle.Asn1.X509;

namespace Covoiturage
{
    internal class GestionBD
    {
        MySqlConnection con;
        static GestionBD gestionBD = null;
        Compte utilisateur = null;
        Client utilCli = null;
        Chauffeur utilChauf = null;
        ObservableCollection<string> ville = null; //Mettre les villes prise en charges

        NavigationViewItem navVueAdmin;
        NavigationViewItem histo;
        NavigationViewItem ajtraj;
        NavigationViewItem ajVille;
        NavigationViewItem ajArr;

        internal ObservableCollection<string> Ville { get => ville; set => ville = value; }
        internal Compte Utilisateur { get => utilisateur; set => utilisateur = value; }
        internal Client UtilCli { get => utilCli; set => utilCli = value; }
        internal Chauffeur UtilChauf { get => utilChauf; set => utilChauf = value; }
        public NavigationViewItem NavVueAdmin { get => navVueAdmin; set => navVueAdmin = value; }
        public NavigationViewItem Histo { get => histo; set => histo = value; }
        public NavigationViewItem Ajtraj { get => ajtraj; set => ajtraj = value; }
        public NavigationViewItem AjVille { get => ajVille; set => ajVille = value; }
        public NavigationViewItem AjArr { get => ajArr; set => ajArr = value; }



        public GestionBD()
        {
            this.con = new MySqlConnection("Server=cours.cegep3r.info;Database=a2022_420326ri_eq4;Uid=2046711;Pwd=2046711");
        }



        //Pour aller chffffercher l'instance (a utilisé avant chaque autre fonction de gestionBD)
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
                    Arret = r.GetString("arret"),
                    DateDepart = r.GetDateTime("dateDepart")
                };
                liste.Add(t);
            }
            r.Close();
            con.Close();
            return liste;
        }

        // get trajet pour ajout facture
        public int getTrajetFacture(int id_chauffeur, int place, string villeDep, string villeArr, string arret, DateTime dateDepart)
        {
            Trajet t = new Trajet();
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from trajet WHERE id_chauffeur LIKE @id_chauffeur AND place_disp LIKE @place AND ville_dep LIKE @villeDep AND ville_arr LIKE @villeArr AND arret LIKE @arret AND dateDepart LIKE @dateDepart";

            commande.Parameters.AddWithValue("@id_chauffeur", id_chauffeur);
            commande.Parameters.AddWithValue("@place", place);
            commande.Parameters.AddWithValue("@villeDep", villeDep);
            commande.Parameters.AddWithValue("@villeArr", villeArr);
            commande.Parameters.AddWithValue("@arret", arret);
            commande.Parameters.AddWithValue("@dateDepart", dateDepart);

            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            if (r.Read())
            {
                int c;
                c = r.GetInt32("id_trajet");

                r.Close();
                con.Close();
                return c;
            }
            else
                return 0;
        }

        // update facture apres ajout arret
        public void updateFacture(int id_facture, double montant)
        {
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "UPDATE facture SET montant = montant + @montant WHERE id_facture = @id_facture";

            commande.Parameters.AddWithValue("@id_facture", id_facture);
            commande.Parameters.AddWithValue("@montant", montant);

            con.Open();
            commande.Prepare();
            commande.ExecuteNonQuery();
            con.Close();
        }

        //Get session
        public ObservableCollection<Session> getSession()
        {
            ObservableCollection<Session> liste = new ObservableCollection<Session>();
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from session";

            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            while (r.Read())
            {
                Session t = new Session()
                {
                    Id_journee = r.GetInt32("id_journee"),
                    Total = r.GetDouble("total"),
                    Profit = r.GetDouble("profit"),
                    Part_conducteur = r.GetDouble("part_conducteur"),
                    Date = r.GetDateTime("date")
                };
                liste.Add(t);
            }
            r.Close();
            con.Close();
            return liste;
        }

        //Get Facture
        public ObservableCollection<Facture> getFacture()
        {
            ObservableCollection<Facture> liste = new ObservableCollection<Facture>();
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from Facture";

            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            while (r.Read())
            {
                Facture t = new Facture()
                {
                    Id_facture = r.GetInt32("id_facture"),
                    Id_trajet = r.GetInt32("id_trajet"),
                    Montant = r.GetDouble("montant"),
                    Date = r.GetDateTime("date")
                };
                liste.Add(t);
            }
            r.Close();
            con.Close();
            return liste;
        }

        // get id facture
        public int getIdfacture(int id)
        {
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select id_facture from facture where id_trajet = @id";

            commande.Parameters.AddWithValue("@id", id);

            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            if (r.Read())
            {
                int c;
                c = r.GetInt32("id_facture");

                r.Close();
                con.Close();
                return c;
            }
            else
                return 0;
        }

        //get un trajet pour une facture 
        public Trajet trajFact(int id_traj)
        {
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from trajet WHERE facture.id_trajet LIKE trajet.@id_traj";

            commande.Parameters.AddWithValue("@id_traj", id_traj);

            con.Close();
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            if (r.Read())
            {
                Trajet c = new Trajet()
                {
                    Id_trajet = r.GetInt32("id_trajet"),
                    Id_chauffeur = r.GetInt32("id_chauffeur"),
                    PlaceDisp = r.GetInt32("place_disp"),
                    VilleDep = r.GetString("ville_dep"),
                    VilleArr = r.GetString("ville_arr"),
                    Arret = r.GetString("arret"),
                    DateDepart = r.GetDateTime("dateDepart")

                };

                r.Close();
                con.Close();

                return c;
            }
            else
            {
                con.Close();

                return null;
            }
        }

        //pour aller cherher les trajet dispo 
        public ObservableCollection<Trajet> getTrajetDisp()
        {
            ObservableCollection<Trajet> liste = new ObservableCollection<Trajet>();
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from trajet WHERE place_disp > 0 AND ";

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
                    Arret = r.GetString("arret"),
                    DateDepart = r.GetDateTime("dateDepart")
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
                    Nom = r.GetString("nom"),
                    Email = r.GetString("email"),
                    Adresse = r.GetString("adresse"),
                    Numero = r.GetString("no_telephone"),
                    VilleDep = r.GetString("ville_dep"),
                };
                liste.Add(c);
            }
            r.Close();
            con.Close();
            return liste;
        }

        //Pour aller chercher la liste de chauffeur
        public ObservableCollection<Chauffeur> getChauffeur()
        {
            ObservableCollection<Chauffeur> liste = new ObservableCollection<Chauffeur>();
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from chauffeur";

            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            while (r.Read())
            {
                Chauffeur c = new Chauffeur()
                {
                    Id_chauffeur = r.GetInt32("id_chauffeur"),
                    Id_compte = r.GetInt32("id_compte"),
                    Prenom = r.GetString("prenom"),
                    Nom= r.GetString("nom"),
                    Adresse = r.GetString("adresse"),
                    Numero = r.GetString("no_telephone"),
                    email = r.GetString("email"),
                    voiture = r.GetString("voiture")
                };
                liste.Add(c);
            }
            r.Close();
            con.Close();
            return liste;
        }

        //Regarder Connexion 
        //Verifie les compte pour voir si present, si pas de compte avec les credentials, retourne null
        public Compte verifConnect(string mdp, string username)
        {
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from compte WHERE nom_utilisateur = '" +username+ "' AND password LIKE '" +mdp+ "'";

            con.Open();
            MySqlDataReader r = commande.ExecuteReader();

            if (r.Read())
            {
                utilisateur = new Compte()
                {
                    Id_compte = r.GetInt32("id_compte"),
                    Type_compte = r.GetString("type_compte"),
                    Nom_utilisateur = r.GetString("nom_utilisateur"),
                    Password = r.GetString("password")
                };

                if(utilisateur.type_compte == "Client")
                {
                    UtilCli = getClientSeul(utilisateur.id_compte);

                    con.Close();
                    navVueAdmin.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
                    histo.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
                    ajtraj.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
                    ajVille.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
                    ajArr.Visibility = Microsoft.UI.Xaml.Visibility.Visible;


                    return utilisateur;
                }
                
                if(utilisateur.type_compte == "Chauffeur")
                {
                    utilChauf = getChauffeurSeul(utilisateur.id_compte);

                    con.Close();
                    navVueAdmin.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
                    histo.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
                    ajtraj.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
                    ajVille.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
                    ajArr.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;

                    return utilisateur;
                }

                if(utilisateur.Type_compte == "Admin")
                {
                    navVueAdmin.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
                    histo.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
                    ajtraj.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
                    ajVille.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
                    ajArr.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;

                    return utilisateur;
                }
               
                r.Close();
                con.Close();

                return utilisateur;
            }
            else
            {               
                r.Close();
                con.Close();
                navVueAdmin.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
                histo.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
                ajtraj.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
                ajVille.Visibility = Microsoft.UI.Xaml.Visibility.Visible;

                return null;   
            }
        }

        //Chercher un client par son id
        public Client getClientSeul( int id)
        {
            ObservableCollection<Client> liste = new ObservableCollection<Client>();
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from client WHERE id_compte LIKE @id";

            commande.Parameters.AddWithValue("@id", id);

            con.Close();
            con.Open();

            MySqlDataReader r = commande.ExecuteReader();
            if (r.Read())
            {
                Client c = new Client()
                {
                    Id_client = r.GetInt32("id_client"),
                    Id_compte = r.GetInt32("id_compte"),
                    Prenom = r.GetString("prenom"),
                    Nom = r.GetString("nom"),
                    Adresse = r.GetString("adresse"),
                    Numero = r.GetString("no_telephone"),
                    VilleDep = r.GetString("ville_dep"),
                };
                r.Close();
                con.Close();
                return c;
            }
            else
            {
                return null;
            }
            
        }

        // get type voiture par chauffeur
        public string getVoiture(int id)
        {
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select voiture from chauffeur where id_chauffeur = @id";

            commande.Parameters.AddWithValue("@id", id);

            con.Close();
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            if (r.Read())
            {
                string voiture;
                voiture = r.GetString("voiture");

                r.Close();
                con.Close();
                return voiture;
            }
            else
                return "";
        }

        //Chercher chauffeur par son id
        public Chauffeur getChauffeurSeul(int id)
        {
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from chauffeur WHERE id_compte LIKE @id";

            commande.Parameters.AddWithValue("@id", id);

            con.Close();
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            if (r.Read())
            {
                Chauffeur c = new Chauffeur()
                {
                    id_chauffeur = r.GetInt32("id_chauffeur"),
                    Id_compte = r.GetInt32("id_compte"),
                    Prenom = r.GetString("prenom"),
                    Nom = r.GetString("nom"),
                    Email = r.GetString("email"),
                    Adresse = r.GetString("adresse"),
                    Numero = r.GetString("no_telephone"),
                    Voiture = r.GetString("voiture"),
                    
                };

                r.Close();
                con.Close();

                return c;
            }
            else
            {
                con.Close();

                return null;
            }

        }

        //Creation d'un administrateur (sa prends un nom d'utilisateur, un mot de passe, et un type de compte 
        //A METTRE DANS UN TRY/CATCH
        public void AjoutCompte(string nomU, string mdp, string typeC)
        {
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "INSERT INTO compte (type_compte,nom_utilisateur,password) VALUES (@typeC1,@nomU1,@mdp1)";

            commande.Parameters.AddWithValue("@typeC1", typeC);
            commande.Parameters.AddWithValue("@mdp1", mdp);
            commande.Parameters.AddWithValue("@nomU1", nomU);

            con.Open();
            commande.Prepare();
            commande.ExecuteNonQuery();
            con.Close();
        }

        // Ajout facture
        public void AjoutFacture(int id_trajet, double montant, DateTime date)
        {
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "INSERT INTO facture (id_trajet,montant,date) VALUES (@id_trajet,@montant,@date)";

            commande.Parameters.AddWithValue("@id_trajet", id_trajet);
            commande.Parameters.AddWithValue("@montant", montant);
            commande.Parameters.AddWithValue("@date", date);

            con.Open();
            commande.Prepare();
            commande.ExecuteNonQuery();
            con.Close();
        }

        //Ajouter un chauffeur
        public void AjoutChauf(int id_com, string pre, string nom, string adres, string tele, string email, string voiture)
        {
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "INSERT INTO chauffeur (id_compte,prenom,nom,adresse,no_telephone,email,voiture) VALUES (@id_com,@pre,@nom,@adres,@tele,@email,@voiture)";

            commande.Parameters.AddWithValue("@id_com", id_com);
            commande.Parameters.AddWithValue("@pre", pre);
            commande.Parameters.AddWithValue("@nom", nom);
            commande.Parameters.AddWithValue("@adres", adres);
            commande.Parameters.AddWithValue("@tele", tele);
            commande.Parameters.AddWithValue("@email", email);
            commande.Parameters.AddWithValue("@voiture", voiture);

            con.Open();
            commande.Prepare();
            commande.ExecuteNonQuery();
            con.Close();
        }

        //Ajout de client
        public void AjoutCli(int id_com, string pre, string nom, string adres, string tele, string email, string villeDep)
        {
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "INSERT INTO client (id_compte,prenom,nom,adresse,email,no_telephone,ville_dep, ville_arr) VALUES (@id_com,@pre,@nom,@adres,@email,@tele,@villeDep)";

            commande.Parameters.AddWithValue("@id_com", id_com);
            commande.Parameters.AddWithValue("@pre", pre);
            commande.Parameters.AddWithValue("@nom", nom);
            commande.Parameters.AddWithValue("@adres", adres);
            commande.Parameters.AddWithValue("@tele", tele);
            commande.Parameters.AddWithValue("@email", email);
            commande.Parameters.AddWithValue("@villeDep", villeDep);


            con.Open();
            commande.Prepare();
            commande.ExecuteNonQuery();
            con.Close();
        }
        //Ajout d'un arret 
        public void AjoutArret(int id_traj, int id_cli, string vil)
        {
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "INSERT INTO arret (id_trajet,id_client,ville) VALUES (@id_traj,@id_cli,@vil)";

            commande.Parameters.AddWithValue("@id_traj", id_traj);
            commande.Parameters.AddWithValue("@id_cli", id_cli);
            commande.Parameters.AddWithValue("@vil", vil);


            con.Open();
            commande.Prepare();
            commande.ExecuteNonQuery();
            con.Close();
        }

        //AJout d'un trajet 
        public void AjoutTraj( int id_chauf, int placeDisp, string villeDep, string villeArr,  string arret, DateTime dateDepart) 
        {
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "INSERT INTO trajet (id_chauffeur,place_disp,ville_dep, ville_arr, arret, dateDepart, estFini) VALUES (@id_chauf,@placeDisp,@villeDep,@villeArr,@arret,@dateDepart,0)";

            commande.Parameters.AddWithValue("@id_chauf", id_chauf);
            commande.Parameters.AddWithValue("@placeDisp", placeDisp);
            commande.Parameters.AddWithValue("@villeDep", villeDep);
            commande.Parameters.AddWithValue("@villeArr", villeArr);
            commande.Parameters.AddWithValue("@arret", arret);
            commande.Parameters.AddWithValue("@dateDepart", dateDepart);


            con.Open();
            commande.Prepare();
            commande.ExecuteNonQuery();
            con.Close();
        }

        //Ajout une ville
        public void AjoutVille(string ville)
        {
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "INSERT INTO ville (nom_ville) VALUES (@ville)";

            commande.Parameters.AddWithValue("@ville", ville);


            con.Open();
            commande.Prepare();
            commande.ExecuteNonQuery();
            con.Close();
        }

        //Get un id_compte
        public int GetIdCompte(string nomU)
        {
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "SELECT * FROM compte WHERE nom_utilisateur LIKE @nomU";

            commande.Parameters.AddWithValue("@nomU", nomU);

            con.Open();
            MySqlDataReader r = commande.ExecuteReader();

            if (r.Read())
            {
                int c;
                c = r.GetInt32("id_compte");

                r.Close();
                con.Close();
                return c;

            }
            else
            {
                return 0;
            }
        }

        //SUPPRIMER UN UTILISATEUR, PAS UTILISER MARCHE AVEC UN ID
        public void SuppCompte(int id)
        {
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "DELETE FROM compte WHERE id_compte LIKE @id";

            commande.Parameters.AddWithValue("@id", id);

            con.Open();
            commande.Prepare();
            commande.ExecuteNonQuery();
            con.Close();
        }

        //Filtre de trajet par date (date deb, date fin) pas utiliser mais 95% sur qu'elle fonctionne
        public ObservableCollection<Trajet> getFiltTrajet(DateOnly dateDeb, DateOnly dateFin)
        {
            ObservableCollection<Trajet> liste = new ObservableCollection<Trajet>();
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from trajet WHERE journee > @dateDeb AND journee < @dateFin";

            commande.Parameters.AddWithValue("@dateDeb", dateDeb);
            commande.Parameters.AddWithValue("@dateFin", dateFin);

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
                    Arret = r.GetString("arret"),
                    DateDepart = r.GetDateTime("dateDepart")
                };
                liste.Add(t);
            }
            r.Close();
            con.Close();
            return liste;
        }

        //Get Type de compte 
        public string GetTypeCompte(string nomU)
        {
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "SELECT * FROM compte WHERE nom_utilisateur LIKE @nomU";

            commande.Parameters.AddWithValue("@nomU", nomU);
            con.Close();
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();

            if (r.Read())
            {
                string c;
                c = r.GetString("type_compte");

                r.Close();
                con.Close();

                return c;
            }
            else
            {
                con.Close();

                return "Invalide";
            }
        }

        // Avoir toutes les villes disponibles
        public ObservableCollection<string> GetVilles()
        {
            ObservableCollection<string> liste = new ObservableCollection<string>();

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "SELECT * FROM ville";

            con.Open();
                MySqlDataReader r = commande.ExecuteReader();

                while (r.Read())
                {
                    string ville = r.GetString("nom_ville");
                    liste.Add(ville);
                }
            r.Close();
            con.Close();

            return liste;
        }

        //Ajout de ville 
        public void ajouVille(string vil)
        {
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "INSERT INTO arret (nom_ville) VALUES (@vil)";

            commande.Parameters.AddWithValue("@vil", vil);

            con.Open();
            commande.Prepare();
            commande.ExecuteNonQuery();
            con.Close();
        }

        // Avoir toute les heures disponibles
        public ObservableCollection<string> GetHeures()
        {
            ObservableCollection<string> liste = new ObservableCollection<string>();

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "SELECT DISTINCT heureDep FROM trajet";

            con.Open();
            MySqlDataReader r = commande.ExecuteReader();

            while (r.Read())
            {
                string c = r.GetString("heureDep") + ":00";
                liste.Add(c);
            }
            r.Close();
            con.Close();

            return liste;
        }

        // Avoir toutes les dates disponibles
        /*public ObservableCollection<string> GetDates()
        {
            ObservableCollection<string> liste = new ObservableCollection<string>();

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "SELECT * from ";

            con.Open();
            MySqlDataReader r = commande.ExecuteReader();

            while (r.Read())
            {
                string c = r.GetString("journee");
                liste.Add(c);
            }
            r.Close();
            con.Close();

            return liste;
        }*/

        // Demander tous les trajets disponibles
        public ObservableCollection<Trajet> getTrajets()
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
                    Arret = r.GetString("arret"),
                    DateDepart = r.GetDateTime("dateDepart")
                };
                liste.Add(t);
            }
            r.Close();
            con.Close();

            return liste;
        }

        //Get trajet avec un id
        public ObservableCollection<Trajet> getTrajetsId(int id)
        {
            ObservableCollection<Trajet> liste = new ObservableCollection<Trajet>();
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from trajet WHERE id_chauffeur LIKE @id";

            commande.Parameters.AddWithValue("@id", id);

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
                    Arret = r.GetString("arret"),
                    DateDepart = r.GetDateTime("dateDepart")
                };
                liste.Add(t);
            }
            r.Close();
            con.Close();

            return liste;
        }


        // Demander les trajets avec ville et date
        public ObservableCollection<Trajet> getTrajetsVD(string ville, DateTime date)
        {
            ObservableCollection<Trajet> liste = new ObservableCollection<Trajet>();
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from trajet WHERE ville_Dep = @ville AND dateDepart = @date AND estFini = 0 AND place_disp > 0";

            commande.Parameters.AddWithValue("@ville", ville);
            commande.Parameters.AddWithValue("@date", date);

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
                    Arret = r.GetString("arret"),
                    DateDepart = r.GetDateTime("dateDepart")
                };
                liste.Add(t);
            }
            r.Close();
            con.Close();

            return liste;
        }

        // Demander les trajets avec ville
        public ObservableCollection<Trajet> getTrajetsV(string ville)
        {
            ObservableCollection<Trajet> liste = new ObservableCollection<Trajet>();
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from trajet WHERE ville_Dep = @ville AND estFini = 0 AND place_disp > 0";

            commande.Parameters.AddWithValue("@ville", ville);

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
                    Arret = r.GetString("arret"),
                    DateDepart = r.GetDateTime("dateDepart")
                };
                liste.Add(t);
            }
            r.Close();
            con.Close();

            return liste;
        }

        // Demander les trajets avec date
        public ObservableCollection<Trajet> getTrajetsD(DateTime date)
        {
            ObservableCollection<Trajet> liste = new ObservableCollection<Trajet>();
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from trajet WHERE dateDepart = @date AND estFini = 0 AND place_disp > 0";

            commande.Parameters.AddWithValue("@date", date);

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
                    Arret = r.GetString("arret"),
                    DateDepart = r.GetDateTime("dateDepart")
                };
                liste.Add(t);
            }
            r.Close();
            con.Close();

            return liste;
        }

        //Fonciton de connexion pour gerer les affaires qui se collapsed ou pas
        public void onCheck()
        {
            //pas finis, peut etre inutile NE PAS SUPPRIMER
        }

        //Fonction pour trajet en cours mais pas finis
        public ObservableCollection<Trajet> getTrajEnCour()
        {
            ObservableCollection<Trajet> liste = new ObservableCollection<Trajet>();
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from trajet WHERE estFini = false";


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
                    Arret = r.GetString("arret"),
                    DateDepart = r.GetDateTime("dateDepart")
                };
                liste.Add(t);
            }
            r.Close();
            con.Close();

            return liste;
        }

        public void updatePlace(int id_traj)
        {
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "UPDATE trajet SET place_disp = place_disp - 1 WHERE id_trajet = @id_traj;";

            commande.Parameters.AddWithValue("@id_traj", id_traj);

            con.Open();
            commande.Prepare();
            commande.ExecuteNonQuery();
            con.Close();
        }

        //Code pour crypter mes mots de passe CRYPTE EN HASH, N'EST PAS D'ÉCRYPTABLE
        /*
         * private string genererSHA256(string texte)
        {
            var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(texte));

            StringBuilder sb = new StringBuilder();
            foreach (Byte b in bytes)
                sb.Append(b.ToString("x2"));

            return sb.ToString();
        }  
        

        //Fonciton 2, crypter en whatever, decryptable, a savoir dans le prochain cours
        public string crypter(string texte, string cle)
{
byte[] iv = new byte[16];
byte[] array;

using (Aes aes = Aes.Create())
{
aes.Key = Encoding.UTF8.GetBytes(cle);
aes.IV = iv;

ICryptoTransform chiffreur = aes.CreateEncryptor(aes.Key, aes.IV);

using (MemoryStream memoryStream = new MemoryStream())
{
using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, chiffreur, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(texte);
                        }

                        array = memoryStream.ToArray();
                    }
}
}

return Convert.ToBase64String(array);
}


         */
    }
}
