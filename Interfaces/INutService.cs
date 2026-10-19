using core.Models;

namespace core.Interfaces
{
    public interface INutService
    {
        List<NutDetails> GetAll();

        NutDetails? Get(int id);

        void Add(NutDetails nut);

        void Delete(int id);

        void Update(int id, NutDetails nut);

        int Count { get; }
    }
}