using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace TestNeoris2.Models;

[Keyless]
public partial class Reporte
{
    public int movimiento_id { get; set; }
    public DateTime? Fecha { get; set; }
    public decimal Valor { get; set; }
    public string Cliente { get; set; }
    public int NumeroCuenta { get; set; }
    public string Tipo { get; set; }
    public decimal SaldoInicial { get; set; }
    public string Estado { get; set; }
    public decimal Movimiento { get; set; }
    public decimal SaldoDisponible { get; set; }

}
