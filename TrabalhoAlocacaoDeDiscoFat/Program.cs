using ConsoleTables;
using TrabalhoAlocacaoDeDiscoFat.Models;
using TrabalhoAlocacaoDeDiscoFat.Service;

var memoryService = new MemoryService();


var menus = new Dictionary<int, Action>();
menus.Add(1, AdicionarArquivo);
menus.Add(2, ExibirMemory);
menus.Add(3, ExibirFat);
menus.Add(4, ExcluirArquivos);
menus.Add(5, ExibirArquivos);

MainMenu();
void MainMenu()
{
    int menu = 0;
    do
    {
        Console.Clear();
        Console.WriteLine("1 - Adiconar Arquivo");
        Console.WriteLine("2 - Visulizar Memeoria Principal");
        Console.WriteLine("3 - Visulizar Fat");
        Console.WriteLine("4 - Deleta Arquivo");
        Console.WriteLine("5 - Visualizar todos Arquivos");
        Console.WriteLine("Digite qualquer outro numero para sair");
        Console.Write("Digite o numero do menu: ");
        menu = Convert.ToInt32(Console.ReadLine());
        Console.Clear();
        menus[menu]();

    } while (menu > 0 || menu < 6);
}


void AdicionarArquivo()
{
    int tamanho;
    string arquivo;
    Console.Write("Escreva o nome do arquivo: ");
    arquivo = Console.ReadLine();
    Console.Write("Escreva o tamanho do arquivo: ");
    tamanho = Convert.ToInt32(Console.ReadLine());
    var indexs = memoryService.GetFreeIndex(tamanho);
    if(indexs == null)
    {
        Console.WriteLine("Sem Espaço");
        Console.ReadLine();
        return;
    }

    var startIndex = memoryService.AddData(indexs, arquivo!);
    memoryService.AddArquivo(arquivo, tamanho, startIndex);
    Console.WriteLine("Arquivo salvo Com sucesso!!!");
    Console.ReadLine();
    //ExibirMemory();
    return;
}


void ExibirMemory()
{
    var data = memoryService.GetMemory();

    for (int i = 0; i < data.Length; i++)
    {
        Console.Write($"|{i}:{data[i]}|\t");
    }

    Console.ReadLine();
}

void ExibirFat()
{
    var data = memoryService.GetFat();
    Console.WriteLine("Tabela Fat");

    var table =  new ConsoleTable("Index", "Next Index", "Memory Poiter");
    for (int i = 0; i < data.Length; i++)
    {
        if (data[i] == null) continue;
        table.AddRow(i, data[i]!.Value.nextIndex, data[i]!.Value.DataIndex);
    }

    table.Write();
    Console.ReadLine();
}

void ExibirArquivos()
{
    var data = memoryService.GetArquivos();
    Console.WriteLine("Arquivos");

    var table = new ConsoleTable("Nome", "Tamanho", "Start Index");
    for (int i = 0; i < data.Count; i++)
        table.AddRow(data[i].Name, data[i].Space, data[i].StartIndex);

    table.Write();
    Console.ReadLine();
}

void ExcluirArquivos()
{
    string nome;

    Console.WriteLine("Digite o nome do aquivo que quer deletar.");
    Console.Write("Nome do arquivo: ");
    nome = Console.ReadLine();
    var arquivo = memoryService.GetArquivo(nome);
    memoryService.RemoveData(arquivo.StartIndex);
    memoryService.ExcluirArquivo(nome);
    Console.WriteLine("Arquivo deletado com sucesso!!!");
    Console.ReadLine();
    return;

}