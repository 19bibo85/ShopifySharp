﻿using ShopifySharp.Infrastructure;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ShopifySharp
{
    /// <summary>
    /// The FulfillmentRequest resource represents a fulfillment request made by the merchant to a fulfillment service for a fulfillment order.
    /// </summary>
    public class FulfillmentRequestService : ShopifyService, IFulfillmentRequestService
    {
        /// <summary>
        /// Creates a new instance of <see cref="FulfillmentRequestService" />.
        /// </summary>
        /// <param name="myShopifyUrl">The shop's *.myshopify.com URL.</param>
        /// <param name="shopAccessToken">An API access token for the shop.</param>
        public FulfillmentRequestService(string myShopifyUrl, string shopAccessToken) : base(myShopifyUrl, shopAccessToken) { }

        public virtual async Task<FulfillmentOrder> CreateAsync(long fulfillmentOrderId, FulfillmentRequest fulfillmentRequest, CancellationToken cancellationToken = default)
        {
            var req = PrepareRequest($@"fulfillment_orders/{fulfillmentOrderId}/fulfillment_request.json");
            var body = fulfillmentRequest.ToDictionary();

            var content = new JsonContent(new
            {
                fulfillment_request = body
            });

            var response = await ExecuteRequestAsync<FulfillmentOrder>(req, HttpMethod.Post, cancellationToken, content, "original_fulfillment_order");
            return response.Result;
        }

        public virtual async Task<FulfillmentOrder> AcceptAsync(long fulfillmentOrderId, string message, CancellationToken cancellationToken = default)
        {
            var req = PrepareRequest($"fulfillment_orders/{fulfillmentOrderId}/fulfillment_request/accept.json");
            var fulfillmentRequest = new FulfillmentRequest { Message = message };
            var body = fulfillmentRequest.ToDictionary();

            var content = new JsonContent(new
            {
                fulfillment_request = body
            });

            var response = await ExecuteRequestAsync<FulfillmentOrder>(req, HttpMethod.Post, cancellationToken, content, "fulfillment_order");
            return response.Result;
        }

        public virtual async Task<FulfillmentOrder> RejectAsync(long fulfillmentOrderId, string message, CancellationToken cancellationToken = default)
        {
            var req = PrepareRequest($@"fulfillment_orders/{fulfillmentOrderId}/fulfillment_request/reject.json");
            var fulfillmentRequest = new FulfillmentRequest { Message = message };
            var body = fulfillmentRequest.ToDictionary();

            var content = new JsonContent(new
            {
                fulfillment_request = body
            });

            var response = await ExecuteRequestAsync<FulfillmentOrder>(req, HttpMethod.Post, cancellationToken, content, "fulfillment_order");
            return response.Result;
        }
    }
}
