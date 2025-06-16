
namespace ConsoleGame
{
    internal class Game
    {
        Player player = new Player();
        Shop shop = new Shop();

        public class Shop
        {
            public Dictionary<string, int> character { get; set; } = new Dictionary<string, int>()
            {
                // Price of characters
                { "Neo", 2000 },
                { "Trinity", 1500 },
                { "Morpheus", 600 }
            };

            public void ShowShop(Player player)
            {
                Console.WriteLine("╔════════════════════════════ Shop ═════════════════════════════╗");
                foreach (var item in character)
                {
                    Console.WriteLine($"  Personagem: {item.Key} - Preço: {item.Value} moedas");
                }
                Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝");
                Console.WriteLine($"Moedas disponíveis: {player.Coins}");
                Console.WriteLine("Digite o nome do personagem que deseja comprar: ");
                string buy = Console.ReadLine();

                if (string.IsNullOrEmpty(buy))
                {
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    return;
                }

                if (character.ContainsKey(buy) && player.Coins >= character[buy])
                {
                    player.Inventory.Add(buy); // Adiciona o personagem ao inventário
                    player.Coins -= character[buy]; // Reduz as moedas do jogador
                    Console.WriteLine($"Você comprou: {buy}!");
                }
                else if (character.ContainsKey(buy) && player.Coins < character[buy])
                {
                    Console.WriteLine("Moedas insuficientes para comprar este personagem.");
                }
                else
                {
                    Console.WriteLine("Personagem não encontrado.");
                }

            }
        }

        public class Player
        {
            private int health; // Saude //encapsulation
            public string Name { get; set; } //Nome
            public int Level { get; set; } //Nivel
            public int Coins { get; set; } //Moedas
            public List<string> Inventory { get; set; } = new List<string>(); //Inventario
            public int Left { get; set; } //Posição esquerda
            public int Top { get; set; } //Posição topo
            protected bool bonus = false; //Bônus Trinity

            public int Health
            {
                get { return health; }
                set
                {
                    if (value >= 0)
                        health = value;
                }
            }
            public Player() //Construtor
            {
                Name = "Default"; //Nome padrão
                Health = 100; //Saúde inicial
                Level = 1; //Nível inicial
                Coins = 600; //moedas iniciais
                Inventory = new List<string>(); //inventario começa vázio
            }
            public bool Bonus { get { return bonus; } set { bonus = value; } } //Propriedade para acessar o bônus
            public virtual void ActivateBonus()
            {

            }
            public void ShowPlayer()//details 
            {
                Console.WriteLine($"         ╔═══════════════════════════ Details ═══════════════════════╗");
                Console.WriteLine($"           Nome: {Name}");
                Console.WriteLine($"           Saúde: {Health}");
                Console.WriteLine($"           Nível: {Level}");
                Console.WriteLine($"           Moedas: {Coins}");
                Console.WriteLine($"           Inventário: " + (Inventory.Count > 0 ? string.Join(", ", Inventory) : "Empty"));
                Console.WriteLine($"         ╚═══════════════════════════════════════════════════════════╝");
                Console.WriteLine($"Deseja voltar ao menu? (S/N)");
                string choice = Console.ReadLine();
                if (choice.ToUpper() == "N")
                {
                    Environment.Exit(0);
                }
            }

        }

