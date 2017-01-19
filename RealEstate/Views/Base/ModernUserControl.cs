// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModernUserControl.cs" company="saramgsilva">
//   Copyright (c) 2014 saramgsilva. All rights reserved.
// </copyright>
// <summary>
//   Class ModernUserControl.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Diagnostics;
using System.Windows.Controls;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using System;
using RealEstate.BL;
using log4net;

namespace RealEstate
{
    /// <summary>
    /// Class ModernUserControl.
    /// </summary>
    public class ModernUserControl : UserControl, IContent
    {
        ILog log = LogManager.GetLogger(typeof(ModernUserControl));
        private bool isEditor = true;

        public bool IsEditor
        {
            get { return isEditor; }
            set { isEditor = value; }
        }

        private bool hasOwnDataContext = false;

        public bool HasOwnDataContext
        {
            get { return hasOwnDataContext; }
            set { hasOwnDataContext = value; }
        }

        public string EditorKey { get; set; }


        /// <summary>
        /// Handles the <see cref="E:FragmentNavigation"/> event.
        /// </summary>
        /// <param name="e">The <see cref="FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs"/> instance containing the event data.</param>
        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
            try
            {
                EditorKey = e.Fragment;
                string[] navigationParams = EditorKey.Split('$');
                EditorViewModel viewModel = null;

                if (IsEditor && (!HasOwnDataContext))
                {
                    HasOwnDataContext = true;

                    PageType pageType = (PageType)Enum.Parse(typeof(PageType), navigationParams[0], true);
                    string mainEditorKey = navigationParams[1];

                    PageViewModel pageViewModel = RealEstateRepository.Instance.Pages[pageType];
                    viewModel = pageViewModel.OpenedEditors[mainEditorKey];
                    //viewModel.PageViewModel = pageViewModel;

                    if (navigationParams.Length > 2)
                    {
                        string subEditorKey = navigationParams[2];
                        viewModel = viewModel?.OpenedEditors[subEditorKey];
                    }
                    DataContext = viewModel;
                    viewModel.Disposed -= ViewModel_Disposed;
                    viewModel.Disposed += ViewModel_Disposed;
                }

                if (navigationParams.Length <= 2)
                    RealEstateRepository.Instance.MainViewModel.AddToHistory(DataContext as EditorViewModel);

                if (FragmentNavigation != null)
                {
                    FragmentNavigation(this, e);
                }
            }
            catch (Exception ex)
            {
                log.HandleError(ex);
            }
        }

        private void ViewModel_Disposed(object sender, EventArgs e)
        {
            EditorViewModel editorViewModel = sender as EditorViewModel;

            if (editorViewModel != null)
            {
                this.DataContext = null;
                HasOwnDataContext = false;
            }
        }

        /// <summary>
        /// Handles the <see cref="E:NavigatedFrom"/> event.
        /// </summary>
        /// <param name="e">The <see cref="FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs"/> instance containing the event data.</param>
        public void OnNavigatedFrom(NavigationEventArgs e)
        {
            try
            {
                Debug.WriteLine("ModernUserControl - OnNavigatedFrom");
                if (NavigatedFrom != null)
                {
                    NavigatedFrom(this, e);
                    Debug.WriteLine("ModernUserControl - OnNavigatedFrom event called");
                }

                if (this.DataContext is MapViewModel)
                {
                    ((MapViewModel)this.DataContext).IsMapVisible = false;
                    ((MapViewModel)this.DataContext).MapVisibility = System.Windows.Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                log.HandleError(ex);
            }
        }

        /// <summary>
        /// Handles the <see cref="E:NavigatedTo"/> event.
        /// </summary>
        /// <param name="e">The <see cref="FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs"/> instance containing the event data.</param>
        public void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                PageViewModel pageViewModel = this.DataContext as PageViewModel;
                if (pageViewModel != null)
                {
                    RealEstateRepository.Instance.MainViewModel.AddToHistory(pageViewModel);
                    pageViewModel.RefreshEditors();
                }
                else
                {
                    if (e.Source.OriginalString.Split('$').Length == 2)
                        RealEstateRepository.Instance.MainViewModel.AddToHistory(this.DataContext as EditorViewModel);
                }

                if (NavigatedTo != null)
                {
                    NavigatedTo(this, e);
                }
            }
            catch (Exception ex)
            {
                log.HandleError(ex);
            }
        }

        /// <summary>
        /// Handles the <see cref="E:NavigatingFrom"/> event.
        /// </summary>
        /// <param name="e">The <see cref="FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs"/> instance containing the event data.</param>
        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            try
            {
                NavigatingFrom?.Invoke(this, e);

                EditorViewModel editorViewModel = this.DataContext as EditorViewModel;
                if (editorViewModel != null && !editorViewModel.IsDisposed && !(editorViewModel is TableViewModel))
                {
                    BeforeSaveResult beforeSaveResult = editorViewModel.Save();

                    if (beforeSaveResult != null && !beforeSaveResult.IsValidData)
                    {
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                log.HandleError(ex);
            }
        }

        /// <summary>
        /// Occurs when [navigating from].
        /// </summary>
        public event NavigatingCancelHandler NavigatingFrom;

        /// <summary>
        /// Occurs when [navigated from].
        /// </summary>
        public event NavigationEventHandler NavigatedFrom;

        /// <summary>
        /// Occurs when [navigated to].
        /// </summary>
        public event NavigationEventHandler NavigatedTo;

        /// <summary>
        /// Occurs when [fragment navigation].
        /// </summary>
        public event FragmentNavigationHandler FragmentNavigation;
    }

    /// <summary>
    /// Delegate NavigatingCancelHandler.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs"/> instance containing the event data.</param>
    public delegate void NavigatingCancelHandler(object sender, NavigatingCancelEventArgs e);

    /// <summary>
    /// Delegate NavigationEventHandler.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs"/> instance containing the event data.</param>
    public delegate void NavigationEventHandler(object sender, NavigationEventArgs e);

    /// <summary>
    /// Delegate FragmentNavigationHandler.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs"/> instance containing the event data.</param>
    public delegate void FragmentNavigationHandler(object sender, FragmentNavigationEventArgs e);
}
