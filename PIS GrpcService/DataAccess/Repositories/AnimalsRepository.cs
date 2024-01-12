using Microsoft.EntityFrameworkCore;
using PIS_GrpcService.Models;

namespace PIS_GrpcService.DataAccess.Repositories;

public class AnimalsRepository
{
    private ApplicationContext context;

    public AnimalsRepository(ApplicationContext dbContext)
    {
        context = dbContext;
    }

    public void Add(Animal animal)
    {
        context.Animals.Add(animal);
        context.SaveChanges();
    }

    public void Delete(int id)
    {
        context.Animals.Remove(context.Animals.First(o => o.Id == id));
        context.SaveChanges();
    }

    public Animal Get(int id)
    {
        return context.Animals.Include(l => l.Act).Single(o => o.Id == id);
    }

    public void Edit(Animal organization)
    {
        context.Animals.Update(organization);
        context.SaveChanges();
    }

    public List<Animal> GetAll()
    {
        return context.Animals.Include(l => l.Act).ToList();
    }
}
