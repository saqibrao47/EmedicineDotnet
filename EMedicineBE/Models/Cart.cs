namespace EMedicineBE.Models
{
    public class Cart
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int MedicineID { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal UnitPrice { get; set; }
        public int Status { get; set; }
    }
}
