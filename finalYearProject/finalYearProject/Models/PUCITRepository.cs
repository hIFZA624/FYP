using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace finalYearProject.Models
{
    public class PUCITRepository
    {
        string connString = null;
        SqlConnection con;
        public  List<ContactUs> complaint = new List<ContactUs>();

        public List<Receipt> restaurantOrderListres = new List<Receipt>();
        public List<Receipt> restaurantOrderList = new List<Receipt>();
        public List<Receipt> restaurantPendingOrder = new List<Receipt>();
        public List<Receipt> restaurantPendingOrderres = new List<Receipt>();
        public List<Restaurent> restaurents = new List<Restaurent>();
        public List<PUCITProducts> temp = new List<PUCITProducts>();
        public List<PUCITCafeCategory> Category = new List<PUCITCafeCategory>();
        public List<PUCITProducts> products = new List<PUCITProducts>();
        public List<PUCITCafeCategory> CategoryPharmacy = new List<PUCITCafeCategory>();
        public List<PUCITProducts> productsPharmacy = new List<PUCITProducts>();
        public List<RestaurentMenu> restaurentsMenu = new List<RestaurentMenu>();
        public List<RestaurentMenu> tempres = new List<RestaurentMenu>();
        public List<CartAndOrder> Cart = new List<CartAndOrder>();
        public List<CartAndOrder> CarttEMP = new List<CartAndOrder>();
        public List<CartAndOrder> order = new List<CartAndOrder>();
        public  List<CommentBox> comments = new List<CommentBox>();
        public PUCITRepository()
        {
              connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RestaurantAndCafe;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
               con = new SqlConnection(connString);
            try
            {
                con.Open();
                string query21 = $"Select * from ContactUs";
                SqlCommand cmd21 = new SqlCommand(query21, con);
                SqlDataReader dr21 = cmd21.ExecuteReader();
                while (dr21.Read())
                {
                    ContactUs c = new ContactUs();
                    c.Id = dr21.GetInt32(0);
                    c.FirstName = dr21.GetValue(1).ToString();
                    c.LastName = dr21.GetValue(2).ToString();
                    c.Email = dr21.GetValue(3).ToString();
                    c.Message = dr21.GetValue(4).ToString();


                    complaint.Add(c);
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                con.Close();
            }
            //Receipt database 
            con.Open();
                string query20 = $"Select * from Comments";
                SqlCommand cmd20 = new SqlCommand(query20, con);
                SqlDataReader dr20 = cmd20.ExecuteReader();
                while (dr20.Read())
                {
                CommentBox c = new CommentBox();
                c.CommentNo = dr20.GetInt32(0);
                c.Name = dr20.GetValue(1).ToString();
                c.date = dr20.GetValue(2).ToString();
                c.UserID = dr20.GetValue(3).ToString();
                c.Comment = dr20.GetValue(4).ToString();

                comments.Add(c);
               }
            
            
                con.Close();
           
            con.Open();
            string query13 = $"Select * from Receipt";
            SqlCommand cmd13 = new SqlCommand(query13, con);
            SqlDataReader dr13 = cmd13.ExecuteReader();
            while (dr13.Read())
            {
                Receipt r = new Receipt();
                r.ReceiptID = dr13.GetInt32(0);
                r.UserID = dr13.GetValue(1).ToString();
                r.RestaurentID = dr13.GetValue(2).ToString();
                r.TotalAmount = dr13.GetValue(3).ToString();
                r.NoOfItems = dr13.GetInt32(4);
                r.UserTime = dr13.GetValue(5).ToString();
                r.OrderTime = dr13.GetDateTime(6);
                r.OrderAccept = dr13.GetInt32(7);
                r.OrderReceived = dr13.GetInt32(8);

                restaurantPendingOrder.Add(r);


            }
            con.Close();

            con.Open();
            string query11 = $"Select * from Orders";
            SqlCommand cmd11 = new SqlCommand(query11, con);
            SqlDataReader dr11 = cmd11.ExecuteReader();
            while (dr11.Read())
            {
                CartAndOrder r = new CartAndOrder();
                r.CartID = dr11.GetInt32(0);
                r.RestaurentID = dr11.GetValue(1).ToString();
                r.MenuID = dr11.GetInt32(2);
                r.UserID = dr11.GetValue(3).ToString();
                r.NameOfItem = dr11.GetValue(4).ToString();
                r.Price = dr11.GetValue(5).ToString();
                r.OriginalPrice = dr11.GetValue(6).ToString();
                r.Quantity = dr11.GetInt32(7);
                r.ReceiptID = dr11.GetInt32(8);
                order.Add(r);
            }


            con.Close();

            con.Open();
            string query10 = $"Select * from Cart";
            SqlCommand cmd10 = new SqlCommand(query10, con);
            SqlDataReader dr10 = cmd10.ExecuteReader();
            while (dr10.Read())
            {
                CartAndOrder r = new CartAndOrder();
                r.CartID = dr10.GetInt32(0);
                r.RestaurentID = dr10.GetValue(1).ToString();
                r.MenuID = dr10.GetInt32(2);
                r.UserID = dr10.GetValue(3).ToString();
                r.NameOfItem = dr10.GetValue(4).ToString();
                r.Price = dr10.GetValue(5).ToString();
                r.OriginalPrice = dr10.GetValue(6).ToString();
                r.Quantity = dr10.GetInt32(7);
                Cart.Add(r);

            }
            con.Close();
            //
            try
            {
                con.Open();
                string query5 = $"Select * from Restaurent";
                SqlCommand cmd5 = new SqlCommand(query5, con);
                SqlDataReader dr5 = cmd5.ExecuteReader();
                while (dr5.Read())
                {
                    Restaurent r = new Restaurent();
                    r.RestaurentID = dr5.GetValue(0).ToString();
                    r.NameOfRestaurants = dr5.GetValue(1).ToString();
                    r.Location = dr5.GetValue(2).ToString();
                    r.PhoneNo = dr5.GetValue(3).ToString();
                    r.OpenUntil = dr5.GetValue(4).ToString();
                    r.DeliveryCharges = dr5.GetValue(5).ToString();
                    r.PhotoPATH = dr5.GetValue(6).ToString();
                    r.Password = dr5.GetValue(7).ToString();
                    restaurents.Add(r);
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                con.Close();
            }
            con.Open();
            string query1 = "Select * from PucitCategory";
            SqlCommand cmd1 = new SqlCommand(query1, con);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                PUCITCafeCategory productCategory = new PUCITCafeCategory();
                productCategory.Id = System.Convert.ToInt32(dr1.GetValue(0));
                productCategory.CategoryName = dr1.GetValue(1).ToString();
                productCategory.Description = dr1.GetValue(2).ToString();
                productCategory.PhotoPath = dr1.GetValue(3).ToString();
                Category.Add(productCategory);
            }
            con.Close();
            con.Open();
            string query = "Select * from PucitProducts";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                PUCITProducts product = new PUCITProducts();
                product.Id = System.Convert.ToInt32(dr.GetValue(0));
                product.Category = dr.GetValue(1).ToString();
                product.Name = dr.GetValue(2).ToString();
                product.Price = System.Convert.ToInt32(dr.GetValue(3));
                product.Quantity = System.Convert.ToInt32(dr.GetValue(4));
                product.PhotoPath = dr.GetValue(5).ToString();
                products.Add(product);
            }
            con.Close();

            //
            con.Open();
            string query3 = "Select * from PharmacyCategory";
            SqlCommand cmd3 = new SqlCommand(query3, con);
            SqlDataReader dr3 = cmd3.ExecuteReader();
            while (dr3.Read())
            {
                PUCITCafeCategory productCategory = new PUCITCafeCategory();
                productCategory.Id = System.Convert.ToInt32(dr3.GetValue(0));
                productCategory.CategoryName = dr3.GetValue(1).ToString();
                productCategory.Description = dr3.GetValue(2).ToString();
                productCategory.PhotoPath = dr3.GetValue(3).ToString();
                CategoryPharmacy.Add(productCategory);
            }
            con.Close();

            //
            con.Open();
            string query4 = "Select * from PharmacyProducts";
            SqlCommand cmd4 = new SqlCommand(query4, con);
            SqlDataReader dr4 = cmd4.ExecuteReader();
            while (dr4.Read())
            {
                PUCITProducts product = new PUCITProducts();
                product.Id = System.Convert.ToInt32(dr4.GetValue(0));
                product.Category = dr4.GetValue(1).ToString();
                product.Name = dr4.GetValue(2).ToString();
                product.Price = System.Convert.ToInt32(dr4.GetValue(3));
                product.Quantity = System.Convert.ToInt32(dr4.GetValue(4));
                product.PhotoPath = dr4.GetValue(5).ToString();
                productsPharmacy.Add(product);
            }
            con.Close();
            try
            {
                con.Open();
                string query6 = $"Select * from RestaurentMenu";
                SqlCommand cmd6 = new SqlCommand(query6, con);
                SqlDataReader dr6 = cmd6.ExecuteReader();
                while (dr6.Read())
                {
                    RestaurentMenu r = new RestaurentMenu();
                    r.MenuID = dr6.GetInt32(0);
                    r.RestaurentID = dr6.GetValue(1).ToString();
                    r.NameOfItem = dr6.GetValue(2).ToString();
                    r.Price = dr6.GetValue(3).ToString();
                    r.Quantity = dr6.GetInt32(4);
                    r.PhotoPATH2 = dr6.GetValue(5).ToString();
                    r.unit = dr6.GetValue(6).ToString();
                    restaurentsMenu.Add(r);
                }
            }

            catch (Exception)
            {

            }
            finally
            {
                con.Close();
            }

        }

        public  void AddCommentD(CommentBox c)
        {
            int id = 0;

            try
            {
                con.Open();
                //$"insert into Cart(RestaurentID,MenuID,UserID,NameOfItem,Price,OriginalPrice,Quantity) values('{r.RestaurentID}','{r.MenuID}','{r.UserID}','{r.NameOfItem}','{r.Price}','{r.OriginalPrice}', '{r.Quantity}') ";
                string query = $"insert into Comments(Name,date,UserID,Comment) values('{c.Name}','{c.date}','{c.UserID}','{c.Comment}') ";
                SqlCommand cmd = new SqlCommand(query, con);
                int no = cmd.ExecuteNonQuery();


                con.Close();
                con.Open();
                string query20 = $"Select * from Comments";
                SqlCommand cmd20 = new SqlCommand(query20, con);
                SqlDataReader dr20 = cmd20.ExecuteReader();
             
                while (dr20.Read())
                {
                    id = dr20.GetInt32(0);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {

                con.Close();
            }
            c.CommentNo = id;
           comments.Add(c);
        }
        public  void UpdateComment(CommentBox c)
        {
            try
            {
                con.Open();
                string query = $"update Comments set Comment='{c.Comment}' where CommentId='{c.CommentNo}'  ";
                SqlCommand cmd = new SqlCommand(query, con);
                int no = cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {

                con.Close();
            }

        }
        public void DeleteComment(int CommentNo)
        {
            try
            {
                con.Open();
                string query = $"delete from Comments where CommentId='{CommentNo}'";
                SqlCommand cmd = new SqlCommand(query, con);
                int no = cmd.ExecuteNonQuery();
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
          }
            //constructor g
            public void addPucitProduct(string category, string name, int price, int quantity, string path)
        {
            int id = 0;
            try
            {
                con.Open();
                string query = $"insert into PucitProducts(Category,Name,Price,Quantity,PhotoPath) values('{category}','{name}','{price}','{quantity}','{path}')";
                SqlCommand cmd = new SqlCommand(query, con);
                int no = cmd.ExecuteNonQuery();
                con.Close();
                con.Open();
                string query1 = "Select * from PucitProducts";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                SqlDataReader dr = cmd1.ExecuteReader();
              
                while (dr.Read())
                {

                    id = System.Convert.ToInt32(dr.GetValue(0));

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
            PUCITProducts product = new PUCITProducts();
            product.Name = name;
            product.Category = category;
            product.Price = price;
            product.Quantity = quantity;
            product.PhotoPath = path;
            product.Id = id;
            products.Add(product);
        }
        public void addPucitProductCategory(string photoPath, string categoryName, string Description)
        {
            PUCITCafeCategory productCategory = new PUCITCafeCategory();
            try
            {
               
                productCategory.PhotoPath = photoPath;
                productCategory.CategoryName = categoryName;
                productCategory.Description = Description;
                con.Open();
                string query = $"insert into PucitCategory(CategoryName,Description,PhotoPath) values('{categoryName}','{Description}','{photoPath}')";
                SqlCommand cmd = new SqlCommand(query, con);
                int no = cmd.ExecuteNonQuery();
                con.Close();
                con.Open();
                string query1 = "Select * from PucitCategory";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                SqlDataReader dr = cmd1.ExecuteReader();

                while (dr.Read())
                {
                    productCategory.Id = System.Convert.ToInt32(dr.GetValue(0));

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
            Category.Add(productCategory);

        }
        public void PUCITCategorydelete(int id)
        {
            try
            {
                string categoryName = null;
                con.Open();
                string query1 = "Select * from PucitCategory";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                SqlDataReader dr1 = cmd1.ExecuteReader();
                while (dr1.Read())
                {
                    if (System.Convert.ToInt32(dr1.GetValue(0)) == id)
                    {
                        categoryName = dr1.GetValue(1).ToString();
                        break;
                    }
                }
                con.Close();
                con.Open();
                string query = null;
                query = $"Delete from PucitCategory where Id='{id}'";
                SqlCommand cmd = new SqlCommand(query, con);
                int insertedRows = cmd.ExecuteNonQuery();
                con.Close();
                con.Open();
                string query2 = null;
                query2 = $"Delete from PucitProducts where Category='{categoryName}'";
                SqlCommand cmd2 = new SqlCommand(query2, con);
                int insertedRows2 = cmd2.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
            foreach (PUCITCafeCategory productCategory in Category)
            {
                if (id == productCategory.Id)
                {
                    Category.Remove(productCategory);
                    break;
                }
            }
            //cart get


        }
        public void addPharmacyProductCategory(string photoPath, string categoryName, string Description)
        {
            PUCITCafeCategory productCategory = new PUCITCafeCategory();
            try
            {
               
                productCategory.PhotoPath = photoPath;
                productCategory.CategoryName = categoryName;
                productCategory.Description = Description;
                con.Open();
                string query = $"insert into PharmacyCategory(CategoryName,Description,PhotoPath) values('{categoryName}','{Description}','{photoPath}')";
                SqlCommand cmd = new SqlCommand(query, con);
                int no = cmd.ExecuteNonQuery();
                con.Close();
                con.Open();
                string query1 = "Select * from PharmacyCategory";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                SqlDataReader dr = cmd1.ExecuteReader();

                while (dr.Read())
                {
                    productCategory.Id = System.Convert.ToInt32(dr.GetValue(0));

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
            CategoryPharmacy.Add(productCategory);

        }
        public void addPharmacyProduct(string category, string name, int price, int quantity, string path)
        {
            int id = 0;
            try
            {
                con.Open();
                string query = $"insert into PharmacyProducts(Category,Name,Price,Quantity,PhotoPath) values('{category}','{name}','{price}','{quantity}','{path}')";
                SqlCommand cmd = new SqlCommand(query, con);
                int no = cmd.ExecuteNonQuery();
                con.Close();
                con.Open();
                string query1 = "Select * from PharmacyProducts";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                SqlDataReader dr = cmd1.ExecuteReader();
               
                while (dr.Read())
                {

                    id = System.Convert.ToInt32(dr.GetValue(0));

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
            PUCITProducts product = new PUCITProducts();
            product.Name = name;
            product.Category = category;
            product.Price = price;
            product.Quantity = quantity;
            product.PhotoPath = path;
            product.Id = id;
            productsPharmacy.Add(product);

        }
        public void AddRestaurentD(Restaurent r)
        {
            try
            {
                con.Open();
                string query = $"insert into Restaurent(RestaurentID,NameOfRestaurants,Location,PhoneNo,OpenUntil,DeliveryCharges,PhotoPATH,Password) values('{r.RestaurentID}','{r.NameOfRestaurants}','{r.Location}','{r.PhoneNo}','{r.OpenUntil}','{r.DeliveryCharges}','{r.PhotoPATH}', '{r.Password}') ";
                SqlCommand cmd = new SqlCommand(query, con);
                int no = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
        }
        public void AddMenuD(RestaurentMenu r)
        {
            int id = 0;
            try
            {
                con.Open();
                string query = $"insert into RestaurentMenu(RestaurentID,NameOfItem,Price,Quantity,PhotoPATH2,unit) values('{r.RestaurentID}','{r.NameOfItem}','{r.Price}','{r.Quantity}','{r.PhotoPATH2}','{r.unit}' ) ";
                SqlCommand cmd = new SqlCommand(query, con);
                int no = cmd.ExecuteNonQuery();
                con.Close();
                con.Open();
                string query6 = $"Select * from RestaurentMenu";
                SqlCommand cmd6 = new SqlCommand(query6, con);
                SqlDataReader dr6 = cmd6.ExecuteReader();
                string name = null;
               
                while (dr6.Read())
                {
                    name = dr6.GetValue(2).ToString();
                    if (name == r.NameOfItem)
                    {
                        id = dr6.GetInt32(0);
                        break;
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
            RestaurentMenu restaurentMenu1 = new RestaurentMenu();
            restaurentMenu1.NameOfItem = r.NameOfItem;
            restaurentMenu1.RestaurentID = r.RestaurentID;
            restaurentMenu1.unit = r.unit;
            restaurentMenu1.PhotoPATH2 = r.PhotoPATH2;
            restaurentMenu1.MenuID = id;
            restaurentMenu1.Price = r.Price;
            restaurentMenu1.Quantity = r.Quantity;
            restaurentsMenu.Add(restaurentMenu1);

        }
       
        public void AddCartD(CartAndOrder r)
        {
            int id = 0; 
            try
            {
                con.Open();
                string query = $"insert into Cart(RestaurentID,MenuID,UserID,NameOfItem,Price,OriginalPrice,Quantity) values('{r.RestaurentID}','{r.MenuID}','{r.UserID}','{r.NameOfItem}','{r.Price}','{r.OriginalPrice}', '{r.Quantity}') ";
                SqlCommand cmd = new SqlCommand(query, con);
                int no = cmd.ExecuteNonQuery();
                con.Close();
                con.Open();
                string query6 = $"Select * from Cart";
                SqlCommand cmd6 = new SqlCommand(query6, con);
                SqlDataReader dr6 = cmd6.ExecuteReader();
                string resid = null;
                string menuid = null;
             
                while (dr6.Read())
                {
                    resid = dr6.GetValue(1).ToString();
                    menuid = dr6.GetValue(2).ToString();
                    if (resid == r.RestaurentID && menuid == System.Convert.ToString(r.MenuID))
                    {
                        id = dr6.GetInt32(0);
                        break;
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
           CartAndOrder restaurentMenu1 = new CartAndOrder();
            restaurentMenu1.NameOfItem = r.NameOfItem;
            restaurentMenu1.RestaurentID = r.RestaurentID;
            restaurentMenu1.MenuID = r.MenuID;
            restaurentMenu1.UserID = r.UserID;

            restaurentMenu1.OriginalPrice = r.OriginalPrice;
            restaurentMenu1.Price = r.Price;
            restaurentMenu1.CartID = id;
            restaurentMenu1.Quantity = r.Quantity;
           
            Cart.Add(restaurentMenu1);
        }
        public void AddOrderD(CartAndOrder r)
        {
            try
            {
                con.Open();
                //$"insert into Cart(RestaurentID,MenuID,UserID,NameOfItem,Price,OriginalPrice,Quantity) values('{r.RestaurentID}','{r.MenuID}','{r.UserID}','{r.NameOfItem}','{r.Price}','{r.OriginalPrice}', '{r.Quantity}') ";
                string query = $"insert into Orders(RestaurentID,MenuID,UserID,NameOfItem,Price,OriginalPrice,Quantity,ReceiptID) values('{r.RestaurentID}','{r.MenuID}','{r.UserID}','{r.NameOfItem}','{r.Price}','{r.OriginalPrice}', '{r.Quantity}','{r.ReceiptID}') ";
                SqlCommand cmd = new SqlCommand(query, con);
                int no = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }

        }
        public void AddReceiptD(Receipt r)
        {
            try
            {
                con.Open();
                //$"insert into Cart(RestaurentID,MenuID,UserID,NameOfItem,Price,OriginalPrice,Quantity) values('{r.RestaurentID}','{r.MenuID}','{r.UserID}','{r.NameOfItem}','{r.Price}','{r.OriginalPrice}', '{r.Quantity}') ";
                string query = $"insert into Receipt(ReceiptID,UserID,RestaurentID,TotalAmount,NoOfItems,UserTime,OrderTime,OrderAccept,OrderReceived) values('{r.ReceiptID}','{r.UserID}','{r.RestaurentID}','{r.TotalAmount}','{r.NoOfItems}','{r.UserTime}', '{r.OrderTime}','{r.OrderAccept}','{r.OrderReceived}') ";
                SqlCommand cmd = new SqlCommand(query, con);
                int no = cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {

                con.Close();
            }

        }
        public void ClearAllCart()
        {
            try
            {


                con.Open();
                string query = $"delete from Cart";
                SqlCommand cmd = new SqlCommand(query, con);
                int no = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }

        }
        public void EditReceipt(Receipt r)
        {
            try
            {
                con.Open();
                string query = $"update Receipt set OrderAccept='{r.OrderAccept}' where ReceiptID='{r.ReceiptID}'";
                SqlCommand cmd = new SqlCommand(query, con);
                int no = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }

        }
        public void RemoveOrder(int CartID)
        {
            try
            {
                con.Open();
                string query = $"delete from Orders where ReceiptID='{CartID}'";
                SqlCommand cmd = new SqlCommand(query, con);
                int no = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }


        }
        public void RemoveReceipt(int ReceiptID)
        {
            try
            {
                con.Open();
                string query = $"delete from Receipt where ReceiptID='{ReceiptID}'";
                SqlCommand cmd = new SqlCommand(query, con);
                int no = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
        
        }
        public void editCategory(PUCITCafeCategory modal,string category)
        {
            try
            {
                con.Open();
                string query;
                if (modal.PhotoPath == null)
                {
                    query = $"update PucitCategory set CategoryName='{modal.CategoryName}',Description='{modal.Description}' where Id='{modal.Id}'";
                }
                else
                    query = $"update PucitCategory set CategoryName='{modal.CategoryName}',Description='{modal.Description}',PhotoPath='{modal.PhotoPath}' where Id='{modal.Id}'";
                SqlCommand cmd = new SqlCommand(query, con);
                int no = cmd.ExecuteNonQuery();
                con.Close();
                con.Open();
                string query1 = $"update PucitProducts set Category='{modal.CategoryName}' where category='{category}'";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                int no1 = cmd1.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
            foreach (PUCITCafeCategory cate in Category)
            {
                if (cate.CategoryName == category)
                {
                    cate.CategoryName = modal.CategoryName;
                    cate.Description = modal.Description;
                    if (modal.PhotoPath != null)
                        cate.PhotoPath = modal.PhotoPath;
                }
            }


        }
        public void RemoveProductProduct(PUCITProducts  r)
        {
            try
            {
                con.Open();
                string query1 = $"Delete from PucitProducts where Id='{r.Id}'";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                int no1 = cmd1.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
        }
        public void UpdateProduct(PUCITProducts modal)
        {
            try
            {
                con.Open();
                string query;
                if (modal.PhotoPath == null)
                {
                    query = $"update PucitProducts set Category='{modal.Category}',Name='{modal.Name}',Price='{modal.Price}',Quantity='{modal.Quantity}' where Id='{modal.Id}'";
                }
                else
                    query = $"update PucitProducts set Category='{modal.Category}',Name='{modal.Name}',Price='{modal.Price}',Quantity='{modal.Quantity}',PhotoPath='{modal.PhotoPath}' where Id='{modal.Id}'";
                SqlCommand cmd = new SqlCommand(query, con);
                int no = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
            foreach (PUCITProducts cate in products)
            {
                if (cate.Id == modal.Id)
                {
                    cate.Category = modal.Category;
                    cate.Name = modal.Name;
                    cate.Price = modal.Price;
                    cate.Quantity = modal.Quantity;
                    if (modal.PhotoPath != null)
                        cate.PhotoPath = modal.PhotoPath;
                }
            }
        }
        public void PharmacyCategorydelete(int id)
        {
            try
            {
                string categoryName = null;
                con.Open();
                string query1 = "Select * from PharmacyCategory";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                SqlDataReader dr1 = cmd1.ExecuteReader();
                while (dr1.Read())
                {
                    if (System.Convert.ToInt32(dr1.GetValue(0)) == id)
                    {
                        categoryName = dr1.GetValue(1).ToString();
                        break;
                    }
                }
                con.Close();
                con.Open();
                string query = null;
                query = $"Delete from PharmacyCategory where Id='{id}'";
                SqlCommand cmd = new SqlCommand(query, con);
                int insertedRows = cmd.ExecuteNonQuery();
                con.Close();
                con.Open();
                string query2 = null;
                query2 = $"Delete from PharmacyProducts where Category='{categoryName}'";
                SqlCommand cmd2 = new SqlCommand(query2, con);
                int insertedRows2 = cmd2.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            } 
            foreach (PUCITCafeCategory productCategory in CategoryPharmacy)
            {
                if (id == productCategory.Id)
                {
                    CategoryPharmacy.Remove(productCategory);
                    break;
                }
            }
        }
        public void editCategoryPharmacy(PUCITCafeCategory modal, string category)
        {
            try
            {
                con.Open();
                string query;
                if (modal.PhotoPath == null)
                {
                    query = $"update PharmacyCategory set CategoryName='{modal.CategoryName}',Description='{modal.Description}' where Id='{modal.Id}'";
                }
                else
                    query = $"update PharmacyCategory set CategoryName='{modal.CategoryName}',Description='{modal.Description}',PhotoPath='{modal.PhotoPath}' where Id='{modal.Id}'";
                SqlCommand cmd = new SqlCommand(query, con);
                int no = cmd.ExecuteNonQuery();
                con.Close();
                con.Open();
                string query1 = $"update PharmacyProducts set Category='{modal.CategoryName}' where category='{category}'";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                int no1 = cmd1.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
            foreach (PUCITCafeCategory cate in CategoryPharmacy)
            {
               if(cate.CategoryName==category)
                {
                    cate.CategoryName = modal.CategoryName;
                    cate.Description = modal.Description;
                    if (modal.PhotoPath != null)
                        cate.PhotoPath = modal.PhotoPath;
                }
            }

        }
        public void RemoveProductPharmacy(PUCITProducts r)
        {
            try
            {
                con.Open();
                string query1 = $"Delete from PharmacyProducts where Id='{r.Id}'";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                int no1 = cmd1.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
        }
        public void updateProductPharmacy(PUCITProducts modal,string category)
        {
            try
            {
                con.Open();
                string query;
                if (modal.PhotoPath == null)
                {
                    query = $"update PharmacyProducts set Category='{modal.Category}',Name='{modal.Name}',Price='{modal.Price}',Quantity='{modal.Quantity}' where Id='{modal.Id}'";
                }
                else
                    query = $"update PharmacyProducts set Category='{modal.Category}',Name='{modal.Name}',Price='{modal.Price}',Quantity='{modal.Quantity}',PhotoPath='{modal.PhotoPath}' where Id='{modal.Id}'";
                SqlCommand cmd = new SqlCommand(query, con);
                int no = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
            foreach (PUCITProducts cate in productsPharmacy)
            {
                if (cate.Id==modal.Id)
                {
                    cate.Category = modal.Category;
                    cate.Name = modal.Name;
                    cate.Price = modal.Price;
                    cate.Quantity = modal.Quantity;
                    if (modal.PhotoPath != null)
                        cate.PhotoPath = modal.PhotoPath;
                }
            }

        }
        public void RemoveRestaurent(string RestaurentID)
        {
            try
            {
                con.Open();
                string query = $"delete from Restaurent where RestaurentID='{RestaurentID}'";
                SqlCommand cmd = new SqlCommand(query, con);
                int no = cmd.ExecuteNonQuery();
                con.Close();
                con.Open();
                string query1 = $"delete from RestaurentMenu where RestaurentID='{RestaurentID}'";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                int no1 = cmd1.ExecuteNonQuery();
                con.Close();
                con.Open();
                string query2 = $"delete from Receipt where RestaurentID='{RestaurentID}'";
                SqlCommand cmd2 = new SqlCommand(query2, con);
                int no2 = cmd2.ExecuteNonQuery();
                con.Close();
                con.Open();
                string query3 = $"delete from Orders where RestaurentID='{RestaurentID}'";
                SqlCommand cmd3 = new SqlCommand(query3, con);
                int no3 = cmd3.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
        }
        public  void EditRestaurent(EditRes r)
        {
            try
            {
                string query = null;
                con.Open();
                if (r.PhotoPATH == null)
                    query = $"update Restaurent set NameOfRestaurants='{r.NameOfRestaurants}',Location='{r.Location}',PhoneNo='{r.PhoneNo}',OpenUntil='{r.OpenUntil}',DeliveryCharges='{r.DeliveryCharges}' where RestaurentID='{r.RestaurentID}'";
                else
                    query = $"update Restaurent set NameOfRestaurants='{r.NameOfRestaurants}',Location='{r.Location}',PhoneNo='{r.PhoneNo}',OpenUntil='{r.OpenUntil}',DeliveryCharges='{r.DeliveryCharges}',PhotoPATH='{r.PhotoPATH}' where RestaurentID='{r.RestaurentID}'  ";
                SqlCommand cmd = new SqlCommand(query, con);
                int no = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }

        }
        public void RemoveMenu(string RestaurentID, int MenuID)
        {
            try
            {
                con.Open();
                string query = $"delete from RestaurentMenu where RestaurentID='{RestaurentID}' and MenuID='{MenuID}' ";
                SqlCommand cmd = new SqlCommand(query, con);
                int no = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
          
        }
        public  void EditMenu(RestaurentMenu r)
        {
            try
            {
                string query = null;
                con.Open();
                if (r.PhotoPATH2 == null)
                    query = $"update RestaurentMenu set NameOfItem='{r.NameOfItem}',Price ='{r.Price}',Quantity ='{r.Quantity}',unit='{r.unit}' where RestaurentID='{r.RestaurentID}' and MenuID='{r.MenuID}'";
                else
                    query = $"update RestaurentMenu set NameOfItem='{r.NameOfItem}',Price ='{r.Price}',Quantity ='{r.Quantity}',PhotoPATH2='{r.PhotoPATH2}',unit='{r.unit}' where RestaurentID='{r.RestaurentID}' and  MenuID='{r.MenuID}'";
                SqlCommand cmd = new SqlCommand(query, con);
                int no = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
        }
        public  void RemoveCart(int CartID)
        {
            try
            {


                con.Open();
                string query = $"delete from Cart where CartID='{CartID}'";
                SqlCommand cmd = new SqlCommand(query, con);
                int no = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally

            {

                con.Close();
            }
            

        }
        public void EditCart(CartAndOrder r)
        {
            try
            {
                con.Open();
                string query = $"update Cart set Price='{r.Price}',Quantity='{r.Quantity}' where CartID='{r.CartID}'  ";
                SqlCommand cmd = new SqlCommand(query, con);
                int no = cmd.ExecuteNonQuery(); 
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
          finally
                {
                con.Close();
            }

        }
        public  void AddContactUsD(ContactUs r)
        {
            try
            {
                con.Open();
                string query = $"insert into ContactUs(firstName,lastName,email,Message) values('{r.FirstName}','{r.LastName}','{r.Email}','{r.Message}') ";
                SqlCommand cmd = new SqlCommand(query, con);
                int no = cmd.ExecuteNonQuery();
                con.Close();
                con.Open();
                string query20 = $"Select * from ContactUs";
                SqlCommand cmd20 = new SqlCommand(query20, con);
                SqlDataReader dr20 = cmd20.ExecuteReader();
                int id = 0;
                while (dr20.Read())
                {
                    id = dr20.GetInt32(0);

                }
             
                r.Id = id;
                complaint.Add(r);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
