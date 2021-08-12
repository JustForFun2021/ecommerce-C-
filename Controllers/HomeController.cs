using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using YodiStore.YodiServiceRef;

namespace YodiStore.Controllers
{

    public class HomeController : Controller
    {
        static YodiServiceClient c = new YodiServiceClient();
        public ActionResult Chat()
        {
            User user = (User)Session["User"];
            ViewBag.User = user;
            return View();
        }


        public ActionResult UserJson(int userId)
        {
            List<User> users = c.ShowUser().ToList();

            return Json(users, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Index()
        {
            var friends = c.GetFriends().ToList();
            ViewBag.Friends = friends;

            var Categories = c.GetCategories().ToList();
            ViewBag.category = Categories;

            var SubCategory = c.GetSubCategories().ToList();
            ViewBag.SubCategory = SubCategory;

            var Products = c.GetProducts().ToList();
            ViewBag.Products = Products;

            return View();
        }

        public ActionResult PrivatArea(int id)
        {

            ViewBag.dff = null;
            string a = c.userCategories(id).FirstOrDefault();

            if (a != null)
            {
                List<Product> userProducts = c.GetSellerProduct(id).ToList().FindAll(x => x.Category.CategoryName == a);
                ViewBag.dff = userProducts;

            }

            List<OrderDetail> orders = c.OrdesrDetailsUserBuy(id).ToList();
            ViewBag.BuerOrder = orders;

            return View();
        }


        public ActionResult PrivatAreaBuyer(int Id)
        {

            List<OrderDetail> orders = c.OrdesrDetailsUserBuy(Id).ToList();

            return Json(orders, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetSubCategoryByCategory(int categorId)
        {

            List<SubCategory> subCategory = c.GetSubCategoriesByIdCategory(categorId).ToList();

            return Json(subCategory, JsonRequestBehavior.AllowGet);
        }


        public ActionResult PrivatAreaSeller(int Id)
        {

            List<OrderDetail> ordersd = c.OrdesrDetailsUserSell(Id).ToList();

            return Json(ordersd, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ProductRatingUpdate(float newRating, int ProId)
        {
            Product product = c.GetProduct(ProId);
            product.Retad = newRating;
            c.updateProduct(product);

            return RedirectToAction("PrivatArea", new { id = product.UserId });
        }


        public ActionResult SellProduct()
        {

            List<Category> Category = c.GetCategories().ToList();
            List<SubCategory> SubCategory = c.GetSubCategories().ToList();
            ViewBag.Categorys = Category;
            ViewBag.SubCategorys = SubCategory;
            return View();
        }
        [HttpGet]
        public ActionResult EditProduct(int productId)
        {
            Product product = c.GetProduct(productId);
            ViewBag.Product = product;
            List<Category> Category = c.GetCategories().ToList();
            List<SubCategory> SubCategory = c.GetSubCategories().ToList();
            ViewBag.Categorys = Category;
            ViewBag.SubCategorys = SubCategory;
            return View();
        }

        public JsonResult UploadFile(HttpPostedFile productImag)
        {
            string image = "";
            if (productImag != null)
            {
                image = System.IO.Path.GetFileName(productImag.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/Imges_Of_Db"), image);
                productImag.SaveAs(path);


            }

            return Json(image, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult uploadUserImage(int userID, HttpPostedFileBase userImag)
        {

            string image = "";
            User user = c.GetUserById(userID);
            if (userImag != null)
            {
                image = System.IO.Path.GetFileName(userImag.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/Imges_Of_Db"), image);
                userImag.SaveAs(path);

                user.Image = image;
                c.updateUser(user);

            }

            return RedirectToAction("PrivatArea", new { id = user.UserId });
        }


        [HttpPost]
        public ActionResult SellProduct(string productName, HttpPostedFile productImag, int category, int SubCategory, string Description, string condition,
            string brand, decimal price, string shippinTo, string other, int shippingCost, int orderBusinessDays, int deliveryMaximumDay, string paidBy)
        {

            string image = "";
            if (productImag != null)
            {
                image = System.IO.Path.GetFileName(productImag.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/Imges_Of_Db"), image);
                productImag.SaveAs(path);


            }

            string whereTo = shippinTo == "worldWide" ? "worldWide" : other;
            User user = (User)Session["User"];

            Shipper newShipper = new Shipper()
            {
                ShippingTo = shippinTo,
                ShippingCost = shippingCost,
                OrderBusinessDays = orderBusinessDays,
                MaxDeliveryDays = deliveryMaximumDay,
                GetPaidBy = paidBy

            };
            c.AddShipper(newShipper);

            Product newProduct = new Product()
            {
                UserId = user.UserId,
                ProductName = productName,
                CategoryId = category,
                SubCatgoreyId = SubCategory,
                Description = Description,
                ProductCondetion = condition,
                Brand = brand,
                Price = price,
            };

            newProduct.Shipper = newShipper;
            c.AddProduct(newProduct);

            return View();
        }



        [HttpPost]
        public ActionResult EditProduct(int productId, int categoryId, string brand, int subCategoryId
            , float discount, decimal price, string condition, string name, int unitInStock, string description, HttpPostedFileBase file)
        {
            string pic = "";
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/Imges_Of_Db"), pic);
                file.SaveAs(path);
            }

            Product product = c.GetProduct(productId);



            product.Description = description;
            if (pic != null)
            {
                product.Image = pic;
            }
            product.Discount = discount;
            product.Price = price;
            product.ProductCondetion = condition;
            product.ProductName = name;
            product.UnitInStock = unitInStock;
            product.SubCatgoreyId = subCategoryId;


            c.updateProduct(product);
            TempData["UpdateSuccess"] = "Update Success";

            return RedirectToAction("PrivatArea", new { id = product.UserId });
        }



        public JsonResult SearchPro(string term)
        {
            //List<string> cName;
            //cName = c.GetCategories().ToList().Where(x => x.CategoryName.StartsWith(term))
            //    .Select(y => y.CategoryName).ToList();
            //var productList1 = c.GetProducts().ToList();
            //var productList2 = productList1.Where(p => p.ProductName.Contains(term)).Take(10); //.Select(x => new { label = x.ProductName })

            //List<string> productName = new List<string>();

            //productName = c.SearchProduct(term).ToList();

            var products = c.GetProducts().ToList();
            var product2 = products.Where(p => p.ProductName.Contains(term)).ToList();

            return Json(product2.ToArray(), JsonRequestBehavior.AllowGet);
        }


        public JsonResult FindFriends(string term)
        {
            var users1 = c.GetUsers().ToList();
            var users2 = users1.Where(u => u.FirstName == term).ToList();

            return Json(users2.ToArray(), JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult SearchProduct(string term)
        {
            Product product = c.GetProducts().ToList().Find(p => p.ProductName == term);
            return View("SingleProduct", product.ProductId);
        }

        public JsonResult SearchSellerProducts(string term, int UserID)
        {
            List<string> cName;
            cName = c.GetSellerProduct(UserID).ToList().FindAll(x => x.ProductName.Contains(term))
                .Select(y => y.ProductName).ToList();


            return Json(cName, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Categry(int categoryId)
        {
            var Productlist = c.GetProducts().Where(p => p.CategoryId == categoryId).ToList();
            var categoryName = Productlist.Select(x => x.Category.CategoryName).FirstOrDefault();
            ViewBag.categoryName = categoryName;
            return View("Categry", Productlist);
        }


        public ActionResult SubCategory(int subCategoryId)
        {
            var Products = c.GetProducts().Where(x => x.SubCatgoreyId == subCategoryId).ToList();
            var CategoryName = Products.Select(y => y.Category.CategoryName).FirstOrDefault();
            var subCategoryName = Products.Select(s => s.SubCatgorey.SubCategoryName).FirstOrDefault();
            ViewBag.CategoryName = CategoryName;
            ViewBag.subCategoryName = subCategoryName;
            var CategoryId = Products.Select(h => h.CategoryId).ToArray();
            ViewBag.CategoryId = CategoryId;
            return View("SubCategory", Products);
        }

        public ActionResult SingleProduct(int productId)
        {

            var product = c.GetProduct(productId);
            ViewBag.Product = product;
            ViewBag.Added = "";


            return View();
        }
        public JsonResult UserProducts(int userId, string categoreyName)
        {
            List<Product> userProducts = c.GetSellerProduct(userId).ToList().FindAll(x => x.Category.CategoryName == categoreyName);

            return Json(userProducts, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UserPage(int useId)
        {
            ViewBag.User = c.GetUserById(useId);

            ViewBag.dff = null;
            string a = c.userCategories(useId).FirstOrDefault();

            if (a != null)
            {
                List<Product> userProducts = c.GetSellerProduct(useId).ToList().FindAll(x => x.Category.CategoryName == a);
                ViewBag.dff = userProducts;

            }

            return View();
        }

        //public JsonResult UserProducts(int userId, string categoreyName)
        //{
        //    List<Product> userProducts = c.GetSellerProduct(userId).ToList().FindAll(x => x.Category.CategoryName == categoreyName);

        //    return Json(userProducts, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult UserProctsCategorey(int Id)
        {
            return Json(c.userCategories(Id), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Contact(string Name, string email, string Message)
        {
            MailMessage mailMessage = new MailMessage("theyodistore@gmail.com", "theyodistore@gmail.com");
            mailMessage.Subject = "Message";
            mailMessage.Body = "From: " + email + "To: " + "theyodistore@gmail.com" + " " + Message;
            SmtpClient smtpClient = new SmtpClient();
            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (SmtpException e)
            {


                ViewBag.Message = e.Message;
            }


            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }



        public ActionResult CurrentCart()
        {
            User user = (User)Session["User"];

            ViewBag.User = user;
            ViewBag.OrderDetails = c.GetOrderDetails().ToList();
            return View();
        }

        public JsonResult EditQuantity(int quantity, int orderDetailsId)
        {
            OrderDetail orderDetail = c.GetOrderDetailById(orderDetailsId);
            orderDetail.Quantity = quantity;
            return Json(orderDetail, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Addtocart(int quantity, int productId)
        {
            Product product = c.GetProduct(productId);
            try
            {
                if (Session["User"] == null)
                {
                    throw new ApplicationException("Please Sing In");
                }

                User user = (User)Session["User"];

                Order order = YodiStore.Models.Model.GiveMeTheAppropriateOrder(user.UserId);
                int orderId = order.OrderId;


                if (YodiStore.Models.Model.checkedIfProductExistingInTheCurrentcart(productId, orderId))
                {
                    YodiStore.Models.Model.AddQuantityToProductInCurrentCart(productId, quantity);
                }
                else
                {
                    YodiStore.Models.Model.AddProductToCart(user.UserId, productId, order, quantity);
                }
                ViewBag.Product = product;
                ViewBag.Added = "Product successfully added";

            }
            catch (Exception ex)
            {
                ViewBag.Product = product;
                ViewBag.Message = ex.Message;
            }

            return RedirectToAction("SingleProduct", new { productId = productId });
        }



        [HttpPost]
        public ActionResult RemoveProduct(int OrderDetailId)
        {
            User user = (User)Session["User"];
            c.RemoveProductFromCart(OrderDetailId);
            ViewBag.User = user;
            ViewBag.OrderDetails = c.GetOrderDetails().ToList();

            return View("CurrentCart");
        }


        [HttpPost]
        public ActionResult BuyingProcess(string FirstName, string LastName, string Country, string City,
            string Street, string ZipCode, string Notes, int OrderId)
        {
            try
            {
                Order od = c.GetOrderId(OrderId);
                if (od.OrderDate == null)
                {
                    ViewBag.OrderDetail = c.GetOrderDetails().ToList();
                    User user = (User)Session["User"];
                    ViewBag.User = user;

                    user.FirstName = FirstName;
                    user.LastName = LastName;
                    user.Country = Country;
                    user.City = City;
                    user.addresses = Street;
                    user.PostalCode = ZipCode;

                    Note note = new Note();
                    note.User = user;
                    note.OrderId = OrderId;
                    note.NoteBody = Notes;

                    c.AddNote(note);
                    c.CloseOrder(od);

                    ViewBag.Buying = "congratulations!your product has been ordered!";

                    return View("CurrentCart");
                }
                else
                {
                    ViewBag.massge = "The order was ordered!";
                }

            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }

            return View();
        }

        [HttpGet]
        public ActionResult BuyingProcess()
        {
            ViewBag.OrderDetail = c.GetOrderDetails().ToList();
            User user = (User)Session["User"];
            ViewBag.User = user;

            return View();
        }



        public ActionResult FriendsPage()
        {

            try
            {
                 if (Session["User"] == null)
                {
                    throw new ApplicationException("Please Sing In");
                }

                 User user = (User)Session["User"];


                 var Friends = c.GetFriends().ToList().Where(u => u.User1 == user.UserId).ToList();
                 List<User> Myfriends = new List<User>();

                 foreach (var item in Friends)
                 {
                     Myfriends.Add(c.GetUserById(item.Friend));
                 }
                 if (Myfriends.Count == 0)
                 {
                      throw new ApplicationException("Invite friends..");
                 }
                 ViewBag.Friends = Myfriends;


            }
            catch (Exception ex)
            {

                ViewBag.Message = ex.Message;
            }
         

            return View();
        }

        [HttpPost]
        public ActionResult AddToWishList(int productId)
        {
            Product product = c.GetProduct(productId);
            ViewBag.Product = product;

            try
            {
                 if (Session["User"] == null)
                {
                    throw new ApplicationException("Please Sing In");
                }

                 User user = (User)Session["User"];

                 int userId = user.UserId;


                 c.AddProductToWishList(productId, userId);

                 

            }
            catch (Exception ex)
            {
               
                ViewBag.Message = ex.Message;
            }



            return View("SingleProduct", ViewBag.Product);
        }




       
        public ActionResult SeeWishList(int UserId)
        {

            User user = c.GetUserById(UserId);
            List<WishList> wishList = c.GetWishList().ToList();

            ViewBag.user = user;
            ViewBag.WishList = wishList;
           
            return View();

        }


        [HttpGet]
        public ActionResult surprise(int UserId, int productId)
        {
            User user = c.GetUserById(UserId);
            Product product = c.GetProduct(productId);
            Order order = c.Makeorder();
            //OrderDetail orderDtail = c.Makeorderdetails();
           

            ViewBag.User = user;
            ViewBag.Product = product;
            ViewBag.Order = order;
           // ViewBag.OrderDtail = orderDtail;
            return View();
        }


        [HttpPost]
        public ActionResult surprise(string FirstName, string LastName, string Country, string City,
            string Street, string ZipCode, string Notes, int OrderId)
        {
            try
            {
                Order od = c.GetOrderId(OrderId);
                if (od.OrderDate == null)
                {
                    ViewBag.OrderDetail = c.GetOrderDetails().ToList();
                    User user = (User)Session["User"];
                    ViewBag.User = user;

                    user.FirstName = FirstName;
                    user.LastName = LastName;
                    user.Country = Country;
                    user.City = City;
                    user.addresses = Street;
                    user.PostalCode = ZipCode;

                    Note note = new Note();
                    note.User = user;
                    note.OrderId = OrderId;
                    note.NoteBody = Notes;

                    c.AddNote(note);
                    c.CloseOrder(od);
                    ViewBag.Buying = "congratulations!your product has been ordered!";
                }
                else
                {
                    ViewBag.massge = "The order was ordered!";
                }

            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }

            return View();

        }

    }
}