        public void ChoosePlayer(string characterName) //choose character
        {
            if (string.IsNullOrEmpty(characterName)) //verifica se o nome do personagem é vazio/nulo
            {
                Console.WriteLine("Nome do personagem inválido.");
                return;
            }

            if (player.Inventory.Contains(characterName)) //verifica se o personagem está no inventário
            {
                switch (characterName.ToLower())
                {
                    case "neo":
                        Neo neo = new Neo();
                        player.Health = neo.Health;
                        player.Name = neo.Name;
                        player.Bonus = neo.Bonus;
                        break;
                    case "trinity":
                        Trinity trinity = new Trinity();
                        player.Health = trinity.Health;
                        player.Name = trinity.Name;
                        player.Bonus = trinity.Bonus;
                        trinity.ActivateBonus(); // Ativa o bônus de Trinity
                        break;
                    case "morpheus":
                        Morpheus morpheus = new Morpheus();
                        player.Health = morpheus.Health;
                        player.Name = morpheus.Name;
                        player.Bonus = morpheus.Bonus;
                        break;
                    default:
                        Console.WriteLine("Personagem não encontrado no inventário.");
                        return;
                }
                Console.WriteLine($"Personagem escolhido: {player.Name}");
            }
            else
            {
                Console.WriteLine("Personagem não encontrado no inventário.");
            }
            Console.WriteLine($"Deseja voltar ao menu? (S/N)");
            string choice = Console.ReadLine();
            if (choice.ToUpper() == "N")
            {
                Environment.Exit(0);
            }
            else
            {
                MenuInfo();
            }
        }
        public class Trinity : Player //hierancy
        {
            public Trinity()
            {
                Name = "Trinity";
                Health = 2000;
                Inventory.Add("Trinity");
                bonus = true;
            }

            public override void ActivateBonus() //Polymorphism
            {
                if (bonus)
                {
                    Health += 500;
                    Console.WriteLine($"{Name} ativou o bônus e ganhou 500 de saúde!");
                }
                else
                {
                    Console.WriteLine($"Bônus não disponível.");
                }
            }

        }


        public class Neo : Player //hierancy
        {
            public Neo()
            {
                Name = "Neo";
                Health = 3000;
                Inventory.Add("Neo");
            }
        }
        public class Morpheus : Player //hierancy
        {
            public Morpheus()
            {
                Name = "Morpheus";
                Health = 2500;
                Inventory.Add("Morpheus");
            }
        }

