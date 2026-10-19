
using core.Interfaces;
using core.Models;

namespace core.Services
{
    public class NutService : INutService
    {
        private readonly List<NutDetails> nuts;
        private int nextId = 3;

        public NutService()
        {
            nuts = new List<NutDetails>
            {
                new NutDetails { Id = 1, Date = DateOnly.FromDateTime(DateTime.Today), Name = "Almonds", Weight = 100 },
                new NutDetails { Id = 2, Date = DateOnly.FromDateTime(DateTime.Today), Name = "Pistachio", Weight = 200 }
            };
        }

        public List<NutDetails> GetAll() => nuts;

        public NutDetails? Get(int id) => nuts.FirstOrDefault(nut => nut.Id == id);

        public void Add(NutDetails nut)
        {
            nut.Id = nextId++;
            nuts.Add(nut);
        }

        public void Delete(int id)
        {
            var nut = Get(id);
            if (nut is null)
                return;

            nuts.Remove(nut);
        }

        public void Update(int id, NutDetails nut)
        {
            var index = nuts.FindIndex(existingNut => existingNut.Id == id);
            if (index == -1 || nut.Id != id)
                return;

            nuts[index] = nut;
        }

        public int Count => nuts.Count;
    }
}