namespace securityApp.Models
{
    public class Scan
    {
        public int Id { get; set; }
        public string sha256 { get; set; }
        public string result { get; set; }
        public bool isMalicious { get; set; }  
    }
}
