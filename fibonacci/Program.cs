static List<int> Fibonacci(int quantidadeElementos)
{
  int a = 1;
  int b = 0; 
  int c = 0;
  List<int> listaFibonacci = new List<int>([b,a]);
  for (int i = 0; i < quantidadeElementos - 2; i++)
  {
    c = a + b;
    b = a;
    a = c;
    listaFibonacci.Add(c);
  }
  return listaFibonacci;
}

Console.WriteLine("Digite um número de elementos a serem gerados na sequência de Fibonacci");
int quantidadeElementos = Convert.ToInt32(Console.ReadLine());
List<int> lista = Fibonacci(quantidadeElementos);

foreach (var i in lista)
{
  Console.Write(i + " ");
}
Console.WriteLine("");

