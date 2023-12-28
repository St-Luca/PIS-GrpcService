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
        var array = new AnimalArray();
        if (dbCaptureAct.Animals != null)
        {
            array = dbCaptureAct.Animals.MapToGrpc();
        }

        return new GrpcCaptureAct
        {
            Id = dbCaptureAct.Id,
            ActDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime((DateTime)dbCaptureAct.Date),
            Amount = dbCaptureAct.Amount,
            Performer = dbCaptureAct.Performer.MapToGrpc(),
            Locality = dbCaptureAct.Locality.MapToGrpc(),
            Applications = dbCaptureAct.Applications.MapToGrpc(),
            Animals = array
        };
    }

    public static CaptureAct MapFromGrpc(this GrpcCaptureAct dbCaptureAct)
    {
        return new CaptureAct
        {
            Id = dbCaptureAct.Id,
            Date = dbCaptureAct.ActDate.ToDateTime(),
            Amount = dbCaptureAct.Amount,
            Performer = dbCaptureAct.Performer.MapFromGrpc(),
            Locality = dbCaptureAct.Locality.MapFromGrpc(),
            Applications = dbCaptureAct.Applications.MapFromGrpc(dbCaptureAct.Performer.MapFromGrpc(), dbCaptureAct.Locality.MapFromGrpc()),
            Animals = dbCaptureAct.Animals.MapFromGrpc()
        };
    }
}