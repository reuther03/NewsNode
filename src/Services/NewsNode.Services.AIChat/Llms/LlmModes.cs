using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NewsNode.Services.AIChat.Llms;

/// <summary>
/// Zawiera metody umożliwiające uruchomienie różnych trybów językowych lub wizualnych, wykorzystując specyficzne, kluczowane usługi klienta czatu.
/// </summary>
public static class LlmModes
{
    /// <summary>
    /// Uruchamia tekstowy tryb czatu Llama3.
    /// <para>
    /// Metoda wykonuje następujące kroki:
    /// </para>
    /// <list type="number">
    ///   <item>
    ///     <description>Pobiera instancję interfejsu <c>IChatClient</c> oznaczoną kluczem "llama3" ze źródła usług hosta.</description>
    ///   </item>
    ///   <item>
    ///     <description>Wyświetla użytkownikowi komunikat "What do you want to ask about?" w konsoli.</description>
    ///   </item>
    ///   <item>
    ///     <description>Wchodzi w pętlę, która nieustannie odczytuje dane wejściowe od użytkownika z konsoli.</description>
    ///   </item>
    ///   <item>
    ///     <description>Jeśli wprowadzony tekst jest pusty, pętla się kończy.</description>
    ///   </item>
    ///   <item>
    ///     <description>Wysyła dane wejściowe użytkownika do klienta czatu przy użyciu metody <c>CompleteAsync</c>, aby wygenerować odpowiedź.</description>
    ///   </item>
    ///   <item>
    ///     <description>Wyświetla odpowiedź klienta czatu w konsoli.</description>
    ///   </item>
    /// </list>
    /// <para>
    /// Metoda wykorzystuje usługę kluczowaną "llama3".
    /// </para>
    /// </summary>
    /// <param name="app">Aplikacja hosta zawierająca niezbędne usługi.</param>
    /// <returns>Obiekt <see cref="Task"/> reprezentujący operację asynchroniczną.</returns>
    public static async Task RunLlama3Async(IHost app)
    {
        var chatClient = app.Services.GetRequiredKeyedService<IChatClient>("llama3");

        Console.WriteLine("What do you want to ask about?");
        while (true)
        {
            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                break;
            }

            var chatCompletion = await chatClient.CompleteAsync(input);
            Console.WriteLine(chatCompletion);
        }
    }
}