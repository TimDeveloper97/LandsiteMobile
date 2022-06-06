using System;
using System.Collections.Generic;
using System.Text;

namespace LandsiteMobile.Controls
{
    public interface IMediaService
    {
        void SaveImageFromByte(byte[] imageByte, string filename);
    }
}
