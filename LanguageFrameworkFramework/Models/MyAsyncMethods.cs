using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace LanguageFrameworkFramework.Models
{
    public class MyAsyncMethods
    {
        public static Task<long?> GetPageLength()
        {
            HttpClient client = new HttpClient();
            var httpTask = client.GetAsync("http://apress.com");
            //Во время ожидания завершения HTTP-запроса
            //можно было бы выполнять другую работу
            return httpTask.ContinueWith((Task<HttpResponseMessage> antecedent) => { return antecedent.Result.Content.Headers.ContentLength; });
        }
        public async static Task<long?> GetPageLength2()
        {
            HttpClient client = new HttpClient();
            var httpMessage = await client.GetAsync("http://apress.com");
            return httpMessage.Content.Headers.ContentLength;
        }
    }
}