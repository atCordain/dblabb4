using System;
using System.Linq;
using System.Media;

namespace PantShirtMatchConsole
{
    class Program
    {
        private static MatchContext context = new MatchContext();
        static void Main(string[] args)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            SeedDatabase();
            DisplayStartMenu();
        }

        private static void SeedDatabase()
        {
            context.Pants.Add(new Pant
            {
                Id = 1,
                Name = "Redpants",
                ImageFilePath = @"https://americanfootballuk-static.myshopblocks.com/images/2018/11/contain/256x256/afe1839c72776d5c1ea52f35110458f5.jpg"
            }); ;

            context.Pants.Add(new Pant
            {
                Id = 2,
                Name = "Normalpants",
                ImageFilePath = @"https://agoora-static.myshopblocks.com/images/2019/07/contain/256x256/64f2ce500c63aa10a348894902765dd9.jpg"
            });
            context.Shirts.Add(new Shirt
            {
                Id = 1,
                Name = "Wolfshirt",
                ImageFilePath = @"https://tamstreasures-static.myshopblocks.com/images/2018/08/contain/256x256/652d3912f93ce162b736d1426074c4bb.jpg"
            });
            context.Shirts.Add(new Shirt
            {
                Id = 2,
                Name = "Placeholder 2",
                ImageFilePath = @"https://cdn.shopify.com/s/files/1/0194/5914/5828/products/MadeHere_Prodcuts_Shirts_1_0f62eba5-f137-435c-8e9d-e7e73975212c_256x.png?v=1575550916"
            });
            context.RatingCategories.Add(new RatingCategory { Id = 1, Name = "Best outfit to steal the Declaration of Independence" });
            context.RatingCategories.Add(new RatingCategory { Id = 2, Name = "Best outfit for petting cats" });
            context.RatingCategories.Add(new RatingCategory { Id = 3, Name = "Best outfit for YOLO" });

            context.SaveChanges();
            context.Looks.Add(new Look { Id = 1, Name = "Wolfmoon", Pant = (Pant)context.Pants.Where(x => x.Id == 1).First(), Shirt = (Shirt)context.Shirts.Where(x => x.Id == 1).First() });
            context.Looks.Add(new Look { Id = 2, Name = "Redpant", Pant = (Pant)context.Pants.Where(x => x.Id == 2).First(), Shirt = (Shirt)context.Shirts.Where(x => x.Id == 2).First() });
            context.Looks.Add(new Look { Id = 3, Name = "Basicman", Pant = (Pant)context.Pants.Where(x => x.Id == 1).First(), Shirt = (Shirt)context.Shirts.Where(x => x.Id == 2).First() });
            context.SaveChanges();
            context.Ratings.Add(new Rating { Id = 1, Look = (Look)context.Looks.Where(x => x.Id == 1).First(), Category = context.RatingCategories.Where(x => x.Id == 1).First(), Points = 1 });
            context.Ratings.Add(new Rating { Id = 2, Look = (Look)context.Looks.Where(x => x.Id == 2).First(), Category = context.RatingCategories.Where(x => x.Id == 1).First(), Points = 2 });
            context.Ratings.Add(new Rating { Id = 3, Look = (Look)context.Looks.Where(x => x.Id == 3).First(), Category = context.RatingCategories.Where(x => x.Id == 1).First(), Points = 3 });
            context.Ratings.Add(new Rating { Id = 4, Look = (Look)context.Looks.Where(x => x.Id == 1).First(), Category = context.RatingCategories.Where(x => x.Id == 2).First(), Points = 3 });
            context.Ratings.Add(new Rating { Id = 5, Look = (Look)context.Looks.Where(x => x.Id == 2).First(), Category = context.RatingCategories.Where(x => x.Id == 2).First(), Points = 2 });
            context.Ratings.Add(new Rating { Id = 6, Look = (Look)context.Looks.Where(x => x.Id == 3).First(), Category = context.RatingCategories.Where(x => x.Id == 2).First(), Points = 1 });
            context.Ratings.Add(new Rating { Id = 7, Look = (Look)context.Looks.Where(x => x.Id == 1).First(), Category = context.RatingCategories.Where(x => x.Id == 3).First(), Points = 99 });
            context.SaveChanges();
        }

