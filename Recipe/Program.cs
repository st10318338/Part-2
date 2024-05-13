using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeApp
{
    // Notice for recipe calories
    public delegate void RecipeCaloriesExceededEventHandler(string recipeName, int totalCalories);

    class Program
    {
        static void Main(string[] args)
        {
            // List to store objects Recipe
            List<Recipe> recipes = new List<Recipe>();

            while (true)
            {
                // Insert a new recipe
                Console.WriteLine("1. Enter a new recipe");
                // Display all the recipes
                Console.WriteLine("2. Display all saved recipes");
                // How to close the program
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        // Method to enter recipe details
                        Recipe recipe = EnterRecipe();
                        // Add recipe to list
                        recipes.Add(recipe);
                        // Confirmation of saving Recipe
                        Console.WriteLine("Recipe saved successfully!");
                        break;
                    case "2":
                        // Method displaying all recipes that are saved
                        DisplayAllRecipes(recipes);
                        break;
                    case "3":
                        // Exiting message
                        Console.WriteLine("Exiting program...");
                        return; // Exit the program
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static Recipe EnterRecipe()
        {
            // Create a new Recipe
            Recipe recipe = new Recipe();

            Console.WriteLine("Enter name of the recipe:");
            string name = Console.ReadLine();
            // Set name of the recipe
            recipe.Name = name;

            // Ask number of ingredients
            Console.WriteLine("Enter number of ingredients in the recipe: ");
            // Read ingredients from the user
            int numIngredients = int.Parse(Console.ReadLine());

            for (int i = 0; i < numIngredients; i++)
            {
                // Buza igama lama Ingredient
                Console.WriteLine($"Enter name of ingredient {i + 1}: ");
                string ingredientName = Console.ReadLine();

                // Ask for number of ingredients
                Console.WriteLine($"Enter quantity of {ingredientName}: ");
                double quantity = double.Parse(Console.ReadLine());

                // Enter units for the measurements
                Console.WriteLine($"Enter units of measurement for {ingredientName}: ");
                string units = Console.ReadLine();

                // Type no. of calories
                Console.WriteLine($"Enter number of calories for {ingredientName}: ");
                int calories = int.Parse(Console.ReadLine());

                // Add ingredient to the recipes
                recipe.AddIngredient(ingredientName, quantity, units, calories);
            }

            // Ask for number of steps
            Console.WriteLine("Enter the number of steps: ");
            int numSteps = int.Parse(Console.ReadLine());

            for (int i = 0; i < numSteps; i++)
            {
                // Enter the steps
                Console.WriteLine($"Enter step {i + 1}: ");
                string step = Console.ReadLine();
                // Add the step to the recipe
                recipe.AddStep(step);
            }
            // Return the completed recipe
            return recipe;
        }

        static void DisplayAllRecipes(List<Recipe> recipes)
        {
            if (recipes.Count == 0)
            {
                // Show if no recipes are saved
                Console.WriteLine("No recipes.");
                return;
            }

            // Heading for list of recipes
            Console.WriteLine("\nList of all saved recipes:");

            foreach (var recipe in recipes.OrderBy(r => r.Name))
            {
                // Show names of saved recipes
                Console.WriteLine($"- {recipe.Name}");
            }
        }
    }

    class Recipe
    {
        // Property to get/set the name of the recipe
        public string Name { get; set; }
        // List to store object (Ingredient)
        private List<Ingredient> ingredients = new List<Ingredient>();
        // Store recipe steps
        private List<string> steps = new List<string>();

        // Notice when recipe calories exceed 300
        public event RecipeCaloriesExceededEventHandler RecipeCaloriesExceeded;

        public void AddIngredient(string name, double quantity, string units, int calories)
        {
            // Create a new Ingredient object
            Ingredient ingredient = new Ingredient(name, quantity, units, calories);
            // Add the ingredient to the list of ingredients
            ingredients.Add(ingredient);
        }

        public void AddStep(string step)
        {
            // Add a step to the recipe
            steps.Add(step);
        }

        public void DisplayRecipe()
        {
            // Show names of the recipe
            Console.WriteLine($"\nRecipe: {Name}");
            // Heading for ingredients list
            Console.WriteLine("\nIngredients:");

            // Store total calories
            int totalCalories = 0;

            foreach (var ingredient in ingredients)
            {
                // Show ingredients
                Console.WriteLine($"- {ingredient.Quantity} {ingredient.Units} of {ingredient.Name} ({ingredient.Calories} calories)");
                // Calculate calories
                totalCalories += ingredient.Calories;
            }

            // Show total calories
            Console.WriteLine("\nTotal Calories: " + totalCalories);

            // Check if calories exceed 300
            if (totalCalories > 300)
            {
                // Invoke event if calories exceed 300
                RecipeCaloriesExceeded?.Invoke(Name, totalCalories);
            }

            Console.WriteLine("\nSteps:");

            for (int i = 0; i < steps.Count; i++)
            {
                // Display steps
                Console.WriteLine($"{i + 1}. {steps[i]}");
            }
        }

        // Method to calculate the total number of calories in the recipe
        public int GetTotalCalories()
        {
            int totalCalories = 0;
            foreach (var ingredient in ingredients)
            {
                totalCalories += ingredient.Calories;
            }
            return totalCalories;
        }
    }

    class Ingredient
    {
        // get/set the name of the ingredient
        public string Name { get; set; }
        // get/set quantity
        public double Quantity { get; set; }
        // get/set the units of measurement
        public string Units { get; set; }
        // get/set the calories
        public int Calories { get; set; }

        public Ingredient(string name, double quantity, string units, int calories)
        {
            // Initialize the name of the ingredient
            Name = name;
            // Initialize quantity
            Quantity = quantity;
            // Initialize units of measurement
            Units = units;
            // Initialize calories
            Calories = calories;
        }
    }
}




