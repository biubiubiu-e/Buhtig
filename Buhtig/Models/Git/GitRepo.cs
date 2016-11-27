using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Buhtig.Annotations;
using Buhtig.Configs;
using Buhtig.Models.User;
using LibGit2Sharp;
using Newtonsoft.Json;
using Buhtig.Interfaces;
using System.Collections.Generic;

namespace Buhtig.Models.Git
{
    [Serializable, JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class GitRepo : INotifyPropertyChanged, IPostProcessingRequired
    {

        private Uri _remoteUri;
        [JsonProperty(PropertyName = "remote_uri")]
        public Uri RemoteUri
        {
            get { return _remoteUri; }
            set
            {
                if (_remoteUri == value) return;
                _remoteUri = value;
                OnPropertyChanged();
            }
        }
        private string _localPath;

        [JsonProperty(PropertyName = "local_path")]
        public string LocalPath
        {
            get { return _localPath; }
            set
            {
                if (_localPath == value) return;
                _localPath = value;
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

        private ObservableCollection<GitBranch> _branches;

        [JsonProperty(PropertyName = "branches")]
        public ObservableCollection<GitBranch> Branches
        {
            get { return _branches; }
            set
            {
                if (_branches == value) return;
                _branches = value;
                OnPropertyChanged();
            }
        }

        private IRepository _innerRepo;

        [JsonIgnore]
        public IRepository InnerRepo
        {
            get { return _innerRepo; }
            set
            {
                if (_innerRepo == value) return;
                _innerRepo = value;
                OnPropertyChanged();
            }
        }

        private Team _belongingTeam;

        [JsonIgnore]
        public Team BelongingTeam
        {
         get { return _belongingTeam; }
            set
            {
                if (_belongingTeam == value) return;
                _belongingTeam = value;
                OnPropertyChanged();
            }
        }

        public GitRepo(Team team, Uri remoteUri)
        {
            BelongingTeam = team;
            RemoteUri = remoteUri;
            var workingDir = string.Format(RuntimeConfigs.LocalWorkSpaceConfig.RepoPathFormat, team.Id);
            LocalPath = Repository.Clone(RemoteUri.AbsoluteUri, workingDir);
            InnerRepo = new Repository(LocalPath);
            Analyze();
        }

        private void Analyze()
        {
            Commits = new ObservableCollection<GitCommit>();
            foreach(var innerCommit in InnerRepo.Commits)
            {
            }
            foreach (var branch in InnerRepo.Branches)
            {
                
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void PostProcess(Dictionary<string, object> requiredInfos)
        {
            var team = (Team)requiredInfos["team"];
            BelongingTeam = team;
            InnerRepo = new Repository(LocalPath);
            Analyze();
        }
    }
}
