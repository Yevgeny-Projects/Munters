using AutoMapper;
using EnsureThat;
using Munters.Assignment.Core;
using Munters.Assignment.Entities;
using Munters.Assignment.Shared.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Munters.Assignment.BL
{
    /// <summary>
    /// This class represent call external images api logic.
    /// </summary>
    public class ImageService : IImageService
    {
        #region Fields

        private IHttpGetRequestSender _sender;
        private IMemoryCache _cache;
        private IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion Fields

        #region Constructor

        public ImageService(IHttpGetRequestSender sender,
                            IHttpContextAccessor httpContextAccessor,
                            IMemoryCache cache,
                            IMapper mapper)
        {
            Ensure.That(sender, nameof(sender)).IsNotNull();
            _sender = sender;

            Ensure.That(cache, nameof(cache)).IsNotNull();
            _cache = cache;

            Ensure.That(mapper, nameof(mapper)).IsNotNull();
            _mapper = mapper;

            Ensure.That(httpContextAccessor, nameof(httpContextAccessor)).IsNotNull();
            _httpContextAccessor = httpContextAccessor;

        }

        #endregion Constructor

        #region Public methods

        /// <summary>
        /// Fetch the URLs of each trending GIF of the day.
        /// Cache previous requests to prevent redundant API calls to the Giphy API. 
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <returns></returns>
        public async Task<IList<ImageResponse>> GetImagesUrl(string apiUrl)
        {
            IList<ImageResponse> images = null;
            images = await ConvertToImageResponse(apiUrl);

            return images;
        }

        /// <summary>
        /// Search & Fetch the URLs of each GIF given a search term as input.
        /// Cache previous requests to prevent redundant API calls to the Giphy API. 
        /// </summary>
        /// <param name="searchBy"></param>
        /// <returns></returns>
        public async Task<IList<ImageResponse>> GetImagesUrlSearchBy(string apiUrl, string searchBy)
        {
            IList<ImageResponse> imagesResponse = null;

            if (!_cache.TryGetValue(searchBy, out imagesResponse))
            {

                var url = String.Format(apiUrl, searchBy);
                Root model = await GetImagesUrlFromApi(url);

                imagesResponse = _mapper.Map<IList<ImageResponse>>(model.data);
                if (imagesResponse != null)
                {
                    _cache.Set(searchBy, imagesResponse,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(60)));
                }
            }

            return imagesResponse;
        }

        #endregion Public methods

        #region Private methods

        /// <summary>
        /// Convert images model to IList<ImageResponse>
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <returns></returns>
        private async Task<IList<ImageResponse>> ConvertToImageResponse(string apiUrl)
        {
            IList<ImageResponse> imagesResponse = null;
            string cacheKey = "images";

            if (!_cache.TryGetValue(cacheKey, out imagesResponse))
            {
                Root model = await GetImagesUrlFromApi(apiUrl);

                imagesResponse = _mapper.Map<IList<ImageResponse>>(model.data);
                if (imagesResponse != null)
                {
                    _cache.Set(cacheKey, imagesResponse,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(60)));
                }
            }

            return imagesResponse;
        }

        /// <summary>
        /// Get images model from api
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <returns></returns>
        private async Task<Root> GetImagesUrlFromApi(string apiUrl)
        {
            var response = await _sender.GetAsync(apiUrl);

            var stringContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Root>(stringContent);
        }

        #endregion Private methods
    }
}