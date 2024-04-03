using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace ViewModel
{
    public class SizeDB : BaseDB
    {
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Size size = new Size();
            size.SizeID = int.Parse(reader["SizeID"].ToString());
            size.SizeName = reader["SizeName"].ToString();
            return size;
        }

        protected override BaseEntity NewEntity()
        {
            return new Size();
        }
        public bool NameExists(string name)
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM Sizes WHERE SizeName=@NAME";
            command.Parameters.Add(new OleDbParameter("@NAME", name));
            List<BaseEntity> list = base.Select();
            if (list.Count == 0) return false;
            return true;
        }
        public bool NameExistsOtherSize(int id, string SName)
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM Sizes WHERE SizeName=@NAME AND NOT SizeID=@ID";
            command.Parameters.Add(new OleDbParameter("@NAME", SName));
            command.Parameters.Add(new OleDbParameter("@ID", id));
            List<BaseEntity> list = base.Select();
            if (list.Count == 0) return false;
            return true;
        }
        public Size SelectBySizeID(int ID)
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM Sizes WHERE SizeID=@ID";
            command.Parameters.Add(new OleDbParameter("@ID", ID));
            List<BaseEntity> list = base.Select();
            if (list.Count == 1) return list[0] as Size;
            return null;
        }
        public SizesLst SelectSizeDB()
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM Sizes";
            List<BaseEntity> list = base.Select();
            if (list.Count() == 0)
                return null;
            return new SizesLst(list);
        }
        public SizesLst SelectSizesConditioned(string condition)
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM Sizes WHERE SizeName LIKE @SName";
            command.Parameters.Add(new OleDbParameter("@SName", "%" + condition + "%"));
            List<BaseEntity> list = base.Select();
            if (list.Count == 0)
                return null;
            return new SizesLst(list);
        }
        public int Insert(Size size)
        {
            string sql = $"INSERT INTO Sizes(SizeName)" +
                $" VALUES('{size.SizeName}')";
            int records = base.SaveChanges(sql);
            size.SizeID = base.lastId;
            return records;
        }
        public int delete(Size size)
        {
            string sql = $"Delete From Sizes WHERE SizeID={size.SizeID}";
            return base.SaveChanges(sql);
        }
        public int Update(Size size)
        {
            string sql = $"UPDATE Sizes SET SizeName='{size.SizeName}'" +
                $"WHERE SizeID={size.SizeID}";
            return base.SaveChanges(sql);
        }
    }
}