        private static void DisplayStartMenu()
        {
            var exitRequested = false;
            while (!exitRequested)
            {
                PrintStartMenuGraphics();
                try
                {
                    var selectedMenuItem = Console.ReadLine().Trim();
                    switch (selectedMenuItem)
                    {
                        case "1":
                            CreateALook();
                            break;
                        case "2":
                            VoteOnLook();
                            break;
                        case "3":
                            ShowTopLooks();
                            break;
                        case "4":
                            Administrate();
                            break;
                        case "5":
                            exitRequested = true;
                            break;
                        default:
                            break;
                    }
                }catch { }
            }

        }

        private static void Administrate()
        {
            Console.Clear();
            Console.WriteLine("Enter super secret admin password:"); ;
            if (Console.ReadLine().Trim() == "password")
            {
                var exitRequested = false;
                while (!exitRequested)
                {
                    PrintAdminMenuGraphics();
                    var selectedMenuItem = Console.ReadLine().Trim();
                    switch (selectedMenuItem)
                    {
                        case "1":
                            AddItem();
                            break;
                        case "2":
                            EditItem();
                            break;
                        case "3":
                            RemoveItem();
                            break;
                        case "5":
                            exitRequested = true;
                            break;
                        default:
                            break;
                    }
                }

            }
            else
            {
                Console.WriteLine("Ah, ah, ah, you didnt say the magic word!");
            }

            Console.ReadKey(true);


        }

        private static void RemoveItem()
        {
            Console.WriteLine("Remove a Shirt(1) or Pant(2)");
            var removeItemType = int.Parse(Console.ReadLine().Trim());
            if (removeItemType == 1)
            {
                var shirts = context.Shirts.ToList();
                foreach (var shirt in shirts)
                {
                    Console.WriteLine($"{shirt.Id}: {shirt.Name}, Link : {shirt.ImageFilePath}");
                }
                Console.WriteLine("Type the number of the shirt you wish to remove:");
                var selectedShirtId = int.Parse(Console.ReadLine().Trim());
                context.Shirts.Remove(context.Shirts.Where(x => x.Id == selectedShirtId).First());
            }
            else if (removeItemType == 2) 
            {
                var pants = context.Pants.ToList();
                foreach (var pant in pants)
                {
                    Console.WriteLine($"{pant.Id}: {pant.Name}, Link : {pant.ImageFilePath}");
                }
                Console.WriteLine("Type the number of the Pant you wish to remove:");
                var selectedPantId = int.Parse(Console.ReadLine().Trim());
                context.Pants.Remove(context.Pants.Where(x => x.Id == selectedPantId).First());

            }
            context.SaveChanges();
            Console.WriteLine("Item removed from database");
            Console.ReadKey(true);
        }

