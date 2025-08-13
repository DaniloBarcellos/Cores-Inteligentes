using System;
using System.Collections.Generic;

namespace GreedyColors
{
    internal class Grafo
    {
        private Dictionary<Cor, List<Cor>> _graph = new Dictionary<Cor, List<Cor>>();
        private Cor _initialColor;
        private Cor _finalColor;
        private int _nSteps;

        public Grafo(Cor initialColor, Cor finalColor, int nSteps)
        {
            _initialColor = initialColor;
            _finalColor = finalColor;
            _nSteps = nSteps;
            GenerateGraph(initialColor, nSteps);
        }

        private void GenerateGraph(Cor color, int nSteps)
        {
            if (nSteps > 1)
            {
                --nSteps; //antigo
                List<Cor> colorsList = new List<Cor> { GenerateRed(color, nSteps),
                                                       GenerateGreen(color, nSteps),
                                                       GenerateBlue(color, nSteps)};
                //--nSteps; //atual
                _graph.Add(color, colorsList);
                foreach (Cor c in colorsList)
                {
                    GenerateGraph(c, nSteps);
                }
            }
            else
            {
                _graph.Add(color, new List<Cor> { _finalColor });
            }
        }

        #region Methods
        public string GraphToString()
        {
            string text = "";
            foreach (Cor key in _graph.Keys)
            {
                text += "{Key: " + key.ColorToString() + "}" + "\n";
                List<Cor> colorsList = _graph[key];
                foreach (Cor c in colorsList)
                {
                    text += c.ColorToString() + " ; ";
                }
                text += "\n";
                text += "\n";
            }

            return text;
        }

        #region Generate Based-value Color
        public Cor GenerateRed(Cor lastColor, int nSteps)
        {
            int red, green, blue;
            red = lastColor.GetRed();
            int avg = CalculateAvg(lastColor);
            double lightness = lastColor.CalculateNextLightness(_finalColor, nSteps);
            Cor redBasedColor;
            int newRed = red;
            int newGreen, newBlue;


            if (Math.Abs(lastColor.GetGreen() - avg) <= Math.Abs(lastColor.GetBlue() - avg))
            {
                blue = avg;
                green = CalculateNecessaryGreen(red, blue, lightness);
                redBasedColor = new Cor(red, green, blue);

                newBlue = avg;
                while (!CheckColorConditions(redBasedColor))
                {
                    if (newBlue <= 0 || newBlue > 255)
                    {
                        newRed = CalculateNewKeepedColor(newBlue, newRed);
                        newBlue = avg; //o newBlue retorna à média inicial
                    }

                    newGreen = CalculateNecessaryGreen(newRed, newBlue, lightness);
                    redBasedColor = new Cor(newRed, newGreen, newBlue);
                    newBlue = CalculateNewAvg(newGreen, newBlue);
                }
            }
            else
            {
                green = avg;
                blue = CalculateNecessaryBlue(red, green, lightness);
                redBasedColor = new Cor(red, green, blue);

                newGreen = avg;
                while (!CheckColorConditions(redBasedColor))
                {
                    if (newGreen <= 0 || newGreen > 255)
                    {
                        newRed = CalculateNewKeepedColor(newGreen, newRed);
                        newGreen = avg; //o newGreen retorna à média inicial
                    }

                    newBlue = CalculateNecessaryBlue(newRed, newGreen, lightness);
                    redBasedColor = new Cor(newRed, newGreen, newBlue);

                    newGreen = CalculateNewAvg(newBlue, newGreen);
                }
            }
            return redBasedColor;
        }

        public Cor GenerateGreen(Cor lastColor, int nSteps)
        {

            int red, green, blue;
            green = lastColor.GetGreen();
            int avg = CalculateAvg(lastColor);
            double lightness = lastColor.CalculateNextLightness(_finalColor, nSteps);
            Cor greenBasedColor;
            int newGreen = green;
            int newRed, newBlue;


            if (Math.Abs(lastColor.GetRed() - avg) <= Math.Abs(lastColor.GetBlue() - avg))
            {
                blue = avg;
                red = CalculateNecessaryRed(green, blue, lightness);
                greenBasedColor = new Cor(red, green, blue);

                newBlue = avg;
                while (!CheckColorConditions(greenBasedColor))
                {
                    if (newBlue <= 0 || newBlue > 255)
                    {
                        newGreen = CalculateNewKeepedColor(newBlue, newGreen);
                        newBlue = avg;
                    }

                    newRed = CalculateNecessaryRed(newGreen, newBlue, lightness);
                    greenBasedColor = new Cor(newRed, newGreen, newBlue);
                    newBlue = CalculateNewAvg(newRed, newBlue);
                }
            }
            else
            {
                red = avg;
                blue = CalculateNecessaryBlue(red, green, lightness);
                greenBasedColor = new Cor(red, green, blue);

                newRed = avg;
                while (!CheckColorConditions(greenBasedColor))
                {
                    if (newRed <= 0 || newRed > 255)
                    {
                        newGreen = CalculateNewKeepedColor(newRed, newGreen);
                        newRed = avg;
                    }

                    newBlue = CalculateNecessaryBlue(newRed, newGreen, lightness);
                    greenBasedColor = new Cor(newRed, newGreen, newBlue);
                    newRed = CalculateNewAvg(newBlue, newRed);
                }
            }
            return greenBasedColor;
        }

