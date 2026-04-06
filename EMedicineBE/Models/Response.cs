namespace EMedicineBE.Models
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }

        public List<Users> listUsers { get; set; }
        public Users User { get; set; }

        public List<Medicines> listMedicines { get; set; }
        public Medicines Medicine { get; set; }

        public List<Cart> listCart { get; set; }
        public Cart Cart { get; set; }

        public List<Orders> listOrders { get; set; }
        public Orders Order { get; set; }

        public List<OrderItems> listOrderItems { get; set; }
        public OrderItems OrderItems { get; set; }
      
    }
}
