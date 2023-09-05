using Domain.Model;
using Application;
using CpfCnpjLibrary;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.Clear();
        Console.WriteLine("Seja bem vindo ao banco Framework");
        Console.WriteLine("Por favor, identifique-se");
        Console.WriteLine("");
        var cliente = Identificacao();
    }

    static Pessoa Identificacao()
    {
        var cliente = new Cliente();

        while (true)
        {
            var listaErros = new List<string>();

            Console.Clear();

            Console.WriteLine("Seu número de identificação:");
            string identificador = Console.ReadLine();

            if (int.TryParse(identificador, out int id))
                cliente.Id = id;
            else
                listaErros.Add("Identificação inválida. Digite apenas números.");

            Console.WriteLine("Seu nome:");
            cliente.Nome = Console.ReadLine();

            Console.WriteLine("Seu CPF:");
            var cpf = Console.ReadLine();

            if (Cpf.Validar(cpf))
                cliente.Cpf = cpf;
            else
                listaErros.Add("CPF digitado não é válido");

            Console.WriteLine("Seu saldo:");
            var saldoInformado = Console.ReadLine();

            if (float.TryParse(saldoInformado, out float saldo) && saldo > 0)
                cliente.Saldo = saldo;
            else
                listaErros.Add("Saldo não é válido. Digite um valor numérico maior que zero.");

            if (listaErros.Count == 0)
            {
                Console.Clear();
                MenuOpcoes(cliente);
                break; 
            }
            else
            {
                Console.Clear();
                foreach (var erro in listaErros)
                {
                    Console.WriteLine(erro);
                }
                Console.WriteLine("\nPressione qualquer tecla para corrigir os erros e continuar...");
                Console.ReadKey();
            }
        }

        return cliente;
    }
    private static void MenuOpcoes(Cliente cliente)
    {
        Console.WriteLine($"Como posso ajudar {cliente.Nome}?");

        Console.WriteLine($"1-Depósito");

        Console.WriteLine($"2-Saque");

        Console.WriteLine($"3-Sair");

        Console.WriteLine($"----------");

        Console.WriteLine($"Selecione uma opção:");

        string respostaUsuario = Console.ReadLine();

        RespostaParaMenuOpcoes(respostaUsuario, cliente);
    }
    private static void RespostaParaMenuOpcoes(
    string respostaUsuario, 
    Cliente cliente)
    {
        switch (respostaUsuario)
        {
            case "1":
                Depositar(ref cliente);
                MenuOpcoes(cliente);
                break;
            case "2": 
                Saque(ref cliente); 
                MenuOpcoes(cliente);
                break;
            case "3":
                Environment.Exit(0);
                break;
            default:
                Console.Clear();
                MenuOpcoes(cliente);
                break;
        }
    }
    private static void Depositar(
    ref Cliente cliente)
    {
        Console.Clear();
        
        Calculo calculo = new Calculo();

        Console.WriteLine("Digite o valor a ser depositado:");        

        float saldoInformadoParaDeposito = float.Parse(Console.ReadLine());

        cliente.Saldo = calculo.Soma(cliente.Saldo, saldoInformadoParaDeposito);

        Console.WriteLine($"Saldo atual é R${cliente.Saldo}:");
    }

    private static void Saque(
    ref Cliente cliente)
    {
        Console.Clear();

        Calculo calculo = new Calculo();

        Console.WriteLine("Digite o valor a ser sacado:");

        float saldoInformadoParaSaque = float.Parse(Console.ReadLine());
        
        cliente.Saldo = calculo.Subtracao(cliente.Saldo, saldoInformadoParaSaque);

        Console.WriteLine($"Saldo atual é R${cliente.Saldo}:");
    }

}