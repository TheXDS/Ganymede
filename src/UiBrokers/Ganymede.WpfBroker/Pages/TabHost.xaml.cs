﻿using System.Windows.Controls;
using TheXDS.Ganymede.WpfBroker.Widgets;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Pages
{
    /// <summary>
    /// Lógica de interacción para TabHost.xaml
    /// </summary>
    public partial class TabHost : TabItem
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="TabHost"/>.
        /// </summary>
        /// <param name="viewModel">
        /// <see cref="PageViewModel"/> que controlará la lógica interactiva de
        /// la página.
        /// </param>
        /// <param name="content">Página visual que contiene los controles y
        /// widgets por medio de los cuales el usuario intectuará con la
        /// aplicación.
        /// </param>
        public TabHost(PageViewModel viewModel, Page content)
        {
            InitializeComponent();
            DataContext = viewModel;
            Content = new UiPageHost()
            {
                Page = content,
            };
            content.DataContext = DataContext;
        }
    }
}
