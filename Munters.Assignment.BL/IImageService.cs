using Munters.Assignment.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Munters.Assignment.BL
{
    public interface IImageService
    {
        Task<IList<ImageResponse>> GetImagesUrl(string apiUrl);

        Task<IList<ImageResponse>> GetImagesUrlSearchBy(string apiUrl, string searchBy);
    }
}