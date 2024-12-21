using System.ComponentModel.DataAnnotations;

namespace Barber_shops.Models
{
    public class imageupload
    {
        [Key]
        public int role{ set; get; }

        public string url {set;get;}
    }
}
