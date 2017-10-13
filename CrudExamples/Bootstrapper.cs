using CrudExamples.ViewModels;
using CrudExamples.Views;
using Microsoft.Practices.Unity;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CrudExamples
{
    internal class Bootstrapper: UnityBootstrapper
    {
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            this.Container.RegisterType<Shell>();
            this.Container.RegisterTypeForNavigation<VesselsListView>();
        }

        protected override DependencyObject CreateShell()
        {
            return this.Container.Resolve<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            ((Shell)this.Shell).Show();
        }
    }
}
