using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Model
{
    public class Request : BaseEntity
    {
        public int RequestID { get; set; }
        public User RequestingUser { get; set; }
        public string RequestContent { get; set; } = string.Empty;
        public string ResponseString
        { 
            get
            {
                string Str = "";
                for(int i = 0; i < RequestContent.Length; i++)
                {
                    Str += RequestContent[i];
                    if (i % 50 == 0 && i != 0)
                        Str += "\n";
                }
                return Str;
            }

        }
        public RequestStatus RequestStatus { get; set; }
        public DateTime RequestDate { get; set; }
        public string RequestingUserName
        {
            get
            {
                return RequestingUser.UserFirstName + " " + RequestingUser.UserFamilyName;
            }
        }
        public string RequestDateShortString
        {
            get
            {
                return RequestDate.Day + "/" + RequestDate.Month + "/" + RequestDate.Year;
            }
        }
        public string RequestStatusString
        {
            get
            {
                return General.GetRequestStatusToString(this.RequestStatus);
            }
        }
        //empty constructor.
        public Request() { return; }
        //copy constructor.
        public Request(Request request)
        {
            this.RequestID = request.RequestID;
            this.RequestingUser = request.RequestingUser;
            this.RequestContent = request.RequestContent;
            this.RequestStatus = request.RequestStatus;
            this.RequestDate = request.RequestDate;
        }
        //new Request with all properties.
        public Request(int requestID, User requestingUser, string requestContent,
            RequestStatus requestStatus, DateTime requestDate)
        {
            this.RequestID = requestID;
            this.RequestingUser = requestingUser;
            this.RequestContent = requestContent;
            this.RequestStatus = requestStatus;
            this.RequestDate = requestDate;
        }
        //new Request without ID.
        public Request(User requestingUser, string requestContent,
            RequestStatus requestStatus, DateTime requestDate)
        {
            this.RequestingUser = requestingUser;
            this.RequestContent = requestContent;
            this.RequestStatus = requestStatus;
            this.RequestDate = requestDate;
        }
        public void UpdateRequestStatus(RequestStatus newStatus)
        {
            RequestDB reqDB = new RequestDB();
            this.RequestStatus = newStatus;
            reqDB.Update(this);
        }
        public void AddRequest()
        {
            RequestDB reqDB = new RequestDB();
            Request req = new Request(this.RequestingUser, this.RequestContent, this.RequestStatus, this.RequestDate);
            reqDB.Insert(req);
        }
    }
}