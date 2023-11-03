using System;
using System.Collections.Generic;

namespace TestNeoris2.Models;

public partial class Movimiento
{
    public int MovimientoId { get; set; }

    public DateTime? Fecha { get; set; }

    public string? TipoMovimiento { get; set; }

    public decimal? Valor { get; set; }

    public int? NumeroCuenta { get; set; }

    public decimal? Saldo { get; set; }

    public virtual Cuentas? NumeroCuentaNavigation { get; set; }
}
