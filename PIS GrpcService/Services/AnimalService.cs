using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using PIS_GrpcService.DataAccess;
using PIS_GrpcService.DataAccess.Repositories;
using PIS_GrpcService.Models;
using PIS_GrpcService.PIS_GrpcService;
using PIS_GrpcService.PIS_GrpcService.Services;
using PIS_GrpcService.Services.Mappers;

namespace PIS_GrpcService.Services;

public class AnimalService : GrpcAnimalService.GrpcAnimalServiceBase
{
    private readonly AnimalsRepository repository;
    private readonly ILogger<AnimalService> _logger;

    public AnimalService(ILogger<AnimalService> logger, AnimalsRepository animalsRepository)
    {
        _logger = logger;
        repository = animalsRepository;
    }

    public override Task<AnimalArray> GetAll(Empty e, ServerCallContext context)
    {
        var response = repository.GetAll().Select(o => o.MapToGrpc()).ToList();

        var result = new AnimalArray();
        result.List.AddRange(response);

        return Task.FromResult(result);
    }

    public override Task<GrpcAnimal?> Get(IdRequest id, ServerCallContext context)
    {
        var response = repository.Get(id.Id)?.MapToGrpc();

        return Task.FromResult(response);
    }

    public override Task<Empty> Edit(GrpcAnimal animal, ServerCallContext context)
    {
        try
        {
            repository.Edit(animal.MapFromGrpc());
        }
        catch (DbUpdateConcurrencyException)
        {
            //_logger.Log();
        }

        return Task.FromResult(new Empty());
    }

    public async override Task<Empty> Delete(IdRequest id, ServerCallContext context)
    {
        try
        {
            var organization = repository.Get(id.Id);

            if (organization != null)
            {
                repository.Delete(id.Id);
            }

        }
        catch (DbUpdateConcurrencyException)
        {
            //_logger.Log();
        }

        return new Empty();
    }

    public async override Task<Empty> Add(GrpcAnimal animal, ServerCallContext context)
    {
        var entityAnimal = animal?.MapFromGrpc();

        if (entityAnimal != null)
        {
            repository.Add(entityAnimal);
        }

        return new Empty();
    }
}
