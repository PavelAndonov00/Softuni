using SIS.HTTP.Common;
using SIS.HTTP.Enums;
using SIS.HTTP.Exceptions;
using SIS.HTTP.Headers;
using SIS.HTTP.Headers.Contracts;
using SIS.HTTP.Requests.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIS.HTTP.Requests
{
    public class HttpRequest : IHttpRequest
    {
        public HttpRequest(string requestString)
        {
            CoreValidator.ThrowIfNullOrEmpty(requestString, nameof(requestString));

            this.FormData = new Dictionary<string, object>();
            this.QueryData = new Dictionary<string, object>();
            this.Headers = new HttpHeaderCollection();

            this.ParseRequest(requestString);
        }

        public string Path { get; private set; }

        public string Url { get; private set; }

        public Dictionary<string, object> FormData { get; }

        public Dictionary<string, object> QueryData { get; }

        public IHttpHeaderCollection Headers { get; }

        public HttpRequestMethod RequestMethod { get; private set; }

        private bool IsValidRequestLine(string[] requestLines)
        {
            var result = requestLines.Length == 3 && requestLines[2] == GlobalConstants.HttpOneProtocolFragment;
            return result;
        }

        private bool IsValidRequestQueryString(string queryString, string[] queryParameters)
        {
            CoreValidator.ThrowIfNullOrEmpty(queryString, nameof(queryString));
            return true; //TODO: Regex query string
        }

        private void ParseRequestMethod(string[] requestLines)
        {
            HttpRequestMethod httpRequestMethod;
            if (!Enum.TryParse(requestLines[0], true, out httpRequestMethod))
            {
                throw new BadRequestException(
                    string.Format(GlobalConstants.UnsupportedHttpMethodExceptionMethod,
                    requestLines[0]));
            }

            this.RequestMethod = httpRequestMethod;
        }

        private void ParseRequestUrl(string[] requestLines)
        {
            this.Url = requestLines[1];
        }

        private void ParseRequestPath()
        {
            this.Path = this.Url.Split("?", StringSplitOptions.RemoveEmptyEntries)[0];
        }

        private void ParseRequestHeaders(string[] requestContent)
        {
            foreach (var line in requestContent)
            {
                if(line == GlobalConstants.HttpEmptyLine || string.IsNullOrEmpty(line))
                {
                    break;
                }

                var splitedLine = line.Split(": ", StringSplitOptions.RemoveEmptyEntries);
                var key = splitedLine[0];
                var value = splitedLine[1];

                var httpHeader = new HttpHeader(key, value);
                this.Headers.AddHeader(httpHeader);
            }
        }

        private void ParseCookies()
        {

        }

        private void ParseQueryParameters()
        {
            if (this.HasQueryString())
            {
                var queryPart = this.Url.Split("#", StringSplitOptions.RemoveEmptyEntries)[0]
                .Split("?", StringSplitOptions.RemoveEmptyEntries)[1];
                var keyValuePairs = queryPart.Split("&", StringSplitOptions.RemoveEmptyEntries);
                foreach (var keyValuePair in keyValuePairs)
                {
                    var splitedKeyValuePair = keyValuePair.Split("=", StringSplitOptions.RemoveEmptyEntries);
                    this.QueryData.Add(splitedKeyValuePair[0], splitedKeyValuePair[1]);
                }
            }
        }

        private bool HasQueryString()
        {
            return this.Url.Split("?").Length > 1;
        }

        private void ParseFormDataParameters(string formData)
        {
            if(!string.IsNullOrEmpty(formData))
            {
                var keyValuePairs = formData.Split("&", StringSplitOptions.RemoveEmptyEntries);
                foreach (var keyValuePair in keyValuePairs)
                {
                    var splitedKeyValuePair = keyValuePair.Split("=", StringSplitOptions.RemoveEmptyEntries);
                    this.FormData.Add(splitedKeyValuePair[0], splitedKeyValuePair[1]);
                }
            }
        }

        private void ParseRequestParameters(string formData)
        {
            this.ParseQueryParameters();
            this.ParseFormDataParameters(formData);
        }

        private void ParseRequest(string requestString)
        {
            string[] splitRequestContent = requestString
                .Split(new[] { GlobalConstants.HttpNewLine }, StringSplitOptions.None);

            string[] requestLines = splitRequestContent[0].Trim()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (!IsValidRequestLine(requestLines))
            {
                throw new BadRequestException();
            }

            this.ParseRequestMethod(requestLines);
            this.ParseRequestUrl(requestLines);
            this.ParseRequestPath();

            this.ParseRequestHeaders(splitRequestContent.Skip(1).ToArray());
            //this.ParseCookies();

            this.ParseRequestParameters(splitRequestContent[splitRequestContent.Length - 1]);
        }

    }
}
