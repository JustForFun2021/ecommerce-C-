using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;



namespace IService
{
    [ServiceContract]
    public interface IYodiService
    {
        /*------ All Get ------*/

        //Get Categories
        [OperationContract]
        List<Category> GetCategories();


        //Get Sub Categories
        [OperationContract]
        List<SubCategory> GetSubCategories();


        //Get Products
        [OperationContract]
        List<Product> GetProducts();


        //Get Product
        [OperationContract]
        Product GetProduct(int proId);


        //Get User
        [OperationContract]
        User GetUser(string email, string password);

        [OperationContract]
        List<Product> GetSellerProduct(int userId);

        [OperationContract]
        List<string> userCategories(int userId);


        // Get Order
        [OperationContract]
        Order GetOrderId(int OrderId);


        ////[OperationContract]
        ////Order GetCurrentOrder(int userId);

        [OperationContract]
        List<OrderDetail> GetOrderDetails();

        [OperationContract]
        OrderDetail GetOrderDetailById(int orderDetailid);

        [OperationContract]
        User GetUserById(int userId);

        [OperationContract]
        Shipper GetShiper(int ProId);

        [OperationContract]
        List<Order> GetOrders();

        [OperationContract]
        OrderDetail FindOrderDetailByProduct(int productId);

        [OperationContract]
        List<User> GetUsers();

        [OperationContract]
        List<Friends> GetFriends();

        [OperationContract]
        int GetuserId(User user);


        [OperationContract]
        List<WishList> GetWishList();


        [OperationContract]
        Order GetOrderByOrder(Order order);


        [OperationContract]
        Order Makeorder();

        [OperationContract]
        OrderDetail Makeorderdetails();

        /*------ All Add ------*/

        //Add User
        [OperationContract]
        void AddUser(User user);


        //Add Order Details
        [OperationContract]
        void AddOrderDetail(OrderDetail o);


        [OperationContract]
        List<SubCategory> GetSubCategoriesByIdCategory(int categoreyId);

        // Add Product
        [OperationContract]
        void AddProduct(Product product);


        // Add Shipper
        [OperationContract]
        void AddShipper(Shipper shipper);


        // Add Order
        [OperationContract]
        void AddOrder(Order order);

        // Add Note
        [OperationContract]
        void AddNote(Note note); 


        // Add Friend
        [OperationContract]
        void AddFriend(int me, int friend);


        // Add ProductToWishList
        [OperationContract]
        void AddProductToWishList(int productId, int userId);

        /*------ User Stuff ------*/

        //Ordesr Details User Sell
        [OperationContract]
        List<OrderDetail> OrdesrDetailsUserSell(int userId);


        //Ordesr Details User Buy
        [OperationContract]
        List<OrderDetail> OrdesrDetailsUserBuy(int userId);


        //update User
        [OperationContract]
        void updateUser(User user);

        [OperationContract]
        void updateProduct(Product product);

        //Show User
        [OperationContract]
        List<User> ShowUser();



        //Search if user name is exist
        [OperationContract]
        bool SearchUsername(string username);


        [OperationContract]
        bool LogInUser(string email, string password);


        /*-----Remove---*/

        [OperationContract]
        void RemoveProductFromCart(int orderdetail);


        /*------ Search Product ------*/

        [OperationContract]
        List<string> SearchProduct(string productName);


        [OperationContract]
        void CloseOrder(Order order);

        




    }
}
