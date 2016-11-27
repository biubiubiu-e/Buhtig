using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Buhtig.Annotations;
using Buhtig.Models.User;
using LibGit2Sharp;
using Newtonsoft.Json;
using Buhtig.Interfaces;

namespace Buhtig.Models.Git
{
    [Serializable, JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class GitCommit : INotifyPropertyChanged, IPostProcessingRequired
    {

        private string _sha;

        [JsonProperty(PropertyName = "sha")]
        public string Sha
        {
            get { return _sha; }
            set
            {
                if (_sha == value) return;
                _sha = value;
                OnPropertyChanged();
            }
        }

        private DateTimeOffset _time;

        [JsonProperty(PropertyName = "time")]
        public DateTimeOffset Time
        {
            get { return _time; }
            set
            {
                if (_time == value) return;
                _time = value;
                OnPropertyChanged();
            }
        }


        private Guid _authorId;

        [JsonProperty(PropertyName = "author_id")]
        public Guid AuthorId
        {
            get { return _authorId; }
            set
            {
                if (_authorId == value) return;
                _authorId = value;
                OnPropertyChanged();
            }
        }

        private Student _author;

        [JsonIgnore]
        public Student Author
        {
            get { return _author; }
            set
            {
                if (_author == value) return;
                _author = value;
                OnPropertyChanged();
            }
        }

        private string _messageShort;

        [JsonProperty(PropertyName = "message_short")]
        public string MessageShort
        {
            get { return _messageShort; }
            set
            {
                if (_messageShort == value) return;
                _messageShort = value;
                OnPropertyChanged();
            }
        }

        private string _message;

        [JsonProperty(PropertyName = "message")]
        public string Message
        {
            get { return _message; }
            set
            {
                if (_message == value) return;
                _message = value;
                OnPropertyChanged();
            }
        }


        private List<GitChange> _changes;

        [JsonProperty(PropertyName = "changes")]
        public List<GitChange> Changes
        {
            get { return _changes; }
            set
            {
                if (_changes == value) return;
                _changes = value;
                OnPropertyChanged();
            }
        }

        public GitCommit(IRepository innerRepo, IEnumerable<Student> members,
            Commit commit, Commit previousCommit = null)
        {
            Sha = commit.Sha;
            Time = commit.Author.When;
            Author =
                members.FirstOrDefault(
                    student => student.GitName == commit.Author.Name && student.GitEmail == commit.Author.Email);
            AuthorId = Author?.Id ?? Guid.Empty;
            MessageShort = commit.MessageShort;
            Message = commit.Message;
            if (previousCommit == null) return;
            Changes = new List<GitChange>();
            foreach (var patchEntryChanges in innerRepo.Diff.Compare<Patch>(previousCommit.Tree, commit.Tree))
            {
                Changes.Add(new GitChange(patchEntryChanges));
            }
        }

        public GitCommit()
        {
            // Reserved for Serialization
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void PostProcess(Dictionary<string, object> requiredInfos)
        {
            var members = (IEnumerable<Student>)requiredInfos["members"];
            Author = members.FirstOrDefault(member => member.Id == AuthorId);
        }
    }
}
