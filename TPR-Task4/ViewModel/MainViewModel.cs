using SL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModel.Model;

namespace ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private IDataRepository _dataRepository;
        private ObservableCollection<LocationView> _locations;
        private LocationView _location;

        public event PropertyChangedEventHandler PropertyChanged;

        private ICommand _onSelectedLocationChanged;
        private ICommand _modifyLocation;
        private ICommand _addLocation;
        private ICommand _deleteLocation;

        private short _id;
        private string _name;
        private decimal _costRate;
        private decimal _availability;
        private DateTime _modifiedData;

        public ObservableCollection<LocationView> Locations { get => _locations; set => _locations = value; }
        public LocationView Location { 
            get => _location;
            set
            {
                _location = value;
                RaisePropertyChanged("Location");
                OnSelectedLocationChanged.Execute(null);
            } }
        public IDataRepository DataRepository { get => _dataRepository; set => _dataRepository = value; }
        public short Id 
        { 
            get => _id; 
            set
            {
                _id = value;
                RaisePropertyChanged("Id");
            }
        }
        public string Name 
        { 
            get => _name;
            set 
            {
                _name = value;
                RaisePropertyChanged("Name");
            }
        }
        public decimal CostRate 
        { 
            get => _costRate;
            set 
            {
                _costRate = value;
                RaisePropertyChanged("CostRate");
            } 
        }
        public decimal Availability
        { 
            get => _availability;
            set
            {
                _availability = value;
                RaisePropertyChanged("Availability");
            } 
        }
        public DateTime ModifiedData 
        { 
            get => _modifiedData;
            set
            {
                _modifiedData = value;
                RaisePropertyChanged("ModifiedData");
            } 
        }

        public ICommand OnSelectedLocationChanged { get => _onSelectedLocationChanged;}
        public ICommand ModifyLocation1 { get => _modifyLocation;}
        public ICommand AddLocation { get => _addLocation;}
        public ICommand DeleteLocation { get => _deleteLocation;}

        public MainViewModel()
        {
            DataRepository = new DataRepository();
            DataRepository.OnRepositoryChange += OnLocationsChanged;
            _location = new LocationView();
            _locations = new ObservableCollection<LocationView>();
            _onSelectedLocationChanged = new Command(OnLocationChanged);
            _addLocation = new Command(AddLocationMethod);
            _deleteLocation = new Command(DeleteLocationMethod);
            _modifyLocation = new Command(ModifyLocation);
            Init();
        }
        private void Init()
        {
            ConvertToLocationViewList();
        }

        private void ConvertToLocationViewList()
        {
            List<short> ids = DataRepository.GetLocationsIds();
            foreach (short id in ids)
            {
                _locations.Add(ConvertToLocationView(id));
            }
        }

        private LocationView ConvertToLocationView(short id)
        {
            LocationView result = new LocationView();
            result.Id = id;
            result.Name = DataRepository.GetLocationName(id);
            result.CostRate = DataRepository.GetLocationCostRate(id);
            result.Availability = DataRepository.GetLocationAvaibility(id);
            result.ModifiedDate = DataRepository.GetLocationModifiedDate(id);
            return result;
        }

        private void SelectLocation()
        {
            Id = Location.Id;
            if(Location.Name != null)
            {
                Name = Location.Name;
            }
            else
            {
                Name = null;
            }
            CostRate = Location.CostRate;
            Availability = Location.Availability;
            if (!Location.ModifiedDate.Equals(DateTime.MinValue))
            {
                ModifiedData = Location.ModifiedDate;
            }
            else
            {
                ModifiedData = DateTime.MinValue;
            }
        }

        private void AddLocationMethod()
        {
            GetLocationFromTextBoxes();
            if (Location.Id > 0 && !DataRepository.GetLocationsIds().Contains(Location.Id))
            {
                DataRepository.AddLocation(Location.Id, Location.Name, Location.CostRate, Location.Availability, Location.ModifiedDate);
            }
            
        }

        private void DeleteLocationMethod()
        {
            short id = Location.Id;
            if (id > 0 && DataRepository.GetLocationsIds().Contains(id))
            {
                DataRepository.DeleteLocation(id);
            }
        }

        private void ModifyLocation()
        {
            GetLocationFromTextBoxes();
            DataRepository.UpdateLocation(Location.Id, Location.Name, Location.CostRate, Location.Availability, Location.ModifiedDate);
        }
        private void GetLocationFromTextBoxes() 
        {
            Location.Id = Id;
            Location.Name = Name;
            Location.CostRate = CostRate;
            Location.Availability = Availability;
            Location.ModifiedDate = ModifiedData;
        }

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnLocationChanged()
        {
            if (Location != null)
                SelectLocation();
        }

        public void OnLocationsChanged()
        {
            ConvertToLocationViewList();
        }

    }
}
