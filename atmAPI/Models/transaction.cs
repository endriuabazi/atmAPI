using System.ComponentModel.DataAnnotations;

namespace atmAPI.Models
{
    public class transaction
    {

        [Key]
        public int transaction_id { get; set; }
        public string transaction_type { get; set; }
      
        public DateTime transaction_date { get; set; } = DateTime.Now;
        public int amount { get; set; }
      

        public account account { get; set; }



    }
}
