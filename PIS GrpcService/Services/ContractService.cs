using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using PIS_GrpcService.DataAccess;
using PIS_GrpcService.Services.Mappers;

namespace PIS_GrpcService.PIS_GrpcService.Services;

public class ContractService : GrpcContractService.GrpcContractServiceBase
{
    private readonly ApplicationContext _dbContext;
    private readonly ILogger<ContractService> _logger;
    public ContractService(ILogger<ContractService> logger, ApplicationContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async override Task<ContractsArray> GetAll(Empty e, ServerCallContext context)
    {
        var contracts = await _dbContext.Contracts.ToListAsync();
        var localityCosts = _dbContext.LocalityCosts
        .Where(lc => contracts.Any(c => c.LocalityCosts.Any(l => l.IdLocality == lc.IdLocality)))
        .ToList();
        var organizationIds = contracts.Select(app => app.IdOrganization).ToList();

        var organizations = await _dbContext.Organizations.Where(org => organizationIds.Contains(org.Id)).ToListAsync();

        var result = new ContractsArray();

        foreach (var contract in contracts)
        {
            contract.LocalityCosts = localityCosts.Where(lc => lc.IdContract == contract.Id).ToList();
            contract.Performer = organizations.FirstOrDefault(d => d.Id == contract.IdOrganization);
            result.List.Add(contract.Map());
        }

        return result;
    }

    public override Task<GrpcContract?> Get(IdRequest request, ServerCallContext context)
    {
        var response = _dbContext.Contracts.FirstOrDefault(o => o.Id == request.Id)?.Map();

        return Task.FromResult(response);
    }

    public override Task<Empty> Edit(GrpcContract contract, ServerCallContext context)
    {
        try
        {
            _dbContext.Update(contract.Map());
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
            var contract = await _dbContext.Contracts.FindAsync(id.Id);

            if (contract != null)
            {
                _dbContext.Contracts.Remove(contract);
                await _dbContext.SaveChangesAsync();
            }

        }
        catch (DbUpdateConcurrencyException)
        {
            //_logger.Log();
        }

        return new Empty();
    }

    public async override Task<Empty> Add(GrpcContract contract, ServerCallContext context)
    {
        var entityContract = contract?.Map();
        _dbContext.Contracts.Add(entityContract);
        await _dbContext.SaveChangesAsync();

        return new Empty();
    }
}