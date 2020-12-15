using Microsoft.Extensions.DependencyInjection;

namespace Rikarin {
    public interface IMvcBuilderConfiguration {
        void Configure(IMvcBuilder builder);
    }
}
