using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using Model;
using System.Linq;
using System.Web;

namespace ViewModel
{
    public abstract class BaseDB
    {
        protected string connString;
        protected OleDbConnection conn;
        protected OleDbCommand command;
        protected OleDbDataReader reader;
        protected int lastId;

        public BaseDB()
        {
            connString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + General.ProjectFolder + "ViewModel/App_Data/StorageManagmentDB.accdb";
            conn = new OleDbConnection(connString);
            command = new OleDbCommand();
            command.Connection = conn;
        }

        protected abstract BaseEntity NewEntity();
        protected abstract BaseEntity CreateModel(BaseEntity entity);

        protected List<BaseEntity> Select()
        {
            List<BaseEntity> list = new List<BaseEntity>();

            try
            {
                conn.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    BaseEntity entity = NewEntity();
                    list.Add(CreateModel(entity));
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message + "\nSQL: " + command.CommandText);
            }
            finally
            {
                if(reader != null) reader.Close();
                if (conn.State == ConnectionState.Open) conn.Close();
            }
            return list;
        }
        protected int SaveChanges(string sql)
        {
            int records = 0;
            try
            {
                command.CommandText = sql;
                conn.Open();
                records = command.ExecuteNonQuery();

                command.CommandText = "select @@Identity";
                var id = command.ExecuteScalar();
                lastId = int.Parse(id.ToString());
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message + "\nSQL" + command.CommandText);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return records;
        }



    }
}