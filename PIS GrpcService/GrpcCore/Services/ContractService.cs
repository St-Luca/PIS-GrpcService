using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using PIS_GrpcService.DataAccess.Repositories;
using PIS_GrpcService.PIS_GrpcService;
using PIS_GrpcService.Services.Mappers;
using Empty = PIS_GrpcService.PIS_GrpcService.Empty;

namespace PIS_GrpcService.GrpcCore.Services;

public class ContractService : GrpcContractService.GrpcContractServiceBase
{
    private readonly MunicipalContractsRepository repository;
    public ContractService(MunicipalContractsRepository contractsRepository)
    {
        repository = contractsRepository;
    }

    public async override Task<ContractsArray> GetAll(Empty e, ServerCallContext context)
    {
        var contracts = repository.GetAll();

        var result = new ContractsArray();

        foreach (var contract in contracts)
        {
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