using System.Threading.Tasks;

using Android.App;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using Android.Graphics.Drawables;
using Android;
using System.Net;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Reflection;

namespace Recipe_App.ViewModels
{
    class ImageResizer
    {
        



        static ImageResizer()
        {
            
        }


        async Task<BitmapFactory.Options> GetBitmapOptionsOfImageAsync(byte[] imageBytes)
        {
            BitmapFactory.Options options = new BitmapFactory.Options
            {
                /*Setting the InJustDecodeBounds property to true while decoding avoids memory allocation,
                 * returning null for the bitmap object but setting OutWidth, OutHeight and OutMimeType .
                 * This technique allows you to read the dimensions and type of the image data prior to
                 * construction (and memory allocation) of the bitmap.*/
                InJustDecodeBounds = true
            };

            // The result will be null because InJustDecodeBounds == true.
            Bitmap result = await BitmapFactory.DecodeByteArrayAsync(imageBytes, 0, imageBytes.Length - 1, options);

            int imageHeight = options.OutHeight;
            int imageWidth = options.OutWidth;

            System.Diagnostics.Debug.WriteLine(string.Format("Original Size= {0}x{1}", imageWidth, imageHeight));

            return options;
        }

        static int CalculateInSampleSize(BitmapFactory.Options options, int reqWidth, int reqHeight)
        {
            // Raw height and width of image
            float height = options.OutHeight;
            float width = options.OutWidth;
            double inSampleSize = 1D;

            if (height > reqHeight || width > reqWidth)
            {
                int halfHeight = (int)(height / 2);
                int halfWidth = (int)(width / 2);

                // Calculate a inSampleSize that is a power of 2 - the decoder will use a value that is a power of two anyway.
                while ((halfHeight / inSampleSize) > reqHeight && (halfWidth / inSampleSize) > reqWidth)
                {
                    inSampleSize *= 2;
                }
            }

            return (int)inSampleSize;
        }

        async Task<Android.Graphics.Bitmap> LoadScaledDownBitmapForDisplayAsync(byte[] imageBytes, BitmapFactory.Options options, int reqWidth, int reqHeight)
        {
            // Calculate inSampleSize
            options.InSampleSize = CalculateInSampleSize(options, reqWidth, reqHeight);

            // Decode bitmap with inSampleSize set
            options.InJustDecodeBounds = false;

            return await Android.Graphics.BitmapFactory.DecodeByteArrayAsync(imageBytes, 0, imageBytes.Length - 1, options);
        }

        public async Task<Bitmap> GetImageForDisplay(string filename, int reqWidth, int reqHeight)
        {
            byte[] imageBytes = null;

            imageBytes = GetResource(filename);
            
            

            BitmapFactory.Options options = await GetBitmapOptionsOfImageAsync(imageBytes);
            var bitmapToDisplay = await LoadScaledDownBitmapForDisplayAsync(imageBytes, options, reqWidth, reqHeight);
            imageBytes = null;
            return bitmapToDisplay;
        }


        public byte[] GetResource(string ResourceName)
        {
            System.Reflection.Assembly asm = Assembly.GetEntryAssembly();

            // list all resources in assembly - for test
            string[] names = asm.GetManifestResourceNames(); //even here my TestImg.png is not presented

            System.IO.Stream stream = asm.GetManifestResourceStream(ResourceName); //this return null of course

            byte[] data = new byte[stream.Length];

            stream.Read(data, 0, (int)stream.Length);

            return data;
        }

    }


}
