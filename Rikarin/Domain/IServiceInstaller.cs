using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Rikarin.Domain {
    public interface IServiceInstaller {
        void Install(IServiceCollection services, IConfiguration configuration);
    }
}