using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;

namespace StorageManagmentWS
{
    public class ColorWS
    {
        public int ColorID { get; set; }
        public string ColorName { get; set; } = string.Empty;
        //empty constructor.
        public ColorWS() { return; }
        //copy constructor.
        public ColorWS(ColorWS color)
        {
            this.ColorID = color.ColorID;
            this.ColorName = color.ColorName;
        }
        public ColorWS(Color color)
        {
            this.ColorID = color.ColorID;
            this.ColorName = color.ColorName;
        }
        //new Category with all properties.
        public ColorWS(int colorID, string colorName)
        {
            this.ColorID = colorID;
            this.ColorName = colorName;
        }
        public ColorWS(string colorName)
        {
            this.ColorName = colorName;
        }
        public Color GetColor()
        {
            Color color = new Color();
            color.ColorID = this.ColorID;
            color.ColorName = this.ColorName;
            return color;
        }
    }
}