using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Security;

namespace ViewModel
{
    public class UserDB : BaseDB
    {
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            User user = new User();
            user.UserID = int.Parse(reader["UserID"].ToString());
            user.UserFirstName = reader["UserFirstName"].ToString();
            user.UserFamilyName = reader["UserFamilyName"].ToString();
            user.UserPassLength = reader["Password"].ToString().Length;
            RoleDB tempRole = new RoleDB();
            user.UserRole = tempRole.SelectByRoleID(int.Parse(reader["RoleID"].ToString()));
            user.UserPhoneNumber = reader["PhoneNumber"].ToString();
            user.UserActive = Convert.ToBoolean(reader["Active"].ToString());

            return user;
        }

        protected override BaseEntity NewEntity()
        {
            return new User();
        }
        public User SelectUserLogin(string phoneNumber, string password)
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM Users WHERE PhoneNumber=@PN AND [Password]=@Pas AND Active=True";
            command.Parameters.Add(new OleDbParameter("@PN", phoneNumber));
            command.Parameters.Add(new OleDbParameter("@Pas", password));
            List<BaseEntity> list = base.Select();
            if (list.Count == 0) return null;
            if (list.Count == 1) return list[0] as User;
            return null;
        }
        public UsersLst GetAllUsers()
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM Users WHERE Active=True";
            List<BaseEntity> list = base.Select();
            if (list.Count == 0) return null;
            return new UsersLst(list);
        }
        public UsersLst SelectUsersConditioned(string FirstNameTB, string LastNameTB, string PhoneTB, bool Old, int role)
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM Users WHERE ";

            int count = 0;
            if (FirstNameTB.Length != 0)
            {
                count++;
                command.CommandText += "UserFirstName LIKE @UFN ";
                command.Parameters.Add(new OleDbParameter("@UFN", "%" + FirstNameTB + "%"));
            }
            if (LastNameTB.Length != 0)
            {
                if (count != 0)
                    command.CommandText += "AND ";
                count++;
                command.CommandText += "UserFamilyName LIKE @ULN ";
                command.Parameters.Add(new OleDbParameter("@ULN", "%" + LastNameTB + "%"));
            }
            if (PhoneTB.Length != 0)
            {
                if (count != 0)
                    command.CommandText += "AND ";
                count++;
                command.CommandText += "PhoneNumber LIKE @UPN ";
                command.Parameters.Add(new OleDbParameter("@UN", "%" + PhoneTB + "%"));
            }
            if (role != -1)
            {
                if (count != 0)
                    command.CommandText += "AND ";
                count++;
                command.CommandText += "RoleID=@ROLE ";
                command.Parameters.Add(new OleDbParameter("@ROLE", role));
            }
            if (!Old)
            {
                if (count != 0)
                    command.CommandText += "AND ";
                count++;
                command.CommandText += "Active=True";
            }
            if (count == 0)
                command.CommandText = "SELECT * FROM Users";

            List<BaseEntity> list = base.Select();
            if (list.Count == 0) return null;
            return new UsersLst(list);
        }
        public bool PhoneExists(string PhoneNumber)
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM Users WHERE PhoneNumber=@PN AND Active=True";
            command.Parameters.Add(new OleDbParameter("@PN", PhoneNumber));
            List<BaseEntity> list = base.Select();
            if (list.Count == 1) return true;
            return false;
        }
        public bool PhoneExistsOtherUser(string PhoneNumber, int UserID)
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM Users WHERE PhoneNumber=@PN AND NOT UserID=@UID AND Active=True";
            command.Parameters.Add(new OleDbParameter("@PN", PhoneNumber));
            command.Parameters.Add(new OleDbParameter("@UID", UserID));
            List<BaseEntity> list = base.Select();
            if (list.Count == 0) return false;
            return true;
        }
        public User SelectByUserID(int ID)
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM Users WHERE UserID=@ID AND Active=True";
            command.Parameters.Add(new OleDbParameter("@ID", ID));
            List<BaseEntity> list = base.Select();
            if (list.Count == 1) return list[0] as User;
            return null;
        }
        public int Insert(User user, string Password)
        {
            string sql = $"INSERT INTO Users([Password], UserFirstName, UserFamilyName, RoleID, PhoneNumber, Active)" +
                $"VALUES('{Password}','{user.UserFirstName}','{user.UserFamilyName}',{user.UserRole.RoleID},'{user.UserPhoneNumber}',{user.UserActive})";
            int records = base.SaveChanges(sql);
            user.UserID = base.lastId;
            return records;
        }
        public int delete(User user)
        {
            user.UserActive = false;
            return Update(user);
        }
        public int Update(User user)
        {
            string sql = $"UPDATE Users SET UserFirstName = '{user.UserFirstName}', UserFamilyName = '{user.UserFamilyName}', RoleID = {user.UserRole.RoleID}, PhoneNumber = '{user.UserPhoneNumber}', Active = {user.UserActive}" +
                $" WHERE UserID={user.UserID}";
            return base.SaveChanges(sql);
        }
    }
}
