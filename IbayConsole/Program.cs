using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;


class Program
{
    static async Task Main(string[] args)
    {
        using var client = new HttpClient();
        string mainChoice, subChoice;
        var response = await client.GetAsync("https://localhost:7001");
        var responseContent = await response.Content.ReadAsStringAsync();

        do
        {
            Console.WriteLine("Menu Ibay, que voulez-vous faire ?");
            Console.WriteLine("1. Utilisateurs");
            Console.WriteLine("2. Produits");
            Console.WriteLine("3. Paniers");
            Console.WriteLine("4. Paiements");
            Console.WriteLine("5. Quitter");
            Console.Write("Entrez votre choix : ");
            mainChoice = Console.ReadLine();

            switch (mainChoice)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("Utilisateurs Sous-Menu");
                    Console.WriteLine("1. Create");
                    Console.WriteLine("2. GetAll");
                    Console.WriteLine("3. GetById");
                    Console.WriteLine("4. Update");
                    Console.WriteLine("5. Delete");
                    Console.WriteLine("7. Back");
                    Console.Write("Enter your choice: ");
                    subChoice = Console.ReadLine();
                    switch (subChoice)
                    {
                        case "1":
                            Console.Clear();
                            response = await client.GetAsync("https://localhost:7001/user/insert");
                            response.EnsureSuccessStatusCode();
                            responseContent = await response.Content.ReadAsStringAsync();
                            Console.WriteLine(responseContent);
                            break;
                        case "2":
                            Console.Clear();
                            response = await client.GetAsync("https://localhost:7001/user");
                            response.EnsureSuccessStatusCode();
                            responseContent = await response.Content.ReadAsStringAsync();
                            Console.WriteLine(responseContent) ;
                            break;
                        case "3":
                            Console.Clear();
                            response = await client.GetAsync("https://localhost:7001/user/id");
                            response.EnsureSuccessStatusCode();
                            responseContent = await response.Content.ReadAsStringAsync();
                            Console.WriteLine(responseContent);
                            break;
                        case "4":
                            Console.Clear();
                            response = await client.GetAsync("https://localhost:7001/user/update");
                            response.EnsureSuccessStatusCode();
                            responseContent = await response.Content.ReadAsStringAsync();
                            Console.WriteLine(responseContent);
                            break;
                        case "5":
                            Console.Clear();
                            response = await client.GetAsync("https://localhost:7001/user/delete/id");
                            response.EnsureSuccessStatusCode();
                            responseContent = await response.Content.ReadAsStringAsync();
                            Console.WriteLine(responseContent);
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("Choix invalide, essayez à nouveau");
                            break;

                    }
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("Produits Sous-Menu");
                    Console.WriteLine("1. Create");
                    Console.WriteLine("2. GetAll");
                    Console.WriteLine("3. GetById");
                    Console.WriteLine("4. Update");
                    Console.WriteLine("5. Delete");
                    Console.WriteLine("6. Search");
                    Console.WriteLine("7. Back");
                    Console.Write("Enter your choice: ");
                    subChoice = Console.ReadLine();
                    switch (subChoice)
                    {
                        case "1":
                            Console.Clear();
                            response = await client.GetAsync("https://localhost:7001/product/insert");
                            response.EnsureSuccessStatusCode();
                            responseContent = await response.Content.ReadAsStringAsync();
                            Console.WriteLine(responseContent);
                            break;
                        case "2":
                            Console.Clear();
                            response = await client.GetAsync("https://localhost:7001/product");
                            response.EnsureSuccessStatusCode();
                            responseContent = await response.Content.ReadAsStringAsync();
                            Console.WriteLine(responseContent);
                            break;
                        case "3":
                            Console.Clear();
                            response = await client.GetAsync("https://localhost:7001/product/id");
                            response.EnsureSuccessStatusCode();
                            responseContent = await response.Content.ReadAsStringAsync();
                            Console.WriteLine(responseContent);
                            break;
                        case "4":
                            Console.Clear();
                            response = await client.GetAsync("https://localhost:7001/product/update");
                            response.EnsureSuccessStatusCode();
                            responseContent = await response.Content.ReadAsStringAsync();
                            Console.WriteLine(responseContent);
                            break;
                        case "5":
                            Console.Clear();
                            response = await client.GetAsync("https://localhost:7001/product/delete/id");
                            response.EnsureSuccessStatusCode();
                            responseContent = await response.Content.ReadAsStringAsync();
                            Console.WriteLine(responseContent);
                            break;
                        case "6":
                            Console.Clear();
                            response = await client.GetAsync("https://localhost:7001/product/search");
                            response.EnsureSuccessStatusCode();
                            responseContent = await response.Content.ReadAsStringAsync();
                            Console.WriteLine(responseContent);
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("Choix invalide, essayez à nouveau");
                            break;

                    }
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("Paniers Sous-Menu");
                    Console.WriteLine("1. Create");
                    Console.WriteLine("2. GetAll");
                    Console.WriteLine("3. GetById");
                    Console.WriteLine("4. Update");
                    Console.WriteLine("5. Delete");
                    Console.WriteLine("7. Back");
                    Console.Write("Enter your choice: ");
                    subChoice = Console.ReadLine();
                    switch (subChoice)
                    {
                        case "1":
                            Console.Clear();
                            response = await client.GetAsync("https://localhost:7001/cart/insert");
                            response.EnsureSuccessStatusCode();
                            responseContent = await response.Content.ReadAsStringAsync();
                            Console.WriteLine(responseContent);
                            break;
                        case "2":
                            Console.Clear();
                            response = await client.GetAsync("https://localhost:7001/cart");
                            response.EnsureSuccessStatusCode();
                            responseContent = await response.Content.ReadAsStringAsync();
                            Console.WriteLine(responseContent);
                            break;
                        case "3":
                            Console.Clear();
                            response = await client.GetAsync("https://localhost:7001/cart/id");
                            response.EnsureSuccessStatusCode();
                            responseContent = await response.Content.ReadAsStringAsync();
                            Console.WriteLine(responseContent);
                            break;
                        case "4":
                            Console.Clear();
                            response = await client.GetAsync("https://localhost:7001/cart/update");
                            response.EnsureSuccessStatusCode();
                            responseContent = await response.Content.ReadAsStringAsync();
                            Console.WriteLine(responseContent);
                            break;
                        case "5":
                            Console.Clear();
                            response = await client.GetAsync("https://localhost:7001/cart/delete/id");
                            response.EnsureSuccessStatusCode();
                            responseContent = await response.Content.ReadAsStringAsync();
                            Console.WriteLine(responseContent);
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("Choix invalide, essayez à nouveau");
                            break;

                    }
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Paiements Sous-Menu");
                    Console.WriteLine("1. Create");
                    Console.WriteLine("2. GetAll");
                    Console.WriteLine("3. GetById");
                    Console.WriteLine("4. Update");
                    Console.WriteLine("5. Delete");
                    Console.WriteLine("7. Back");
                    Console.Write("Enter your choice: ");
                    subChoice = Console.ReadLine();
                    switch (subChoice)
                    {
                        case "1":
                            Console.Clear();
                            response = await client.GetAsync("https://localhost:7001/payment/insert");
                            response.EnsureSuccessStatusCode();
                            responseContent = await response.Content.ReadAsStringAsync();
                            Console.WriteLine(responseContent);
                            break;
                        case "2":
                            Console.Clear();
                            response = await client.GetAsync("https://localhost:7001/payment");
                            response.EnsureSuccessStatusCode();
                            responseContent = await response.Content.ReadAsStringAsync();
                            Console.WriteLine(responseContent);
                            break;
                        case "3":
                            Console.Clear();
                            response = await client.GetAsync("https://localhost:7001/payment/id");
                            response.EnsureSuccessStatusCode();
                            responseContent = await response.Content.ReadAsStringAsync();
                            Console.WriteLine(responseContent);
                            break;
                        case "4":
                            Console.Clear();
                            response = await client.GetAsync("https://localhost:7001/payment/update");
                            response.EnsureSuccessStatusCode();
                            responseContent = await response.Content.ReadAsStringAsync();
                            Console.WriteLine(responseContent);
                            break;
                        case "5":
                            Console.Clear();
                            response = await client.GetAsync("https://localhost:7001/payment/delete/id");
                            response.EnsureSuccessStatusCode();
                            responseContent = await response.Content.ReadAsStringAsync();
                            Console.WriteLine(responseContent);
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("Choix invalide, essayez à nouveau");
                            break;
                    }
                    break;
                case "5":
                    Console.Clear();
                    Console.WriteLine("Fermeture de la console");
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Choix invalide, essayez à nouveau");
                    break;
            }
        } while (mainChoice != "5");
    }
}