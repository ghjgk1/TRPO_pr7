using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TRPO_pr7
{
    class Patient : BaseViewModel
    {
        private string _id;
        [JsonIgnore]
        public string ID
        {
            get => _id;
            set
            {
                if (value != _id)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                if (value != _lastName)
                {
                    _lastName = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _middleName;
        public string MiddleName
        {
            get => _middleName;
            set
            {
                if (value != _middleName)
                {
                    _middleName = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _birthday;
        public string Birthday
        {
            get => _birthday;
            set
            {
                if (value != _birthday)
                {
                    _birthday = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _lastAppointment;
        public string LastAppointment
        {
            get => _lastAppointment;
            set
            {
                if (value != _lastAppointment)
                {
                    _lastAppointment = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _lastDoctor;
        public string LastDoctor
        {
            get => _lastDoctor;
            set
            {
                if (value != _lastDoctor)
                {
                    _lastDoctor = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _diagnosis;
        public string Diagnosis
        {
            get => _diagnosis;
            set
            {
                if (value != _diagnosis)
                {
                    _diagnosis = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _recomendations;
        public string Recomendations
        {
            get => _recomendations;
            set
            {
                if (value != _recomendations)
                {
                    _recomendations = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
