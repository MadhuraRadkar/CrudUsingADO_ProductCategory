using System.Data.SqlClient;
namespace CrudUsingADO_ProductCategory.Models
{
    public class ProductDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        IConfiguration configuration;

        public ProductDAL(IConfiguration configuration)
        {
            this.configuration = configuration;
            con = new SqlConnection(this.configuration.GetConnectionString("defaultConnection"));
        }

        public IEnumerable<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            string qry = "select p.*,c.cname from product p inner join category c on p.cid=c.cid where p.isActive=1";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while(dr.Read())
                {
                    Product prod = new Product();
                    prod.Id = Convert.ToInt32(dr["id"]);
                    prod.Name = dr["name"].ToString();
                    prod.Price = Convert.ToDouble(dr["price"]);
                    prod.ImageUrl = dr["imageurl"].ToString();
                    prod.Cid = Convert.ToInt32(dr["cid"]);
                    prod.Cname = dr["cname"].ToString();
                    prod.IsActive = Convert.ToInt32(dr["isActive"]);

                    products.Add(prod);
                }
            }
            con.Close();
            return products;
        }

        public IEnumerable<Catg> GetCatgs()
        {
            List<Catg> catgs = new List<Catg>();
            string qry = "select * from category";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            if ( dr.HasRows)
            {
                while(dr.Read())
                {
                     Catg catg = new Catg();
                    catg.Cid = Convert.ToInt32(dr["cid"]);
                    catg.Cname = dr["cname"].ToString() ;
                    catgs.Add(catg);
                }
            }
            con.Close ();
            return catgs;
        }

        public Product GetProductById(int id)
        {
            Product prod = new Product ();
            string qry = "select p.*,c.cname from product p inner join category c on c.cid=p.cid where p.id=@id and isActive=1";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            dr = cmd.ExecuteReader();
            if(dr.HasRows)
            {
                while(dr.Read())
                {
                    prod.Id = Convert.ToInt32(dr["id"]);
                    prod.Name = dr["name"].ToString();
                    prod.Price = Convert.ToDouble(dr["price"]);
                    prod.ImageUrl = dr["imageurl"].ToString();
                    prod.IsActive = Convert.ToInt32(dr["isActive"]);
                    prod.Cid = Convert.ToInt32(dr["cid"]);
                    prod.Cname = dr["cname"].ToString();
                }
            }
            con.Close();
            return prod;
        }

        public int AddProduct(Product prod)
        {
            prod.IsActive = 1;
            int result = 0;
           string qry = "insert into Product values(@name,@price,@imageurl,@cid,@isActive)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@name", prod.Name);
            cmd.Parameters.AddWithValue("@price", prod.Price);
            cmd.Parameters.AddWithValue("@imageurl", prod.ImageUrl);
            cmd.Parameters.AddWithValue("@cid", prod.Cid);
            cmd.Parameters.AddWithValue("@isActive", prod.IsActive);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close() ;
            return result;


        }

        public int UpdateProduct(Product prod)
        {
            prod.IsActive = 1;
            int result = 0;
            string qry = "Update product set name=@name,price=@price,imageurl=@imageurl,cid=@cid where id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@name", prod.Name);
            cmd.Parameters.AddWithValue("@price", prod.Price);
            cmd.Parameters.AddWithValue("@imageurl", prod.ImageUrl);
            cmd.Parameters.AddWithValue("@cid", prod.Cid);
            cmd.Parameters.AddWithValue("@id", prod.Id);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;

        }

        public int DeleteProduct(int id)
        {
            int result = 0;
            string qry = "update Product set isActive=0 where id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
    }
}
