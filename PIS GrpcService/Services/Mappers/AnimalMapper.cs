using PIS_GrpcService.Models;
using PIS_GrpcService.PIS_GrpcService;

namespace PIS_GrpcService.Services.Mappers;

public static class AnimalMapper
{
    public static List<GrpcAnimal> Map(this List<Animal> animals)
    {
        return animals.Select(x => x.Map()).ToList();
    }

    public static GrpcAnimal Map(this Animal dbAnimal)
    {
        return new GrpcAnimal
        {
            Id = dbAnimal.Id,
            Category = dbAnimal.Category,
            Sex = dbAnimal.Sex,
            Breed = dbAnimal.Breed,
            Size = dbAnimal.Size,
            Coat = dbAnimal.Coat,
            Color = dbAnimal.Color,
            Ears = dbAnimal.Ears,
            CapturedAct = dbAnimal.CapturedAct,
            Mark = dbAnimal.Mark,
            IdentChip = dbAnimal.IdentChip

        };
    }

    public static Animal Map(this GrpcAnimal dbAnimal)
    {
        return new Animal
        {
            Id = dbAnimal.Id,
            Category = dbAnimal.Category,
            Sex = dbAnimal.Sex,
            Breed = dbAnimal.Breed,
            Size = dbAnimal.Size,
            Coat = dbAnimal.Coat,
            Color = dbAnimal.Color,
            Ears = dbAnimal.Ears,
            CapturedAct = dbAnimal.CapturedAct,
            Mark = dbAnimal.Mark,
            IdentChip = dbAnimal.IdentChip
        };
    }
}
