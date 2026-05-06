var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/orcamentos", (CriarOrcamentoRequest request) =>
{
    var erros = new List<string>();

    if (request.ClienteId <= 0)
        erros.Add("clienteId é obrigatório.");

    if (request.VeiculoId <= 0)
        erros.Add("veiculoId é obrigatório.");

    if (request.Itens == null || !request.Itens.Any())
        erros.Add("O orçamento deve possuir pelo menos 1 item.");

    if (request.Itens != null)
    {
        for (int i = 0; i < request.Itens.Count; i++)
        {
            var item = request.Itens[i];

            if (string.IsNullOrWhiteSpace(item.Descricao))
                erros.Add($"Item {i + 1}: descrição é obrigatória.");

            if (item.Quantidade <= 0)
                erros.Add($"Item {i + 1}: quantidade deve ser maior que zero.");

            if (item.ValorUnitario <= 0)
                erros.Add($"Item {i + 1}: valor unitário deve ser maior que zero.");
        }
    }

    if (erros.Any())
    {
        return Results.BadRequest(new
        {
            sucesso = false,
            erros
        });
    }

    var itensCalculados = request.Itens.Select(i => new
    {
        i.Descricao,
        i.Quantidade,
        i.ValorUnitario,
        ValorTotal = i.Quantidade * i.ValorUnitario
    }).ToList();

    var valorTotal = itensCalculados.Sum(i => i.ValorTotal);

    var orcamento = new
    {
        Id = 1,
        request.ClienteId,
        request.VeiculoId,
        Status = "Aberto",
        ValorTotal = valorTotal,
        DataCriacao = DateTime.UtcNow,
        Itens = itensCalculados
    };

    return Results.Ok(new
    {
        sucesso = true,
        mensagem = "Orçamento criado com sucesso.",
        dados = orcamento
    });

});

app.Run();

public record CriarOrcamentoRequest(
    int ClienteId,
    int VeiculoId,
    List<OrcamentoItemRequest> Itens
);

public record OrcamentoItemRequest(
    string Descricao,
    int Quantidade,
    decimal ValorUnitario
);