        public Cor GenerateBlue(Cor lastColor, int nSteps)
        {
            int red, green, blue;
            blue = lastColor.GetBlue();
            int avg = CalculateAvg(lastColor);
            double lightness = lastColor.CalculateNextLightness(_finalColor, nSteps);
            Cor blueBasedColor;
            int newBlue = blue;
            int newRed, newGreen;


            if (Math.Abs(lastColor.GetRed() - avg) <= Math.Abs(lastColor.GetGreen() - avg))
            {
                green = avg;
                red = CalculateNecessaryRed(green, blue, lightness);
                blueBasedColor = new Cor(red, green, blue);

                newGreen = avg;
                while (!CheckColorConditions(blueBasedColor))
                {
                    if (newGreen <= 0 || newGreen > 255)
                    {
                        newBlue = CalculateNewKeepedColor(newGreen, newBlue);
                        newGreen = avg;
                    }

                    newRed = CalculateNecessaryRed(newGreen, newBlue, lightness);
                    blueBasedColor = new Cor(newRed, newGreen, newBlue);
                    newGreen = CalculateNewAvg(newRed, newGreen);
                }
            }
            else
            {
                red = avg;
                green = CalculateNecessaryGreen(red, blue, lightness);
                blueBasedColor = new Cor(red, green, blue);

                newRed = avg;
                while (!CheckColorConditions(blueBasedColor))
                {
                    if (newRed <= 0 || newRed > 255)
                    {
                        newBlue = CalculateNewKeepedColor(newRed, newBlue);
                        newRed = avg;
                    }

                    newGreen = CalculateNecessaryGreen(newRed, newBlue, lightness);
                    blueBasedColor = new Cor(newRed, newGreen, newBlue);
                    newRed = CalculateNewAvg(newGreen, newRed);
                }
            }
            return blueBasedColor;
        }
        #endregion

        #region Calculate Necessary Color
        public int CalculateNecessaryRed(int green, int blue, double lightness)
        {
            return (int)((lightness - (0.701 * green) - (0.087 * blue)) / 0.212);
        }

        public int CalculateNecessaryGreen(int red, int blue, double lightness)
        {
            return (int)((lightness - (0.212 * red) - (0.087 * blue)) / 0.701);
        }

        public int CalculateNecessaryBlue(int red, int green, double lightness)
        {
            return (int)((lightness - (0.212 * red) - (0.701 * green)) / 0.087);
        }
        #endregion

        #region Helpful Methods
        private int CalculateAvg(Cor color)
        {
            return (int)((color.GetRed() + color.GetGreen() + color.GetBlue()) / 3);
        }

        private bool CheckColorConditions(Cor color)
        {
            double lightness = color.GetLightness();
            if (Cor.verifyRGBValues(color))
            {
                if (color.GetLightness() < lightness * 1.05 && color.GetLightness() > 0.95 * lightness)
                {
                    return true;
                }
            }
            return false;
        }


        //transformar em 1 método só
        private int CalculateNewAvg(int value, int avg)
        {
            if (value < 0)
            {
                if ((int)Math.Round(0.9 * avg) <= 4)
                {
                    return avg - 1;
                }
                return (int)Math.Round(0.9 * avg);
            }
            if (value > 255)
            {
                return (int)Math.Round(1.1 * avg);
            }
            return avg;
        }

        private int CalculateNewKeepedColor(int avg, int keepedColor)
        {
            if (avg <= 0)
            {
                return (int)Math.Round(0.9 * keepedColor);
            }
            if (avg > 255)
            {
                return (int)Math.Round(1.1 * keepedColor);
            }
            return keepedColor;
        }

        #endregion

        #region Getters
        public Dictionary<Cor, List<Cor>> GetGraph()
        {
            return _graph;
        }

        public Cor GetInitialColor()
        {
            return _initialColor;
        }

        public Cor GetFinallColor()
        {
            return _finalColor;
        }
        #endregion

        #endregion
    }
}
