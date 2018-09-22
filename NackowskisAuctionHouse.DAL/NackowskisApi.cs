using NackowskisAuctionHouse.DAL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace NackowskisAuctionHouse.DAL
{
    public class NackowskisApi : INackowskisApi
    {
        private readonly string ApiNackowskisKey;
        private readonly Uri ApiAuctionBaseAdress;
        private readonly Uri ApiBidBaseAdress;

        public NackowskisApi()
        {
            //ApiNackowskisKey = configuration.GetValue<string>("ApiNackowskiKey");
            ApiNackowskisKey = "1060";
            ApiAuctionBaseAdress = new Uri($"http://nackowskis.azurewebsites.net/api/Auktion/");
            ApiBidBaseAdress = new Uri($"http://nackowskis.azurewebsites.net/api/Bud/");
        }

        public async Task<HttpResponseMessage> CreateAuction(Auction auction)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = ApiAuctionBaseAdress;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));


                var json = JsonConvert.SerializeObject(auction);

                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
                return await client.PostAsync("", stringContent);
            }
        }

        public async Task<HttpResponseMessage> CreateBid(Bid bid)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = ApiBidBaseAdress;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                var json = JsonConvert.SerializeObject(bid);

                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
                return await client.PostAsync("", stringContent);
            }

        }

        public async Task<HttpResponseMessage> DeleteAuction(int auctionId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = ApiAuctionBaseAdress;
                client.DefaultRequestHeaders.Accept.Clear();

                return await client.DeleteAsync(ApiNackowskisKey + "/" + auctionId);
            }

            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> DeleteBid(int bidId)
        {


            throw new NotImplementedException();
        }

        public async Task<HttpResponseMessage> EditAuction(Auction model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = ApiAuctionBaseAdress;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                var json = JsonConvert.SerializeObject(model);

                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
                return await client.PutAsync("", stringContent);
            }
        }



        public async Task<Auction> GetAuction(int auctionId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = ApiAuctionBaseAdress;
                client.DefaultRequestHeaders.Accept.Clear();

                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Auction));
                HttpResponseMessage response = await client.GetAsync(ApiNackowskisKey + "/" + auctionId);
                response.EnsureSuccessStatusCode();

                Stream responseStream = response.Content.ReadAsStreamAsync().Result;

                return (Auction)serializer.ReadObject(responseStream);
            }

        }

        public async Task<List<Auction>> GetAuctions()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = ApiAuctionBaseAdress;
                client.DefaultRequestHeaders.Accept.Clear();

                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<Auction>));
                HttpResponseMessage response = await client.GetAsync(ApiNackowskisKey);

                response.EnsureSuccessStatusCode();

                Stream responseStream = response.Content.ReadAsStreamAsync().Result;

                return (List<Auction>)serializer.ReadObject(responseStream);
            }
        }

        public Task<Bid> GetBid(int auctionId, int bidId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Bid>> GetBids(int auctionId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = ApiBidBaseAdress;
                client.DefaultRequestHeaders.Accept.Clear();

                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<Bid>));
                HttpResponseMessage response = await client.GetAsync(ApiNackowskisKey + "/" + auctionId);
                response.EnsureSuccessStatusCode();

                Stream responseStream = response.Content.ReadAsStreamAsync().Result;

                return (List<Bid>)serializer.ReadObject(responseStream);
            }


            throw new NotImplementedException();
        }

    }
}
