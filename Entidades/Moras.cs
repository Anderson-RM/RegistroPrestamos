using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RPrestamosDetalle.Entidades
{
    class Moras
    {
        [Key]
        public int MoraId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public double Total { get; set; }

        [ForeignKey("MoraId")]
        public virtual List<MorasDetalle> Detalles { get; set; } = new List<MorasDetalle>();
    }
}
