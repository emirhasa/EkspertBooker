using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EkspertBookerMobileApp.Helper
{
    public static class MediaFileToByteArray
    {
        public static byte[] GetByteArray(MediaFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.GetStream().CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
