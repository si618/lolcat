## lolcat ✨

[![build and test](https://github.com/si618/lolcat/actions/workflows/workflow.yml/badge.svg)](https://github.com/si618/lolcat/actions/workflows/workflow.yml)

.NET implementation of [lolcat](https://github.com/busyloop/lolcat)

## What? 🧐

![The Rainbow](./assets/Nom.webp "The Rainbow")

## Screenshot 📸

## Kudos 🍻

Ported with thanks from the [PowerShell module](https://github.com/andot/lolcat) implementation

## Installation 🚧

```csharp
> dotnet add package lolcat
> dotnet add package Spectre.Console

// Console app with top-level statements
using Lolcat;
using Spectre.Console;

AnsiConsole.Clear();

// Ansi output is the default escape sequence
var text = "Someday we'll find it, the rainbow connection";
var style = new RainbowStyle();
var lolcat = new Rainbow(style);
var ansi = lolcat.Convert(text);
Console.WriteLine(ansi);

// Spectre.Console output
text = "The lovers, the dreamers and me";
lolcat.Style = style with
{
    EscapeSequence = EscapeSequence.Spectre,
    Frequency = 1,
    Spread = 5,
    Seed = 42
};
var spectre = lolcat.Convert(text);
AnsiConsole.MarkupLine(spectre);
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
> dotnet test
```

## Benchmark ⚗️

```bash
> cd ./tests/Lolcat.Benchmarks
> dotnet run -c release
```