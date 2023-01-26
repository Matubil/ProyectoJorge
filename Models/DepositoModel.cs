using System.ComponentModel.DataAnnotations;

namespace DapperApi2022.Models
{
    public class DepositoModel
    {
        [Key]
        public int inter_id { get; set; }
        public string transf_cbu { get; set; }
        public string transf_titular { get; set; }
        public string transf_ctaorigen { get; set; }
        public string transf_banco { get; set; }

    }
}


