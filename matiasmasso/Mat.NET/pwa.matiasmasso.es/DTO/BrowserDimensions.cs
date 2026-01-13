using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BrowserDimension
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public BrowserDimension(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
