using System.Windows.Media.Imaging;
using Zadatak.Utils;

namespace Zadatak.Models
{
    public class Movie 
    {
        public int IDMovie { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
        public BitmapImage Image
        {
            get { return ImageUtils.ByteArrayToBitmapImage(Picture) ?? new BitmapImage(); }
        }

    }
}
