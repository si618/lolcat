# 🌈 lolcat 🦄

[![all the things](https://github.com/si618/lolcat/actions/workflows/workflow.yml/badge.svg)](https://github.com/si618/lolcat/actions/workflows/workflow.yml)
[![NuGet](https://img.shields.io/nuget/v/lolcat.png)](https://www.nuget.org/packages/lolcat)
[![NuGet](https://img.shields.io/nuget/dt/lolcat.png)](https://www.nuget.org/stats/packages/lolcat?groupby=ClientName)
[![License](https://img.shields.io/badge/license-Apache_2.0-blue.svg)](LICENSE)

[lolcat](https://github.com/busyloop/lolcat) dotnet library inspired by the [lolcat powershell module](https://github.com/andot/lolcat) 🙇‍

## 🧐 What?

![The Rainbow](https://raw.githubusercontent.com/si618/lolcat/main/assets/Nom.webp "The Rainbow")

## 📸 Screenshots

![Ouroboros](https://raw.githubusercontent.com/si618/lolcat/main/assets/Ouroboros.webp "Ouroboros")

![AlienIsBeautiful](https://raw.githubusercontent.com/si618/lolcat/main/assets/AlienIsBeautiful.gif "Alien is beautiful")

## 🚧 Installation

```bash
> dotnet add package lolcat
```

## 🏗 Build️

```bash
> dotnet --list-sdks
7.0.100 [/usr/share/dotnet/sdk]

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

## ⚗ Benchmark

[Benchmark charts](https://si618.github.io/lolcat/dev/bench)

```bash
> cd ./tests/Lolcat.Benchmarks
> dotnet run -c release
```

## 🎉 Demos

Code for [static](https://github.com/si618/lolcat/blob/main/src/Lolcat.Demo/Program.cs) and [animated](https://github.com/si618/lolcat/blob/main/src/Lolcat.Demo.Animated/Program.cs) demos

```bash
> cd ../src/Lolcat.Demo
> dotnet run
> cd ../Lolcat.Demo.Animated
> dotnet run
```
