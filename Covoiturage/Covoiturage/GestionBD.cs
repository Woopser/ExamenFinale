﻿using MySql.Data.MySqlClient;
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
                    Nom = r.GetString("nom"),
                    Email = r.GetString("email"),
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
                Compte c = new Compte()
                {
                    Id_compte = r.GetInt32("id_compte"),
                    Type_compte = r.GetString("type_compte"),
                    Nom_utilisateur = r.GetString("nom_utilisateur"),
                    Password = r.GetString("password")
                };
                
                r.Close();
                con.Close();
                return c;
                
             }
            else
            {
                
                r.Close();
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
        //Ajouter un Chauffeur + Ajouter un clients a mettre ici

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
                    HeureDep = r.GetInt32("heureDep"),
                    HeureArr = r.GetInt32("heureArr")
                };
                liste.Add(t);
            }
            r.Close();
            con.Close();
            return liste;
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
