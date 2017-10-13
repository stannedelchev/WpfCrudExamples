using CrudExamples.Helpers;
using CrudExamples.Views;
using MahApps.Metro.Controls;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CrudExamples
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Shell : MetroWindow
    {
        private readonly IRegionManager regionManager;

        public Shell(IRegionManager regionManager)
        {
            this.regionManager = regionManager;

            InitializeComponent();

            this.Loaded += OnShellLoaded;
        }

        private void OnShellLoaded(object sender, RoutedEventArgs e)
        {
            this.regionManager.RequestNavigate(Constants.VesselsListRegionName, nameof(VesselsListView));
        }
    }
}
