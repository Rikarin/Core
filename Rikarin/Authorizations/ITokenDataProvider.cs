using System.Collections.Generic;

namespace Rikarin.Authorizations {
    public interface ITokenDataProvider {
        IDictionary<string, object> GetDataPart(long accountId);
    }
}