        public void IniciarJogo()
        {
            Console.WriteLine("Você está na Matrix RPG!");
            Console.WriteLine("Escolha um nível:");
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║ 1. Nível 1 - Iniciante               ║");
            Console.WriteLine($"║ 2. Nível 2 - Intermediário {(player.Level >= 2 ? "(+)" : "(-)")}       ║");
            Console.WriteLine($"║ 3. Nível 3 - Avançado     {(player.Level >= 3 ? "(+)" : "(-)")}        ║");
            Console.WriteLine("║ 4. Voltar para o Menu                ║ ");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.Write("Digite o número do nível: ");
            string? nivel = Console.ReadLine();

            if (string.IsNullOrEmpty(nivel))
            {
                Console.WriteLine("Nível inválido. Escolha um nível válido.");
            }
            else
            {
                switch (nivel)
                {
                    case "1":
                        if (player != null && player.Level >= 1)
                        {
                            NivelUm();
                        }
                        else
                            Console.WriteLine("Você não pode escolher o Nível 1.");
                        break;
                    case "2":
                        if (player != null && player.Level >= 2)
                        {
                            NivelDois();
                        }
                        else if (player != null && player.Level > 1)
                        {
                            Console.WriteLine("Você já completou o Nível 2. Avance para o próximo nível!");
                        }
                        else
                        {
                            Console.WriteLine("Você precisa completar o Nível 1 para acessar o Nível 2.");
                        }
                        break;
                    case "3":
                        if (player != null && player.Level >= 3)
                        {
                            NivelTres();
                        }
                        else
                        {
                            Console.WriteLine("Você precisa completar o Nível 2 para acessar o Nível 3.");
                        }
                        break;
                    case "4":
                        MenuInfo();
                        break;
                    default:
                        Console.WriteLine("Nível inválido. Escolha um nível válido.");
                        break;
                }
            }
            Console.WriteLine("Pressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
            MenuInfo();        }

        public void NivelUm()
        {
                Console.CursorVisible = false;
                int larguraTela = Console.WindowWidth;
                int alturaTela = Console.WindowHeight;
                Random rand = new Random();
                int playerLeft = 2;
                int playerTop = alturaTela / 2;
                List<(int left, int top)> agentes = new List<(int, int)>();
                List<(int left, int top)> bullets = new List<(int, int)>();
                bool isAlive = true;
                int agentesMortos = 0;

                string[] playerRenders = {
                    " >==[M]=> "
                };

                string[] agentRenders = {
                    "   _A_   ",
                    "   /A\\   "
                };

                string[] bulletRenders = { "-", "~", "█" };

                // Inicializa agentes em posições aleatórias na direita
                for (int i = 0; i < 3; i++)
                {
                    int altura = rand.Next(1, alturaTela - agentRenders.Length - 1);
                    agentes.Add((larguraTela - 8, altura));
                }

                while (isAlive)
                {
                    Console.Clear();
                    Console.SetCursorPosition(0, 0);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Agentes mortos: {agentesMortos} | Moedas: {player.Coins} | Nível: {player.Level}");
                    Console.ResetColor();

                    // INPUT: Move player ou atira
                    if (Console.KeyAvailable)
                    {
                        var key = Console.ReadKey(true).Key;
                        if (key == ConsoleKey.UpArrow && playerTop > 1)
                            playerTop--;
                        if (key == ConsoleKey.DownArrow && playerTop < alturaTela - playerRenders.Length - 1)
                            playerTop++;
                        if (key == ConsoleKey.Spacebar)
                            bullets.Add((playerLeft + 10, playerTop + playerRenders.Length / 2));
                    }

                    // DESENHA PLAYER
                    for (int i = 0; i < playerRenders.Length; i++)
                    {
                        Console.SetCursorPosition(playerLeft, playerTop + i);
                        Console.Write(playerRenders[i]);
                    }

                    // ATUALIZA E DESENHA TIROS
                    for (int i = bullets.Count - 1; i >= 0; i--)
                    {
                        var (left, top) = bullets[i];
                        if (left >= larguraTela - 1)
                        {
                            bullets.RemoveAt(i);
                            continue;
                        }
                        Console.SetCursorPosition(left, top);
                        Console.Write(bulletRenders[(left / 2) % bulletRenders.Length]);
                        bullets[i] = (left + 2, top);
                    }

                    // ATUALIZA E DESENHA AGENTES
                    for (int i = agentes.Count - 1; i >= 0; i--)
                    {
                        var (left, top) = agentes[i];

                        // COLISÃO: agente com player
                        if (left <= playerLeft + 8 && left >= playerLeft)
                        {
                            if ((top < playerTop + playerRenders.Length) && (top + agentRenders.Length > playerTop))
                            {
                                isAlive = false;
                                break;
                            }
                        }

                        // COLISÃO: tiro com agente
                        bool morto = false;
                        for (int j = bullets.Count - 1; j >= 0; j--)
                        {
                            var (bLeft, bTop) = bullets[j];
                            if (bLeft >= left && bLeft <= left + 7 && bTop >= top && bTop < top + agentRenders.Length)
                            {
                                agentes.RemoveAt(i);
                                bullets.RemoveAt(j);
                                morto = true;
                                agentesMortos++;
                                player.Coins += 120;
                                // Adiciona novo agente na direita
                                agentes.Add((larguraTela - 8, rand.Next(1, alturaTela - agentRenders.Length - 1)));
                                if (agentesMortos >= 10)
                                {
                                    player.Level++;
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.SetCursorPosition(larguraTela / 2 - 7, alturaTela / 2);
                                    Console.WriteLine("VOCÊ VENCEU O NÍVEL!");
                                    Console.ResetColor();
                                    Thread.Sleep(2000);
                                    Console.CursorVisible = true;
                                    Console.Clear();
                                    IniciarJogo();
                                    return;
                                }
                            }
                        }
                        if (morto) continue;

                        // DESENHA AGENTE
                        if (left >= 0 && left < larguraTela - 6)
                        {
                            for (int j = 0; j < agentRenders.Length; j++)
                            {
                                Console.SetCursorPosition(left, top + j);
                                Console.Write(agentRenders[j]);
                            }
                        }
                        // MOVE AGENTE
                        agentes[i] = (left - 2, top);
                        if (left < 0)
                        {
                            agentes.RemoveAt(i);
                            agentes.Add((larguraTela - 8, rand.Next(1, alturaTela - agentRenders.Length - 1)));
                        }
                    }

                    // GAME OVER
                    if (!isAlive)
                    {
                        Console.SetCursorPosition(larguraTela / 2 - 5, alturaTela / 2);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("GAME OVER!");
                        Console.ResetColor();
                        Thread.Sleep(2000);
                        break;
                    }

                    Thread.Sleep(60);
                }
                Console.CursorVisible = true;
        }

        private string ComputeSha256Hash(string input)
            {
                using (var sha256 = System.Security.Cryptography.SHA256.Create())
                {
                    var bytes = System.Text.Encoding.UTF8.GetBytes(input);
                    var hash = sha256.ComputeHash(bytes);
                    return BitConverter.ToString(hash).Replace("-", "").ToLower();
                }
            }
        public void NivelDois()
        {
            Console.Clear();
            string key = "redpill";
            string message = "Bem-vindo a Zion!";
            string hash = ComputeSha256Hash(key);
            int maxAttempts = player.Name == "Trinity" ? 3 : 2;
            int attemptsLeft = maxAttempts;
            bool trinityBonusUsed = false;

            // Bordas e interface


            string hashShort = hash.Length > 48 ? hash.Substring(0, 48) : hash;
            string hashRest = hash.Length > 48 ? hash.Substring(48) : "";


            Console.WriteLine(@"
                ⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿   Cypto Challenge     ⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
                ⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
                ⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⠿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
                ⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠟⠉⠉⠉⠀⠀⠉⠙⠿⣿⣿⣿⣿⣿⣿⣿⣿⣿
                ⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⠛⠛⠉⠉⠉⠉⠙⠛⠿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠟⠉⠀⠰⣏⣤⣤⣆⣠⣀⣀⣀⣀⡉⠛⣿⣿⣿⣿⣿⣿
                ⣿⣿⣿⣿⣿⣿⣿⠋⠀⢲⢢⢤⣀⡀⠀⠀⠀⠀⠀⠀⠈⢻⣿⣿⣿⣿⣿⡟⠀⠀⠀⠀⠘⣿⣾⡝⠻⢷⠿⠟⡿⠋⠀⠘⣿⣿⣿⣿⣿
                ⣿⣿⣿⣿⠟⠋⠀⠠⣔⣮⣭⠉⠙⠉⢁⡥⠞⠀⠀⠀⠀⠈⣿⣿⣿⣿⣿⡇⠂⠀⠀⠒⢄⠈⠉⠿⣧⡄⠲⠟⣿⣶⣤⡄⠈⠻⣿⣿⣿
                ⣿⣿⣋⣥⣤⣲⣼⣽⣿⣿⠗⣦⣄⡴⠋⠔⠁⠀⠀⠀⠀⠀⢸⣿⣿⣿⣿⣿⡤⠀⠀⠀⠀⠉⠈⢀⠹⠿⡟⠒⠛⢿⣿⣾⣤⠄⠙⢿⣿
                ⣿⣿⣿⣿⣿⣿⣿⣿⣿⠏⠉⠀⠀⠀⡰⠀⢩⠂⠀⣆⠀⠀⣸⣿⣿⣿⣿⣿⣷⡤⠀⠒⠢⡄⢄⠀⠡⣄⠑⣤⡄⠀⠙⢻⣿⣿⣿⣾⣿
                ⣿⣿⣿⣿⣿⣿⣿⣿⣃⡀⠈⣷⡆⢠⣅⣶⠀⢤⣿⠁⠀⣸⣿⣿⣿⣿⣿⣿⣿⣿⣷⡀⠀⠹⣀⠈⠻⣿⣽⡛⢿⣧⠀⠀⠻⣿⣿⣿⣿
                ⣿⣿⣿⣿⣿⣿⣿⠇⠀⠀⣽⡟⢠⣿⡿⠀⠀⡼⠤⢄⣰⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣆⢀⣹⡄⠀⠀⠹⡄⠀⠈⢆⠀⢨⣿⣿
                ⣿⣿⣿⣿⣿⣿⣿⠠⡀⠀⣿⠃⠾⡿⠃⠀⢠⠃⠀⢨⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣿⣿⣿⣿
                ⣿⣿⣿⣿⣿⣿⣿⣧⣄⣤⠃⠀⢸⠁⠀⠀⣾⣀⣴⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
                ⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣧⣦⣼⣷⣦⣼⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
                ⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
            // Render a linha da borda, o hash e o alinhamento correto
            Console.WriteLine("\n");
            Console.WriteLine($"Hash alvo: {hashShort,-48}");
            if (!string.IsNullOrEmpty(hashRest))
                Console.WriteLine($"{hashRest,-58}");
            Console.WriteLine($"{hashRest,-58}");

            Console.WriteLine($"Vida: {player.Health,-6} | Moedas: {player.Coins,-6} | Tentativas: {attemptsLeft}/{maxAttempts,-2}{"",-13}");


            while (true)
            {
                Console.Write("\n[>] Insira a chave: ");
                string? input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Chave inválida. Tente novamente.");
                    continue;
                }

                attemptsLeft--;
                if (input.ToLower() == key)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
                    Console.WriteLine("║                [DECRYPTION SUCCESSFUL]                ║");
                    Console.WriteLine("╚═══════════════════════════════════════════════════════╝");
                    Console.WriteLine($"Mensagem descriptografada: {message}");
                    player.Level = 3; 
                    player.Coins += 200; // Recompensa
                    Console.WriteLine($"Parabéns! Você desbloqueou o Nível 3!");
                    Console.WriteLine($" Vida: {player.Health}. Moedas: {player.Coins}.");
                    Console.WriteLine("Pressione qualquer tecla para voltar ao menu...");
                    Console.ReadKey();
                    return;
                }
                else
                {
                    Console.WriteLine("Chave incorreta. Sistema de segurança ativado!");
                    player.Health -= 50;
                    if (player.Health <= 0)
                    {
                        // Bônus Trinity: mais uma chance
                        if (player.Name == "Trinity" && !trinityBonusUsed)
                        {
                            Console.WriteLine("Trinity ativou sua habilidade especial: Segunda Chance!");

                            player.Health = 100;
                            trinityBonusUsed = true;
                            attemptsLeft = maxAttempts;
                            continue;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
                            Console.WriteLine("║                 [SYSTEM LOCKOUT]                       ║");
                            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
                            Console.Write($"Game Over. Vida: {player.Health}.");
                            Console.WriteLine("Você retornou ao Nível 1.");
                            player.Level = 1; // Volta para o nível 1
                            IniciarJogo();
                            Console.ReadKey();
                            return;
                        }
                    }
                    if (attemptsLeft == 0)
                    {
                        // Se for Trinity e ainda não usou o bônus, oferece o bônus
                        if (player.Name == "Trinity" && !trinityBonusUsed)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("\nTrinity ativou seu bônus especial: +1 tentativa extra!");
                            Console.WriteLine("Dica: A chave é a escolha que você fez para ver a verdade.");
                            Console.ResetColor();
                            trinityBonusUsed = true;
                            attemptsLeft = 1; // Ganha só mais uma tentativa
                            continue;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
                            Console.WriteLine("║                 [SYSTEM LOCKOUT]                       ║");
                            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
                            Console.Write($"Game Over.  Vida: {player.Health}.");
                            Console.WriteLine("Você retornou ao Nível 1.");
                            player.Level = 1; // Volta para o nível 1
                            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
                            Console.ReadKey();
                            return;
                        }
                    }
                    Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
                    Console.WriteLine($" Vida: {player.Health} | Moedas: {player.Coins} | Tentativas: {attemptsLeft}/{maxAttempts}                ");
                    Console.WriteLine("╚═══════════════════════════════════════════════════════╝");
                }
            }
        }

