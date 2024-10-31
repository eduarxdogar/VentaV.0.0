using System;
using System.Collections.Generic;

namespace WsVenta.Models
{
    public partial class Venta
    {
        public long Id { get; set; }
        public DateTime Fecha { get; set; }
        public long? IdCliente { get; set; }
        public decimal Total { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; }
        public virtual Concepto Concepto { get; set; }
    }
}
