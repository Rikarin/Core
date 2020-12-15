using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.Extensions.Logging;

namespace Rikarin {
    public class DefaultAssemblyProvider : IAssemblyProvider {
        protected ILogger log;

        public Func<Assembly, bool> IsCandidateAssembly { get; } = x => !x.FullName.StartsWith("Microsoft", StringComparison.OrdinalIgnoreCase);

        public DefaultAssemblyProvider(ILogger logger) {
            log = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IEnumerable<Assembly> GetAssemblies(string path, bool recursive) {
            var assemblies = new List<Assembly>();

            assemblies.AddRange(GetAssembliesFromPath(path, recursive));
            return assemblies.Distinct().ToList();
        }

        IEnumerable<Assembly> GetAssembliesFromPath(string path, bool recursive) {
            var ret = new List<Assembly>();

            if (!string.IsNullOrEmpty(path) && Directory.Exists(path)) {
                log.LogInformation("Discovering and loading assemblies from path '{0}'", path);

                foreach (var extensionPath in Directory.EnumerateFiles(path, "*.dll")) {
                    try {
                        var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(extensionPath);
                        if (IsCandidateAssembly(assembly)) {
                            ret.Add(assembly);
                            log.LogInformation("Assembly '{0}' version {1} is discovered and loaded",
                                               assembly.GetName().Name,
                                               assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version);
                        }
                    } catch (Exception e) {
                        log.LogWarning("Error loading assembly '{0}'", extensionPath);
                        log.LogWarning(e.ToString());
                    }
                }
            } else {
                log.LogWarning(
                    string.IsNullOrEmpty(path)
                        ? "Discovering and loading assemblies from path skipped: path not provided"
                        : "Discovering and loading assemblies from path '{0}' skipped: path not found", path);
            }

            if (recursive) {
                foreach (var subpath in Directory.GetDirectories(path)) {
                    ret.AddRange(GetAssembliesFromPath(subpath, true));
                }
            }

            return ret;
        }
    }
}
