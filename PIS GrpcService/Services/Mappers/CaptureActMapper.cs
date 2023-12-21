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
            Amount = dbCaptureAct.Amount,
            Performer = dbCaptureAct.Performer.Map(),
            Locality = dbCaptureAct.Locality.Map(),
            Applications = dbCaptureAct.Applications.Map(),
            Animals = new AnimalArray()//dbCaptureAct.Animals.Map()
        };
    }

    public static CaptureAct Map(this GrpcCaptureAct dbCaptureAct)
    {
        return new CaptureAct
        {
            Id = dbCaptureAct.Id,
            ActDate = dbCaptureAct.ActDate.ToDateTime(),
            Amount = dbCaptureAct.Amount,
            Performer = dbCaptureAct.Performer.Map(),
            Locality = dbCaptureAct.Locality.Map(),
            Applications = dbCaptureAct.Applications.Map(),
            Animals = new List<Animal>()//dbCaptureAct.Animals.Map()
        };
    }
}