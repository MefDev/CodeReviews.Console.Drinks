﻿using Services;
using Inputs;
using Spectre.Console;
using Menus;

class Program
{
    public async static Task Main()
    {
        try
        {
            while (true)
            {
                DrinkMenu.RenderLine("DodgerBlue1", "Restaurant drink Menu");
                using HttpClient client = new();
                ManageDrinks drinks = new(client);
                await ProcessDrinkMenu(drinks);
                var answer = AnsiConsole
                    .Prompt(new TextPrompt<string>("Quit? ")
                    .AddChoice("Yes")
                    .AddChoice("No"));
                if (answer.Equals("Yes")) Environment.Exit(0);
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.Markup(ex.Message);
        }
    }

    public async static Task ProcessDrinkMenu(ManageDrinks drinks)
    {
        var categories = await drinks.ProcessCategories();
        CategoryMenu.DisplayCategories(categories);
        var categoryName = UserInput.GetCategoryName();
        var drinkList = await drinks.ProcessDrinksByCategory(categoryName);
        DrinkMenu.DisplayDrinks(drinkList);
    }
}
