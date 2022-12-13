// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

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
    public sealed partial class AjoutTraj : Page
    {
        string villeDep;
        string villeArr;
        string arret;
        DateTime dateDepart;
        int place;

        Chauffeur c = GestionBD.getInstance().UtilChauf;

        
        public AjoutTraj()
        {
            this.InitializeComponent();
            VilleDep.ItemsSource = GestionBD.getInstance().GetVilles();
            VilleArr.ItemsSource = GestionBD.getInstance().GetVilles();
            VilleArret.ItemsSource = GestionBD.getInstance().GetVilles();
        }

        private void VilleDep_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            villeDep = VilleDep.SelectedItem.ToString();
        }

        private void VilleArr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            villeArr = VilleArr.SelectedItem.ToString();
        }

        private void VilleArret_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            arret = VilleArret.SelectedItem.ToString();
        }


        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {

            if(c.Voiture == "VUS")
            {
                place = 5;
            }
            else if(c.Voiture == "Berline")
            {
                place = 3;
            }


            GestionBD.getInstance().AjoutTraj(c.Id_chauffeur, place, villeDep, villeArr, arret, dateDepart);
            Frame.Navigate(typeof(MainAffiche));
        }

        private void calendar_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            dateDepart = calendar.Date.Value.Date;
        }
    }
}
