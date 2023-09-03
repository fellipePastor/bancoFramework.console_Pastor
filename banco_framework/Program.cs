using Domain.Model;
using System.ComponentModel.Design;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.Clear();
        Console.WriteLine("Seja bem vindo ao banco Framework");
        Console.WriteLine("Por favor, identifique-se");
        Console.WriteLine("");
        var pessoa = Identificacao();
    }

    static Pessoa Identificacao()
    {
        var pessoa = new Pessoa();

        Console.WriteLine("Seu número de identificação:");
        pessoa.Id = int.Parse(Console.ReadLine());

        Console.WriteLine("Seu nome:");
        pessoa.Nome = Console.ReadLine();

        Console.WriteLine("Seu CPF:");
        pessoa.Cpf = Console.ReadLine();
        Console.Clear();

        MenuOpcoes(pessoa);
        
        Console.ReadKey();
        
        return pessoa;
    }
    private static void MenuOpcoes(Pessoa pessoa)
    {
        Console.WriteLine($"Como posso ajudar {pessoa.Nome}?");

        Console.WriteLine($"1-Depósito");

        Console.WriteLine($"2-Saque");

        Console.WriteLine($"3-Sair");

        Console.WriteLine($"----------");

        Console.WriteLine($"Selecione uma opção:");

        string respostaUsuario = Console.ReadLine();

        RespostaParaMenuOpcoes(respostaUsuario, pessoa);
    }
    private static void RespostaParaMenuOpcoes(string respostaUsuario,Pessoa pessoa)
    {
        switch (respostaUsuario)
        {
            case "1":
                Depositar();
                break;
            case "2": 
                Saque(); 
                break;
            case "3":
                Environment.Exit(0);
                break;
            default:
                Console.Clear();
                MenuOpcoes(pessoa);
                break;
        }
    }

    private static void Saque()
    {
        Console.WriteLine("Saque");
    }

    private static void Depositar()
    {
        Console.WriteLine("Depósito");
    }
}