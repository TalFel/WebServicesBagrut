using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Model
{
    public class User : BaseEntity
    {
        //properties
        public int UserID {get; set;}
        public string UserFirstName { get; set; } = string.Empty;
        public string UserFamilyName { get; set; } = string.Empty;
        public int UserPassLength { get; set; }
        public Role UserRole { get; set; }
        public string UserPhoneNumber { get; set; } = string.Empty;
        public bool UserActive { get; set; }
        public RequestsLst Requests { get; set; }
        public OrdersLst Orders
        {
            get
            {
                OrderDB orderDB = new OrderDB();
                return orderDB.SelectByUserID(UserID);
            }
        }
        public bool IsAdmin {
            get
            {
                return UserRole.IsAdminRole;
            }
        }
        //empty constructor.
        public User(){ return; }
        //copy constructor.
        public User(User usrtocpy) {
            this.UserPassLength = usrtocpy.UserPassLength;
            this.UserID = usrtocpy.UserID;
            this.UserFirstName = usrtocpy.UserFirstName;
            this.UserFamilyName = usrtocpy.UserFamilyName;
            this.UserRole = new Role(usrtocpy.UserRole);
            this.UserPhoneNumber = usrtocpy.UserPhoneNumber;
            this.UserActive = usrtocpy.UserActive;
        }
        //new user with all properties.
        public User(int usrID, string usrFirstName, string usrFamilyName,
            Role usrRole, string usrPhoneNumber, bool usrApproved)
        {
            this.UserPassLength = 0;
            this.UserID = usrID;
            this.UserFirstName = usrFirstName;
            this.UserFamilyName = usrFamilyName;
            this.UserRole = usrRole;
            this.UserPhoneNumber = usrPhoneNumber;
            this.UserActive = usrApproved;
        }
        //new unapproved user.
        public User(int usrID, string usrFirstName, string usrFamilyName,
            Role usrRole, string usrPhoneNumber)
        {
            this.UserPassLength = 0;
            this.UserID = usrID;
            this.UserFirstName = usrFirstName;
            this.UserFamilyName = usrFamilyName;
            this.UserRole = usrRole;
            this.UserPhoneNumber = usrPhoneNumber;
            this.UserActive = false;
        }
        public User UpdateUser(string RoleVal, string RoleText, string firstName, string lastName, string cell, bool userActive)
        {
            UserDB usrDB = new UserDB();

            Role newUsrRole = new Role(int.Parse(RoleVal), RoleText);
            this.UserRole = newUsrRole;
            this.UserFirstName = firstName;
            this.UserFamilyName = lastName;
            this.UserPhoneNumber = cell;
            this.UserActive = userActive;

            usrDB.Update(this);

            return this;
        }
        public bool PhoneExistsOtherUser(string PhoneNumber)
        {
            UserDB usrDB = new UserDB();
            return usrDB.PhoneExistsOtherUser(PhoneNumber, this.UserID);
        }
        public int GetUserPassLength()
        {
            UserDB usrDB = new UserDB();
            return this.UserPassLength;
        }
        public void DeleteUser()
        {
            UserDB usrDB = new UserDB();
            usrDB.delete(this);
        }
        public RequestsLst SelectRequestOfUser()
        {
            RequestDB requestDB = new RequestDB();
            return requestDB.SelectRequestsByUser(this);
        }
    }
}