        private static void EditItem()
        {
            Console.WriteLine("Edit a Shirt(1) or Pant(2)");
            var editItemType = int.Parse(Console.ReadLine().Trim());
            if (editItemType == 1)
            {
                var shirts = context.Shirts.ToList();
                foreach (var shirt in shirts)
                {
                    Console.WriteLine($"{shirt.Id}: {shirt.Name}, Link : {shirt.ImageFilePath}");
                }
                Console.WriteLine("Type the number of the shirt you wish to edit:");
                var selectedShirtId = int.Parse(Console.ReadLine().Trim());

                Console.WriteLine("Do you wish to edit the Name (1) or Link (2)");
                var editItemArea = int.Parse(Console.ReadLine().Trim());
                if (editItemArea == 1)
                {
                    Console.WriteLine("Type the new Name");
                    context.Shirts.Where(x => x.Id == selectedShirtId).First().Name = Console.ReadLine().Trim();
                }
                else if (editItemArea == 2)
                {
                    Console.WriteLine("Type the new Link");
                    context.Shirts.Where(x => x.Id == selectedShirtId).First().ImageFilePath = Console.ReadLine().Trim();
                }
            }
            else if (editItemType == 2)
            {
                var pants = context.Pants.ToList();
                foreach (var pant in pants)
                {
                    Console.WriteLine($"{pant.Id}: {pant.Name}, Link : {pant.ImageFilePath}");
                }
                Console.WriteLine("Type the number of the Pant you wish to edit:");
                var selectedPantId = int.Parse(Console.ReadLine().Trim());
                Console.WriteLine("Do you wish to edit the Name (1) or Link (2)");
                var editItemArea = int.Parse(Console.ReadLine().Trim());
                if (editItemArea == 1)
                {
                    Console.WriteLine("Type the new Name");
                    context.Pants.Where(x => x.Id == selectedPantId).First().Name = Console.ReadLine().Trim();
                }
                else if (editItemArea == 2)
                {
                    Console.WriteLine("Type the new Link");
                    context.Pants.Where(x => x.Id == selectedPantId).First().ImageFilePath = Console.ReadLine().Trim();
                }
            }
            context.SaveChanges();
            Console.WriteLine("Item updated in database");
            Console.ReadKey(true);
        }

        private static void AddItem()
        {
            Console.WriteLine("Enter Item Name:");
            var itemName = Console.ReadLine().Trim();
            Console.WriteLine("Enter Item Image Link:");
            var itemImageLink = Console.ReadLine().Trim();

            Console.WriteLine("Add as Shirt(1) or Pant(2)");
            var addAsShirt = int.Parse(Console.ReadLine().Trim());
            if (addAsShirt == 1)
            {
                context.Shirts.Add(new Shirt { Id = context.Shirts.ToList().OrderByDescending(y => y.Id).First().Id + 1, Name = itemName, ImageFilePath = itemImageLink });
            } 
            else if(addAsShirt == 2)  
            {
                context.Pants.Add(new Pant { Id = context.Pants.ToList().OrderByDescending(y => y.Id).First().Id + 1, Name = itemName, ImageFilePath = itemImageLink });
            }
            context.SaveChanges();
            Console.WriteLine("Item added to database");
            Console.ReadKey(true);
        }

        private static void PrintAdminMenuGraphics()
        {
            Console.Clear();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("--           Administration          --");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Add an item");
            Console.WriteLine("2. Edit an item");
            Console.WriteLine("3. Remove an item");
            Console.WriteLine("5. Exit");
        }

        private static void ShowTopLooks()
        {
            Console.WriteLine("Top three looks per category;");
            var categories = context.RatingCategories.ToList();
            foreach (var category in categories)
            {
                Console.WriteLine($"Category: {category.Name}");
                var topRatings = context.Ratings.ToList().Where(x => x.Category == category).OrderByDescending(x => x.Points).Take(3).ToList();
                foreach (var rating in topRatings)
                {
                    Console.WriteLine($"{rating.Points} points, Look: {context.Looks.Where(x => x == rating.Look).First().Name}");
                }
            }

            Console.ReadKey(true);
        }

