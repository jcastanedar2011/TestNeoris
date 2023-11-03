using System;
using System.Collections.Generic;

namespace TestNeoris2.Models;

public partial class Cliente
{
    public int Clienteid { get; set; }

    public int? PersonaId { get; set; }

    public string? Contraseña { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<Cuentas> Cuenta { get; set; } = new List<Cuentas>();

    public virtual Persona? Persona { get; set; }
}
