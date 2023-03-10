# Project Ibay

## Introduction

Ce projet a pour but de fournir une API . Il utilise Entity Framework (EF) pour gérer les opérations de la base de données et ASP.NET Core pour la logique métier.


## Prérequis

Avant de commencer à travailler sur ce projet, vous devez avoir les outils suivants installés sur votre ordinateur :

• .NET Core 3.1 ou supérieur

• Visual Studio 2019 ou Visual Studio Code

## Installation

Téléchargez ou clonez ce repository sur votre ordinateur.

Ouvrez le projet dans Visual Studio ou Visual Studio Code.

Dans Visual Studio, appuyez sur Ctrl + Shift + B pour compiler le projet. Dans Visual Studio Code, ouvrez la console de commande et entrez la commande ```dotnet build``` pour compiler le projet.

Une fois le projet compilé avec succès, ouvrez la console de commande dans le répertoire du projet et entrez la commande suivante pour créer la base de données :

``` dotnet ef database update```

## Utilisation

Dans Visual Studio, appuyez sur F5 pour lancer l'application. Dans Visual Studio Code, ouvrez la console de commande et entrez la commande dotnet run pour lancer l'application.

Il faut deux visual studio ouvert en lançant l'api en premier  puis la console

Ouvrez votre navigateur web et accédez à l'URL http://localhost:7001/ pour accéder à l'application.

## Commandes Entity Framework

Les commandes Entity Framework suivantes sont utilisées pour gérer la base de données :

``` dotnet ef database update ```: Crée la base de données si elle n'existe pas et met à jour les tables en fonction des modifications apportées aux modèles.

``` dotnet ef migrations add [nom_migration] ``` :Ajoute une nouvelle migration pour les modifications apportées aux modèles.

``` dotnet ef migrations remove ``` :Supprime la dernière migration ajoutée.

``` dotnet ef database drop ``` :Supprime la base de données.

## Conclusion

Ce projet vous fournit une API qui utilise une base de données gérée avec Entity Framework. Vous pouvez utiliser les commandes Entity Framework pour gérer la base de données et les modèles, ainsi que les fonctionnalités d'ASP.NET Core pour implémenter la logique métier de l'application.

Il faut utiliser Swagger pour pouvoir insérer des données.

Pour la console, nous pouvons faire que des GETAll car c'est la seule route qui n'a pas besoin de paramètre, et le système de routing que nous avons fait avant la modification du sujet ne permet pas d'ajouter des données via la route 
