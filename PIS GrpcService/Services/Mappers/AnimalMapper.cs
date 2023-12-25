using PIS_GrpcService.Models;
using PIS_GrpcService.PIS_GrpcService;

namespace PIS_GrpcService.Services.Mappers;

public static class AnimalMapper
{
    public static List<GrpcAnimal> MapToGrpc(this List<Animal> animals)
    {
        return animals.Select(x => x.MapToGrpc()).ToList();
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
            IdCaptureAct = dbAnimal.IdCapturedAct,
            Mark = dbAnimal.Mark,
            IdentChip = dbAnimal.IdentChip
        };
    }
}
