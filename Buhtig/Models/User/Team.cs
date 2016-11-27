using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Buhtig.Annotations;
using Buhtig.Models.Git;
using Newtonsoft.Json;
using SharpDX.Collections;

namespace Buhtig.Models.User
{
    [Serializable, JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Team : INotifyPropertyChanged
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

        private ObservableCollection<Student> _members;

        [JsonProperty(PropertyName = "members")]
        public ObservableCollection<Student> Members
        {
            get { return _members; }
            set
            {
                if (_members == value) return;
                _members = value;
                OnPropertyChanged();
            }
        }

        private GitRepo _repo;

        [JsonProperty(PropertyName = "repo")]
        public GitRepo Repo
        {
            get { return _repo; }
            set
            {
                if (_repo == value) return;
                _repo = value;
                OnPropertyChanged();
            }
        }

        public Team(string name, IEnumerable<Student> members, Uri remoteUri)
        {
            Id = Guid.NewGuid();
            Name = name;
            Members = new ObservableCollection<Student>();
            foreach (var member in members)
            {
                Members.Add(member);
            }
            Repo = new GitRepo(this, remoteUri);
        }

        public Team()
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
