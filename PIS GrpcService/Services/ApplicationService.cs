﻿using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using PIS_GrpcService.DataAccess;
using PIS_GrpcService.PIS_GrpcService;
using PIS_GrpcService.PIS_GrpcService.Services;
using PIS_GrpcService.Services.Mappers;

namespace PIS_GrpcService.Services;

public class ApplicationService : GrpcApplicationService.GrpcApplicationServiceBase
{
    private readonly ApplicationContext _dbContext;
    private readonly ILogger<ApplicationService> _logger;

    public ApplicationService(ILogger<ApplicationService> logger, ApplicationContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public override Task<ApplicationArray> GetAll(Empty e, ServerCallContext context)
    {
        var response = _dbContext.Applications.Select(o => o.Map()).ToList();

        var result = new ApplicationArray();
        result.List.AddRange(response);

        return Task.FromResult(result);
    }

    public override Task<GrpcApplication?> Get(IdRequest id, ServerCallContext context)
    {
        var response = _dbContext.Applications.FirstOrDefault(o => o.Id == id.Id)?.Map();

        return Task.FromResult(response);
    }

    public override Task<Empty> Edit(GrpcApplication application, ServerCallContext context)
    {
        try
        {
            _dbContext.Update(application.Map());
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
            var application = await _dbContext.Applications.FindAsync(id.Id);

            if (application != null)
            {
                _dbContext.Applications.Remove(application);
                await _dbContext.SaveChangesAsync();
            }

        }
        catch (DbUpdateConcurrencyException)
        {
            //_logger.Log();
        }

        return new Empty();
    }

    public async override Task<Empty> Add(GrpcApplication application, ServerCallContext context)
    {
        var entityApplication = application?.Map();
        _dbContext.Applications.Add(entityApplication);
        await _dbContext.SaveChangesAsync();

        return new Empty();
    }
}