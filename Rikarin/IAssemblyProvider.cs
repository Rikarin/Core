using System.Collections.Generic;
using System.Reflection;

namespace Rikarin {
    public interface IAssemblyProvider {
        IEnumerable<Assembly> GetAssemblies(string path, bool recursive);
    }
}
