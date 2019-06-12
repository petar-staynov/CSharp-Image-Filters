using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Interop;
using DrawingImage = System.Drawing.Image;
using Image = System.Windows.Controls.Image;

namespace CSharp_Image_Manipulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BitmapImage OriginalBitmap;
        private BitmapImage LastBitmapData;
        private Image LastImageData;

        public MainWindow()
        {
            InitializeComponent();
            this.OriginalBitmap = new BitmapImage();
            this.LastImageData = new Image();
            this.LastBitmapData = new BitmapImage();
        }


        private void BrowseButton_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";
            dlg.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;
            dlg.ShowDialog();

            //var bool2 = System.Windows.Forms.DialogResult.OK;


            string selectedFileName = dlg.FileName;
            FilenameLabel.Content = selectedFileName;
            if (String.IsNullOrEmpty(selectedFileName)) return;

            if (LastBitmapData.UriSource == null)
            {
                LastBitmapData.BeginInit();
                LastBitmapData.UriSource = new Uri(selectedFileName);
                LastBitmapData.EndInit();
            }
            else
            {
                LastBitmapData = new BitmapImage();
                LastBitmapData.BeginInit();
                LastBitmapData.UriSource = new Uri(selectedFileName);
                LastBitmapData.EndInit();
            }

            OriginalBitmap = LastBitmapData;

            Image image = new Image();
            image.Source = LastBitmapData;

            LastImageData = image;
            ImageDisplay.Source = LastBitmapData;
        }

        private void GrayscaleButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!ImagesHasBeenSelected()) return;

            //create a blank LastBitmapData the same size as original
            Bitmap imageBitmap = BitmapImage2Bitmap(LastBitmapData);

            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(imageBitmap);

            //create the grayscale ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(
                new float[][]
                {
                    new float[] {.3f, .3f, .3f, 0, 0},
                    new float[] {.59f, .59f, .59f, 0, 0},
                    new float[] {.11f, .11f, .11f, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1}
                });

            //create some image attributes
            ImageAttributes attributes = new ImageAttributes();

            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            //draw the original image on the new image
            //using the grayscale color matrix
            g.DrawImage(imageBitmap, new Rectangle(0, 0, imageBitmap.Width, imageBitmap.Height),
                0, 0, imageBitmap.Width, imageBitmap.Height, GraphicsUnit.Pixel, attributes);

            //dispose the Graphics object
            g.Dispose();

            //Turn Bitmap back to BitmapImage
            BitmapImage imageBitmapImage = BitmapToImageSource(imageBitmap);

            // Save the new Image  
            Image newLastImage = new Image();
            newLastImage.Source = imageBitmapImage;


            LastImageData = newLastImage;
            LastBitmapData = imageBitmapImage;
            ImageDisplay.Source = imageBitmapImage;
        }

        private void InvertColorButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!ImagesHasBeenSelected()) return;

            //create a blank LastBitmapData the same size as original
            Bitmap imageBitmap = BitmapImage2Bitmap(LastBitmapData);

            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(imageBitmap);

            //create the Invert ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(
                new float[][]
                {
                    new float[] {-1, 0, 0, 0, 0},
                    new float[] {0, -1, 0, 0, 0},
                    new float[] {0, 0, -1, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {1, 1, 1, 0, 1}
                });


            //create some image attributes
            ImageAttributes attributes = new ImageAttributes();

            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            //draw the original image on the new image
            //using the grayscale color matrix
            g.DrawImage(imageBitmap, new Rectangle(0, 0, imageBitmap.Width, imageBitmap.Height),
                0, 0, imageBitmap.Width, imageBitmap.Height, GraphicsUnit.Pixel, attributes);

            //dispose the Graphics object
            g.Dispose();

            //Turn Bitmap back to BitmapImage
            BitmapImage imageBitmapImage = BitmapToImageSource(imageBitmap);

            // Save the new Image  
            Image newLastImage = new Image();
            newLastImage.Source = imageBitmapImage;


            LastImageData = newLastImage;
            LastBitmapData = imageBitmapImage;
            ImageDisplay.Source = imageBitmapImage;
        }

        private void SepiaEffectButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!ImagesHasBeenSelected()) return;

            //create a blank LastBitmapData the same size as original
            Bitmap imageBitmap = BitmapImage2Bitmap(LastBitmapData);

            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(imageBitmap);

            //create the Invert ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(
                new float[][]
                {
                    new float[] {.393f, .349f, .272f, 0, 0},
                    new float[] {.769f, .686f, .534f, 0, 0},
                    new float[] {.189f, .168f, .131f, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1}
                });


            //create some image attributes
            ImageAttributes attributes = new ImageAttributes();

            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            //draw the original image on the new image
            //using the grayscale color matrix
            g.DrawImage(imageBitmap, new Rectangle(0, 0, imageBitmap.Width, imageBitmap.Height),
                0, 0, imageBitmap.Width, imageBitmap.Height, GraphicsUnit.Pixel, attributes);

            //dispose the Graphics object
            g.Dispose();

            //Turn Bitmap back to BitmapImage
            BitmapImage imageBitmapImage = BitmapToImageSource(imageBitmap);

            // Save the new Image  
            Image newLastImage = new Image();
            newLastImage.Source = imageBitmapImage;


            LastImageData = newLastImage;
            LastBitmapData = imageBitmapImage;
            ImageDisplay.Source = imageBitmapImage;
        }

        private void OriginalButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!ImagesHasBeenSelected()) return;

            // Save the new Image  
            Image newLastImage = new Image();
            newLastImage.Source = OriginalBitmap;

            LastImageData = newLastImage;
            LastBitmapData = OriginalBitmap;
            ImageDisplay.Source = OriginalBitmap;
        }

        private bool ImagesHasBeenSelected()
        {
            if (LastImageData.Source == null)
            {
                MessageBoxResult alert =
                    MessageBox.Show("You haven't opened an image",
                        "Alert",
                        MessageBoxButton.OK,
                        MessageBoxImage.Question);
                return false;
            }

            return true;
        }

        private void SaveImage_OnClick(object sender, RoutedEventArgs e)
        {
            if (!ImagesHasBeenSelected()) return;


            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";
            saveFileDialog.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();

                encoder.Frames.Add(BitmapFrame.Create((BitmapSource) LastImageData.Source));

                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    encoder.Save(stream);
                }
            }
        }

        private Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }


        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }

        private BitmapImage Bitmap2BitmapImage(Bitmap bitmap)
        {
            IntPtr hBitmap = bitmap.GetHbitmap();
            BitmapImage retval;

            try
            {
                retval = (BitmapImage) Imaging.CreateBitmapSourceFromHBitmap(
                    hBitmap,
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());
            }
            finally
            {
                DeleteObject(hBitmap);
            }

            return retval;
        }
    }
}