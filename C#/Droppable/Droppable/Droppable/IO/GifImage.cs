using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace Droppable.IO
{
    public class GifImage
    {
        private Image _bitmap;

        public Image Bitmap
        {
            get { return this._bitmap; }
        }

        private int _width;

        public int Width
        {
            get { return this._width; }
        }

        private int _height;

        public int Height
        {
            get { return this._height; }
        }

        private FrameDimension _dimension;

        public FrameDimension Dimension
        {
            get { return this._dimension; }
        }

        private int _frameCount;

        public int FrameCount
        {
            get { return this._frameCount; }
        }

        private int _currentFrame;

        public int CurrentFrame
        {
            get { return this._currentFrame; }
        }

        public GifImage(Image img)
        {
            this._bitmap = img;

            this._width = img.Width;
            this._height = img.Height;

            this._dimension = new FrameDimension(this._bitmap.FrameDimensionsList[0]);
            this._frameCount = this._bitmap.GetFrameCount(this._dimension);
        }

        public Image Get()
        {
            this.Advance();

            return this._bitmap;
        }

        public void Destroy()
        {
            this._bitmap.Dispose();
        }

        private void Advance()
        {
            this._bitmap.SelectActiveFrame(this.Dimension, this._currentFrame);

            if (this._currentFrame >= this._frameCount - 1)
                this._currentFrame = 0;
            else
                this._currentFrame++;
        }
    }
}
