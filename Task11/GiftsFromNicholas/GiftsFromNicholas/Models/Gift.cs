using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftsFromNicholas.Models
{
    public abstract class Gift
    {
        public abstract Enum Toy { get; set; }
        public abstract Sweets Sweets { get; set; }
        public abstract string Wish { get; set; }
        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine($"Toy: {Toy}");
            sb.AppendLine($"Sweets: {Sweets}");
            sb.AppendLine($"Wish: {Wish}");
            return sb.ToString();
        }
    }
}
