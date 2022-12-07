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
    public sealed partial class MainAffiche : Page
    {
        public MainAffiche()
        {
            this.InitializeComponent();
            lvTraj.ItemsSource = GestionBD.getInstance().getTrajet();
        }

        private void lvTraj_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Pour afficher les details d'un trajet
        }

        private void arret_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
