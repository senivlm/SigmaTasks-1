using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GiftsFromNicholas.Models
{
    public class GiftsBaseConverter : IJsonConverter<GiftsBase>
    {
        /// <summary>
        /// Converts JSON string to GiftsBase object.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        /// <param name="json"></param>
        /// <returns>GiftsBase object.</returns>
        public GiftsBase Convert(string json)
        {
            if (String.IsNullOrWhiteSpace(json))
            {
                throw new ArgumentException("The value is either null or whitespace.");
            }
            return JsonConvert.DeserializeObject<GiftsBase>(json);
        }
    }
}
