using System;

namespace Rikarin.Authorizations {
    public interface ITokenProvider {
        string PublicKey { get; }
        string CreateToken<T>(T data, TimeSpan lifetime);
        bool ValidateToken(string token);
        T GetData<T>(string token);
    }
}
