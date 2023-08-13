using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace AppSistemaVentas.Library
{
    
    public class UploadImage
    {
        private BitmapImage _bitmapImage;
        private byte[] avatar;

        public async Task <object[]> cargarImageAsync()
        {
            avatar = null;
            var picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.SuggestedStartLocation = PickerLocationId.Desktop;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");
            _bitmapImage = new BitmapImage();
            StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                using (IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read))
                {
                    _bitmapImage.SetSource(fileStream);
                }
                using (IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read))
                {
                    BinaryReader reader = new BinaryReader(fileStream.AsStream());
                    avatar = reader.ReadBytes((int)fileStream.Size);
                }    
            }
            object[] objects = { avatar, _bitmapImage };
            return objects;
        }
        public async Task<byte[]> ImagebyteAsync (BitmapImage image)
        {
            RandomAccessStreamReference streamRef = RandomAccessStreamReference.CreateFromUri(image.UriSource);
            IRandomAccessStreamWithContentType streamWithContent = await streamRef.OpenReadAsync();
            BinaryReader reader = new BinaryReader(streamWithContent.AsStream());
            avatar = reader.ReadBytes((int)streamWithContent.Size);
            return avatar;
        }
        public async Task<BitmapImage> ImageFromBufferAsync(byte[] byteAvatar)
        {
            _bitmapImage = new BitmapImage();
            using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
            {
                using (DataWriter writer = new DataWriter(stream.GetOutputStreamAt(0)))
                {
                    writer.WriteBytes(byteAvatar);
                    await writer.StoreAsync();
                }
                await _bitmapImage.SetSourceAsync(stream);
            }
            return _bitmapImage;
        }
    }
}
