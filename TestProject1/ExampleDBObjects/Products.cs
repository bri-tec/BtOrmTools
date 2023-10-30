namespace TestProject1
{
    public class Products : DBModelsBaseObject
    {
        [DatabaseAttributes(isID = true)]
        public int Id
        {
            get; set;
        }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public double Qty_In_Stock { get; set; }

    }
}