        public void NivelTres()
        {
            Console.Clear();
            Console.WriteLine("NÍVEL 3: O Labirinto do Código");
            Console.WriteLine("Desarme a bomba da Matrix! Repita a sequência de códigos corretamente.");
            Console.WriteLine("A cada rodada, a sequência aumenta. Digite cada código na ordem, rápido!");
            Console.WriteLine("Aperte qualquer tecla para começar...");
            Console.ReadKey();

            string[] codigos = { "ZX9", "101", "MTRX", "@#1", "NEO", "TRI", "AGT", "Z1O", "XOX", "777" };
            Random rand = new Random();
            List<string> sequencia = new List<string>();
            int rodadas = 4;
            int tempoLimite = 10; // segundos por rodada

            for (int rodada = 1; rodada <= rodadas; rodada++)
            {
                sequencia.Add(codigos[rand.Next(codigos.Length)]);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Rodada {rodada}/{rodadas}");
                Console.WriteLine("Memorize a sequência:");
                Console.WriteLine(string.Join("  ", sequencia));
                Console.ResetColor();
                Thread.Sleep(2000 + rodada * 500); // Mostra a sequência por um tempo crescente
                Console.Clear();
                Console.WriteLine($"Digite a sequência (um código por linha). Você tem {tempoLimite + rodada * 2} segundos!");

                bool acertou = true;
                DateTime inicio = DateTime.Now;
                for (int i = 0; i < sequencia.Count; i++)
                {
                    string resposta = ReadLineComTimeout(tempoLimite + rodada * 2 - (int)(DateTime.Now - inicio).TotalSeconds);
                    if (resposta == null || resposta.ToUpper() != sequencia[i])
                    {
                        acertou = false;
                        break;
                    }
                }

                if (acertou)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Sequência correta!");
                    Console.ResetColor();
                    player.Coins += 150;
                    Thread.Sleep(1200);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Sequência errada ou tempo esgotado!");
                    Console.ResetColor();
                    player.Health -= 100;
                    if (player.Health <= 0)
                    {
                        Console.WriteLine("Você perdeu toda a vida! Voltando ao Nível 1...");
                        player.Level = 1;
                        IniciarJogo();
                        return;
                    }
                    Thread.Sleep(1200);
                    break;
                }
            }

