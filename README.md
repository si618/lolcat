## ✨ lolcat

[![all the things](https://github.com/si618/lolcat/actions/workflows/workflow.yml/badge.svg)](https://github.com/si618/lolcat/actions/workflows/workflow.yml)
[![NuGet](https://img.shields.io/nuget/v/lolcat.png)](https://www.nuget.org/packages/lolcat/)
[![License](https://img.shields.io/badge/license-Apache_2.0-blue.svg)](LICENSE)

.NET implementation of [lolcat](https://github.com/busyloop/lolcat)

## 🧐 What?

![The Rainbow](./assets/Nom.webp "The Rainbow")

## 📸 Screenshot

![Ouroboros](./assets/Ouroboros.webp "Ouroboros")

## 🍻 Kudos

Ported with thanks from the [PowerShell module](https://github.com/andot/lolcat) implementation

## 🚧 Installation

```bash
> dotnet add package lolcat
```

## 🎉 Demo

[Code](src/Lolcat.Demo/Program.cs)

```bash
> cd ./src/Lolcat.Demo
> dotnet run
```

## 🏗 Build️

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

## 🧪 Test

```bash
> dotnet test
```

## ⚗ Benchmark️

```bash
> cd ./tests/Lolcat.Benchmarks
> dotnet run -c release
```