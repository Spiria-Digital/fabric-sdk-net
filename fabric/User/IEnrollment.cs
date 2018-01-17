using System.Security.Cryptography;

namespace fabricsdk.fabric.User 
{
    /**
        The contract between the Certificate Authority provider and the SDK
     */
    public interface IEnrollment
    {
        KeyedHashAlgorithm Key {get;} //TODO : Right type?

        string Certificate {get;}
    }
}