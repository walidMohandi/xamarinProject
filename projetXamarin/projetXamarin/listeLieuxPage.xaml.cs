﻿using projetXamarin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace projetXamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class listeLieuxPage : ContentPage
    {
        public listeLieuxPage()
        {
            InitializeComponent();
            BindingContext = new lieuxViewModel();
        }
    }
}