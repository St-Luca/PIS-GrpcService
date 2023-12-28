using PIS_GrpcService.Models;
using PIS_GrpcService.PIS_GrpcService;
using System.Diagnostics.Contracts;

namespace PIS_GrpcService.Services.Mappers;

public static class AnimalMapper
{
    public static AnimalArray MapToGrpc(this List<Animal> localities)
    {
        var locs = localities.Select(x => x.MapToGrpc()).ToList();
        var res = new AnimalArray();
        res.List.AddRange(locs);
        return res;
    }

    public static List<Animal> MapFromGrpc(this AnimalArray animalArray)
    {
        var animals = animalArray.List.ToList();

        return animals.Select(x => x.MapFromGrpc()).ToList();
    }

    public static GrpcAnimal MapToGrpc(this Animal dbAnimal)
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
            Tail = dbAnimal.Tail,
            IdCapturedAct = dbAnimal.IdCaptureAct,
            Mark = dbAnimal.Mark,
            IdentChip = dbAnimal.IdentChip

        };
    }

    public static Animal MapFromGrpc(this GrpcAnimal dbAnimal)
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
            Tail = dbAnimal.Tail,
            IdCaptureAct = dbAnimal.IdCapturedAct,
            Mark = dbAnimal.Mark,
            IdentChip = dbAnimal.IdentChip
        };
    }
}