        private static void VoteOnLook()
        {
            // Pick some random stuff from the database, a category and two looks
            Random random = new Random();
            var categoryId = 1 + random.Next(context.RatingCategories.ToList().OrderByDescending(x => x.Id).First().Id );
            var category = context.RatingCategories.Where(x => x.Id == categoryId).ToList().First();
            var firstLookId = 1 + random.Next(context.Looks.ToList().OrderByDescending(x => x.Id).First().Id);
            var secondLookId = 1 + random.Next(context.Looks.ToList().OrderByDescending(x => x.Id).First().Id);
            var firstLook = context.Looks.Where(x => x.Id == firstLookId).First();
            var secondLook = context.Looks.Where(x => x.Id == secondLookId).First();

            // Spell it out for the dude on the other side of the screen
            Console.WriteLine($"Time to vote for : {category.Name}");
            Console.WriteLine("First Look(1):");
            Console.WriteLine($"Name: {firstLook.Name}");
            Console.WriteLine($"Shirt: {firstLook.Shirt.Name}, {firstLook.Shirt.ImageFilePath}");
            Console.WriteLine($"Pant: {firstLook.Pant.Name}, {firstLook.Pant.ImageFilePath}");
            Console.WriteLine("Second Look(2):");
            Console.WriteLine($"Name: {secondLook.Name}");
            Console.WriteLine($"Shirt: {secondLook.Shirt.Name}, {secondLook.Shirt.ImageFilePath}");
            Console.WriteLine($"Pant: {secondLook.Pant.Name}, {secondLook.Pant.ImageFilePath}");
            Console.WriteLine("INSTRUCTIONS: Press 1 for the first look to win and 2 for the second look to win");

            // Catch the sweet honey that is user input and trap it in the database
            var winningLook = int.Parse(Console.ReadLine().Trim());
            if (winningLook == 1)
            {
                var rating = (from r in context.Ratings.ToList()
                             where r.Category == category && r.Look == firstLook
                             select r).FirstOrDefault();
                if (rating != null)
                {
                    rating.Points++;
                }
                else
                {
                    context.Ratings.Add(new Rating { Id = context.Ratings.ToList().OrderByDescending(y => y.Id).First().Id + 1, Look = firstLook, Category = category, Points = 1 });
                }
            }
            else if (winningLook == 2)
            {
                var rating = (from r in context.Ratings.ToList()
                              where r.Category == category && r.Look == secondLook
                              select r).FirstOrDefault();
                if (rating != null)
                {
                    rating.Points++;
                }
                else
                {
                    context.Ratings.Add(new Rating { Id = context.Ratings.ToList().OrderByDescending(y => y.Id).First().Id + 1, Look = secondLook, Category = category, Points = 1 });
                }
            }
            context.SaveChanges();
            Console.WriteLine("New rating saved");
            Console.ReadKey(true);
        }

        private static void CreateALook()
        {
            Console.Clear();
            Console.WriteLine("Pick a Shirt for your look from these:");
            var shirts = context.Shirts.ToList();
            foreach (var shirt in shirts)
            {
                Console.WriteLine($"{shirt.Id}: {shirt.Name}, Link : {shirt.ImageFilePath}");
            }
            var selectedShirtId = int.Parse(Console.ReadLine().Trim());
            Console.Clear();
            Console.WriteLine("Pick a Shirt for your look from these:");
            var pants = context.Pants.ToList();
            foreach (var pant in pants)
            {
                Console.WriteLine($"{pant.Id}: {pant.Name}, Link : {pant.ImageFilePath}");
            }
            var selectedPantId = int.Parse(Console.ReadLine().Trim());

            Console.WriteLine("Pick a name for your look:");
            var lookName = Console.ReadLine().Trim();
            var looks = context.Pants.ToList();
            Console.WriteLine("Saving look to database");

            context.Looks.Add(new Look
            {
                Id = (int)context.Looks.ToList().OrderByDescending(y => y.Id).First().Id + 1,
                Name = lookName, 
                Pant = (Pant)context.Pants.Where(x => x.Id == selectedPantId).First(),
                Shirt = (Shirt)context.Shirts.Where(x => x.Id == selectedShirtId).First()
            });
            context.SaveChanges();
            Console.WriteLine("Look saved!");
            Console.ReadKey(true);
        }

        private static void PrintStartMenuGraphics()
        {
            Console.Clear();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("--               Welcome             --");
            Console.WriteLine("--                 to                --");
            Console.WriteLine("--           Pant Shirt Match        --");
            Console.WriteLine("--              (Console)            --");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Create a look");
            Console.WriteLine("2. Vote on look");
            Console.WriteLine("3. Show top looks");
            Console.WriteLine("4. Administrate");
            Console.WriteLine("5. Exit");
        }
    }
}
