using System;
using System.Collections.Generic;

namespace TestNeoris.Models;

public partial class Cuentum
{
    public int NumeroCuenta { get; set; }

    public string? TipoCuenta { get; set; }

    public decimal? SaldoInicial { get; set; }

    public string? Estado { get; set; }
}
