using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Model
{
    public class General
    {
        public static string ProjectFolder = "D:/StorageProjet/";
        //public static string ProjectFolder = "D:/StorageProjet/";
        /*-------------------SignUp/In-------------------*/
        /*-------------------Profile & Admin-------------------*/
        /*-------------------UsersAdmin-------------------*/
        public static User GetUser(string cell, string pass)
        {
            UserDB usrDB = new UserDB();
            return usrDB.SelectUserLogin(cell, pass);
        }
        public static User CreateNewUser(string RoleVal, string RoleText, string firstName, string lastName, string cell, string password)
        {
            UserDB usrDB = new UserDB();

            Role newUsrRole = new Role(int.Parse(RoleVal), RoleText);
            User newUsr = new User(0, firstName, lastName, newUsrRole, cell);

            usrDB.Insert(newUsr, password);

            return newUsr;
        }
        public static bool PhoneExists(string PhoneNumber)
        {
            UserDB usrDB = new UserDB();
            return usrDB.PhoneExists(PhoneNumber);
        }
        public static UsersLst SelectUsersDB()
        {
            UserDB usrDB = new UserDB();
            return usrDB.GetAllUsers();
        }
        public static UsersLst SelectUsersConditionedDB(string FirstNameTB, string LastNameTB, string PhoneTB, bool Old, int role)
        {
            UserDB usrDB = new UserDB();
            return usrDB.SelectUsersConditioned( FirstNameTB, LastNameTB, PhoneTB, Old, role);
        }
        /*-------------------RolesAdmin-------------------*/
        public static RolesLst SelectRolesDB()
        {
            RoleDB rolDB = new RoleDB();
            return rolDB.SelectRolesDB();
        }
        public static bool RoleNameExists(string name)
        {
            RoleDB rolDB = new RoleDB();
            return rolDB.NameExists(name);
        }
        public static RolesLst SelectRolesConditioned(string Rolename)
        {
            RoleDB rolDB = new RoleDB();
            return rolDB.SelectRolesConditioned(Rolename);
        }
        /*-------------------Requests-------------------*/

        public static RequestsLst SelectRequestsDB()
        {
            RequestDB reqDB = new RequestDB();
            return reqDB.SelectRequestsDB();
        }
        public static string GetRequestStatusToString(RequestStatus status)
        {
            switch ((int)status)
            {
                case 0:
                    return "חדש";
                case 1:
                    return "בטיפול";
                case 2:
                    return "טופל";
                default:
                    return "נמחק";
            }
        }
        public static RequestsLst SelectConditionedRequests(string sqlCondition)
        {
            RequestDB reqDB = new RequestDB();
            return reqDB.SelectConditionedRequestsDB(sqlCondition);
        }
        /*-------------------Sizes-------------------*/
        public static SizesLst SelectSizesDB()
        {
            SizeDB sizeDB = new SizeDB();
            return sizeDB.SelectSizeDB();
        }
        public static SizesLst SelectSizesConditioned(string condition)
        {
            SizeDB sizeDB = new SizeDB();
            return sizeDB.SelectSizesConditioned(condition);
        }
        public static bool SizeNameExists(string name)
        {
            SizeDB sizeDB = new SizeDB();
            return sizeDB.NameExists(name);
        }
        /*-------------------Colors-------------------*/
        public static ColorLst SelectColorsDB()
        {
            ColorDB colorDB = new ColorDB();
            return colorDB.SelectColorsDB();
        }
        public static ColorLst SelectColorsConditioned(string condition)
        {
            ColorDB colorDB = new ColorDB();
            return colorDB.SelectColorsConditioned(condition);
        }
        public static bool ColorNameExists(string name)
        {
            ColorDB colorDB = new ColorDB();
            return colorDB.ColorNameExists(name);
        }
        /*-----------------------------------------*/
        public static CategoriesLst SelectCategoriesDB()
        {
            CategoryDB categoryDB = new CategoryDB();
            return categoryDB.SelectCategoriesDB();
        }
        public static CategoriesLst SelectCategoriesConditioned(string condition, bool active)
        {
            CategoryDB categoryDB = new CategoryDB();
            return categoryDB.SelectCategoriesConditioned(condition, active);
        }
        public static bool ImageNameExists(string name)
        {
            CategoryDB categoryDB = new CategoryDB();
            return categoryDB.ImageNameExists(name);
        }
        public static bool CategoryNameExists(string name)
        {
            CategoryDB categoryDB = new CategoryDB();
            return categoryDB.NameExists(name);
        }
        /*-------------------------------------------*/
        public static ProductsLst SelectProductsDB()
        {
            ProductDB productDB = new ProductDB();
            return productDB.SelectProductsDB();
        }
        public static DateTime GetDateWithoutMilliseconds(DateTime d)
        {
            return new DateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, d.Second);
        }
        public static string StatusToString(Status status)
        {
            switch (status)
            {
                case Status.NotOrdered:
                    return "בוטל";
                    break;
                case Status.Ordered:
                    return "הוזמן";
                    break;
                case Status.InProcess:
                    return "בהכנה";
                    break;
                case Status.Ready:
                    return "מוכן";
                    break;
                case Status.Given:
                    return "הובא";
                    break;
                case Status.Returned:
                    return "הוחזר";
                    break;
                default:
                    return "שאל את המחסנאי";
            }
        }
        public static OrdersLst SelectOrdersDB()
        {
            OrderDB ODB = new OrderDB();
            return ODB.SelectAll();
        }
        public static OrdersLst SelectOrdersByNameAndOrdered(string name, string orderby, bool includePast)
        {
            OrderDB ODB = new OrderDB();
            return ODB.SelectOrdersByNameAndOrdered(name, orderby, includePast);
        }
        public static int NuberOfOrdersForToday()
        {
            OrderDB odb = new OrderDB();
            return odb.NuberOfOrdersForToday();
        }
        public static int NumberOfOrdersOrderedForToday()
        {
            OrderDB odb = new OrderDB();
            return odb.NumberOfOrdersOrderedForToday();
        }
        public static int NumberOfOrdersInProcessForToday()
        {
            OrderDB odb = new OrderDB();
            return odb.NumberOfOrdersInProcessForToday();
        }
        public static int NumberOfOrdersCompletedForToday()
        {
            OrderDB odb = new OrderDB();
            return odb.NumberOfOrdersCompletedForToday();
        }
        public static int NuberOfRequestsForToday()
        {
            RequestDB rdb = new RequestDB();
            return rdb.NuberOfRequestsForToday();
        }
        public static int NumberOfRequestsRequestedForToday()
        {
            RequestDB rdb = new RequestDB();
            return rdb.NumberOfRequestsRequestedForToday();
        }
        public static int NumberOfRequestsInProcessForToday()
        {
            RequestDB rdb = new RequestDB();
            return rdb.NumberOfRequestsInProcessForToday();
        }
        public static int NumberOfRequestsCompletedForToday()
        {
            RequestDB rdb = new RequestDB();
            return rdb.NumberOfRequestsCompletedForToday();
        }
        public static BestProductsLst Get3MostOrderedProducts()
        {
            BestProductsDB piodb = new BestProductsDB();
            BestProductsLst lst = piodb.GetBestProducts();
            if (lst.Count < 3)
                return null;
            BestProductsLst prodLst = new BestProductsLst();
            for(int i = 0; i < 3; i++)
            {
                prodLst.Add(lst[i]);
            }
            return prodLst;
        }
        public static string toDateConverter(DateTime date)
        {
            return date.Date.Year + "-" + date.Date.Month + "-" + date.Date.Day;
        }
    }
}