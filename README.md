## 🌈 lolcat 🦄

[![all the things](https://github.com/si618/lolcat/actions/workflows/workflow.yml/badge.svg)](https://github.com/si618/lolcat/actions/workflows/workflow.yml)
[![NuGet](https://img.shields.io/nuget/v/lolcat.png)](https://www.nuget.org/packages/lolcat)
[![NuGet](https://img.shields.io/nuget/dt/lolcat.png)](https://www.nuget.org/stats/packages/lolcat?groupby=ClientName)
[![License](https://img.shields.io/badge/license-Apache_2.0-blue.svg)](LICENSE)

[lolcat](https://github.com/busyloop/lolcat) dotnet library ported from [powershell module](https://github.com/andot/lolcat) 🙇‍

## 🧐 What?

![The Rainbow](./assets/Nom.webp "The Rainbow")

## 📸 Screenshots

![Ouroboros](./assets/Ouroboros.webp "Ouroboros")

![AlienIsBeautiful](./assets/AlienIsBeautiful.gif "Alien is beautiful")

## 🚧 Installation

```bash
> dotnet add package lolcat
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

## ⚗ Benchmark

[Github charts](https://si618.github.io/lolcat/dev/bench)

```bash
> cd ./tests/Lolcat.Benchmarks
> dotnet run -c release
```

## 🎉 Demo

[Static](src/Lolcat.Demo/Program.cs) or [animated]([Code](src/Lolcat.Demo.Animated/Program.cs)
)

```bash
> cd ./src/Lolcat.Demo
> dotnet run
> cd ../Lolcat.Demo.Animated
> dotnet run
```
