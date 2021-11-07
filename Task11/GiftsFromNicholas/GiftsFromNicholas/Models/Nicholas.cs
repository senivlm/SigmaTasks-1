using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftsFromNicholas.Models
{
    public class Nicholas
    {
        private static readonly Lazy<Nicholas> _lazy = new(() => new Nicholas());
        public GiftsBase Gifts { get; }
        private Nicholas()
        {
            string path = Directory.GetParent(
                Directory.GetParent(
                    Directory.GetParent(Environment.CurrentDirectory).FullName)
                    .FullName)
                .FullName + @"\Data\giftsBase.json";
            string json = File.ReadAllText(path);
            Gifts = new GiftsBaseConverter().Convert(json);
        }

        public static Nicholas Instance => _lazy.Value;

        /// <summary>
        /// Gets gift from factory.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <param name="stats"></param>
        /// <param name="factory"></param>
        /// <param name="ignoreBad">Defines the way of gift choosing.</param>
        /// <returns>Gift depending on stats and value of ignoreBad</returns>
        public Gift GiveGift(ChildStats stats, IGiftFactory factory, bool ignoreBad)
        {
            if (stats == null)
            {
                throw new ArgumentNullException(nameof(stats), "The value is null.");
            }

            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory), "The value is null.");
            }

            return factory.CreateGift(stats, ignoreBad);
        }
    }
}
