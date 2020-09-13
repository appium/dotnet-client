using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace OpenQA.Selenium.Appium.ImageComparison
{
    public abstract class ComparisonResult
    {
        /// <summary>
        /// The visualization of the matching result represented as base64-encoded PNG image.
        /// </summary>
        public string Visualization => Result["visualization"].ToString();

        protected Dictionary<string, object> Result { get; }

        protected ComparisonResult(Dictionary<string, object> result)
        {
            Result = result;
        }

        public void SaveVisualizationAsFile(string fileName)
        {
            File.WriteAllBytes(fileName, Convert.FromBase64String(Visualization));
        }

        protected Rectangle ConvertToRect(object value)
        {
            var rect = value as Dictionary<string, object>;
            return new Rectangle(
                Convert.ToInt32(rect["x"]),
                Convert.ToInt32(rect["y"]),
                Convert.ToInt32(rect["width"]),
                Convert.ToInt32(rect["height"])
            );
        }

        protected List<Point> ConvertToPoint(object value)
        {
            var points = value as object[];
            var convertedPoints = new List<Point>();
            foreach(var point in points)
            {
                var currentPoint = point as Dictionary<string, object>;
                convertedPoints.Add(new Point(
                    Convert.ToInt32(currentPoint["x"]),
                    Convert.ToInt32(currentPoint["y"])
                ));
            }

            return convertedPoints;
        }
    }
}
