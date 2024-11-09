using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using YamlDotNet.Serialization; // Use YamlDotNet for YAML generation
using System.Text.RegularExpressions;

namespace VS2019_YoloAnnotationToolApp
{
    public class LabelInfo
    {
        public int ClassIndex { get; set; }
        public float NormalizedXCenter { get; set; }
        public float NormalizedYCenter { get; set; }
        public float NormalizedWidth { get; set; }
        public float NormalizedHeight { get; set; }
    }

    public class DatasetYamlGenerator
    {
        public void GenerateConfig(string filepath, string rootPath, string trainPath, string valPath, string testPath, Dictionary<string, int> classNames)
        {
            string configData = BuildConfigString(rootPath, trainPath, valPath, testPath, classNames);
            string fileNamePath = Path.Combine(filepath, "data.yaml");
            string datasetPath = Path.Combine(filepath, "datasets");
            Directory.CreateDirectory(datasetPath);
            string datasetImagePath = Path.Combine(datasetPath, "images\\train");
            Directory.CreateDirectory(datasetImagePath);
            string datasetlabelsPath = Path.Combine(datasetPath, "labels\\train");
            Directory.CreateDirectory(datasetlabelsPath);
            File.WriteAllText(fileNamePath, configData);
        }

        private string BuildConfigString(string rootPath, string trainPath, string valPath, string testPath, Dictionary<string, int> classNames)
        {
            string trainPathRelative = trainPath;
            string valPathRelative = valPath;
            string testPathRelative = "";

            string content = "# YOLOv5 Dataset Configuration\n";
            content += $"# Example usage: python train.py --data train.yaml\n";
            content += $"# parent\n";
            content += $"# ├── yolov5\n";
            content += $"# └── datasets\n";
            content += $"#        └── images \n";
            content += $"#        └── labels \n";
            content += $"# Train/val/test sets as 1) dir: path/to/imgs, 2) file: path/to/imgs.txt, or 3) list: [path/to/imgs1, path/to/imgs2, ..]\n";

            content += $"path: {rootPath}  # dataset root dir\n";
            content += $"train: {trainPathRelative}  # train images (relative to 'path') 128 images\n";
            content += $"val: {valPathRelative} # val images (relative to 'path')\n";
            content += $"test: {testPathRelative}\n\n";
            content += "# Classes\n";
            content += $"names:\n";

            foreach (var pair in classNames)
            {
                // Properly format class names with spaces
                string formattedName = Regex.Replace(pair.Key, @"([A-Z][a-z]+)", " $1").TrimStart();
                formattedName = formattedName.Replace("trafficlight", "traffic light");
                formattedName = formattedName.Replace("firehydrant", "fire hydrant");
                formattedName = formattedName.Replace("stopsign", "stop sign");
                formattedName = formattedName.Replace("parkingmeter", "parking meter");
                formattedName = formattedName.Replace("sportsball", "sports ball");
                formattedName = formattedName.Replace("baseballbat", "baseball bat");
                formattedName = formattedName.Replace("baseballglove", "baseball glove");
                formattedName = formattedName.Replace("tennisracket", "tennis racket");
                formattedName = formattedName.Replace("wineglass", "wine glass");
                formattedName = formattedName.Replace("hotdog", "hot dog");
                formattedName = formattedName.Replace("pottedplant", "potted plant");
                formattedName = formattedName.Replace("diningtable", "dining table");
                formattedName = formattedName.Replace("cellphone", "cell phone");
                formattedName = formattedName.Replace("teddybear", "teddy bear");
                formattedName = formattedName.Replace("hairdrier", "hair drier");

                content += $" {pair.Value}: {formattedName}\n";
            }

            return content;
        }
    }
}
