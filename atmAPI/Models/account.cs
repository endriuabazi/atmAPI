using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace atmAPI.Models
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int account_id { get; set; }
        public string account_name { get; set; }
        public int balance { get; set; }
        public char currency { get; set; }
        public Client client { get; set; }

        public ICollection<Transaction> transactions { get; set; }

    }
}
