namespace TestProject1
{
    public class Callers : DBModelsBaseObject
    {
        [DatabaseAttributes(isID = true)]
        public int Id
        {
            get; set;
        }

        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Title { get; set; }
        public string? Telephone_No { get; set; }
        public string? Email { get; set; }

    }
}
