using System;
using System.Collections.Generic;

namespace TestNeoris.Models;

public partial class Cliente
{
    public int Clienteid { get; set; }

    public int? PersonaId { get; set; }

    public string? Contraseña { get; set; }

    public string? Estado { get; set; }

    public virtual Persona? Persona { get; set; }
}
