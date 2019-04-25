using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFStudiiCaz.StudiuCaz1;
using EFStudiiCaz.StudiuCaz2;
using EFStudiiCaz.StudiuCaz3;
using EFStudiiCaz.StudiuCaz4;

namespace EFStudiiCaz
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("============== Studiu caz 1 =================");

            using (var context = new ModelSelfRefrences())
            {
                var selfReference = new SelfReference
                {
                    Name = "test"
                };

                Console.WriteLine("Create entitate de selfReferenceAcestea sunt in momentul actual in db rezultatele:");

                foreach (var x in context.SelfReferences)
                {
                    Console.WriteLine($"{x.SelfReferenceId} - {x.Name}");
                }

                context.SelfReferences.Add(selfReference);
                context.SaveChanges();
            }

            Console.WriteLine("=============================================");
            Console.WriteLine("============== Studiu caz 2 =================");

            using (var context = new ProductContext())
            {
                var product = new Product
                {
                    SKU = 147,
                    Description = "Expandable Hydration Pack",
                    Price = 19.97M,
                    ImageURL = "/pack147.jpg"
                };
                context.Products.Add(product);
                product = new Product
                {
                    SKU = 178,
                    Description = "Rugged Ranger Duffel Bag",
                    Price = 39.97M,
                    ImageURL = "/pack178.jpg"
                };
                context.Products.Add(product);
                product = new Product
                {
                    SKU = 186,
                    Description = "Range Field Pack",
                    Price = 98.97M,
                    ImageURL = "/noimage.jp"
                };
                context.Products.Add(product);
                product = new Product
                {
                    SKU = 202,
                    Description = "Small Deployment Back Pack",
                    Price = 29.97M,
                    ImageURL = "/pack202.jpg"
                };
                context.Products.Add(product);
                context.SaveChanges();
            }
            using (var context = new ProductContext())
            {
                foreach (var p in context.Products)
                {
                    Console.WriteLine("{0} {1} {2} {3}", p.SKU, p.Description,
                        p.Price.ToString("C"), p.ImageURL);
                }
            }

            Console.WriteLine("=============================================");
            Console.WriteLine("============== Studiu caz 3 =================");

            byte[] thumbBits = new byte[100];
            byte[] fullBits = new byte[2000];
            using (var context = new PhotographContext())
            {
                var photo = new Photograph
                {
                    Title = "My Dog",
                    ThumbnailBits = thumbBits
                };
                var fullImage = new PhotographFullImage
                {
                    HighResolutionBits =
                        fullBits
                };
                photo.PhotographFullImage = fullImage;
                context.Photographs.Add(photo);
                context.SaveChanges();
            }
            using (var context = new PhotographContext())
            {
                foreach (var photo in context.Photographs)
                {
                    Console.WriteLine("Photo: {0}, ThumbnailSize {1} bytes",
                        photo.Title, photo.ThumbnailBits.Length);
                    // explicitly load the "expensive" entity,
                    context.Entry(photo).Reference(p =>
                        p.PhotographFullImage).Load();
                    Console.WriteLine("Full Image Size: {0} bytes",
                        photo.PhotographFullImage.HighResolutionBits.Length);
                }
            }

            Console.WriteLine("=============================================");
            Console.WriteLine("============== Studiu caz 4 =================");

            using (var context = new BusinessContext())
            {
                var business = new Business
                {
                    Name = "Corner Dry Cleaning",
                    LicenseNumber = "100x1"
                };
                context.Businesses.Add(business);
                var retail = new Retail
                {
                    Name = "Shop and Save",
                    LicenseNumber =
                        "200C",
                    Address = "101 Main",
                    City = "Anytown",
                    State = "TX",
                    ZIPCode = "76106"
                };
                context.Businesses.Add(retail);
                var web = new eCommerce
                {
                    Name = "BuyNow.com",
                    LicenseNumber =
                        "300AB",
                    URL = "www.buynow.com"
                };
                context.Businesses.Add(web);
                context.SaveChanges();
            }

            using (var context = new BusinessContext())
            {
                Console.WriteLine("\n--- All Businesses ---");
                foreach (var b in context.Businesses)
                {
                    Console.WriteLine("{0} (#{1})", b.Name, b.LicenseNumber);
                }

                Console.WriteLine("\n--- Retail Businesses ---");
                foreach (var r in context.Businesses.OfType<Retail>())
                {
                    Console.WriteLine("{0} (#{1})", r.Name, r.LicenseNumber);
                    Console.WriteLine("{0}", r.Address);
                    Console.WriteLine("{0}, {1} {2}", r.City, r.State, r.ZIPCode);
                }

                Console.WriteLine("\n--- eCommerce Businesses ---");
                foreach (var e in context.Businesses.OfType<eCommerce>())
                {
                    Console.WriteLine("{0} (#{1})", e.Name, e.LicenseNumber);
                    Console.WriteLine("Online address is: {0}", e.URL);
                }
            }
        }
    }
}
