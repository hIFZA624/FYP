using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace finalYearProject.Models
{
   static public class RestaurentRepository
    {
        
      static  string connString = null;
        static SqlConnection con;
        public static List<Restaurent> restaurents = new List<Restaurent>();
        public static List<RestaurentMenu> restaurentsMenu = new List<RestaurentMenu>();
        static RestaurentRepository()
        {
            connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RestaurantAndCafe;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            con = new SqlConnection(connString);
            //Restaurant

            /*
            try
            {
                con.Open();
                string query = $"Select * from Restaurent";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Restaurent r = new Restaurent();
                    r.RestaurentID = dr.GetValue(0).ToString();
                    r.NameOfRestaurants = dr.GetValue(1).ToString();
                    r.Location = dr.GetValue(2).ToString();
                    r.PhoneNo = dr.GetValue(3).ToString();
                    r.OpenUntil = dr.GetValue(4).ToString();
                    r.DeliveryCharges = dr.GetValue(5).ToString();
                    r.PhotoPATH = dr.GetValue(6).ToString();
                    r.Password = dr.GetValue(7).ToString();
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
            //restaurentsMenu.Add(new RestaurentMenu { MenuID = 1, RestaurentID = "f19@gmail.com", NameOfItem = "buryani", Price = "1000", Quantity = 2, PhotoPATH2 = "", unit = "box" });
            try
            {
                con.Open();
                string query = $"Select * from RestaurentMenu";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    RestaurentMenu r = new RestaurentMenu();
                    r.MenuID = dr.GetInt32(0);
                    r.RestaurentID = dr.GetValue(1).ToString();
                    r.NameOfItem = dr.GetValue(2).ToString();
                    r.Price = dr.GetValue(3).ToString();
                    r.Quantity = dr.GetInt32(4);
                    r.PhotoPATH2 = dr.GetValue(5).ToString();
                    r.unit = dr.GetValue(6).ToString();
                    restaurentsMenu.Add(r);
                }
            }

            catch (Exception)
            {

            }
            finally
            {
                con.Close();
            }*/
        }
        
       /* public  void AddRestaurentD(Restaurent r)
        {
            try
            {
                con.Open();
                string query = $"insert into Restaurent(RestaurentID,NameOfRestaurants,Location,PhoneNo,OpenUntil,DeliveryCharges,PhotoPATH,Password) values('{r.RestaurentID}','{r.NameOfRestaurants}','{r.Location}','{r.PhoneNo}','{r.OpenUntil}','{r.DeliveryCharges}','{r.PhotoPATH}', '{r.Password}') ";
                SqlCommand cmd = new SqlCommand(query, con);
                int no = cmd.ExecuteNonQuery();
                if(no==1)
                {

                }
                else
                {

                }
            }
            catch (Exception)
            {


            }
            finally
            {
                con.Close();
            }

        }*/
        //Add Menu in database
        /*
        public static void AddMenuD(RestaurentMenu r)
        {
            try
            {
                con.Open();
                string query = $"insert into RestaurentMenu(RestaurentID,NameOfItem,Price,Quantity,PhotoPATH2,unit) values('{r.RestaurentID}','{r.NameOfItem}','{r.Price}','{r.Quantity}','{r.PhotoPATH2}','{r.unit}' ) ";
                SqlCommand cmd = new SqlCommand(query, con);
                int no = cmd.ExecuteNonQuery();


            }
            catch (Exception e)
            {


            }
            finally
            {
                con.Close();
            }

        }*/
    }
}
