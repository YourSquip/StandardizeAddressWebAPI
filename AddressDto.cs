namespace StandardizeAddressWebAPI
{
    //can be changed to record
    public class AddressDto
    {
        public string country { get; set; }
        public string region { get; set; }
        public string city { get; set; }
        public string street { get; set; }

        public int house { get; set; }
        public int flat { get; set;}
    }
    
}
