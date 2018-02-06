using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace ExpenseService.Utilities
{
    public class BinaryMediaTypeFormatter: MediaTypeFormatter
    {
        private static readonly Type _supportedType = typeof(byte[]);

        public  BinaryMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("image/jpeg"));
        }

        public override bool CanReadType(Type type)
        {
            return type == _supportedType;
        }

        public override bool CanWriteType(Type type)
        {
            return type == _supportedType;
        }

        public override async Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            using (var memoryStream = new MemoryStream())
            {
                await readStream.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
        {
            return base.WriteToStreamAsync(type, value, writeStream, content, transportContext);
        }
    }
}