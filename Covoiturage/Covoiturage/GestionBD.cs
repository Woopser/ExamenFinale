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

        public static GestionBD getInstance()
        {
            if(gestionBD == null)
                gestionBD = new GestionBD();
            return gestionBD;
        }

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
                    Id = r.GetInt32("id"),
                    Id_chauffeur = r.GetInt32("id_chauffeur"),
                    PlaceDisp = r.GetInt32("place_disp"),
                    VilleDep = r.GetString("ville_dep"),
                    VilleArr = r.GetString("ville_arr"),
                    HeureDep = r.GetString("heureDep"),
                    HeureArr = r.GetString("heureArr")
                };
                liste.Add(t);
            }
            r.Close();
            con.Close();
            return liste;
        }
    }
}
