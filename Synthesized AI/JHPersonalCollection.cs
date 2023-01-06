using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using iText.IO.Image;
using Microsoft.Win32;

namespace JHPersonalCollection
{
    internal class Environment
    {
        // Securely get python path
        // Source: Shashi Bhushan, Albert MN, albusSimba, Jesse Houk
        // https://stackoverflow.com/questions/41920032/automatically-find-the-path-of-the-python-executable
        public static string GetPythonPath(string requiredVersion = "", string maxVersion = "")
        {
            string[] possiblePythonLocations = new string[3] {
                @"HKLM\SOFTWARE\Python\PythonCore\",
                @"HKCU\SOFTWARE\Python\PythonCore\",
                @"HKLM\SOFTWARE\Wow6432Node\Python\PythonCore\"
            };

            //Version number, install path
            Dictionary<string, string> pythonLocations = new Dictionary<string, string>();

            foreach (string possibleLocation in possiblePythonLocations)
            {
                string regKey = possibleLocation.Substring(0, 4), actualPath = possibleLocation.Substring(5);
                RegistryKey theKey = (regKey == "HKLM" ? Registry.LocalMachine : Registry.CurrentUser);
                RegistryKey theValue = theKey.OpenSubKey(actualPath);

                if (theValue != null)
                {
                    foreach (var v in theValue.GetSubKeyNames())
                    {
                        RegistryKey productKey = theValue.OpenSubKey(v);
                        if (productKey != null)
                        {
                            try
                            {
                                string pythonExePath = productKey.OpenSubKey("InstallPath").GetValue("ExecutablePath").ToString();

                                // Comment this in to get (Default) value instead
                                // string pythonExePath = productKey.OpenSubKey("InstallPath").GetValue("").ToString();

                                if (pythonExePath != null && pythonExePath != "")
                                {
                                    //Console.WriteLine("Got python version; " + v + " at path; " + pythonExePath);
                                    pythonLocations.Add(v.ToString(), pythonExePath);
                                }
                            }
                            catch
                            {
                                //Install path doesn't exist
                            }
                        }
                    }
                }
            }

            if (pythonLocations.Count > 0)
            {
                System.Version desiredVersion = new System.Version(requiredVersion == "" ? "0.0.1" : requiredVersion),
                    maxPVersion = new System.Version(maxVersion == "" ? "999.999.999" : maxVersion);

                string highestVersion = "", highestVersionPath = "";

                foreach (KeyValuePair<string, string> pVersion in pythonLocations)
                {
                    //TODO; if on 64-bit machine, prefer the 64 bit version over 32 and vice versa
                    int index = pVersion.Key.IndexOf("-"); //For x-32 and x-64 in version numbers
                    string formattedVersion = index > 0 ? pVersion.Key.Substring(0, index) : pVersion.Key;

                    System.Version thisVersion = new System.Version(formattedVersion);
                    int comparison = desiredVersion.CompareTo(thisVersion),
                        maxComparison = maxPVersion.CompareTo(thisVersion);

                    if (comparison <= 0)
                    {
                        //Version is greater or equal
                        if (maxComparison >= 0)
                        {
                            desiredVersion = thisVersion;

                            highestVersion = pVersion.Key;
                            highestVersionPath = pVersion.Value;
                        }
                        else
                        {
                            //Console.WriteLine("Version is too high; " + maxComparison.ToString());
                        }
                    }
                    else
                    {
                        //Console.WriteLine("Version (" + pVersion.Key + ") is not within the spectrum.");
                    }
                }

                //Console.WriteLine(highestVersion);
                //Console.WriteLine(highestVersionPath);
                return highestVersionPath;
            }

            return "";
        }

        public static string GetCurrentDirectory(
            string dirChar = "\\", 
            bool includeQuotes = true)
        {
            string [] dir = System.IO.Directory.GetCurrentDirectory().Split('\\');
            string path = "";
            for (int i = 0; i < dir.Length; i++)
            {
                if (dir[i].Contains(" "))
                {
                    if (includeQuotes)
                    {
                        dir[i] = "\"" + dir[i] + "\"";
                    }
                }
                if (dir[i] == "bin")
                    break;
                path += dir[i] + dirChar;
            }

            return path;
        }
    }

