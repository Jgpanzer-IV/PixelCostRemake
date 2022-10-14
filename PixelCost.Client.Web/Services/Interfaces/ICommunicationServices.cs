using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace PixelCost.Client.Web.Services.Interfaces
{
    public interface ICommunicationServices
    {
        Task<Tuple<Type?, ProblemDetails?, HttpStatusCode>> RetrieveEntity<Type>(string relativePath, string accessToken, string apiName);
        Task<Tuple<Type?, ProblemDetails?, HttpStatusCode>> RetrieveEntityByClaimId<Type>(string relativePath, string userId, string accessToken, string apiName);
        Task<Tuple<Type?, ProblemDetails?, HttpStatusCode>> RetrieveEntityById<Type>(string relativePath, long id, string accessToken, string apiName);
        Task<Tuple<Type?, ProblemDetails?, HttpStatusCode>> PostEntityByObject<Type>(string relativePath, Type entity, string accessToken, string apiName);
        Task<Tuple<Type?, ProblemDetails?, HttpStatusCode>> PatchEntityByObject<Type>(string relativePath, long id ,Type entity, string accessToken, string apiName);
        Task<Tuple<bool, HttpStatusCode>> DeleteEntityById(string relativePath, long id, string accessToken, string apiName);
    }
}
