using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftsFromNicholas.Models
{
    public interface IJsonConverter<out T>
    {
        T Convert(string json);
    }
}
