using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Couchbase;
using Couchbase.Configuration.Client;
using Couchbase.N1QL;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace MulthiThreadLoginSaver
{
    public class DtoAccount
    {
        public DtoAccount(long _Id, double _cash)
        {
            this.Id = _Id;
            this.Cash = _cash;
            using (StreamReader r = new StreamReader("json1.json"))
            {
                this.json = r.ReadToEnd();
            }
        }
        public long Id { get; set; }
        public double Cash { get; set; }
        string json;
    }
    class Account
    {
        public int accountId { get; set; }
        public string token { get; set; }
        public Account(int _accountId, string _token)
        {
            this.accountId = _accountId;
            this.token = _token;
        }
    }
    class Program
    {
        const string TargetServer = "http://localhost/api/v1";
        public static async void LoginAndSave(List<Account> accountList, int i)
        {
            Console.WriteLine(i);
            HttpClient http = new HttpClient();
            foreach (Account member in accountList)
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, TargetServer + "account/login");
                request.Headers.Add("accountid", member.accountId.ToString());
                request.Headers.Add("deviceId", "1234");
                request.Headers.Add("Authorization", "Bearer " + member.token);
                request.Content = new StringContent(JsonConvert.SerializeObject(member), System.Text.Encoding.UTF8, "application/json");
                try
                {
                    HttpResponseMessage response = await http.SendAsync(request);
                }catch(Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            http = new HttpClient();
            foreach (Account member in accountList)
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, TargetServer + "account/save");
                request.Headers.Add("accountid", member.accountId.ToString());
                request.Headers.Add("deviceId", "1234");
                request.Headers.Add("Authorization", "Bearer " + member.token);
                request.Content = new StringContent(JsonConvert.SerializeObject(new DtoAccount(member.accountId, 0)), System.Text.Encoding.UTF8, "application/json");
                try
                {
                    HttpResponseMessage response = await http.SendAsync(request);
                }catch(Exception e)
                {
                    Console.WriteLine(e);
                }
            }

        }


    }
}
