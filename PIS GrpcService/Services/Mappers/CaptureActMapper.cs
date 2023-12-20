using PIS_GrpcService.Models;
using PIS_GrpcService.PIS_GrpcService;
using System.Diagnostics.Acts;

namespace PIS_GrpcService.Services.Mappers;

public static class CaptureActMapper
{
    public static List<GrpcContract> Map(this List<CaptureAct> acts)
    {
        return acts.Select(x => x.Map()).ToList();
    }

    public static GrpcCaptureAct Map(this CaptureAct dbCaptureAct)
    {
        return new GrpcCaptureAct
        {
            Id = dbCaptureAct.Id,
            ActDate = dbCaptureAct.ActDate,
            CapturedAnimal = dbCaptureAct.CapturedAnimal,
            Amount = dbCaptureAct.Amount,
            Performer = dbCaptureAct.Performer,
            Customer = dbCaptureAct.Customer,
            Localities = dbCaptureAct.Localities
        };
    }

    public static CaptureAct Map(this GrpcCaptureAct dbCaptureAct)
    {
        return new CaptureAct
        {
            Id = dbCaptureAct.Id,
            ActDate = dbCaptureAct.ActDate,
            CapturedAnimal = dbCaptureAct.CapturedAnimal,
            Amount = dbCaptureAct.Amount,
            Performer = dbCaptureAct.Performer,
            Customer = dbCaptureAct.Customer,
            Localities = dbCaptureAct.Localities
        };
    }
}