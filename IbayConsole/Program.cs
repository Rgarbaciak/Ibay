internal class Program
{
    private const string API_URL = "https://localhost:7001/";
    private static HttpClient client;

    static async Task<string> GetApiAsync()
    {
        string data = string.Empty;
        var response = await client.GetAsync(API_URL);
        if (response.IsSuccessStatusCode)
        {
            data = await response.Content.ReadAsStringAsync();
        }
        return data;
    }

    private static void Main(string[] args)
    {
        client = new HttpClient();
        Console.WriteLine("PROJET IBAY");

        while(true)
        {
            args = Console.ReadLine().Split(' ');
            var command = args[0];

            switch (command)
            {
                case "data":
                    var result = GetApiAsync().GetAwaiter().GetResult();
                    Console.WriteLine(result);
                    break;
                case "exit":
                    Environment.Exit(0);
                    break; 
                default:
                    break;
            }
        }
    }
}