            if (player.Health > 0 && sequencia.Count == rodadas)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Parabéns! Você desarmou a bomba e venceu a Matrix!");
                Console.ResetColor();
                player.Level = 4;
                player.Coins += 500;
            }
            else
            {
                Console.WriteLine("A bomba explodiu! Tente novamente.");
                player.Level = 2;
            }

            Console.WriteLine("Pressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
        }

        public string ReadLineComTimeout(int segundos)
        {
            if (segundos <= 0) return null;
            Task<string> task = Task.Run(() => Console.ReadLine());
            bool terminou = task.Wait(TimeSpan.FromSeconds(segundos));
            if (terminou)
                return task.Result;
            else
                return null;
        }
        public void Manual()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@" 
                  _________________________
                 /    Matrix RPG Manual    \
                /___________________________\
                |  _______________________  |
                | |                       | |
                | |  'There is no spoon.' | |
                | |   - Spoon Boy, 1999   | |
                | |                       | |
                | |  Controles:           | |
                | |  ↑ - Move para cima   | |
                | |  ↓ - Move para baixo  | |
                | |  Espaço - Atira       | |
                | |                       | |
                | |  Nível 1:             | |
                | |  Derrote 10 agentes   | |
                | |  Cada agente = 120₵   | |
                | |                       | |
                | |  Nível 2:             | |
                | |  Decifre o código     | |
                | |  Use as dicas         | |
                | |                       | |
                | |  Nível 3:             | |
                | |  Memorize sequências  | |
                | |  Digite rapidamente   | |
                | |                       | |
                | |_______________________| |
                |___________________________|");
            Console.WriteLine("\nDeseja voltar ao menu? (S/N)");
            string choice = Console.ReadLine();
            if (choice.ToUpper() == "N")
            {
                Environment.Exit(0);
            }
        }
        public void MenuInfo()
        {
            Console.Clear();
            for (int i = 0; i < 3; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(@"● ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(@"◯ ");
                Console.ResetColor();
                Thread.Sleep(350);
            }
            Console.WriteLine();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"
                ███    ███  █████   ████████ ██████  ██ ██   ██
                ████  ████ ██   ██     ██    ██   ██ ██  ██ ██ 
                ██ ████ ██ ███████     ██    ██████  ██   ███  
                ██  ██  ██ ██   ██     ██    ██   ██ ██  ██ ██ 
                ██      ██ ██   ██     ██    ██   ██ ██ ██   ██  
                ");
            Console.WriteLine("         ╔══════════════════════════ Menu ═══════════════════════════╗");
            Console.WriteLine("         ║ 1. Jogar                                                  ║");
            Console.WriteLine("         ║ 2. Sair                                                   ║");
            Console.WriteLine("         ║ 3. Manual                                                 ║");
            Console.WriteLine("         ║ 4. Loja                                                   ║");
            Console.WriteLine("         ║ 5. Personalizar Personagem                                ║");
            Console.WriteLine("         ║ 6. Detalhes do Personagem                                 ║");
            Console.WriteLine("         ╚═══════════════════════════════════════════════════════════╝");
            Console.Write("          Escolha uma opção =>  ");
            string option = Console.ReadLine();

            if (string.IsNullOrEmpty(option))
            {
                Console.WriteLine("(X) Opção inválida. Tente novamente.");
                return;
            }

            switch (option)
            {
                case "1":
                    IniciarJogo();
                    break;
                case "2":
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Simulador encerrando ");
                    for (int i = 0; i <= 3; i++)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(@"● ");
                        Console.Write(@"◯ ");
                        if (i == 3)
                        {
                            Console.Write(@"Até logo!");
                        }
                        Console.ResetColor();
                        Thread.Sleep(350);
                    }
                    Console.WriteLine();
                    break;
                case "3":
                    Manual();
                    MenuInfo();
                    break;
                case "4":
                    Console.Clear();
                    shop.ShowShop(player);
                    MenuInfo();
                    break;
                case "5":
                    if (player.Inventory.Count == 0)
                    {
                        Console.WriteLine("Você ainda não tem personagens no inventário. Adicione um personagem primeiro.");

                        
                    }
                    else
                    {
                        Console.WriteLine("Escolha um personagem do inventário:");
                        Console.WriteLine($"╔═════════════════════════════════╗");
                        foreach (var character in player.Inventory) // Lista de personagens
                        {
                            Console.WriteLine($"  {character}");
                        }
                        Console.WriteLine($"╚═════════════════════════════════╝");
                        string characterName = Console.ReadLine();
                        ChoosePlayer(characterName); // Chama o método para escolher o personagem
                    }
                    break;
                case "6":
                    player.ShowPlayer();
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }

        }
            static void Main(string[] args)
            {
                Game game = new Game();
                game.MenuInfo();

            }
        
    }
}