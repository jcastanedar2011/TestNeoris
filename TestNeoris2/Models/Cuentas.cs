using System;
using System.Collections.Generic;

namespace TestNeoris2.Models;

public partial class Cuentas
{
    public int NumeroCuenta { get; set; }

    public string? TipoCuenta { get; set; }

    public decimal? SaldoInicial { get; set; }

    public string? Estado { get; set; }

    public int? Clienteid { get; set; }

    public virtual Cliente? Cliente { get; set; }

    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
}
