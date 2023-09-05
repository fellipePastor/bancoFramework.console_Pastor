using Domain.Model;
using Application;
using CpfCnpjLibrary;
using Repository;

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

    static Cliente Identificacao()
    {
        Console.Clear();
        int clientId = ObterIdentificacao();

        Cliente cliente = ObterClienteBancoDeDados(clientId);

        if (cliente == null)
        {
            cliente = PreencherDadosCliente(clientId);
            InserirCliente(cliente);
        }

        Console.Clear();
        MenuOpcoes(cliente);

        return cliente;
    }

    static int ObterIdentificacao()
    {
        while (true)
        {
            Console.WriteLine("Seu número de identificação:");
            if (int.TryParse(Console.ReadLine(), out int id))
                return id;

            Console.WriteLine("Identificação inválida. Digite apenas números.");
        }
    }

    static Cliente PreencherDadosCliente(int clientId)
    {
        Cliente cliente = new Cliente();

        Console.WriteLine("Seu nome:");
        cliente.Nome = Console.ReadLine();

        Console.WriteLine("Seu CPF:");
        cliente.Cpf = ObterCpfValido();

        Console.WriteLine("Seu saldo:");
        cliente.Saldo = ObterSaldoValido();

        cliente.Id = clientId;

        return cliente;
    }

    static string ObterCpfValido()
    {
        while (true)
        {
            string cpf = Console.ReadLine();
            if (Cpf.Validar(cpf))
                return cpf;

            Console.WriteLine("CPF digitado não é válido. Digite um CPF válido:");
        }
    }

    static float ObterSaldoValido()
    {
        while (true)
        {
            if (float.TryParse(Console.ReadLine(), out float saldo) && saldo > 0)
                return saldo;

            Console.WriteLine("Saldo não é válido. Digite um valor numérico maior que zero:");
        }
    }

    private static Cliente ObterClienteBancoDeDados(int id)
    {
        BancoFrameworkOperacoes bancoDeDados = new BancoFrameworkOperacoes();

        var cliente = bancoDeDados.GetById(id);

        return cliente;
    }

    private static void InserirCliente(Cliente cliente)
    {
        BancoFrameworkOperacoes bancoDeDados = new BancoFrameworkOperacoes();

        bancoDeDados.Insert(cliente);
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
        BancoFrameworkOperacoes bancoDeDados = new BancoFrameworkOperacoes();

        Calculo calculo = new Calculo();

        Console.Clear();

        Console.WriteLine("Digite o valor a ser depositado:");        

        float saldoInformadoParaDeposito = float.Parse(Console.ReadLine());

        cliente.Saldo = calculo.Soma(cliente.Saldo, saldoInformadoParaDeposito);

        bancoDeDados.UpdateSaldo(cliente.Id,cliente.Saldo); 

        Console.WriteLine($"Saldo atual é R${cliente.Saldo}:");
    }

    private static void Saque(
    ref Cliente cliente)
    {
        BancoFrameworkOperacoes bancoDeDados = new BancoFrameworkOperacoes();
        
        Calculo calculo = new Calculo();

        Console.Clear();

        Console.WriteLine("Digite o valor a ser sacado:");

        float saldoInformadoParaSaque = float.Parse(Console.ReadLine());
        
        cliente.Saldo = calculo.Subtracao(cliente.Saldo, saldoInformadoParaSaque);

        bancoDeDados.UpdateSaldo(cliente.Id,cliente.Saldo); 

        Console.WriteLine($"Saldo atual é R${cliente.Saldo}:");
    }

}