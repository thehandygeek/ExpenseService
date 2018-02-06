using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ExpenseService.Utilities
{
    public class BinaryResult: IHttpActionResult
    {
        private readonly Byte[] binaryData;
        private readonly string contentType;

        public BinaryResult(Byte[] binaryData, string contentType = null)
        {
            this.binaryData = binaryData;
            this.contentType = contentType;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(binaryData)
                };
                response.Content.LoadIntoBufferAsync(binaryData.Length).Wait();

                response.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);

                return response;
            }, cancellationToken);
        }
    }
}