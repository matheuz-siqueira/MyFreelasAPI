namespace MyFreelas.Application.Interfaces;

public interface IHashidService
{
    string Encode(int id);
    int Decode(string id);
    void IsHash(string id);
}
