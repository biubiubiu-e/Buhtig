using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Buhtig.Annotations;
using Newtonsoft.Json;

namespace Buhtig.Models.User
{
    [Serializable, JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Student : INotifyPropertyChanged
    {

        private Guid _id;

        [JsonProperty(PropertyName = "id")]
        public Guid Id
        {
            get { return _id; }
            set
            {
                if (_id == value) return;
                _id = value;
                OnPropertyChanged();
            }
        }

        private int _studentNumber;

        [JsonProperty(PropertyName = "student_number")]
        public int StudentNumber
        {
            get { return _studentNumber; }
            set
            {
                if (_studentNumber == value) return;
                _studentNumber = value;
                OnPropertyChanged();
            }
        }

        private string _name;

        [JsonProperty(PropertyName = "name")]
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        private long _mobile;

        [JsonProperty(PropertyName = "mobile")]
        public long Mobile
        {
            get { return _mobile; }
            set
            {
                if (_mobile == value) return;
                _mobile = value;
                OnPropertyChanged();
            }
        }

        private string _gitName;

        [JsonProperty(PropertyName = "git_name")]
        public string GitName
        {
            get { return _gitName; }
            set
            {
                if (_gitName == value) return;
                _gitName = value;
                OnPropertyChanged();
            }
        }
        private string _gitEmail;

        [JsonProperty(PropertyName = "git_email")]
        public string GitEmail
        {
            get { return _gitEmail; }
            set
            {
                if (_gitEmail == value) return;
                _gitEmail = value;
                OnPropertyChanged();
            }
        }

        public Student(int studentNumber, string name, long mobile, 
            string gitName, string gitEmail)
        {
            Id = Guid.NewGuid();
            StudentNumber = studentNumber;
            Name = name;
            Mobile = mobile;
            GitName = gitName;
            GitEmail = gitEmail;
        }

        public Student()
        {
            // Reserved for Serialization
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
