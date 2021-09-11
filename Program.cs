using System;
using Dio.Series.Classes;

namespace Dio.Series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario.ToUpper() != "X")
            {
                switch (opcaoUsuario)
                {
                    case "1":
                        
                        ListarSeries();
                        Console.WriteLine("\nAperte qualquer tecla para continuar");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "2":
                        Console.Clear();
                        InserirSerie();
                        break;
                    case "3":
                        Console.Clear();
                        AtualizarSerie();
                        break;
                    case "4":
                        Console.Clear();
                        ExcluirSerie();
                        break;
                    case "5":
                        Console.Clear();
                        VisualizarSerie();
                        break;
                    case "C":
                        Console.Clear();
                        break;

                    default:
                        Console.Write("Escolha uma opção correta");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }

                opcaoUsuario = ObterOpcaoUsuario();
            }

            Console.WriteLine("Obrigado por utilizar nossos serviços.");
            Console.ReadLine();
        }

        private static void ExcluirSerie()
        {
            ListarSeries();

            Console.Write("\nDigite o id da série: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            Console.Write("Voce realmente deseja excluir ? (S/N)");
            string decisao = Console.ReadLine();
            switch(decisao.ToUpper())
            {
                case "S":
                    repositorio.Exclui(indiceSerie);
                    Console.Clear();
                    break;
                case "N":
                    Console.Clear();
                    break;
            }
        }

        

        private static void VisualizarSerie()
        {
            Console.Write("Digite o id da série: \n");
            int indiceSerie = int.Parse(Console.ReadLine());

            var serie = repositorio.RetornaPorId(indiceSerie);

            Console.WriteLine(serie);
            Console.ReadKey();
            Console.Clear();
        }

        private static void AtualizarSerie()
        {
            Console.Write("Digite o id da série: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            // https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=netcore-3.1
            // https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getname?view=netcore-3.1
            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
            }
            var serie = dados();
            repositorio.Atualiza(indiceSerie,serie);
            Console.Clear();
        }

        private static void ListarSeries()
        {
            Console.WriteLine("Listar séries");

            var lista = repositorio.Lista();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhuma série cadastrada.");
                Console.Clear();
                return;
            }

            foreach (var serie in lista)
            {
                var excluido = serie.retornaExcluido();
                Console.WriteLine("#ID {0}: - {1}  {2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? "*Excluido*":""));
            }
        }

        private static void InserirSerie()
        {
            
            Console.WriteLine("Inserir nova série ");

            // https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=netcore-3.1
            // https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getname?view=netcore-3.1
            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
            }
            var serie = dados();
            repositorio.Insere(serie);
            Console.Clear();

        }

        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("DIO Séries a seu dispor!!!");
            Console.WriteLine("Informe a opção desejada:");

            Console.WriteLine("1- Listar séries");
            Console.WriteLine("2- Inserir nova série");
            Console.WriteLine("3- Atualizar série");
            Console.WriteLine("4- Excluir série");
            Console.WriteLine("5- Visualizar série");
            Console.WriteLine("C- Limpar Tela");
            Console.WriteLine("X- Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }

        static Serie dados()
        {
            
            Console.Write("\nDigite o gênero entre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());
            
            Console.Write("\nDigite o Título da Série: ");
            string entradaTitulo = Console.ReadLine();
            
            Console.Write("\nDigite o Ano de Início da Série: ");
            int entradaAno = int.Parse(Console.ReadLine());
            
            Console.Write("\nDigite a Descrição da Série: ");
            string entradaDescricao = Console.ReadLine();

            Serie novaSerie = new Serie(id: repositorio.ProximoId(),
                                                    genero: (Genero)entradaGenero,
                                                    titulo: entradaTitulo,
                                                    ano: entradaAno,
                                                    descricao: entradaDescricao);

            return novaSerie;
        }
    }
}