using System;

namespace Rikarin.Authorizations {
    public interface ITokenBuilder {
        string GenerateToken(long accountId, TimeSpan lifetime);
    }
}
