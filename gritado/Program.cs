static string seAcalmando(string grito)
{
  int s = 0;

    if (grito.Count(c => c == '?') > 1)
    {
      int primeiraInterrogacao = grito.IndexOf("?");
      Console.WriteLine(primeiraInterrogacao);
      grito = grito.Replace("?", "!");
      grito = grito.Insert(primeiraInterrogacao, "?").Trim();
      int primeiraExclamacao = grito.IndexOf("!");
      Console.WriteLine(primeiraExclamacao);
      grito = grito.Replace("!", "");
      grito = grito.Insert(primeiraExclamacao, "!").Trim();
    }
  return grito;
}

Console.WriteLine("Grite!");
string grito = Console.ReadLine();
Console.WriteLine(seAcalmando(grito));