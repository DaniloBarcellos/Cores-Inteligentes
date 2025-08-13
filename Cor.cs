using System;
using System.Collections.Generic;

namespace GreedyColors
{
    public class Cor
    {
        private int _red;
        private int _green;
        private int _blue;
        private double _lightness;

        public Cor(int red, int green, int blue)
        {
            _red = red;
            _green = green;
            _blue = blue;
            _lightness = CalculateLightness(_red, _green, _blue);
        }

        #region Methods
        //Lightness formula: Y'240 Adobe (0-255)
        //0.212 * red + 0.701 * green + 0.087 * blue;
        private static double CalculateLightness(int red, int green, int blue)
        {
            return 0.212 * red + 0.701 * green + 0.087 * blue;
        }

        public double CalculateLightnessSteps(Cor finalColor, int nSteps)
        {
            return Math.Abs(_lightness - finalColor.GetLightness()) / (nSteps + 1);
        }

        public double CalculateNextLightness(Cor finalColor, int nSteps)
        {
            if (finalColor.GetLightness() < _lightness)
            {
                return (_lightness - CalculateLightnessSteps(finalColor, nSteps));
            }

            return (CalculateLightnessSteps(finalColor, nSteps) + _lightness);
        }

        public double CalculateEuclidianDistance(Cor finalColor)
        {
            double red = Math.Pow(_red - finalColor.GetRed(), 2);
            double green = Math.Pow(_green - finalColor.GetGreen(), 2);
            double blue = Math.Pow(_blue - finalColor.GetBlue(), 2);

            return Math.Sqrt(red + green + blue);
        }

        public static bool verifyRGBValues(int red, int green, int blue)
        {
            return (red >= 0 && red <= 255) && (green >= 0 && green <= 255) && (blue >= 0 && blue <= 255);
        }

        public static bool verifyRGBValues(Cor color)
        {
            return (color.GetRed() >= 0 && color.GetRed() <= 255) && (color.GetGreen() >= 0 && color.GetGreen() <= 255) && (color.GetBlue() >= 0 && color.GetBlue() <= 255);
        }

        public string ColorToString()
        {
            return "(" + _red + "," + _green + "," + _blue + "; lightness: " + _lightness + ")";
        }
        #endregion

        #region Getters
        public double GetLightness() { return _lightness; }
        public int GetRed() { return _red; }
        public int GetGreen() { return _green; }
        public int GetBlue() { return _blue; }
        #endregion

    }
}
