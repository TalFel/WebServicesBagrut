using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Model
{
    public class Color : BaseEntity
    {
        public int ColorID { get; set; }
        public string ColorName { get; set; } = string.Empty;
        //empty constructor.
        public Color() { return; }
        //copy constructor.
        public Color(Color color)
        {
            this.ColorID = color.ColorID;
            this.ColorName = color.ColorName;
        }
        //new Category with all properties.
        public Color(int colorID, string colorName)
        {
            this.ColorID = colorID;
            this.ColorName = colorName;
        }
        public Color(string colorName)
        {
            this.ColorName = colorName;
        }
        public int DeleteColor()
        {
            ColorDB colorDB = new ColorDB();
            return colorDB.delete(this);
        }
        public bool NameExistsOtherColor(string name)
        {
            ColorDB colorDB = new ColorDB();
            return colorDB.NameExistsOtherColor(this, name);
        }
        public void UpdateColor(string name)
        {
            this.ColorName = name;
            ColorDB colorDB = new ColorDB();
            colorDB.Update(this);
        }
        public void AddColor()
        {
            ColorDB colorDB = new ColorDB();
            this.ColorID = colorDB.Insert(this);
        }
    }
}
