using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buhtig.Interfaces
{
    public interface IPostProcessingRequired
    {
        void PostProcess(Dictionary<string, object> requiredInfos);
    }
}
