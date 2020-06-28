using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RPrestamosDetalle.Entidades
{
    class MorasDetalle
    {
        [Key]
        public int MorasDetalleId { get; set; }
        public int MoraId { get; set; }
        public int PrestamoId { get; set; }
        public double Valor { get; set; }

        public MorasDetalle(int moraid, int prestamoid, double valor)
        {
            MorasDetalleId = 0;
            MoraId = moraid;
            PrestamoId = prestamoid;
            Valor = valor;
        }
    }
}
