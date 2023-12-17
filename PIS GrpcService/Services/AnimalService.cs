using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using PIS_GrpcService.DataAccess;
using PIS_GrpcService.Models;
using PIS_GrpcService.PIS_GrpcService;
using PIS_GrpcService.PIS_GrpcService.Services;
using PIS_GrpcService.Services.Mappers;

namespace PIS_GrpcService.Services;

public class AnimalService : GrpcAnimalService.GrpcAnimalServiceBase
{
    private readonly ApplicationContext _dbContext;
    private readonly ILogger<AnimalService> _logger;

    public AnimalService(ILogger<AnimalService> logger, ApplicationContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public override Task<AnimalArray> GetAll(Empty e, ServerCallContext context)
    {
        var response = _dbContext.Animals.Select(o => o.Map()).ToList();

        var result = new AnimalArray();
        result.List.AddRange(response);

        return Task.FromResult(result);
    }

    public override Task<GrpcAnimal?> Get(IdRequest id, ServerCallContext context)
    {
        var response = _dbContext.Animals.FirstOrDefault(o => o.Id == id.Id)?.Map();

        return Task.FromResult(response);
    }

    public override Task<Empty> Edit(GrpcAnimal animal, ServerCallContext context)
    {
        try
        {
            _dbContext.Update(animal.Map());
            _dbContext.SaveChangesAsync();
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
            var animal = await _dbContext.Animals.FindAsync(id.Id);

            if (animal != null)
            {
                _dbContext.Animals.Remove(animal);
                await _dbContext.SaveChangesAsync();
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
        var entityAnimal = animal?.Map();
        _dbContext.Animals.Add(entityAnimal);
        await _dbContext.SaveChangesAsync();

        return new Empty();
    }
}
