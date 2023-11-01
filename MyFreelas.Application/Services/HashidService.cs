using HashidsNet;
using MyFreelas.Application.Exceptions.BaseException;
using MyFreelas.Application.Interfaces;

namespace MyFreelas.Application.Services;

public class HashidService : IHashidService
{
    private readonly IHashids _hashids;
    public HashidService(IHashids hashids)
    {
        _hashids = hashids; 
    }
    public int Decode(string id)
    {
        return _hashids.DecodeSingle(id); 
    }

    public string Encode(int id)
    {
        return _hashids.Encode(id);
    }

    public void IsHash(string id)
    {
        var isHash = _hashids.TryDecodeSingle(id, out _);
        if(!isHash)
        {
            throw new InvalidIDException("invalid id");
        }
    }
}
