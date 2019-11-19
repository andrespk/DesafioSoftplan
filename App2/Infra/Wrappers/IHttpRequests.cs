using System;
using System.Threading.Tasks;

namespace App2.Infra.Wrappers
{
    public interface IHttpRequests
    {
        /// <summary>
        /// Execute asynchronously a HTTP GET request to Uri passed by parameter.
        /// </summary>
        /// <param name="targetUri">The target Uri of the request.</param>
        /// <returns>A string with server response.</returns>
        Task<string> ExecuteGetRequestAsync(string targetUri, Action<Exception> exceptionHandler = null);

        /// <summary>
        /// Execute a HTTP POST request to Uri passed by parameter.
        /// </summary>
        /// <param name="targetUri">The target Uri of the request.</param>
        /// <param name="body">The body (payload) content of the request.</param>
        /// <returns>A HttpResponseMessage with server response.</returns>
        Task<string> ExecutePostRequestAsync<T>(string targetUri, T obj, Action<Exception> exceptionHandler = null);

        /// <summary>
        /// Execute a HTTP PUT request to Uri passed by json object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="targetUri"></param>
        /// <param name="obj"></param>
        /// <param name="exceptionHandler"></param>
        /// <returns></returns>
        Task<string> ExecutePutRequestAsJsonAsync<T>(string targetUri, T obj, Action<Exception> exceptionHandler = null);

        /// <summary>
        /// Execute a HTTP PUT request to Uri passed by json object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="targetUri"></param>
        /// <param name="obj"></param>
        /// <param name="exceptionHandler"></param>
        /// <returns></returns>
        Task<string> ExecuteDeleteRequestAsync<T>(string targetUri, T obj, Action<Exception> exceptionHandler = null);
    }
}