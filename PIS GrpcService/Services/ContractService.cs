using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using PIS_GrpcService.DataAccess;
using PIS_GrpcService.DataAccess.Repositories;
using PIS_GrpcService.Services.Mappers;

namespace PIS_GrpcService.PIS_GrpcService.Services;

public class ContractService : GrpcContractService.GrpcContractServiceBase
{
    private readonly MunicipalContractsRepository repository;
    private readonly LocalityCostsRepository localityCostsRepository;
    private readonly OrganizationsRepository organizationsRepository;
    private readonly ILogger<ContractService> _logger;
    public ContractService(ILogger<ContractService> logger, MunicipalContractsRepository contractsRepository, LocalityCostsRepository localityCosts, OrganizationsRepository organizations)
    {
        _logger = logger;
        repository = contractsRepository;
        localityCostsRepository = localityCosts;
        organizationsRepository = organizations;
    }

    public async override Task<ContractsArray> GetAll(Empty e, ServerCallContext context)
    {
        var contracts = repository.GetAll();
            //.Include(c => c.LocalityCosts) // Загрузка связанных LocalityCosts для каждого контракта
            //.ToListAsync();

        //var organizationIds = contracts.Select(app => app.IdOrganization).ToList();
        /*var organizations = organizationsRepository.GetAll()
            .Where(org => organizationIds.Contains(org.Id))
            .ToList();*/

        var result = new ContractsArray();

        foreach (var contract in contracts)
        {
            //contract.Performer = organizations.First(d => d.Id == contract.IdOrganization);
            //contract.LocalityCosts = localityCostsRepository.GetAll().Where(lc => lc.IdContract == contract.Id).ToList();

            result.List.Add(contract.MapToGrpc());
        }

        return result;
    }

    public override Task<GrpcContract?> Get(IdRequest request, ServerCallContext context)
    {
        var response = repository.Get(request.Id)?.MapToGrpc();

        return Task.FromResult(response);
    }

    public override Task<Empty> Edit(GrpcContract contract, ServerCallContext context)
    {
        try
        {
            repository.Edit(contract.MapFromGrpc());
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
            var contract = repository.Get(id.Id);

            if (contract != null)
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

    public async override Task<Empty> Add(GrpcContract contract, ServerCallContext context)
    {
        var entityContract = contract?.MapFromGrpc();

        if (entityContract != null)
        {
            repository.Add(entityContract);
        }

        return new Empty();
    }
}