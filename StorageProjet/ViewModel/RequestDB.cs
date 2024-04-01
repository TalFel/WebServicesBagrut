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
    public class RequestDB : BaseDB
    {
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Request request = new Request();
            request.RequestID = int.Parse(reader["RequestID"].ToString());
            request.RequestContent = reader["RequestContent"].ToString();
            request.RequestStatus = (RequestStatus)int.Parse(reader["RequestProcessed"].ToString());
            request.RequestDate = DateTime.Parse(reader["RequestDate"].ToString());
            User u = new User();
            u.UserID = int.Parse(reader["Users.UserID"].ToString());
            u.UserFirstName = reader["UserFirstName"].ToString();
            u.UserFamilyName = reader["UserFamilyName"].ToString();
            RoleDB tempRole = new RoleDB();
            u.UserRole = tempRole.SelectByRoleID(int.Parse(reader["RoleID"].ToString()));
            u.UserPhoneNumber = reader["PhoneNumber"].ToString();
            u.UserActive = Convert.ToBoolean(reader["Active"].ToString());
            request.RequestingUser = u;

            return request;
        }

        protected override BaseEntity NewEntity()
        {
            return new Request();
        }
        public Request SelectByRequestID(int ID)
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM Requests inner join Users WHERE RequestID=@ID AND NOT RequestProcessed = 3";
            command.Parameters.Add(new OleDbParameter("@ID", ID));
            List<BaseEntity> list = base.Select();
            if (list.Count == 1) return list[0] as Request;
            return null;
        }
        public RequestsLst SelectRequestsDB()
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM [Requests] INNER JOIN [Users] ON Requests.UserID = Users.UserID WHERE NOT RequestProcessed = 3";
            List<BaseEntity> list = base.Select();
            if (list.Count == 0)
                return null;
            return new RequestsLst(list);
        }
        public RequestsLst SelectConditionedRequestsDB(string sqlCondition)
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM [Requests] INNER JOIN [Users] ON Requests.UserID = Users.UserID " + sqlCondition;
            List<BaseEntity> list = base.Select();
            if (list.Count == 0)
                return null;
            return new RequestsLst(list);
        }
        public int Insert(Request req)
        {
            string sql = $"INSERT INTO Requests(UserID, RequestContent, RequestProcessed, RequestDate)" +
                $" VALUES({req.RequestingUser.UserID}, '{req.RequestContent}', {(int)req.RequestStatus}, #{General.toDateConverter(req.RequestDate)}#)";
            int records = base.SaveChanges(sql);
            req.RequestID = base.lastId;
            return records;
        }
        public int delete(Request req)
        {
            string sql = $"Delete From Requests WHERE UserID={req.RequestID}";
            return base.SaveChanges(sql);
        }
        public int Update(Request req)
        {
            string sql = $"UPDATE [Requests] SET UserID = {req.RequestingUser.UserID}, RequestContent='{req.RequestContent}', RequestProcessed={(int)req.RequestStatus}, RequestDate=#{General.toDateConverter(req.RequestDate)}#" +
                $" WHERE RequestID={req.RequestID}";
            return base.SaveChanges(sql);
        }
        public RequestsLst SelectRequestsByUser(User user)
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM [Requests] INNER JOIN [Users] ON Requests.UserID = Users.UserID WHERE Users.UserID=@UID AND NOT RequestProcessed=3";
            command.Parameters.Add(new OleDbParameter("@UID", user.UserID));
            List<BaseEntity> list = base.Select();
            if (list.Count == 0)
                return null;
            return new RequestsLst(list);
        }
        public int NuberOfRequestsForToday()
        {
            RequestsLst requests = SelectRequestsDB();
            RequestsLst forToday = new RequestsLst(requests.FindAll(req => { return req.RequestStatus != RequestStatus.Deleted && req.RequestDate.Date == DateTime.Today.Date; }));
            return forToday.Count;
        }

        public int NumberOfRequestsRequestedForToday()
        {
            RequestsLst requests = SelectRequestsDB();
            RequestsLst forToday = new RequestsLst(requests.FindAll(req => { return req.RequestStatus == RequestStatus.Requested && req.RequestDate.Date == DateTime.Today.Date; }));
            return forToday.Count;
        }
        public int NumberOfRequestsInProcessForToday()
        {
            RequestsLst requests = SelectRequestsDB();
            RequestsLst forToday = new RequestsLst(requests.FindAll(req => { return req.RequestStatus == RequestStatus.InProcess && req.RequestDate.Date == DateTime.Today.Date; }));
            return forToday.Count;
        }
        public int NumberOfRequestsCompletedForToday()
        {
            RequestsLst requests = SelectRequestsDB();
            RequestsLst forToday = new RequestsLst(requests.FindAll(req => { return req.RequestStatus == RequestStatus.Completed && req.RequestDate.Date == DateTime.Today.Date; }));
            return forToday.Count;
        }
        //public enum RequestStatus
        //{
        //    Requested,
        //    InProcess,
        //    Completed,
        //    Deleted
        //}
    }
}
