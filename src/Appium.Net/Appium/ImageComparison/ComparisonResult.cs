using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace OpenQA.Selenium.Appium.ImageComparison
{
    public abstract class ComparisonResult
    {
        public string Visualization
        {
            get { return Result["visualization"].ToString(); }
        }

        protected Dictionary<string, object> Result { get; }

        public ComparisonResult(Dictionary<string, object> result)
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
            var points = value as List<Dictionary<string, object>>;
            var convertedPoints = new List<Point>();

            foreach(var point in points)
            {
                convertedPoints.Add(new Point(
                    Convert.ToInt32(point["x"]),
                    Convert.ToInt32(point["y"])
                ));
            }

            return convertedPoints;
        }
    }
}
