using FirstFloor.ModernUI.Presentation;
using log4net;
using RealEstate.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RealEstate
{
    public class PageViewModel : NotifyPropertyChanged, INavigationViewModel
    {
        ILog log = LogManager.GetLogger(typeof(PageViewModel));

        public PageViewModel()
        {
            OpenedEditors = new Dictionary<string, EditorViewModel>();
            NavigatingFromCommand = new RelayCommand(NavigatingFrom);
            NavigatedFromCommand = new RelayCommand(NavigatedFrom);
            NavigatedToCommand = new RelayCommand(NavigatedTo);
            FragmentNavigationCommand = new RelayCommand(FragmentNavigation);
            LoadedCommand = new RelayCommand(LoadData);
            RemoveEditorCommand = new RelayCommand(RemoveEditorExecute);
            //RealEstateRepository.Instance.Pages.Add(this.PageType, this);
            //RealEstateRepository.Instance.PagesByUri.Add( this.PageUriString, this);
        }



        public Dictionary<string, EditorViewModel> OpenedEditors { get; set; }


        public virtual void InitLinks()
        { }

        public PageType PageType;
        public string PageUriString;

        private LinkCollection links;
        public LinkCollection Links
        {
            get { return links; }
            set
            {
                links = value;
                OnPropertyChanged("Links");
            }
        }

        private Uri selectedSource;
        public Uri SelectedSource
        {
            get { return selectedSource; }
            set
            {
                selectedSource = value;
                try
                {
                    if (SelectedSource is ModernUri && OpenedEditors.ContainsKey((selectedSource as ModernUri).EditorKey))
                        CurrentEditor = OpenedEditors[(selectedSource as ModernUri).EditorKey];
                }
                catch (Exception ex)
                {
                    log.HandleError(ex);
                }
                OnPropertyChanged("SelectedSource");
            }
        }

        private EditorViewModel currentEditor;
        public EditorViewModel CurrentEditor
        {
            get { return currentEditor; }
            set
            {
                currentEditor = value;
                //   RealEstateRepository.Instance.CurrentPage = currentPage;
                //      OnPropertyChanged("CurrentPage");
            }
        }

        /// <summary>
        /// Gets or sets the loaded command.
        /// </summary>
        /// <value>The loaded command.</value>
        public ICommand RemoveEditorCommand { get; set; }
        private void RemoveEditorExecute(object parameter)
        {
            try
            {
                EditorViewModel editorViewModel = (parameter as ModernLink)?.ViewModel as EditorViewModel;
                RemoveEditor(editorViewModel);
            }
            catch (Exception ex)
            {
                log.HandleError(ex);
            }
        }

        public void RemoveEditor(EditorViewModel editorViewModel, bool arrangeHistory = true)
        {
            if (editorViewModel != null)
            {
                RealEstateRepository.Instance.MainViewModel.RemoveFromHistory(this.PageType, editorViewModel.EditorKey, arrangeHistory);

                editorViewModel.Dispose();
                if (this.CurrentEditor == editorViewModel)
                {
                    //IList<HistoryItem> history = RealEstateRepository.Instance.MainViewModel.History;
                    int currentIndex = Links.IndexOf(editorViewModel.Link);

                    if (currentIndex > 0)
                    {
                        ModernLink prevLink = Links[currentIndex - 1] as ModernLink;
                        // CurrentEditor = prevLink.ViewModel as EditorViewModel;
                        SelectedSource = prevLink.Source as ModernUri;
                    }
                }

                if (Links.Contains(editorViewModel.Link))
                    Links.Remove(editorViewModel.Link);

                if (OpenedEditors.ContainsKey(editorViewModel.EditorKey))
                    OpenedEditors.Remove(editorViewModel.EditorKey);
            }
        }

        /// <summary>
        /// Gets or sets the navigating from command.
        /// </summary>
        /// <value>The navigating from command.</value>
        public ICommand NavigatingFromCommand { get; set; }

        /// <summary>
        /// Gets or sets the navigated from command.
        /// </summary>
        /// <value>The navigated from command.</value>
        public ICommand NavigatedFromCommand { get; set; }

        /// <summary>
        /// Gets or sets the navigated to command.
        /// </summary>
        /// <value>The navigated to command.</value>
        public ICommand NavigatedToCommand { get; set; }

        /// <summary>
        /// Gets or sets the fragment navigation command.
        /// </summary>
        /// <value>The fragment navigation command.</value>
        public ICommand FragmentNavigationCommand { get; set; }

        /// <summary>
        /// Gets or sets the loaded command.
        /// </summary>
        /// <value>The loaded command.</value>
        public ICommand LoadedCommand { get; set; }

        /// <summary>
        /// Loads the data.
        /// </summary>
        private void LoadData(object parameter)
        {
        }

        /// <summary>
        /// Navigateds from.
        /// </summary>
        private void NavigatedFrom(object parameter)
        {
            // called when we navigated to another view
        }

        /// <summary>
        /// Navigateds to.
        /// </summary>
        private void NavigatedTo(object parameter)
        {
            RealEstateRepository.Instance.CurrentPage = this;
        }

        public void RefreshEditors()
        {
            foreach (var editor in OpenedEditors.Values.ToList())
            {
                System.Data.Entity.EntityState entityState = new GeneralBL().GetEntityState(editor.Entity);

                if (editor.Entity != null && entityState == System.Data.Entity.EntityState.Detached)
                {
                    RemoveEditor(editor);
                }
            }

            OnPropertyChanged(null);
        }

        /// <summary>
        /// Fragments the navigation.
        /// </summary>
        private void FragmentNavigation(object parameter)
        {
        }

        /// <summary>
        /// Navigatings from.
        /// </summary>
        private void NavigatingFrom(object parameter)
        {
            // Called when we will navigate to new view
        }

    }


}
