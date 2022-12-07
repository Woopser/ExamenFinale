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
    public sealed partial class ChoixCompte : Page
    {
        public ChoixCompte()
        {
            this.InitializeComponent();
        }

        
        private void typeU_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           string typawat = typeU.SelectedItem.ToString();
           if(typawat == "Administrateur")
            {
                Frame.Navigate(typeof(creationAdministrateur));
            }
           else if(typawat == "Chauffeur")
            {
                Frame.Navigate(typeof(creationChauffeur));
            }
           else if(typawat == "Client")
            {
                Frame.Navigate (typeof(creationClient));
            }
        }
    }
}
