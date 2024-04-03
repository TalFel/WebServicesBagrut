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
    public class ColorDB : BaseDB
    {
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Color color = entity as Color;
            color.ColorID = int.Parse(reader["ColorID"].ToString());
            color.ColorName = reader["ColorName"].ToString();
            return color;
        }

        protected override BaseEntity NewEntity()
        {
            return new Color();
        }
        public Color SelectByColorID(int ID)
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM Colors WHERE ColorID=@ID";
            command.Parameters.Add(new OleDbParameter("@ID", ID));
            List<BaseEntity> list = base.Select();
            if (list.Count == 1) return list[0] as Color;
            return null;
        }
        public ColorLst SelectColorsDB()
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM Colors";
            List<BaseEntity> list = base.Select();
            if (list.Count == 0) return null;
            return new ColorLst(list);
        }
        public ColorLst SelectColorsConditioned(string condition)
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM Colors WHERE ColorName LIKE @CON";
            command.Parameters.Add(new OleDbParameter("@CON", "%" + condition + "%"));
            List<BaseEntity> list = base.Select();
            if (list.Count == 0) return null;
            return new ColorLst(list);
        }
        public bool ColorNameExists(string name)
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM Colors WHERE ColorName=@NAME";
            command.Parameters.Add(new OleDbParameter("@NAME", name));
            List<BaseEntity> list = base.Select();
            if (list.Count == 0) return false;
            return true;
        }
        public bool NameExistsOtherColor(Color color, string name)
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM Colors WHERE ColorName=@NAME AND NOT ColorID=@ColID";
            command.Parameters.Add(new OleDbParameter("@NAME", name));
            command.Parameters.Add(new OleDbParameter("@ColID", color.ColorID));
            List<BaseEntity> list = base.Select();
            if (list.Count == 0) return false;
            return true;
        }
        public int Insert(Color col)
        {
            string sql = $"INSERT INTO Colors(ColorName)" +
                $"VALUES('{col.ColorName}')";
            int records = base.SaveChanges(sql);
            col.ColorID = base.lastId;
            return records;
        }
        public int delete(Color col)
        {
            string sql = $"Delete From Colors WHERE ColorID={col.ColorID}";
            return base.SaveChanges(sql);
        }
        public int Update(Color col)
        {
            string sql = $"UPDATE Colors SET ColorName = '{col.ColorName}'" +
                $"WHERE ColorID={col.ColorID}";
            return base.SaveChanges(sql);
        }
    }
}
