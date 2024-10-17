using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using StockGrpcProto;

namespace StockGrpc.Services;

public class StockExchangeService : StockExchange.StockExchangeBase
{
    private static readonly Random Random = new();
    
    public override Task<StockValue> GetStockValue(StockRequest request,
        ServerCallContext context)
    {
        return Task.FromResult(GetStockValue());
    }

    public override async Task StreamStockValue(StockRequest request, IServerStreamWriter<StockValue> responseStream,
        ServerCallContext context)
    {
        while (true)
        {
            await responseStream.WriteAsync(GetStockValue());
            await Task.Delay(1000);
        }
    }

    private StockValue GetStockValue()
    {
        return new StockValue
        {
            Amount = Random.Next(5000, 10000) / 100d,
            Currency = "CAD",
            Time = Timestamp.FromDateTime(DateTime.UtcNow)
        };
    }
}