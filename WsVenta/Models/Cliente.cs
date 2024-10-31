using System;
using System.Collections.Generic;

namespace WsVenta.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Venta = new HashSet<Venta>();
        }

        public long Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Venta> Venta { get; set; }
    }
}
