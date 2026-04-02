namespace EMedicineBE.Models
{
    public class Orders
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string OrderNo { get; set; }
        public decimal TotalAmount { get; set; }
        //public DateTime OrderDate { get; set; }

        public int Status { get; set; }
    }
}
