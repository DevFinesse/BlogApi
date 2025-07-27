using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Shared.DataTransferObjects;
using System.Text;


namespace BlogApi
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter() 
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type? type)
        {
            if (typeof(PostDto).IsAssignableFrom(type) || typeof(IEnumerable<PostDto>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            return false;
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            if (context.Object is IEnumerable<PostDto>)
            {
                foreach (var post in (IEnumerable<PostDto>)context.Object)
                {
                    FormatCsv(buffer, post);
                }
            }
            else
            {
                FormatCsv(buffer, (PostDto)context.Object);
            }
            await response.WriteAsync(buffer.ToString());
        }

        private static void FormatCsv(StringBuilder buffer, PostDto post) 
        {
            buffer.AppendLine($"{post.Id},\"{post.Title},\"{post.Content}\"");
        }
    }
}
