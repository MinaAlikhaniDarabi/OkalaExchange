\# Answers to technical questions

\## 1. How long did you spend on the coding assignment? What would you
add to your solution if you had more time?

I spent approximately 6-8 hours on the coding assignment

If I had more time, I would:
 - Add User domain to show I know about entities and aggregate. 
 - Db connection. 
 - customExceptions for the domain.
 - Observability System.



\## 2. What was the most useful feature that was added to the latest version of your language of choice?

The most useful feature in \*\*.NET 9\*\* is \*\*Primary Constructors in
classes\*\*, which improves readability and reduces boilerplate code for
simple services or DTOs.

\### Example:

 public class ExchangeService(HttpClient httpClient,
IConfiguration config) : IExchangeService { private readonly string
\_coinMarketCapApiKey = config\[\"CoinMarketCap:ApiKey\"\]!; private
readonly string \_coinMarketCapBaseUrl =
config\[\"CoinMarketCap:BaseUrl\"\]!;

private readonly string \_exchangeRatesApiKey =
config\[\"ExchangeRatesApi:ApiKey\"\]!; private readonly string
\_exchangeRatesBaseUrl = config\[\"ExchangeRatesApi:BaseUrl\"\]!;

private readonly HttpClient \_httpClient = httpClient;

\## 3. How would you track down a performance issue in production? Have you ever had to do this? 
yes I Have done that. using Logs and trace
System and profiler

\## 4. What was the latest technical book you have read or tech conference you have been to? What did you learn Domain-Driven Design
Reference Definitions and Pattern Summaries,Eric Evans I checked some
knowledge about aggregate functionality in DDD 
\## 5. What do you think about this technical assessment? 
I liked it 
\## 6. Please, describe yourself using JSON 
{ "name": "mina",
  "description": "در من هست هزار من،که گر بشکند یک من، زاده شود منِ دیگری" }
