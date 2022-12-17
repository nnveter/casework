namespace App2;

using CaseWork.Models.Dto;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Windows.Foundation.Collections;

public class ReqService
{
    public HttpClient client;
    public ReqService()
    {
        client = new HttpClient();
    }
    public async Task<string> Post<T>(string url, T? values, string? bearer = null)
    {
        if (bearer != null) client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer);
        if (values != null ) {
            
            var myContent = JsonSerializer.Serialize(values, typeof(T));
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(url, byteContent);
            String res = await response.Content.ReadAsStringAsync();

            res = res.TrimEnd('"');
            res = res.TrimStart('"');

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return "|" + res;
            }
            return res;
        }
        
        return null;
    }

    public async Task<string> Get(string url, string? bearer = null)
    {
        if (bearer != null) client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer);

        var response = await client.GetAsync(url);
        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            return null;
        }

        String res = await response.Content.ReadAsStringAsync();
        res = res.TrimEnd('"');
        res = res.TrimStart('"');
        return res;
    }

    public async Task<string> GetTask(string url, string? bearer = null, TasksAccessFilter accessFilter = TasksAccessFilter.Executor, TasksTypeFilter typeFilter = TasksTypeFilter.All)
    {
        if (bearer != null) client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer);
        TasksByFilterAr Filter = new TasksByFilterAr() { TasksAccessFilter = accessFilter, TasksTypeFilter = typeFilter };
        if (Filter != null)
        {

            var myContent = JsonSerializer.Serialize(Filter, typeof(TasksByFilterAr));


            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url),
                Content = new StringContent(myContent, Encoding.UTF8, MediaTypeNames.Application.Json),
            };

            var response = await client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return responseBody;
        }

        return null;
    }






}