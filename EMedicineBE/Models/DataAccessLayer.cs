using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Data;
using System.Data.SqlClient;

namespace EMedicineBE.Models
{
    public class DataAccessLayer
    {
        public Response register(Users users, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_register", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", users.FirstName);
            cmd.Parameters.AddWithValue("@LastName", users.LastName);
            cmd.Parameters.AddWithValue("@Email", users.Email);
            cmd.Parameters.AddWithValue("@Fund", 0);
            cmd.Parameters.AddWithValue("@Type", "Users");
            cmd.Parameters.AddWithValue("@Status", "Pending");
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();

            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Registration successful";
            }
            else
            {
                response.StatusCode = 500;
                response.StatusMessage = "Registration failed";
            }
            return response;
        }

        public Response login(Users users, SqlConnection connection)
        {
            SqlDataAdapter da = new SqlDataAdapter("sp_login", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Email", users.Email);
            da.SelectCommand.Parameters.AddWithValue("@Password", users.Password);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Response response = new Response();
            Users user = new Users();
            if (dt.Rows.Count > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Login successful";
                response.User = user;
                // Optionally populate response.User from dt.Rows[0] here if you need
            }
            else
            {
                response.StatusCode = 401;
                response.StatusMessage = "Invalid email or password";
                response.User = null;
            }

            return response;
        }

        public Response viewUser(Users users, SqlConnection connection)
        {
            SqlDataAdapter da = new SqlDataAdapter("sp_viewUsers", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@ID", users.ID);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Response response = new Response();
            Users user = new Users();
            if (dt.Rows.Count > 0)
            {
                user.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                user.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                user.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                user.Email = Convert.ToString(dt.Rows[0]["Email"]);
                user.Password = Convert.ToString(dt.Rows[0]["Password"]);
                user.Type = Convert.ToString(dt.Rows[0]["Type"]);
                user.Fund = Convert.ToDecimal(dt.Rows[0]["Fund"]);
                user.CreatedOn = Convert.ToDateTime(dt.Rows[0]["CreatedOn"]);
                response.StatusCode = 200;
                response.StatusMessage = "Users retrieved successfully";
                // Optionally populate response.UsersList from dt here if you need
            }
            else
            {
                response.StatusCode = 404;
                response.StatusMessage = "No users found";
                response.User = user;
            }
            return response;
        }

        public Response updateUserProfile(Users users, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_updateUserProfile", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", users.ID);
            cmd.Parameters.AddWithValue("@FirstName", users.FirstName);
            cmd.Parameters.AddWithValue("@LastName", users.LastName);
            cmd.Parameters.AddWithValue("@Email", users.Email);
            cmd.Parameters.AddWithValue("@Password", users.Password);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "User profile updated successfully";
            }
            else
            {
                response.StatusCode = 500;
                response.StatusMessage = "Failed to update user profile";
            }
            return response;
        }


        public Response addToCart(Cart cart, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_addToCart", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", cart.ID);
            cmd.Parameters.AddWithValue("@MedicineID", cart.MedicineID);
            cmd.Parameters.AddWithValue("@Quantity", cart.Quantity);
            cmd.Parameters.AddWithValue("@UnitPrice", cart.UnitPrice);
            cmd.Parameters.AddWithValue("@Discount", cart.Discount);
            cmd.Parameters.AddWithValue("@TotalPrice", cart.TotalPrice);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Medicine added to cart successfully";
            }
            else
            {
                response.StatusCode = 500;
                response.StatusMessage = "Failed to add medicine to cart";
            }
            return response;
        }

        public Response placeOrder(Users users, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_placeOrder", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", users.ID);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Order placed successfully";
            }
            else
            {
                response.StatusCode = 500;
                response.StatusMessage = "Failed to place order";
            }
            return response;
        }

        public Response orderList(Users users, SqlConnection connection)
        {
            SqlDataAdapter da = new SqlDataAdapter("sp_orderList", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@ID", users.ID);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Response response = new Response();
            List<Orders> ordersList = new List<Orders>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Orders order = new Orders
                    {
                        ID = Convert.ToInt32(row["ID"]),
                        UserID = Convert.ToInt32(row["UserID"]),
                        OrderTotal = Convert.ToDecimal(row["OrderTotal"]),
                        OrderNo = Convert.ToString(row["OrderNo"]),
                        OrderStatus = Convert.ToInt32(row["OrderStatus"]),

                    };
                    ordersList.Add(order);
                }
                response.StatusCode = 200;
                response.StatusMessage = "Orders retrieved successfully";
                response.listOrders = ordersList;
            }
            else
            {
                response.StatusCode = 404;
                response.StatusMessage = "No orders found";
                response.listOrders = null;
            }
            return response;

        }

        public Response addUpdateMedicine(Medicines medicines, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_addUpdateMedicine", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", medicines.ID);
            cmd.Parameters.AddWithValue("@Name", medicines.Name);
            cmd.Parameters.AddWithValue("@Description", medicines.Description);
            cmd.Parameters.AddWithValue("@Manufacturer", medicines.Manufacturer);
            cmd.Parameters.AddWithValue("@Price", medicines.UnitPrice);
            cmd.Parameters.AddWithValue("@Discount", medicines.Discount);
            cmd.Parameters.AddWithValue("@Quantity", medicines.Quantity);
            cmd.Parameters.AddWithValue("@ExpDate", medicines.ExpDate);
            cmd.Parameters.AddWithValue("@ImageUrl", medicines.ImageUrl);
            cmd.Parameters.AddWithValue("@Status", medicines.Status);
            cmd.Parameters.AddWithValue("@Type", medicines.Type);

            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Medicine added/updated successfully";
            }
            else
            {
                response.StatusCode = 500;
                response.StatusMessage = "Failed to add/update medicine";
            }
            return response;
        }

        public Response userList(Users users, SqlConnection connection)
        {
            SqlDataAdapter da = new SqlDataAdapter("sp_userList", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            da.Fill(dt);
            Response response = new Response();
            List<Users> listUsers = new List<Users>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Users user = new Users
                    {
                        ID = Convert.ToInt32(row["ID"]),
                        FirstName = Convert.ToString(row["FirstName"]),
                        LastName = Convert.ToString(row["LastName"]),
                        Email = Convert.ToString(row["Email"]),
                        Password = Convert.ToString(row["Password"]),
                        Type = Convert.ToString(row["Type"]),
                        Status = Convert.ToInt32(row["Status"]),
                        Fund = Convert.ToDecimal(row["Fund"]),
                        CreatedOn = Convert.ToDateTime(row["CreatedOn"])
                    };
                    listUsers.Add(user);
                }
                response.StatusCode = 200;
                response.StatusMessage = "Users retrieved successfully";
                response.listUsers = listUsers;
            }
            else
            {
                response.StatusCode = 404;
                response.StatusMessage = "No users found";
                response.listUsers = null;
            }
            return response;
        }
    }
}
