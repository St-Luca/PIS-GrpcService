﻿using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using PIS_GrpcService.DataAccess;
using PIS_GrpcService.DataAccess.Repositories;
using PIS_GrpcService.Services.Mappers;

namespace PIS_GrpcService.PIS_GrpcService.Services;

public class LocalityService : GrpcLocalityService.GrpcLocalityServiceBase
{
    private readonly LocalitiesRepository repository;
    private readonly ILogger<LocalityService> _logger;
    public LocalityService(ILogger<LocalityService> logger, LocalitiesRepository localities)
    {
        _logger = logger;
        repository = localities;
    }

    public override Task<LocalityArray> GetAll(Empty e, ServerCallContext context)
    {
        var response = repository.GetAll().Select(o => o.MapToGrpc()).ToList();

        var result = new LocalityArray();
        result.List.AddRange(response);

        return Task.FromResult(result);
    }

    public override Task<GrpcLocality?> Get(IdRequest request, ServerCallContext context)
    {
        var response = repository.Get(request.Id)?.MapToGrpc();

        return Task.FromResult(response);
    }
}
