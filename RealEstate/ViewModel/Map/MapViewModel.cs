using FirstFloor.ModernUI.Presentation;
using RealEstate.BL;
using RealEstate.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RealEstate
{
    public class MapViewModel : EditorViewModel
    {
        #region Ctor

        public MapViewModel()
        {
        }

        #endregion Ctor

        #region Properties

        private string address;
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }

        private int zoom;
        public int Zoom
        {
            get { return zoom; }
            set
            {
                zoom = value;
                OnPropertyChanged("Zoom");
            }
        }


        private System.Windows.Visibility mapVisibility = System.Windows.Visibility.Hidden;
        public System.Windows.Visibility MapVisibility
        {
            get { return mapVisibility; }
            set
            {
                mapVisibility = value;
                OnPropertyChanged("MapVisibility");
            }
        }

        private bool isMapVisible = false;
        public bool IsMapVisible
        {
            get { return isMapVisible; }
            set
            {
                isMapVisible = value;
                OnPropertyChanged("IsMapVisible");
            }
        }


        #endregion Properties

        #region Commands

        #endregion Commands

        #region Methods

        public override void Init()
        {

            //  EntityTitle = Debt.Amount.ToString();


            OnPropertyChanged(null);

            base.Init();
        }

        public override void RefreshEntityTitle()
        {
            string senderName = string.Empty;

            base.RefreshEntityTitle();
        }

        #endregion Methods
    }
}
