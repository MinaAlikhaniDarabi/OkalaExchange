using MediatR;
using OkalaExchange.Domain.Exchage.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkalaExchange.ApplicationService.Exchange.Queries
{
    public class GetLatestQuoteQuery : IRequest<CryptoQuote>
    {
        public string CryptoCode { get; set; }
        public IEnumerable<string> TargetCurrencies { get; set; }

    }
}
