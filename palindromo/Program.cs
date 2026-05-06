static bool EPalindromo(string palavra)
{
  bool resultado = false;
  string palavraInvertida = "";
  palavra = palavra.ToLower().Replace(" ", "");

  for (int i = palavra.Length - 1; i >= 0; i--)
  {
    palavraInvertida += palavra[i]; 
  }

  if (palavraInvertida == palavra)
    resultado = true;

  return resultado;
}

Console.WriteLine("Digite uma palavra ou frase e veja se é um palíndromo");
string entrada = Console.ReadLine();

if (entrada == null || entrada == "") 
{
  Console.WriteLine("Palavra não enviada.");
  return;
}

Console.WriteLine(EPalindromo(entrada) ? "É palindromo" : "Não é um palindromo");

