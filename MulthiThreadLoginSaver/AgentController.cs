using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MulthiThreadLoginSaver
{
    class AgentController
    {
        Program program;
        static void Main(string[] args)
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            List<Account> accountList = new List<Account>();
            /*
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("config.json");
            
            var jsonConfiguration = builder.Build();
            var definition = new CouchbaseClientDefinition();
            jsonConfiguration.GetSection("couchbase:basic").Bind(definition);
            var clientConfig = new ClientConfiguration(definition);

            var cluster = new Cluster(clientConfig);
            cluster.Authenticate("Administrator", "Administrator");
            var bucket = cluster.OpenBucket("myBucket", "Administrator");
            
            var flightQuery = new QueryRequest().Statement("SELECT ParsedItem FROM myBucket where ParsedItem.accountId=32");
            var QueryResult = bucket.QueryAsync<dynamic>(flightQuery);
            var values = QueryResult.Result.Rows;
            */

            accountList.Add(new Account(109, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhY2NvdW50aWQiOiIxMDkifQ.PDq7vtWvEac6jN9IY5iVR2yrrDIDjNxsT_7wk_H0Uek"));

            Console.WriteLine("hi");

            /*
            for (int i = 0; i < 50000; i++)
            {
                LoginAndSave(accountList, i);
            }*/

            Parallel.For(0, 5000, (i) =>
            {
                Program.LoginAndSave(accountList, i);
            });
            // Login Process
            Console.WriteLine(watch.ElapsedMilliseconds);
        }
    }
}
