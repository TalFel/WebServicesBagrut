using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;

namespace StorageManagmentWS
{
    public class SizeWS
    {
        public int SizeID { get; set; }
        public string SizeName { get; set; }
        //empty constructor.
        public SizeWS() { return; }
        //copy constructor.
        public SizeWS(SizeWS size)
        {
            this.SizeID = size.SizeID;
            this.SizeName = size.SizeName;
        }
        public SizeWS(Size size)
        {
            this.SizeID = size.SizeID;
            this.SizeName = size.SizeName;
        }
        //new Category with all properties.
        public SizeWS(int sizeID, string sizeName)
        {
            this.SizeID = sizeID;
            this.SizeName = sizeName;
        }
        public SizeWS(string sizeName)
        {
            this.SizeName = sizeName;
        }
        public Size GetSize()
        {
            Size size = new Size();
            size.SizeID = this.SizeID;
            size.SizeName = this.SizeName;
            return size;
        }
    }
}