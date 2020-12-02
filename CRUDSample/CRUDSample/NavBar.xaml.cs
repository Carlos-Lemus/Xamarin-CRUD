using BottomBar.XamarinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRUDSample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavBar : BottomBarPage
    {
        public NavBar()
        {
            InitializeComponent();
        }
    }
}