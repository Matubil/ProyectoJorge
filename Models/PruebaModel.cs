using System.ComponentModel.DataAnnotations;

namespace DapperApi2022.Models
{
    public class PruebaModel
    {
        [Key]
        public int id { get; set; }
        public string nombre { get; set; }
       

    }
}


