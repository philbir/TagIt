using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagIt;
internal class ColorUtils
{
    public static string GetRandomHexColor()
    {
        var random = new Random();

        return $"#{random.Next(0x1000000):X6}";
    }
}
