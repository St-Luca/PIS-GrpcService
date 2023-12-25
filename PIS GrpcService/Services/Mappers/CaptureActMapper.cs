using PIS_GrpcService.Models;
using PIS_GrpcService.PIS_GrpcService;

namespace PIS_GrpcService.Services.Mappers;

public static class CaptureActMapper
{
    public static List<GrpcCaptureAct> MapToGrpc(this List<CaptureAct> acts)
    {
        return acts.Select(x => x.MapToGrpc()).ToList();
    }

    public static GrpcCaptureAct MapToGrpc(this CaptureAct dbCaptureAct)
    {
        return new GrpcCaptureAct
        {
            Id = dbCaptureAct.Id,
            ActDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime((DateTime)dbCaptureAct.ActDate),
            Amount = dbCaptureAct.Amount,
            Performer = dbCaptureAct.Performer.MapToGrpc(),
            Locality = dbCaptureAct.Locality.MapToGrpc(),
            Applications = dbCaptureAct.Applications.MapToGrpc(),
            Animals = new AnimalArray()//dbCaptureAct.Animals.Map()
        };
    }

    public static CaptureAct MapFromGrpc(this GrpcCaptureAct dbCaptureAct)
    {
        return new CaptureAct
        {
            Id = dbCaptureAct.Id,
            ActDate = dbCaptureAct.ActDate.ToDateTime(),
            Amount = dbCaptureAct.Amount,
            Performer = dbCaptureAct.Performer.MapFromGrpc(),
            Locality = dbCaptureAct.Locality.MapFromGrpc(),
            Applications = dbCaptureAct.Applications.MapFromGrpc(),
            Animals = new List<Animal>()//dbCaptureAct.Animals.Map()
        };
    }
}