using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Model
{
    public class Size : BaseEntity
    {
        public int SizeID { get; set; }
        public string SizeName { get; set; }
        //empty constructor.
        public Size() { return; }
        //copy constructor.
        public Size(Size size)
        {
            this.SizeID = size.SizeID;
            this.SizeName = size.SizeName;
        }
        //new Category with all properties.
        public Size(int sizeID, string sizeName)
        {
            this.SizeID = sizeID;
            this.SizeName = sizeName;
        }
        public Size(string sizeName)
        {
            this.SizeName = sizeName;
        }
        public int DeleteSize()
        {
            SizeDB sizeDB = new SizeDB();
            return sizeDB.delete(this);
        }
        public void AddSize()
        {
            SizeDB sizeDB = new SizeDB();
            this.SizeID = sizeDB.Insert(this);
        }
        public void UpdateSize(string sizeText)
        {
            this.SizeName = sizeText;
            SizeDB sizeDB = new SizeDB();
            sizeDB.Update(this);
        }
        public bool NameExistsOtherSize(string SName)
        {
            SizeDB sizeDB = new SizeDB();
            return sizeDB.NameExistsOtherSize(this.SizeID, SName);
        }
    }
}
