using Account.Service.Request;
using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace Account.Service.Client
{
    public interface IAccount
    {
        [Get("/Account/{accountNumber}")]
        Task<HttpResponseMessage> GetAccount(string accountNumber);


        [Post("/Account")]
        Task<HttpResponseMessage> Account(TransferAccount transferAccount);
    }



}
