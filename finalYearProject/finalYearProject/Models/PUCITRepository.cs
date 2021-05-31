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
        public List<PUCITProducts> temp = new List<PUCITProducts>();
        public List<PUCITCafeCategory> Category = new List<PUCITCafeCategory>();
        public List<PUCITProducts> products = new List<PUCITProducts>();
        public List<PUCITCafeCategory> CategoryPharmacy = new List<PUCITCafeCategory>();
        public List<PUCITProducts> productsPharmacy = new List<PUCITProducts>();
        public PUCITRepository()
        {
            connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RestaurantAndCafe;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            con = new SqlConnection(connString);
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

        }
        public void addPucitProduct(string category, string name, int price, int quantity, string path)
        {
            con.Open();
            string query = $"insert into PucitProducts(Category,Name,Price,Quantity,PhotoPath) values('{category}','{name}','{price}','{quantity}','{path}')";
            SqlCommand cmd = new SqlCommand(query, con);
            int no = cmd.ExecuteNonQuery();
            con.Close();
        }
        public void addPucitProductCategory(string photoPath, string categoryName, string Description)
        {
           PUCITCafeCategory productCategory = new PUCITCafeCategory();
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
            con.Close();
            Category.Add(productCategory);

        }
        public void PUCITCategorydelete(int id)
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
            con.Close();
            foreach (PUCITCafeCategory productCategory in Category)
            {
                if (id == productCategory.Id)
                {
                    Category.Remove(productCategory);
                    break;
                }
            }
        }
        public void addPharmacyProductCategory(string photoPath, string categoryName, string Description)
        {
            PUCITCafeCategory productCategory = new PUCITCafeCategory();
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
            con.Close();
            Category.Add(productCategory);

        }
        public void addPharmacyProduct(string category, string name, int price, int quantity, string path)
        {
            con.Open();
            string query = $"insert into PharmacyProducts(Category,Name,Price,Quantity,PhotoPath) values('{category}','{name}','{price}','{quantity}','{path}')";
            SqlCommand cmd = new SqlCommand(query, con);
            int no = cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
