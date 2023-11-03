using System;
using System.Collections.Generic;

namespace TestNeoris.Models;
{
    public partial class Reporte
    {
        public DateTime? Fecha { get; set; }

        public string Cliente { get; set; }
        public int NumeroCuenta { get; set; }
        public string Tipo { get; set; }
        public string SaldoInicial { get; set; }
        public bool Estado { get; set; }
        public decimal Movimiento { get; set; }
        public decimal SaldoDisponible { get; set; }

    }
}