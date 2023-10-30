namespace TestProject1
{
    public class Orders : DBModelsBaseObject
    {
        [DatabaseAttributes(isID = true)]
        public int Id
        {
            get; set;
        }

        public DateTime Date_Ordered { get; set; }

        public int Product_ID { get; set; }

        public double Qty { get; set; }

        public int Caller_ID { get; set; }

        public bool Cancelled { get; set; }

        public bool Complete { get; set; }

    }
}
