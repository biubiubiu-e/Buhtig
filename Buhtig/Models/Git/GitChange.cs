using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Buhtig.Annotations;
using LibGit2Sharp;
using Newtonsoft.Json;

namespace Buhtig.Models.Git
{
    [Serializable, JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class GitChange : INotifyPropertyChanged
    {

        private GitChangeSummary _summary;

        [JsonProperty(PropertyName = "summary")]
        public GitChangeSummary Summary
        {
            get { return _summary; }
            set
            {
                if (_summary == value) return;
                _summary = value;
                OnPropertyChanged();
            }
        }

        private GitChangeBlob _oldBlob;

        [JsonProperty(PropertyName = "old_blob")]
        public GitChangeBlob OldBlob
        {
            get { return _oldBlob; }
            set
            {
                if (_oldBlob == value) return;
                _oldBlob = value;
                OnPropertyChanged();
            }
        }

        private GitChangeBlob _newBlob;

        [JsonProperty(PropertyName = "new_blob")]
        public GitChangeBlob NewBlob
        {
            get { return _newBlob; }
            set
            {
                if (_newBlob == value) return;
                _newBlob = value;
                OnPropertyChanged();
            }
        }

        public GitChange(PatchEntryChanges patchEntryChanges)
        {
            Summary = new GitChangeSummary(patchEntryChanges.Status, patchEntryChanges.LinesAdded,
                patchEntryChanges.LinesDeleted);
            OldBlob = new GitChangeBlob(patchEntryChanges.OldOid, patchEntryChanges.OldPath, patchEntryChanges.OldMode);
            NewBlob = new GitChangeBlob(patchEntryChanges.Oid, patchEntryChanges.Path, patchEntryChanges.Mode);
        }

        public GitChange()
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
