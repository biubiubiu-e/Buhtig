using System;
using System.IO;
using Buhtig.Configs;
using Buhtig.Models.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Buhtig.Tests
{
    [TestClass]
    public class GitTest
    {
        [TestMethod]
        public void CloneTest()
        {
            RuntimeConfigs.LoadDefaults();
            if (!Directory.Exists(RuntimeConfigs.LocalWorkSpaceConfig.WorkingDir))
            {
                Directory.CreateDirectory(RuntimeConfigs.LocalWorkSpaceConfig.WorkingDir);
            }
            var xieaoran = new Student(1140310201, "Robert Xie", 18846441857L, "Robert Xie", "admin@xieaoran.com");
            var team = new Team("TestTeam", new[] {xieaoran}, new Uri("https://github.com/xieaoran/library"));
        }
    }
}
