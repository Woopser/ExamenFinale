using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covoiturage
{
    internal class Facture
    {
        public int id_facture;
        public int id_trajet;
        public double montant;

        public int Id_facture { get => id_facture; set => id_facture = value; }
        public int Id_trajet { get => id_trajet; set => id_trajet = value; }
        public double Montant { get => montant; set => montant = value; }
    }
}
