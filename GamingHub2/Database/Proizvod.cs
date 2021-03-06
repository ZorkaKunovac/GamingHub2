using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamingHub2.Database
{
    public class Proizvod
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Polje je obavezno")]
        [StringLength(50, ErrorMessage = "{0} mora biti izmedju {2} i {1} znakova.", MinimumLength = 3)]
        public string NazivProizvoda { get; set; }

        [Required(ErrorMessage = "Polje je obavezno")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal ProdajnaCijena { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Popust { get; set; }
        [Required(ErrorMessage = "Polje je obavezno")]
        public bool? Status { get; set; }
        public int IgraKonzolaID { get; set; }
        public virtual IgraKonzola IgraKonzola { get; set; }


    }
}
