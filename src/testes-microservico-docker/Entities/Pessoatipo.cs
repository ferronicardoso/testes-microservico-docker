using System;
using System.Collections.Generic;

namespace TestesMicroservicoDocker.Entities;

public partial class Pessoatipo
{
    public int Idpessoatipo { get; set; }

    public string Tag { get; set; } = null!;

    public string Descricao { get; set; } = null!;

    public virtual ICollection<Pessoa> Pessoas { get; set; } = new List<Pessoa>();
}
