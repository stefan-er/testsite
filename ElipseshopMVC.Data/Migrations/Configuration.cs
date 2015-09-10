namespace ElipseshopMVC.Data.Migrations
{
    using ElipseshopMVC.Common;
    using ElipseshopMVC.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<ElipseshopDbContext>
    {
        private UserManager<User> userManager;
        private IRandomGenerator random;

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            //TODO: Remove in production
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ElipseshopDbContext context)
        {
            this.userManager = new UserManager<User>(new UserStore<User>(context));
            random = new RandomGenerator();

            this.SeedRoles(context);
            this.SeedUsers(context);

            this.SeedLanguages(context);
            this.SeedCategories(context);
            this.SeedCategoryPictures(context);
            this.SeedColors(context);
            this.SeedSizes(context);
            this.SeedProducts(context);
        }

        private void SeedRoles(ElipseshopDbContext context)
        {
            context.Roles.AddOrUpdate(x => x.Name, new IdentityRole(GlobalConstants.AdminRole));
            context.Roles.AddOrUpdate(x => x.Name, new IdentityRole(GlobalConstants.UserRole));

            context.SaveChanges();
        }
        private void SeedUsers(ElipseshopDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            for (int i = 0; i < 3; i++)
            {
                var user = new User
                {
                    Email = string.Format("{0}@{1}.com", this.random.RandomString(6, 16), this.random.RandomString(6, 16)),
                    UserName = "User" + (i+1).ToString(),
                };

                this.userManager.Create(user, "123456");
                this.userManager.AddToRole(user.Id, GlobalConstants.UserRole);
            }

            var adminUser = new User
            {
                Email = "admin@mysite.com",
                UserName = "Administrator"
            };

            this.userManager.Create(adminUser, "admin123456");
            this.userManager.AddToRole(adminUser.Id, GlobalConstants.AdminRole);
        }
        private void SeedLanguages(ElipseshopDbContext context)
        {
            Language enLang = new Language();
            enLang.Name = "English";
            enLang.Abbreviation = "EN";

            context.Languages.AddOrUpdate(x => x.Name, enLang);

            Language bgLang = new Language();
            bgLang.Name = "Bulgarian";
            bgLang.Abbreviation = "BG";

            context.Languages.AddOrUpdate(x => x.Name, bgLang);

            context.SaveChanges();
        }
        private void SeedCategories(ElipseshopDbContext context)
        {
            Language enLang = context.Languages.Where(x => x.Abbreviation == "EN").Single();
            Language bgLang = context.Languages.Where(x => x.Abbreviation == "BG").Single();

            Category menCat = new Category();
            AddCategoryLanguage(context, "Men", enLang, menCat);
            AddCategoryLanguage(context, "Мъже", bgLang, menCat);

            Category tshirtsCatM = new Category();
            tshirtsCatM.ParentCategory = menCat;
            AddCategoryLanguage(context, "T-Shirts", enLang, tshirtsCatM);
            AddCategoryLanguage(context, "Тениски", bgLang, tshirtsCatM);

            Category pulloversCatM = new Category();
            pulloversCatM.ParentCategory = menCat;
            AddCategoryLanguage(context, "Pullovers", enLang, pulloversCatM);
            AddCategoryLanguage(context, "Пуловери", bgLang, pulloversCatM);

            Category shirtsCatM = new Category();
            shirtsCatM.ParentCategory = menCat;
            AddCategoryLanguage(context, "Shirts", enLang, shirtsCatM);
            AddCategoryLanguage(context, "Ризи", bgLang, shirtsCatM);

            Category panthsCatM = new Category();
            panthsCatM.ParentCategory = menCat;
            AddCategoryLanguage(context, "Panths", enLang, panthsCatM);
            AddCategoryLanguage(context, "Панталони", bgLang, panthsCatM);

            Category shoesCatM = new Category();
            shoesCatM.ParentCategory = menCat;
            AddCategoryLanguage(context, "Shoes", enLang, shoesCatM);
            AddCategoryLanguage(context, "Обувки", bgLang, shoesCatM);

            Category swimwearCatM = new Category();
            swimwearCatM.ParentCategory = menCat;
            AddCategoryLanguage(context, "Swimwear", enLang, swimwearCatM);
            AddCategoryLanguage(context, "Бански", bgLang, swimwearCatM);

            Category underwearCatM = new Category();
            underwearCatM.ParentCategory = menCat;
            AddCategoryLanguage(context, "Underwear", enLang, underwearCatM);
            AddCategoryLanguage(context, "Бельо", bgLang, underwearCatM);

            Category accessoriesCatM = new Category();
            accessoriesCatM.ParentCategory = menCat;
            AddCategoryLanguage(context, "Accessories", enLang, accessoriesCatM);
            AddCategoryLanguage(context, "Аксесоари", bgLang, accessoriesCatM);

            Category watchesCatM = new Category();
            watchesCatM.ParentCategory = accessoriesCatM;
            AddCategoryLanguage(context, "Watches", enLang, watchesCatM);
            AddCategoryLanguage(context, "Часовници", bgLang, watchesCatM);

            Category swissWatchesCatM = new Category();
            swissWatchesCatM.ParentCategory = watchesCatM;
            AddCategoryLanguage(context, "Swiss Watches", enLang, swissWatchesCatM);
            AddCategoryLanguage(context, "Швейцарски часовници", bgLang, swissWatchesCatM);

            Category rolexWatchesCatM = new Category();
            rolexWatchesCatM.ParentCategory = swissWatchesCatM;
            AddCategoryLanguage(context, "Rolex", enLang, rolexWatchesCatM);
            AddCategoryLanguage(context, "Ролекс", bgLang, rolexWatchesCatM);

            Category wengerWatchesCatM = new Category();
            wengerWatchesCatM.ParentCategory = swissWatchesCatM;
            AddCategoryLanguage(context, "Wenger", enLang, wengerWatchesCatM);
            AddCategoryLanguage(context, "Венгер", bgLang, wengerWatchesCatM);

            Category beltsCatM = new Category();
            beltsCatM.ParentCategory = accessoriesCatM;
            AddCategoryLanguage(context, "Belts", enLang, beltsCatM);
            AddCategoryLanguage(context, "Колани", bgLang, beltsCatM);

            Category womenCat = new Category();
            AddCategoryLanguage(context, "Women", enLang, womenCat);
            AddCategoryLanguage(context, "Жени", bgLang, womenCat);

            Category dressesCatW = new Category();
            dressesCatW.ParentCategory = womenCat;
            AddCategoryLanguage(context, "Dresses", enLang, dressesCatW);
            AddCategoryLanguage(context, "Рокли", bgLang, dressesCatW);

            Category topsCatW = new Category();
            topsCatW.ParentCategory = womenCat;
            AddCategoryLanguage(context, "Tops", enLang, topsCatW);
            AddCategoryLanguage(context, "Топове", bgLang, topsCatW);

            Category panthsCatW = new Category();
            panthsCatW.ParentCategory = womenCat;
            AddCategoryLanguage(context, "Panths", enLang, panthsCatW);
            AddCategoryLanguage(context, "Панталони", bgLang, panthsCatW);

            Category skirtsCatW = new Category();
            skirtsCatW.ParentCategory = womenCat;
            AddCategoryLanguage(context, "Skirts", enLang, skirtsCatW);
            AddCategoryLanguage(context, "Поли", bgLang, skirtsCatW);

            Category shoesCatW = new Category();
            shoesCatW.ParentCategory = womenCat;
            AddCategoryLanguage(context, "Shoes", enLang, shoesCatW);
            AddCategoryLanguage(context, "Обувки", bgLang, shoesCatW);

            Category accessoriesCatW = new Category();
            accessoriesCatW.ParentCategory = womenCat;
            AddCategoryLanguage(context, "Accessories", enLang, accessoriesCatW);
            AddCategoryLanguage(context, "Аксесоари", bgLang, accessoriesCatW);

            Category bagsCatW = new Category();
            bagsCatW.ParentCategory = accessoriesCatW;
            AddCategoryLanguage(context, "Bags", enLang, bagsCatW);
            AddCategoryLanguage(context, "Чанти", bgLang, bagsCatW);

            Category italianBagsCatW = new Category();
            italianBagsCatW.ParentCategory = bagsCatW;
            AddCategoryLanguage(context, "Italian bags", enLang, italianBagsCatW);
            AddCategoryLanguage(context, "Италиански чанти", bgLang, italianBagsCatW);

            Category misssixtyBagsCatW = new Category();
            misssixtyBagsCatW.ParentCategory = italianBagsCatW;
            AddCategoryLanguage(context, "Miss Sixty", enLang, misssixtyBagsCatW);
            AddCategoryLanguage(context, "Мис Сиксти", bgLang, misssixtyBagsCatW);

            Category furlaBagsCatW = new Category();
            furlaBagsCatW.ParentCategory = italianBagsCatW;
            AddCategoryLanguage(context, "Furla", enLang, furlaBagsCatW);
            AddCategoryLanguage(context, "Фурла", bgLang, furlaBagsCatW);

            context.SaveChanges();
        }
        private void SeedCategoryPictures(ElipseshopDbContext context)
        {
            Category menCat = context.Categories
                .Where(x => x.CategoryLanguages
                    .Where(y => y.Language.Abbreviation == "EN")
                    .FirstOrDefault()
                    .Name == "Men")
                .Single();

            Category womenCat = context.Categories
                .Where(x => x.CategoryLanguages
                    .Where(y => y.Language.Abbreviation == "EN")
                    .FirstOrDefault()
                    .Name == "Women")
                .Single();

            CategoryPicture menMainPic = new CategoryPicture();
            menMainPic.Category = menCat;
            menMainPic.Url = "MenMain.jpg";
            menMainPic.IsMain = true;

            context.CategoryPictures.AddOrUpdate(x => x.Url, menMainPic);

            CategoryPicture menFirstPic = new CategoryPicture();
            menFirstPic.Category = menCat;
            menFirstPic.Url = "MenFirst.jpg";
            menFirstPic.IsMain = false;

            context.CategoryPictures.AddOrUpdate(x => x.Url, menFirstPic);

            CategoryPicture menSecondPic = new CategoryPicture();
            menSecondPic.Category = menCat;
            menSecondPic.Url = "MenSecond.jpg";
            menSecondPic.IsMain = false;

            context.CategoryPictures.AddOrUpdate(x => x.Url, menSecondPic);

            CategoryPicture menThirdPic = new CategoryPicture();
            menThirdPic.Category = menCat;
            menThirdPic.Url = "MenThird.jpg";
            menThirdPic.IsMain = false;

            context.CategoryPictures.AddOrUpdate(x => x.Url, menThirdPic);

            CategoryPicture menFourthPic = new CategoryPicture();
            menFourthPic.Category = menCat;
            menFourthPic.Url = "MenFourth.jpg";
            menFourthPic.IsMain = false;

            context.CategoryPictures.AddOrUpdate(x => x.Url, menFourthPic);

            CategoryPicture womenMainPic = new CategoryPicture();
            womenMainPic.Category = womenCat;
            womenMainPic.Url = "WomenMain.jpg";
            womenMainPic.IsMain = true;

            context.CategoryPictures.AddOrUpdate(x => x.Url, womenMainPic);

            CategoryPicture womenFirstPic = new CategoryPicture();
            womenFirstPic.Category = womenCat;
            womenFirstPic.Url = "WomenFirst.jpg";
            womenFirstPic.IsMain = false;

            context.CategoryPictures.AddOrUpdate(x => x.Url, womenFirstPic);

            CategoryPicture womenSecondPic = new CategoryPicture();
            womenSecondPic.Category = womenCat;
            womenSecondPic.Url = "WomenSecond.jpg";
            womenSecondPic.IsMain = false;

            context.CategoryPictures.AddOrUpdate(x => x.Url, womenSecondPic);

            CategoryPicture womenThirdPic = new CategoryPicture();
            womenThirdPic.Category = womenCat;
            womenThirdPic.Url = "WomenThird.jpg";
            womenThirdPic.IsMain = false;

            context.CategoryPictures.AddOrUpdate(x => x.Url, womenThirdPic);

            context.SaveChanges();
        }
        private void SeedColors(ElipseshopDbContext context)
        {
            Language enLang = context.Languages.Where(x => x.Abbreviation == "EN").Single();
            Language bgLang = context.Languages.Where(x => x.Abbreviation == "BG").Single();

            List<Tuple<string, string, string>> colors = GetColors();

            foreach (Tuple<string, string, string> color in colors)
            {
                Color dbColor = new Color();
                dbColor.HexCode = color.Item1;

                ColorLanguage dbColorLangugeEn = new ColorLanguage();
                dbColorLangugeEn.Color = dbColor;
                dbColorLangugeEn.Language = enLang;
                dbColorLangugeEn.Name = color.Item2;

                context.ColorLanguages.AddOrUpdate(x => x.Name, dbColorLangugeEn);

                ColorLanguage dbColorLangugeBg = new ColorLanguage();
                dbColorLangugeBg.Color = dbColor;
                dbColorLangugeBg.Language = bgLang;
                dbColorLangugeBg.Name = color.Item3;

                context.ColorLanguages.AddOrUpdate(x => x.Name, dbColorLangugeBg);
            }

            context.SaveChanges();
        }
        private void SeedSizes(ElipseshopDbContext context)
        {
            string[] sizes = new string[] { "XS", "S", "M", "L", "XL", "XXL" };

            foreach (string size in sizes)
            {
                Size dbSize = new Size();
                dbSize.Name = size;

                context.Sizes.AddOrUpdate(x => x.Name, dbSize);
            }

            context.SaveChanges();
        }
        private void SeedProducts(ElipseshopDbContext context)
        {
            Language enLang = context.Languages.Where(x => x.Abbreviation == "EN").Single();
            Language bgLang = context.Languages.Where(x => x.Abbreviation == "BG").Single();

            List<Tuple<Category, decimal, decimal?, Tuple<string, string>, Tuple<string, string>, List<Tuple<Color, Size, int>> ,List<Tuple<string, bool>>>> 
                products = GetProducts(context);

            foreach (var product in products)
            {
                Product dbProduct = new Product();
                dbProduct.Category = product.Item1;
                dbProduct.Price = product.Item2;
                dbProduct.NewPrice = product.Item3;

                ProductLanguage dbProductLangEn = new ProductLanguage();
                dbProductLangEn.Product = dbProduct;
                dbProductLangEn.Language = enLang;
                dbProductLangEn.Title = product.Item4.Item1;
                dbProductLangEn.Description = product.Item4.Item2;

                context.ProductLanguages.Add(dbProductLangEn);

                ProductLanguage dbProductLangBg = new ProductLanguage();
                dbProductLangBg.Product = dbProduct;
                dbProductLangBg.Language = bgLang;
                dbProductLangBg.Title = product.Item5.Item1;
                dbProductLangBg.Description = product.Item5.Item2;

                context.ProductLanguages.Add(dbProductLangBg);

                foreach (Tuple<Color, Size, int> pcsQuantity in product.Item6)
                {
                    PCSQuantity dbPCSQuantity = new PCSQuantity();
                    dbPCSQuantity.Product = dbProduct;
                    dbPCSQuantity.Color = pcsQuantity.Item1;
                    dbPCSQuantity.Size = pcsQuantity.Item2;
                    dbPCSQuantity.Quantity = pcsQuantity.Item3;

                    context.PCSQuantities.Add(dbPCSQuantity);
                }

                foreach (Tuple<string, bool> picture in product.Item7)
                {
                    ProductPicture dbProductPicture = new ProductPicture();
                    dbProductPicture.Product = dbProduct;
                    dbProductPicture.Url = picture.Item1;
                    dbProductPicture.IsMain = picture.Item2;

                    context.ProductPictures.Add(dbProductPicture);
                }
            }

            context.SaveChanges();
        }

        private void AddCategoryLanguage(ElipseshopDbContext context, string name, Language language, Category category)
        {
            CategoryLanguage categoryLang = new CategoryLanguage();
            categoryLang.Category = category;
            categoryLang.Language = language;
            categoryLang.Name = name;

            context.CategoryLanguages.Add(categoryLang);
        }
        private List<Tuple<string, string, string>> GetColors()
        {
            List<Tuple<string, string, string>> colors = new List<Tuple<string, string, string>>
                {
                    new Tuple<string, string, string>("#000000", "Black", "Черен"),
                    new Tuple<string, string, string>("#3B3131", "Oil", "Петрол"),
                    new Tuple<string, string, string>("#504A4B", "DarkGray", "DarkGray"),
                    new Tuple<string, string, string>("#808080", "Gray", "Gray"),
                    new Tuple<string, string, string>("#B6B6B4", "LightGray", "LightGray"),
                    new Tuple<string, string, string>("#C0C0C0", "Silver", "Silver"),
                    new Tuple<string, string, string>("#000080", "Navy", "Navy"),
                    new Tuple<string, string, string>("#0020C2", "Blue", "Blue"),
                    new Tuple<string, string, string>("#82CAFF", "SkyBlue", "SkyBlue"),
                    new Tuple<string, string, string>("#ADDFFF", "LightBlue", "LightBlue"),
                    new Tuple<string, string, string>("#E0FFFF", "LightCyan", "LightCyan"),
                    new Tuple<string, string, string>("#E0FFFF", "LightSlate", "LightSlate"),
                    new Tuple<string, string, string>("#00FFFF", "Aqua", "Aqua"),
                    new Tuple<string, string, string>("#43C6DB", "Turquoise", "Turquoise"),
                    new Tuple<string, string, string>("#728C00", "VenomGreen", "VenomGreen"),
                    new Tuple<string, string, string>("#008000", "Green", "Green"),
                    new Tuple<string, string, string>("#41A317", "LimeGreen", "LimeGreen"),
                    new Tuple<string, string, string>("#4CC417", "GreenApple", "GreenApple"),
                    new Tuple<string, string, string>("#00FF00", "ElectricGreen", "ElectricGreen"),
                    new Tuple<string, string, string>("#98FF98", "MintGreen", "MintGreen"),
                    new Tuple<string, string, string>("#CCFB5D", "GreenTea", "GreenTea"),
                    new Tuple<string, string, string>("#FFD700", "Gold", "Gold"),
                    new Tuple<string, string, string>("#FDD017", "BrightGold", "BrightGold"),
                    new Tuple<string, string, string>("#FFE87C", "SunYellow", "SunYellow"),
                    new Tuple<string, string, string>("#FFFF00", "Yellow", "Yellow"),
                    new Tuple<string, string, string>("#FFF380", "CornYellow", "CornYellow"),
                    new Tuple<string, string, string>("#FFFFCC", "Cream", "Cream"),
                    new Tuple<string, string, string>("#F5F5DC", "Beige", "Beige"),
                    new Tuple<string, string, string>("#F7E7CE", "Champagne", "Champagne"),
                    new Tuple<string, string, string>("#FFE5B4", "Peach", "Peach"),
                    new Tuple<string, string, string>("#FFCBA4", "DeepPeach", "DeepPeach"),
                    new Tuple<string, string, string>("#FF1493", "DeepPink", "DeepPink"),
                    new Tuple<string, string, string>("#F660AB", "HotPink", "HotPink"),
                    new Tuple<string, string, string>("#FAAFBE", "Pink", "Pink"),
                    new Tuple<string, string, string>("#FDD7E4", "PigPink", "PigPink"),
                    new Tuple<string, string, string>("#E8ADAA", "Rose", "Rose"),
                    new Tuple<string, string, string>("#F9B7FF", "BlossomPink", "BlossomPink"),
                    new Tuple<string, string, string>("#E77471", "LightCoral", "LightCoral"),
                    new Tuple<string, string, string>("#FDEEF4", "Pearl", "Pearl"),
                    new Tuple<string, string, string>("#F535AA", "NeonPink", "NeonPink"),
                    new Tuple<string, string, string>("#FF00FF", "Magenta", "Magenta"),
                    new Tuple<string, string, string>("#B93B8F", "Plum", "Plum"),
                    new Tuple<string, string, string>("#4B0082", "Indigo", "Indigo"),
                    new Tuple<string, string, string>("#B048B5", "MediumOrchid", "MediumOrchid"),
                    new Tuple<string, string, string>("#8D38C9", "Violet", "Violet"),
                    new Tuple<string, string, string>("#8E35EF", "Purple", "Purple"),
                    new Tuple<string, string, string>("#C45AEC", "TyrianPurple", "TyrianPurple"),
                    new Tuple<string, string, string>("#E238EC", "Crimson", "Crimson"),
                    new Tuple<string, string, string>("#F75D59", "BeanRed", "BeanRed"),
                    new Tuple<string, string, string>("#FF0000", "Red", "Red"),
                    new Tuple<string, string, string>("#F70D1A", "FerrariRed", "FerrariRed"),
                    new Tuple<string, string, string>("#E41B17", "DarkRed", "DarkRed"),
                    new Tuple<string, string, string>("#990012", "RedWine", "RedWine"),
                    new Tuple<string, string, string>("#8C001A", "Burgundy", "Burgundy"),
                    new Tuple<string, string, string>("#FF8040", "MangoOrange", "MangoOrange"),
                    new Tuple<string, string, string>("#FFA500", "Orange", "Orange"),
                    new Tuple<string, string, string>("#FF8C00", "DarkOrange", "DarkOrange"),
                    new Tuple<string, string, string>("#CD853F", "Peru", "Peru"),
                    new Tuple<string, string, string>("#8B4513", "SaddleBrown", "SaddleBrown"),
                    new Tuple<string, string, string>("#A52A2A", "Brown", "Brown"),
                    new Tuple<string, string, string>("#800000", "Maroon", "Maroon"),
                    new Tuple<string, string, string>("#FFEFDB", "AntiqueWhite", "AntiqueWhite"),
                    new Tuple<string, string, string>("#FFF8DC", "CornSilk", "CornSilk"),
                    new Tuple<string, string, string>("#FFFAFA", "Snow", "Snow"),
                    new Tuple<string, string, string>("#FFFFFF", "White", "White"),
                };

            return colors;
        }
        private List<Tuple<Category, decimal, decimal?, Tuple<string, string>, Tuple<string, string>, List<Tuple<Color, Size, int>>, List<Tuple<string, bool>>>> 
            GetProducts(ElipseshopDbContext context)
        {
            Category tshirtsCat = context.Categories
                .Where(x => x.CategoryLanguages
                    .Where(y => y.Language.Abbreviation == "EN")
                    .FirstOrDefault()
                    .Name == "T-Shirts")
                .Single();

            Category missSixtyBagsCat = context.Categories
                .Where(x => x.CategoryLanguages
                    .Where(y => y.Language.Abbreviation == "EN")
                    .FirstOrDefault()
                    .Name == "Miss Sixty")
                .Single();

            Color whiteColor = context.Colors.Where(x => x.HexCode == "#FFFFFF").Single();
            Color redColor = context.Colors.Where(x => x.HexCode == "#FF0000").Single();
            Color brownColor = context.Colors.Where(x => x.HexCode == "#A52A2A").Single();
            Color greenColor = context.Colors.Where(x => x.HexCode == "#008000").Single();
            Color lightSlateColor = context.Colors.Where(x => x.HexCode == "#FDD017").Single();
            Color aquaColor = context.Colors.Where(x => x.HexCode == "#FFFF00").Single();

            Size sSize = context.Sizes.Where(x => x.Name == "S").Single();
            Size mSize = context.Sizes.Where(x => x.Name == "M").Single();
            Size lSize = context.Sizes.Where(x => x.Name == "L").Single();
            Size xlSize = context.Sizes.Where(x => x.Name == "XL").Single();
            Size xxlSize = context.Sizes.Where(x => x.Name == "XXL").Single();

            var products = new List<Tuple<Category, decimal, decimal?, Tuple<string, string>, Tuple<string, string>, List<Tuple<Color, Size, int>>, List<Tuple<string, bool>>>>
                {
                    new Tuple<Category, decimal, decimal?, Tuple<string, string>, Tuple<string, string>, List<Tuple<Color, Size, int>>, List<Tuple<string, bool>>>
                        (
                            tshirtsCat, 
                            124, 
                            89, 
                            new Tuple<string, string>("Ralph Lauren t-shirt", "<ul><li>Ingredients: Cotton</li><li>Type: Sport</li></ul>"), 
                            new Tuple<string, string>("Ralph Lauren тениска", "<ul><li>Състав: Памук</li><li>Тип: Спортна</li></ul>"),
                            new List<Tuple<Color, Size, int>>
                                {
                                    new Tuple<Color, Size, int>(whiteColor, sSize, 3),
                                    new Tuple<Color,Size, int>(whiteColor, xlSize, 1),
                                },
                            new List<Tuple<string, bool>>
                                {
                                    new Tuple<string, bool>("Men/TShirts/RalphLaurenNo2/1.jpg", true),
                                    new Tuple<string, bool>("Men/TShirts/RalphLaurenNo2/2.jpg", false),
                                }
                        ),
                    new Tuple<Category, decimal, decimal?, Tuple<string, string>, Tuple<string, string>, List<Tuple<Color, Size, int>>, List<Tuple<string, bool>>>
                        (
                            tshirtsCat, 
                            84, 
                            null, 
                            new Tuple<string, string>("Energie t-shirt", "<ul><li>Ingredients: Cotton</li><li>Type: Sport</li></ul>"), 
                            new Tuple<string, string>("Energie тениска", "<ul><li>Състав: Памук</li><li>Тип: Спортна</li></ul>"),
                            new List<Tuple<Color, Size, int>>
                                {
                                    new Tuple<Color, Size, int>(redColor, mSize, 2),
                                    new Tuple<Color,Size, int>(redColor, lSize, 3),
                                    new Tuple<Color,Size, int>(redColor, xlSize, 1),
                                },
                            new List<Tuple<string, bool>>
                                {
                                    new Tuple<string, bool>("Men/TShirts/EnergieAstronauts/1.jpg", true),
                                    new Tuple<string, bool>("Men/TShirts/EnergieAstronauts/2.jpg", false),
                                    new Tuple<string, bool>("Men/TShirts/EnergieAstronauts/3.jpg", false),
                                }
                        ),
                    new Tuple<Category, decimal, decimal?, Tuple<string, string>, Tuple<string, string>, List<Tuple<Color, Size, int>>, List<Tuple<string, bool>>>
                        (
                            tshirtsCat, 
                            69, 
                            27, 
                            new Tuple<string, string>("Lee t-shirt", "<ul><li>Ingredients: Cotton</li><li>Type: Polo</li></ul>"), 
                            new Tuple<string, string>("Lee тениска", "<ul><li>Състав: Памук</li><li>Тип: Поло</li></ul>"), 
                            new List<Tuple<Color, Size, int>>
                                {
                                    new Tuple<Color, Size, int>(brownColor, xlSize, 1),
                                },
                            new List<Tuple<string, bool>>
                                {
                                    new Tuple<string, bool>("Men/TShirts/LeeBeigePolo/1.jpg", true),
                                    new Tuple<string, bool>("Men/TShirts/LeeBeigePolo/2.jpg", false),
                                }
                        ),
                    new Tuple<Category, decimal, decimal?, Tuple<string, string>, Tuple<string, string>, List<Tuple<Color, Size, int>>, List<Tuple<string, bool>>>
                        (
                            tshirtsCat, 
                            90, 
                            28, 
                            new Tuple<string, string>("Energie t-shirt", "<ul><li>Ingredients: Cotton</li><li>Type: Sport</li></ul>"), 
                            new Tuple<string, string>("Energie тениска", "<ul><li>Състав: Памук</li><li>Тип: Спортна</li></ul>"), 
                            new List<Tuple<Color, Size, int>>
                                {
                                    new Tuple<Color, Size, int>(greenColor, mSize, 4),
                                    new Tuple<Color, Size, int>(greenColor, xlSize, 3),
                                    new Tuple<Color, Size, int>(lightSlateColor, xxlSize, 1),
                                },
                            new List<Tuple<string, bool>>
                                {
                                    new Tuple<string, bool>("Men/TShirts/EnergieCrownGreen/1.jpg", true),
                                    new Tuple<string, bool>("Men/TShirts/EnergieCrownGreen/2.jpg", false),
                                }
                        ),
                    new Tuple<Category, decimal, decimal?, Tuple<string, string>, Tuple<string, string>, List<Tuple<Color, Size, int>>, List<Tuple<string, bool>>>
                        (
                            missSixtyBagsCat, 
                            121, 
                            76, 
                            new Tuple<string, string>("Miss Sixty purse", "<ul><li>Miss Sixty purse</li><li>Closing type: zip</li><li>Double handle</li><li>3 internal pockets</li><li>2 outer pockets</li><li>100% polyurethane</li><li>36х23х4 cm</li></ul>"), 
                            new Tuple<string, string>("Miss Sixty чанта", "<ul><li>Miss Sixty дамска чанта</li><li>Начин на закопчаване: цип</li><li>Двойна дръжка</li><li>3 вътрешни джоба</li><li>2 външни джоба</li><li>100% полиуретан</li><li>36х23х4 см</li></ul>"), 
                            new List<Tuple<Color, Size, int>>
                                {
                                    new Tuple<Color, Size, int>(null, null, 3),
                                },
                            new List<Tuple<string, bool>>
                                {
                                    new Tuple<string, bool>("Women/Bags/MissSixtyBrown/1.jpg", true),
                                }
                        ),
                    new Tuple<Category, decimal, decimal?, Tuple<string, string>, Tuple<string, string>, List<Tuple<Color, Size, int>>, List<Tuple<string, bool>>>
                        (
                            tshirtsCat, 
                            48, 
                            19, 
                            new Tuple<string, string>("Adidas t-shirt", "<ul><li>Men sports t-shirt with short sleeve</li><li> Type: Polo </li><li> Ingredients: 100% Cotton </li></ul>"), 
                            new Tuple<string, string>("Adidas тениска", "<ul><li>Мъжка спортна тениска с къс ръкав</li><li>Тип: Поло</li><li>Състав: 100% Памук</li></ul>"), 
                            new List<Tuple<Color, Size, int>>
                                {
                                    new Tuple<Color, Size, int>(aquaColor, mSize, 0),
                                },
                            new List<Tuple<string, bool>>
                                {
                                    new Tuple<string, bool>("Men/TShirts/AdidasPolo/1.jpg", true),
                                    new Tuple<string, bool>("Men/TShirts/AdidasPolo/2.jpg", false),
                                }),
                };

            return products;
        }
    }
}
