using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class Palette
{
    public static Color fromHex(string color)
    {
        var c = color.TrimStart('#');
        string R = c.Substring(0, 2);
        string G = c.Substring(2, 2);
        string B = c.Substring(4, 2);
        float r = int.Parse(R, System.Globalization.NumberStyles.HexNumber) / 256f;
        float g = int.Parse(G, System.Globalization.NumberStyles.HexNumber) / 256f;
        float b = int.Parse(B, System.Globalization.NumberStyles.HexNumber) /256f;
        return new Color(r, g, b);
    }
    public static Color color1 = fromHex("#DAFF7D");
    public static Color color2 = fromHex("#B2EF9B");
    public static Color color3 = fromHex("#8C86AA");
    public static Color color4 = fromHex("#81559B");
    public static Color color5 = fromHex("#7E3F8F");
    public static Color tansparent = new Color(0, 0, 0, 0);
}
