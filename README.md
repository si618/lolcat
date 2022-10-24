## lolcat ✨

C# library implementation of [lolcat](https://github.com/busyloop/lolcat)

## What? 🧐

![The Rainbow](./assets/Nom.webp "The Rainbow")

## Screenshot 📸

## Kudos 🍻

Ported with thanks from the [Powershell implementation](https://github.com/andot/lolcat)

## Installation 👷

```csharp
> dotnet add package lolcat

// Console app with top-level statements
using Lolcat;
using Spectre.Console;

// Ansi output
var text = "Somewhere over the rainbow...";
var style = new RainbowStyle(EscapeSequence.Ansi);
var rainbow = new Rainbow(style);
var ansi = rainbow.Convert(text);
Console.Write(ansi);

// Spectre.Console output
lolcat.Style.EscapeSequence = EscapeSequence.Spectre;
var spectre = rainbow.Convert(text);
AnsiConsole.Markup(spectre);
```

## Build 🏗️

```bash
> dotnet --list-sdks
6.0.402 [/usr/share/dotnet/sdk]

> git --version
git version 2.37.3

> git clone https://github.com/si618/lolcat.git
Cloning into 'lolcat'...

> cd lolcat
> dotnet build
```

## Test 🧪

```bash
> dotnet test --no-restore
```

## Benchmark ⚗️

```bash
> dotnet run --project ./tests/Lolcat.Benchmarks/Lolcat.Benchmarks.csproj -c release
```