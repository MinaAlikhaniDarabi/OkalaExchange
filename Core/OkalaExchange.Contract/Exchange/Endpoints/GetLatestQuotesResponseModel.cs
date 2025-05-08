using OkalaExchange.Domain.Exchage.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkalaExchange.Contracts.Exchange.Endpoints
{
    public class GetLatestQuotesResponseModel
    {
        public CryptoQuote CryptoQuote { get; set; }
    }
}
