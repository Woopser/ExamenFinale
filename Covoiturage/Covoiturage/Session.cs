using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covoiturage
{
    internal class Session
    {
        public int id_journee;
        public double total;
        public double profit;
        public double part_conducteur;
        public DateTime date;

        public int Id_journee { get => id_journee; set => id_journee = value; }
        public double Total { get => total; set => total = value; }
        public double Profit { get => profit; set => profit = value; }
        public double Part_conducteur { get => part_conducteur; set => part_conducteur = value; }
        public DateTime Date { get => date; set => date = value; }
    }
}
