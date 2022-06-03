using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace GIFdisassembler
{
    class Program
    {

        [STAThread]
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("GIF File: ");
                Image gif = GetGIF();
                Image[] frames = GetFrames(gif);
                if(frames == null)
                {
                    continue;
                }

                Console.WriteLine("Path to safe: ");
                SaveFrames(frames);

                Console.Write("Continue? [y/other]: ");
                if (Console.ReadLine().ToUpper() != "Y")
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Getting a GIF file
        /// </summary>
        /// <returns>Image gif, Class Image</returns>
        private static Image GetGIF()
        {
            OpenFileDialog openFileDialogForGetGIFFile = new OpenFileDialog();
            if (openFileDialogForGetGIFFile.ShowDialog() == DialogResult.OK)
            {
                Image image = Image.FromFile(openFileDialogForGetGIFFile.FileName);
                return image;
            }
            else
            {
                Console.WriteLine("ERROR: The file could not be found. The file is not specified.");
                return null;
            }
        }

        /// <summary>
        /// Saves frames to the specified folder
        /// </summary>
        /// <param name="frames">Frames obtained from GIF</param>
        private static void SaveFrames(Image[] frames)
        {
            SaveFileDialog openFileDialogForSaveFile = new SaveFileDialog();
            if (openFileDialogForSaveFile.ShowDialog() == DialogResult.OK)
            {

                int id = 1;
                foreach (var frame in frames)
                {
                    frame.Save(openFileDialogForSaveFile.FileName + "_" + id + ".png", ImageFormat.Png);
                    id++;
                }
            }
            else
            {
                Console.WriteLine("ERROR: Failed to save files. The folder is not specified.");
            }
        }

        /// <summary>
        /// Gets frames from GIF
        /// </summary>
        /// <param name="gif">GIF image format</param>
        /// <returns>Array of frames from a GIF file</returns>
        private static Image[] GetFrames(Image gif)
        {
            List<Image> frames = new List<Image>();
            int length = gif.GetFrameCount(FrameDimension.Time);
            for (int i = 0; i < length; i++)
            {
                gif.SelectActiveFrame(FrameDimension.Time, i);
                frames.Add(new Bitmap(gif));
            }
            return frames.ToArray();
        }
    }
}
