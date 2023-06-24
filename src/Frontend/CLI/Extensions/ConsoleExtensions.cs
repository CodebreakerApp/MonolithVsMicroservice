using CodeBreaker.Services.Games.Transfer.Api;
using Spectre.Console;

namespace CodeBreaker.CLI.Extensions;

internal static class ConsoleExtensions
{
    public static IEnumerable<Field> PromptFields(this IAnsiConsole console, GameType gameType) =>
        PromptFields(console, gameType, false);

    public static IEnumerable<Field> PromptFields(this IAnsiConsole console, GameType gameType, bool outputSelections)
    {
        var selection = new SelectionPrompt<string>().WrapAround().AddChoices(gameType.PossibleFields.Select(field => field.Color!));
        var colors = new string[gameType.Holes];

        for (int i = 0; i < colors.Length; i++)
        {
            selection.Title($"Select peg {i + 1}/{gameType.Holes}");
            colors[i] = console.Prompt(selection);

            if (outputSelections)
            {
                console.WriteColor(colors[i]);
                console.WriteLine();
            }
        }

        return colors.Select(color => new Field() { Color = color });
    }

    public static void WriteColors(this IAnsiConsole console, IEnumerable<Field>? fields) =>
        WriteColors(console, fields?.Select(x => x.Color));

    public static void WriteColors(this IAnsiConsole console, IEnumerable<Services.Live.Transfer.Field>? fields) =>
        WriteColors(console, fields?.Select(x => x.Color));

    public static void WriteColors(this IAnsiConsole console, IEnumerable<string?>? colors) =>
        WriteColors(console, colors, false);

    public static void WriteColorsLine(this IAnsiConsole console, IEnumerable<Field>? fields) =>
        WriteColorsLine(console, fields?.Select(x => x.Color));

    public static void WriteColorsLine(this IAnsiConsole console, IEnumerable<string?>? colors) =>
        WriteColors(console, colors, true);

    private static void WriteColors(this IAnsiConsole console, IEnumerable<string?>? colors, bool withNewLine)
    {
        if (colors == null)
            return;

        foreach (var color in colors)
        {
            if (color == null)
                continue;

            console.WriteColor(color);
            console.Write(" ");
        }

        if (withNewLine)
            console.WriteLine();
    }

    public static void WriteGameEnd(this IAnsiConsole console, bool won)
    {
        var text = won
            ? "You won!"
            : "You lost!";
        var color = won
            ? Color.Green
            : Color.Red;
        console.WriteLine();
        console.Write(new FigletText(text).LeftJustified().Color(color));
    }

    public static void WriteColor(this IAnsiConsole console, string color)
    {
        var foreground = console.ParseColor(color);
        var background = foreground == Color.Black ? Color.Grey : Color.Black;
        console.Write(new Markup(color, new Style(foreground, background)));
    }

    public static Color ParseColor(this IAnsiConsole console, string colorName) => colorName.ToLowerInvariant() switch
    {
        "red" => Color.Red,
        "green" => Color.Green,
        "blue" => Color.Blue,
        "yellow" => Color.Yellow,
        "orange" => Color.Orange1,
        "white" => Color.White,
        "black" => Color.Black,
        "magenta" => Color.Magenta1,
        _ => Color.Default
    };

    public static void WaitForKey(this IAnsiConsole console, ConsoleKey key)
    {
        while(Console.ReadKey(true).Key != key)
        {}
    }
}
