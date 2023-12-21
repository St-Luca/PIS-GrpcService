using PIS_GrpcService.Models;
using PIS_GrpcService.PIS_GrpcService;

namespace PIS_GrpcService.Services.Mappers;

public static class CaptureActMapper
{
    public static List<GrpcCaptureAct> Map(this List<CaptureAct> acts)
    {
        return acts.Select(x => x.Map()).ToList();
    }

    public static GrpcCaptureAct Map(this CaptureAct dbCaptureAct)
    {
        return new GrpcCaptureAct
        {
            Id = dbCaptureAct.Id,
            ActDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime((DateTime)dbCaptureAct.ActDate),
            CapturedAnimal = dbCaptureAct.CapturedAnimal.Map(),
            Amount = dbCaptureAct.Amount,
            Performer = dbCaptureAct.Performer.Map(),
            //Localities = dbCaptureAct.Localities.Map(),
            Applications = dbCaptureAct.Applications.Map()
        };
    }

    public static CaptureAct Map(this GrpcCaptureAct dbCaptureAct)
    {
        return new CaptureAct
        {
            Id = dbCaptureAct.Id,
            ActDate = dbCaptureAct.ActDate.ToDateTime(),
            CapturedAnimal = dbCaptureAct.CapturedAnimal.Map(),
            Amount = dbCaptureAct.Amount,
            Performer = dbCaptureAct.Performer.Map(),
            Locality = dbCaptureAct.Locality.Map(),
            Applications = dbCaptureAct.Applications.Map()
        };
    }
}