    class Utils
    {
        public static string[] ProcessPythonListResponce(string resp)
        {
            string[] stringDelimiters = { "\r\n" };
            return resp.Split(stringDelimiters, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string[] ProcessPythonMultiLineListResponce(string resp, char[] delimiters = null)
        {
            return resp.Split(delimiters ?? new char[] { '`' }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Create a byte array from a path using a memory stream
        /// https://stackoverflow.com/questions/59793245/how-do-you-create-an-image-object-from-a-memorystream-in-c-sharp-using-itext7
        /// </summary>
        /// <param name="path"></param>
        /// <returns>ImageData</returns>
        //public static ImageData CreateImageFromMemoryStream(string path)
        //{
        //    byte[] imageBytes = System.IO.File.ReadAllBytes(path);
        //    //MemoryStream mem = new MemoryStream(imageBytes);
        //    Image image = new iText.IO.Image.Image(imageBytes);
        //    return ImageDataFactory.Create(imageBytes);

        //}

        //
        /// Load images without keeping them in memory
        // may want to consult https://stackoverflow.com/questions/17784382/how-do-i-close-an-image-open-in-a-picturebox-so-i-can-delete-it
        public static Image CreateImageFromFileStream(string path)
        {

            FileStream fs = new FileStream(@path, FileMode.Open, FileAccess.Read);
            Image img = Image.FromStream(fs);
            return img;
        }

        /// <summary>
        /// courtsey of Jordão from https://stackoverflow.com/questions/3661799/file-delete-failing-when-image-fromfile-was-called-prior-it-despite-making-copy/3661892#3661892
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Image</returns>
        public static Image CreateNonIndexedImage(ref Image targetImage, string path)
        {
            targetImage?.Dispose();
            using (var sourceImage = Image.FromFile(path))
            {
                targetImage = new Bitmap(sourceImage.Width, sourceImage.Height,
                  PixelFormat.Format32bppArgb);
                using (var canvas = Graphics.FromImage(targetImage))
                {
                    canvas.DrawImageUnscaled(targetImage, 0, 0);
                }
                return targetImage;
            }
        }

        //public static Image AltCreateNonIndexedImage(string path)
        //{
        //    Image sourceImage = null;

        //    try
        //    {
        //        sourceImage = Image.FromFile(path);
            
        //        var targetImage = new Bitmap(sourceImage.Width, sourceImage.Height,
        //            PixelFormat.Format32bppArgb);
        //        using (var canvas = Graphics.FromImage(targetImage))
        //        {
        //            canvas.DrawImageUnscaled(sourceImage, 0, 0);
        //        }
        //        return targetImage;
        //    }
        //    catch (Exception ex)
        //    {
                
        //    }
        //    finally
        //    {
        //       // sourceImage.Close();
        //        sourceImage.Dispose();
        //        sourceImage = null;
        //    }
        //}

        [DllImport("Kernel32.dll", EntryPoint = "CopyMemory")]
        private extern static void CopyMemory(IntPtr dest, IntPtr src, uint length);

        public static Image CreateIndexedImage(string path)
        {
            using (var sourceImage = (Bitmap)Image.FromFile(path))
            {
                var targetImage = new Bitmap(sourceImage.Width, sourceImage.Height,
                  sourceImage.PixelFormat);
                var sourceData = sourceImage.LockBits(
                  new Rectangle(0, 0, sourceImage.Width, sourceImage.Height),
                  ImageLockMode.ReadOnly, sourceImage.PixelFormat);
                var targetData = targetImage.LockBits(
                  new Rectangle(0, 0, sourceImage.Width, sourceImage.Height),
                  ImageLockMode.WriteOnly, targetImage.PixelFormat);
                CopyMemory(targetData.Scan0, sourceData.Scan0,
                  (uint)sourceData.Stride * (uint)sourceData.Height);
                sourceImage.UnlockBits(sourceData);
                targetImage.UnlockBits(targetData);
                targetImage.Palette = sourceImage.Palette;
                return targetImage;
            }
        }
    }
}
