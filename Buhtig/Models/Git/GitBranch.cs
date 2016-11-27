using Buhtig.Annotations;
using Buhtig.Interfaces;
using LibGit2Sharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Buhtig.Models.Git
{
    [Serializable, JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class GitBranch : INotifyPropertyChanged
    {
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

        private ObservableCollection<GitCommit> _commits;

        [JsonProperty(PropertyName = "commits")]
        public ObservableCollection<GitCommit> Commits
        {
            get { return _commits; }
            set
            {
                if (_commits == value) return;
                _commits = value;
                OnPropertyChanged();
            }
        }

        public GitBranch(Branch branch, IEnumerable<GitCommit> commits)
        {
            Name = branch.CanonicalName;
            Commits = new ObservableCollection<GitCommit>();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
