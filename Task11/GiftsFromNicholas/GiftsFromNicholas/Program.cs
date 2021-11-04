using System;

namespace GiftsFromNicholas
{
    class Program
    {
        static void Main(string[] args)
        {
            Nicholas nicholas = Nicholas.GetInstance();
            
            GiftBuilder boyBuilder = new BoyGiftBuilder();
            GiftBuilder girlBuilder = new GirlGiftBuilder();
            nicholas.MakeGift("Maksym", 100, 43, boyBuilder, false);
            Gift gift = boyBuilder.GetGift();
            Console.WriteLine("Gift for Maksym");
            Console.WriteLine(gift);
            nicholas.MakeGift("Oksana", 40, 100, girlBuilder, false);
            gift = girlBuilder.GetGift();
            Console.WriteLine("Gift for Oksana");
            Console.WriteLine(gift);
            nicholas.MakeGift("Iryna", 40, 100, girlBuilder, true);
            gift = girlBuilder.GetGift();
            Console.WriteLine("Gift for Iryna ignoring bad actions");
            Console.WriteLine(gift);
            nicholas.MakeGift("Mykhailo", 48, 53, boyBuilder, false);
            Console.WriteLine("Gift for Mykhailo ignoring bad actions");
            Console.WriteLine(gift);
        }
    }
}
