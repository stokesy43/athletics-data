using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System.Net.Http;
using Newtonsoft.Json;
using StapeleyDigital.AthleticsData.Dto;
using System.Collections.Generic;
using HtmlAgilityPack;
using System.Web;

namespace StapeleyDigital.AthleticsData.Functions
{
    public static class RefreshPerformanceData
    {
        [FunctionName("RefreshPerformanceData")]
        public static void Run([TimerTrigger("0 0 * * * *")]TimerInfo myTimer, TraceWriter log)
        {
            log.Info($"C# Timer trigger function executed at: {DateTime.Now}");

            // Get list of athletes to find
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/athletes");

            var athletes = new List<AthleteDto>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://stapeleydigitalathleticsdataapi.azurewebsites.net/");

                log.Info($"sending request to: {request.RequestUri.ToString()}");

                var response = client.SendAsync(request).Result;

                log.Info($"response code from data: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    var results = JsonConvert.DeserializeObject<IEnumerable<AthleteDto>>(data);

                    athletes.AddRange(results);
                }
            }
            
            foreach(var athlete in athletes)
            {
                log.Info($"processing performances for athlete: {athlete.Name}");

                var url = $"http://www.thepowerof10.info/athletes/profile.aspx?athleteid={athlete.PowerOf10Id}&viewby=points";

                HtmlWeb web = new HtmlWeb();
                HtmlDocument document = web.Load(url);

                // With LINQ	
                var nodes = document.DocumentNode.SelectNodes("//div[@id='cphBody_pnlPerformances']/table[@class='alternatingrowspanel']//tr");

                var performances = new List<PerformanceForCreationDto>();

                foreach (var node in nodes)
                {
                    //ignore header row
                    if (!node.InnerText.Contains("EventPerf"))
                    {
                        var performance = ExtractPerformance(node, log);

                        if (performance != null)
                        {
                            performances.Add(performance);                            
                        }
                    }
                }

                // loop through all the performances and add via api
                foreach(var performance in performances)
                {
                    performance.AthleteId = athlete.Id;                    

                    // serialize
                    var serializedItemToCreate = JsonConvert.SerializeObject(performance);

                    request = new HttpRequestMessage(HttpMethod.Post, "/api/performances")
                    {
                        Content = new StringContent(serializedItemToCreate, System.Text.Encoding.Unicode, "application/json")
                    };

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://stapeleydigitalathleticsdataapi.azurewebsites.net/");

                        log.Info($"sending request to: {request.RequestUri.ToString()}");

                        var response = client.SendAsync(request).Result;
                                                
                        if (response.IsSuccessStatusCode)
                        {
                            var data = response.Content.ReadAsStringAsync().Result;
                            var result = JsonConvert.DeserializeObject<PerformanceDto>(data);

                            log.Info($"performance created with id {result.Id}");
                        }
                        else
                        {
                            log.Error($"performance not created with message {response.StatusCode} ");
                            log.Info($"event: {performance.Event}");
                            log.Info($"meeting: {performance.MeetingId}");
                        }
                        
                    }
                }
            }

        }

        private static PerformanceForCreationDto ExtractPerformance(HtmlNode node, TraceWriter log)
        {
            try
            {
                var perf = new PerformanceForCreationDto();

                var data = node.SelectNodes("td");

                // Column 1 is row index

                // Column 2 is event
                perf.Event = data[1].InnerText;

                // Column 3 is performance
                perf.PerformanceValue = data[2].InnerText;

                // column 9 is age
                perf.Age = Convert.ToInt16(data[8].InnerText);

                // Column 10 is position
                perf.Position = data[9].InnerText;

                // Column 14 is venue
                var uri = new Uri(new Uri("http://test.com"), data[13].FirstChild.Attributes["href"].Value);

                var parameters = HttpUtility.ParseQueryString(uri.Query);

                perf.MeetingId = parameters.Get("meetingId");
                perf.Venue = data[13].FirstChild.InnerText; 

                // Column 15 is meeting
                perf.MeetingName = data[14].InnerText;

                // Column 16 is date
                perf.Date = Convert.ToDateTime(data[15].InnerText);

                return perf;
            }
            catch(Exception ex)
            {
                log.Error("error extracting performance data", ex);
                return null;
            }
        }

    }
}
