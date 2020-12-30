using FTX.SDK.Request;
using FTX.SDK.Response;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace FTX.SDK
{
    public class FtxClient
    {
        private readonly string _Secret;

        private readonly string _Key;

        private string ServerUrl => "https://ftx.com";

        public FtxClient(string secret , string key)
        {
            this._Secret = secret;
            this._Key = key;
        }

        public string GetAccount()
        {
            var method = Method.GET;
            var endpoint = $"/api/account";
            var client = new RestClient(ServerUrl);
            var request = new RestRequest(endpoint, Method.GET);
            var _nonce = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            var hashMaker = new HMACSHA256(Encoding.UTF8.GetBytes(_Secret));
            var signaturePayload = $"{_nonce}{method.ToString().ToUpper()}{endpoint}";
            var hash = hashMaker.ComputeHash(Encoding.UTF8.GetBytes(signaturePayload));
            var hashString = BitConverter.ToString(hash).Replace("-", string.Empty);
            var signature = hashString.ToLower();

            request.AddHeader("FTX-KEY", _Key);
            request.AddHeader("FTX-SIGN", signature);
            request.AddHeader("FTX-TS", _nonce.ToString());
            var response = client.Execute(request);
            if (response.IsSuccessful)
            {
                return response.Content;
            }
            else
            {
                throw new Exception(response.ErrorMessage);
            }
        }


        public List<WalletBalanceDto> GetWalletBalance()
        {
            var method = Method.GET;
            var endpoint = $"/api/wallet/balances";
            var client = new RestClient(ServerUrl);
            var request = GetAuthRequest(endpoint , method);
            var response = client.Execute<ResponseBase<WalletBalanceDto>>(request);
            if (response.IsSuccessful)
            {
                if (response.Data.success)
                {

                    return response.Data.result;
                }
                else
                {
                    throw new Exception(response.Data.error);
                }
            }
            else
            {
                throw new Exception(response.ErrorMessage);
            }
        }

        public List<SpotMarginOfferDto> GetSpotMarginOffers()
        {
            var method = Method.GET;
            var endpoint = $"/api/spot_margin/offers";
            var client = new RestClient(ServerUrl);
            var request = GetAuthRequest(endpoint, method);
            var response = client.Execute<ResponseBase<SpotMarginOfferDto>>(request);
            if (response.IsSuccessful)
            {
                return response.Data.result;
            }
            else
            {
                throw new Exception(response.Data.error);
            }
        }


        public List<SpotMarginLendingInfoDto> GetSpotMarginLendingInfo()
        {
            var method = Method.GET;
            var endpoint = $"/api/spot_margin/lending_info";
            var client = new RestClient(ServerUrl);
            var request = GetAuthRequest(endpoint, method);
            var response = client.Execute<ResponseBase<SpotMarginLendingInfoDto>>(request);
            if (response.IsSuccessful)
            {
                return response.Data.result;
            }
            else
            {
                throw new Exception(response.Data.error);
            }
        }

        public string PostSpotMarginOffers(PostSpotMarginOffersRequest data)
        {
            var method = Method.POST;
            var endpoint = $"/api/spot_margin/offers";
            var client = new RestClient(ServerUrl);
            var jsonStr = System.Text.Json.JsonSerializer.Serialize(data);
            var request = GetAuthRequest(endpoint, method, jsonStr);
            request.AddParameter("application/json", jsonStr, ParameterType.RequestBody);
            var response = client.Execute<ResponseBase<object>>(request);
            if (response.IsSuccessful)
            {
                return response.Content;
            }
            else
            {
                throw new Exception(response.Data.error);
            }
        }


        private RestRequest GetAuthRequest(string endpoint  , Method method , string json = null)
        {
            var request = new RestRequest(endpoint, method);
            var _nonce = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            var hashMaker = new HMACSHA256(Encoding.UTF8.GetBytes(_Secret));
            var signaturePayload = $"{_nonce}{method.ToString().ToUpper()}{endpoint}{(method==Method.POST? json:"")}";
            var hash = hashMaker.ComputeHash(Encoding.UTF8.GetBytes(signaturePayload));
            var hashString = BitConverter.ToString(hash).Replace("-", string.Empty);
            var signature = hashString.ToLower();
            request.AddHeader("FTX-KEY", _Key);
            request.AddHeader("FTX-SIGN", signature);
            request.AddHeader("FTX-TS", _nonce.ToString());
            return request;
        }

    }
}
