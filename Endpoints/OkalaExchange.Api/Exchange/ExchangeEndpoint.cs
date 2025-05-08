using MediatR;
using OkalaExchange.Contracts.Exchange.Endpoints;
using OkalaExchange.ApplicationService.Exchange.Queries;
using OkalaExchange.ApplicationService.Common;
using OkalaExchange.Contracts;

namespace OkalaExchange.Api.Exchange
{
    public static class ExchangeEndpoint
    {
        public static void MapExchangeEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("/GetLatestQuotes", async (GetLatestQuotesRequestModel request, IMediator mediator) =>
            {
                var result = new GetLatestQuotesResponseModel();
                var targetCurrencies = new[]
               {
                    
                    "EUR",
                    "BRL",
                    "GBP",
                    "AUD"
                };
                var query = new GetLatestQuoteQuery()

                {
                    CryptoCode = request.CryptoCode,
                    TargetCurrencies=targetCurrencies
                };
             
        var response = await mediator.Send(query);
                result.CryptoQuote = response;
                return new BaseResponse<GetLatestQuotesResponseModel>() { Data = result };

            });
    }
    }
}
