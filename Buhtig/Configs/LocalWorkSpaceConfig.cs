using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Buhtig.Configs
{
    public class LocalWorkSpaceConfig
    {
        public string WorkingDir { get; set; }
        public string RepoPathFormat { get; set; }

        public LocalWorkSpaceConfig()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var appName = Resources.Strings.AppName;
            WorkingDir = Path.Combine(appData, appName, Resources.Strings.RepoFolder);
            RepoPathFormat = Path.Combine(WorkingDir, "{0}");
        }
    }
}