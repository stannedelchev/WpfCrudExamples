using MahApps.Metro.Controls;
using Prism.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CrudExamples.InteractionActions
{
    public class PopupMetroWindowAction: PopupWindowAction
    {
        protected override Window CreateWindow()
        {
            base.CreateWindow();
            return new MetroWindow();
        }
    }
}
