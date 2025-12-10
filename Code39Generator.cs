using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

public class Code39Generator
{
    private static readonly Dictionary<char, string> Code39Map = new Dictionary<char, string>
    {
        {'0', "101001101101"}, {'1', "110100101011"}, {'2', "101100101011"},
        {'3', "110110010101"}, {'4', "101001101011"}, {'5', "110100110101"},
        {'6', "101100110101"}, {'7', "101001011011"}, {'8', "110100101101"},
        {'9', "101100101101"}, {'A', "110101001011"}, {'B', "101101001011"},
        {'C', "110110100101"}, {'D', "101011001011"}, {'E', "110101100101"},
        {'F', "101101100101"}, {'G', "101010011011"}, {'H', "110101001101"},
        {'I', "101101001101"}, {'J', "101011001101"}, {'K', "110101010011"},
        {'L', "101101010011"}, {'M', "110110101001"}, {'N', "101011010011"},
        {'O', "110101101001"}, {'P', "101101101001"}, {'Q', "101010110011"},
        {'R', "110101011001"}, {'S', "101101011001"}, {'T', "101011011001"},
        {'U', "110010101011"}, {'V', "100110101011"}, {'W', "110011010101"},
        {'X', "100101101011"}, {'Y', "110010110101"}, {'Z', "100110110101"},
        {'-', "100101011011"}, {'.', "110010101101"}, {' ', "100110101101"},
        {'$', "100100100101"}, {'/', "100100101001"}, {'+', "100101001001"},
        {'%', "101001001001"}, {'*', "100101101101"}
    };

    public static Bitmap Generate(string data, int height, int scale = 2)
    {
        data = data.ToUpper();

        // Add start and stop characters
        string encoded = "*" + data + "*";
        StringBuilder bars = new StringBuilder();

        foreach (char c in encoded)
        {
            if (Code39Map.ContainsKey(c))
            {
                bars.Append(Code39Map[c]);
                bars.Append("0");
            }
        }

        int width = bars.Length * scale;

        Bitmap bitmap = new Bitmap(width, height);
        using (Graphics g = Graphics.FromImage(bitmap))
        {
            g.Clear(Color.White);

            int x = 0;
            foreach (char bit in bars.ToString())
            {
                if (bit == '1')
                {
                    g.FillRectangle(Brushes.Black, x, 0, scale, height - 20);
                }
                x += scale;
            }

            // Draw text
            using (Font font = new Font("Arial", 10))
            {
                string displayText = data;
                SizeF textSize = g.MeasureString(displayText, font);
                float textX = (width - textSize.Width) / 2;
                g.DrawString(displayText, font, Brushes.Black, textX, height - 18);
            }
        }

        return bitmap;
    }

    public static void Main()
    {
        Bitmap barcode = Generate("HELLO123", 100, 3);
        barcode.Save("barcode39_manual.png");
        Console.WriteLine("Barcode generated successfully!");
    }
}