﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GamingHub2.Database
{
    public class Narudzba
    {
        public Narudzba()
        {
            // Izlazis = new HashSet<Izlazi>();
            NarudzbaStavke = new HashSet<NarudzbaStavka>();
        }
        [Key]
        public int NarudzbaId { get; set; }
       // public string BrojNarudzbe { get; set; }
        public string KorisnikID { get; set; }
        public DateTime Datum { get; set; }

        public bool Status { get; set; }
        public bool? Otkazano { get; set; }
        public virtual Korisnik Korisnik { get; set; }
        public virtual ICollection<NarudzbaStavka> NarudzbaStavke { get; set; }
    }
}
