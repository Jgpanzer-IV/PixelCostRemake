using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using PixelCost.Client.Web.Services.Interfaces;

namespace PixelCost.Client.Web.Services.Implements
{
    public class CommunicationServices : ICommunicationServices
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public CommunicationServices(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task<Tuple<bool, HttpStatusCode>> DeleteEntityById(string relativePath, long id, string accessToken, string apiName)
        {
            relativePath += id;

            HttpRequestMessage requestMessage = new(HttpMethod.Delete, relativePath);

            Tuple<object?, ProblemDetails?, HttpStatusCode> result = await PrincipalCommunication<object>(requestMessage, accessToken, apiName, null);

            return (result.Item3 == HttpStatusCode.NoContent) ? 
                new Tuple<bool, HttpStatusCode>(true, result.Item3) :
                new Tuple<bool, HttpStatusCode>(false, result.Item3);
        }

        public async Task<Tuple<Type?, ProblemDetails?, HttpStatusCode>> PatchEntityByObject<Type>(string relativePath,long id,Type entity, string accessToken, string apiName)
        {
            relativePath += id;

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Patch, relativePath);

            Tuple<Type?, ProblemDetails?, HttpStatusCode> result = await PrincipalCommunication<Type>(request, accessToken, apiName, entity);

            return result;

        }

        public async Task<Tuple<Type?, ProblemDetails?, HttpStatusCode>> PostEntityByObject<Type>(string relativePath, Type entity, string accessToken, string apiName)
        {
            
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, relativePath);

            Tuple<Type?, ProblemDetails?, HttpStatusCode> result = await PrincipalCommunication<Type>(request,accessToken,apiName,entity);

            return result;

        }

        public Task<Tuple<Type?, ProblemDetails?, HttpStatusCode>> RetrieveEntity<Type>(string relativePath, string accessToken, string apiName)
        {
            throw new NotImplementedException();
        }

        public async Task<Tuple<Type?, ProblemDetails?, HttpStatusCode>> RetrieveEntityByClaimId<Type>(string relativePath, string userId, string accessToken, string apiName)
        {
            relativePath += userId;

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, relativePath);

            Tuple<Type?, ProblemDetails?, HttpStatusCode> result = await PrincipalCommunication<Type>(request, accessToken, apiName, default);

            return new Tuple<Type?, ProblemDetails?, HttpStatusCode>(result.Item1, result.Item2, result.Item3);
           
        }

        public async Task<Tuple<Type?, ProblemDetails?, HttpStatusCode>> RetrieveEntityById<Type>(string relativePath, long id, string accessToken, string apiName)
        {

            relativePath += id;
            HttpRequestMessage request = new(HttpMethod.Get,relativePath);

            Tuple<Type?,ProblemDetails?, HttpStatusCode> result = await PrincipalCommunication<Type>(request,accessToken, apiName, default);

            return result;

        }


        /// <summary>
        /// This method used as a base request principal for every type of request.
        /// </summary>
        /// <param name="uri">The uri address of api to request to.</param>
        /// <param name="userId">Claim user-id or userId are option parameter used to request entity base on the userId</param>
        /// <param name="id">Entity-id or id is option parameter used to request entity base on the entity-id</param>
        /// <param name="entity">Entity to be post or patch to making change in the api server</param>
        /// <returns>(ErrorMessage->string , statusCode->int , RetrievedEntity->object)</returns>
        private async Task<Tuple<T?, ProblemDetails?, HttpStatusCode>> PrincipalCommunication<T>(HttpRequestMessage requestMessage, string accessToken, string apiName, T? entity)
        {

            // Create clident from specified api name.
            HttpClient httpClient = _httpClientFactory.CreateClient(apiName);

            // Set access token to the request message.
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            // Check for entity to be posted whether it exists or not
            if (entity != null)
            {

                //Serialize object into json format
                var jsonContent = JsonSerializer.Serialize(entity);
                // Store the json object into StringContent type with UTF8 codeSpace and json as media type
                StringContent requsetContent = new(jsonContent, Encoding.UTF8, "application/json");
                // Set the stringContent to the conten of the request message.
                requestMessage.Content = requsetContent;
            }

            HttpResponseMessage response;
            try
            {
                // Send the request message to the specified api server.
                response = await httpClient.SendAsync(requestMessage);
            }
            catch (Exception) {

                return new Tuple<T?, ProblemDetails?, HttpStatusCode>(default, null, HttpStatusCode.InternalServerError);
            }
            
            T? value = default;
            ProblemDetails? message = null;
            HttpStatusCode statusCode = response.StatusCode;

            // Handle for the success status code
            if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
            {
                value = await response.Content.ReadFromJsonAsync<T?>();
            }
            // Handle for the BadRequest response to deserialize the problemDetail object.
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                message = await response.Content.ReadFromJsonAsync<ProblemDetails?>();
            }

            return new Tuple<T?, ProblemDetails?, HttpStatusCode>(value, message, statusCode);
        }

    }
}
