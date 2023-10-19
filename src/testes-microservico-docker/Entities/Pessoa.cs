using System;
using System.Collections.Generic;

namespace TestesMicroservicoDocker.Entities;

public partial class Pessoa
{
    public int Idpessoa { get; set; }

    public string Nomecompleto { get; set; } = null!;

    public string? Nomeresumido { get; set; }

    public int Idpessoatipo { get; set; }

    public string Documento { get; set; } = null!;

    public DateTime? Datanascimento { get; set; }

    public DateTime? Datafundacao { get; set; }

    public virtual Pessoatipo IdpessoatipoNavigation { get; set; } = null!;
}
