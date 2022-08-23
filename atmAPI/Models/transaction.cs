using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace atmAPI.Models
{
    public class transaction
    {

        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int transaction_id { get; set; }
        public string transaction_type { get; set; }
      
        
        public int amount { get; set; }
      

        public account account { get; set; }



    }
}
