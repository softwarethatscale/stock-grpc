using Grpc.Core;
using Grpc.Net.Client;
using StockGrpcProto;

using var channel = GrpcChannel.ForAddress("http://localhost:5100");
var client = new StockExchange.StockExchangeClient(channel);

var request = new StockRequest
{
    Symbol = "MSFT",
};

var response = await client.GetStockValueAsync(request);
Console.WriteLine($"Amount: {response.Amount}, Currency: {response.Currency}, Time: {response.Time}");

var call = client.StreamStockValue(request);
await foreach (var response2 in call.ResponseStream.ReadAllAsync())
{
    Console.WriteLine($"Amount: {response2.Amount}, Currency: {response2.Currency}, Time: {response2.Time}");
}