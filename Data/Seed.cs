using marketplaceE.appDbContext;
using marketplaceE.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;


namespace marketplaceE.Data
{
    public class Seed
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<User> _hasher;
        public Seed(AppDbContext context, IPasswordHasher<User> hasher)
        {
            _context = context;
            _hasher=hasher;
        }

        public void SeedDb()
        {
            if (!_context.Users.Any())
            {
                var path1 = Path.Combine(Directory.GetCurrentDirectory(),"Photos", "женечка.png");
                var path2 = Path.Combine(Directory.GetCurrentDirectory(),"Photos", "4kHsrMPpVCU.jpg");
                var user1 = new User
                {
                    
                    UserName = "Арина",
                    Email = "example@mail.com",
                    Role = RolesOfUsers.User,
                    CreatedAt = DateTime.UtcNow,
                    DateOfBirth = new DateOnly(2000, 5, 10)
                };
                user1.PasswordHash = _hasher.HashPassword(user1, "Zhopa123456");

                var user2 = new User
                {
                    
                    UserName = "Барабулька",
                    Email = "chtoto@mail.com",
                    Role = RolesOfUsers.Master,
                    CreatedAt = DateTime.UtcNow,
                    DateOfBirth = new DateOnly(2001, 2, 2)
                };
                user2.PasswordHash = _hasher.HashPassword(user2, "PRropa123456");

                var user3 = new User
                {
                    
                    UserName = "господи",
                    Email = "christ@mail.com",
                    Role = RolesOfUsers.User,
                    CreatedAt = DateTime.UtcNow,
                    DateOfBirth = new DateOnly(1999, 5, 10),
                    UserPhoto = File.Exists(path2)
                        ? File.ReadAllBytes(path2)
                        : null
                };
                user3.PasswordHash = _hasher.HashPassword(user3, "Z123456789Qq");

                var user4 = new User
                {
                   
                    UserName = "Ирина",
                    Email = "irina@mail.com",
                    Role = RolesOfUsers.Master,
                    CreatedAt = DateTime.UtcNow,
                    DateOfBirth = new DateOnly(2003, 6, 11),
                    UserPhoto = File.Exists(path1)
                        ? File.ReadAllBytes(path1)
                        : null
                };
                user4.PasswordHash = _hasher.HashPassword(user4, "Zhopa123456");


                _context.Users.AddRange(user1,user2,user3,user4);
                _context.SaveChanges();
            }

            if (!_context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Name = "Вязка", Description = "Из шерсти и тп" },
                    new Category { Name = "Деревянные изделия", Description = "Игрушки, мебель, декор" },
                    new Category { Name = "Текстиль", Description = "Одежда, постельное, аксессуары" }
                };
                _context.Categories.AddRange(categories);
                _context.SaveChanges();
            }

            if (!_context.Products.Any())///тут и товары, и их картинки
            {
                var user2 = _context.Users.FirstOrDefault(u => u.Id == 2);
                var category1 = _context.Categories.FirstOrDefault(c => c.Id == 1);
                var category2 = _context.Categories.FirstOrDefault(c => c.Id == 2);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Photos", "firsst.jpg");
                var path2 = Path.Combine(Directory.GetCurrentDirectory(), "Photos", "sec.jpg");
                var path3 = Path.Combine(Directory.GetCurrentDirectory(), "Photos", "doll.jpg");
                var path4 = Path.Combine(Directory.GetCurrentDirectory(), "Photos", "doll2.jpg");
                var product1 = new Product
                {

                    Name = "Варежки",
                    Description = "пушистые очень",
                    IsService = false,
                    Price = 1000,
                    CreatedAt = DateTime.UtcNow,
                    MasterId = 2,
                    Master = user2,
                    Categories = new List<Category> { category1 },
                    Images = new List<ProductImages>
                    {
                        new ProductImages
                        {
                            
                            Url = File.ReadAllBytes(path)
                        },
                        new ProductImages
                        {
                            Url=File.ReadAllBytes(path2)
                        }

                    }
                };

                var product2 = new Product
                {

                    Name = "Кукла",
                    Description = "Создание куклы на заказ",
                    IsService = true,
                    Price = 15000,
                    CreatedAt = DateTime.UtcNow,
                    MasterId = 2,
                    Master = user2,
                    Categories = new List<Category> { category1, category2 },
                    Images = new List<ProductImages>
                    {
                        new ProductImages
                        {
                            Url = File.ReadAllBytes(path3)
                        },
                        new ProductImages
                        {
                            Url=File.ReadAllBytes(path4)
                        }

                    }
                };

                _context.Products.AddRange(product1, product2);
                _context.SaveChanges();
            }


            if (!_context.Requests.Any())
            {
                var user1 = _context.Users.FirstOrDefault(u => u.Id == 1);
                var category = _context.Categories.FirstOrDefault(c => c.Id == 1);
                var request = new Request
                {

                    Title = "Нужен уникальный продукт",
                    Description = "Ищу мастера, который может сделать уникальный товар на заказ",
                    CustomerId = 1,
                    Customer = user1,
                    CreatedAt = DateTime.UtcNow,
                    Categories = new List<Category> { category }
                };
                _context.Requests.Add(request);
                _context.SaveChanges();
            }


            if (!_context.Orders.Any())
            {
                var customer1 = _context.Users.FirstOrDefault(u => u.Id == 1);
                var master1 = _context.Users.FirstOrDefault(u => u.Id == 2);
                var product1 = _context.Products.FirstOrDefault(c => c.Id == 1);
                var rew = _context.Reviews.FirstOrDefault(f => f.Id == 1);

                var master2 = _context.Users.FirstOrDefault(u => u.Id == 4);
                var request = _context.Requests.FirstOrDefault(c => c.Id == 1);

                var orders = new List<Order> {
                    new Order
                    {
                        CustomerId = 1,
                        Customer = customer1,
                        MasterId = 2,
                        Master= master1,
                        ProductId = 1,
                        Product=product1,
                        RequestId=null,
                        Request = null,
                        Status = OrderStatus.Completed,
                        Review= rew,
                        CreatedAt = DateTime.UtcNow.AddDays(-1)
                    },

                    new Order
                    {
                        CustomerId = 1,
                        Customer = customer1,
                        MasterId = 4,
                        Master= master2,
                        ProductId = null,
                        Product=null,
                        RequestId=1,
                        Request = request,
                        Status = OrderStatus.InProgress,
                        Review= null,
                        CreatedAt = DateTime.UtcNow.AddDays(-1)
                    }
                };

                _context.Orders.AddRange(orders);
                _context.SaveChanges();
            }

            if (!_context.Reviews.Any())
            {
                var user1 = _context.Users.FirstOrDefault(u => u.Id==1);
                var user2 = _context.Users.FirstOrDefault(u => u.Id==2);
                var order1 = _context.Orders.FirstOrDefault(o => o.Id == 1);



                var reviev=new Review
                {
                    AuthorId =1,
                    Author = user1,
                    ReceiverId = 2,
                    Receiver = user2,
                    OrderId = 1,
                    Order = order1,
                    Rating = 5,
                    Text = "Отличный заказ, все понравилось!",
                    CreatedAt = DateTime.UtcNow
                };

                _context.Reviews.Add(reviev);
                _context.SaveChanges();
            }

            if(!_context.Responses.Any())
            {
                var user1 = _context.Users.FirstOrDefault(u => u.Id == 1);
                var user2 = _context.Users.FirstOrDefault(u => u.Id == 2);
                var user3 = _context.Users.FirstOrDefault(u => u.Id == 4);
                var product1 = _context.Products.FirstOrDefault(p => p.Id == 1);
                var request1 = _context.Requests.FirstOrDefault(r => r.Id == 1);

                var responses = new List<Response>
                {
                    new Response
                    {
                        SenderId =1,
                        Sender = user1,
                        Message = "Интересует ваш продукт, есть ли доставка?",
                        ProductId = 1,
                        Product = product1,
                        RequestId = null,
                        Request = null,
                        CreatedAt = DateTime.UtcNow
                    },
                    new Response
                    {
                        SenderId = 4,
                        Sender = user3,
                        Message = "Готова взяться",
                        ProductId = 1,
                        Product = product1,
                        RequestId =     1,
                        Request = request1,
                        CreatedAt = DateTime.UtcNow
                    }
                };

                _context.Responses.AddRange(responses);
                _context.SaveChanges();
            }

           

            if (!_context.Payments.Any())
            {
                var order1 = _context.Orders.FirstOrDefault(o=>o.Id == 1);
                var order2 = _context.Orders.FirstOrDefault(o => o.Id == 2);
                var payments = new List<Payment>
                {
                    new Payment
                    {
                        OrderId = 1,
                        Order = order1,
                        Amount = 55000,
                        PaymentMethod = "CreditCard",
                        PaidAt = DateTime.UtcNow.AddDays(-3) // три дня назад
                    },
                    new Payment
                    {
                        OrderId = 2,
                        Order = order2,
                        Amount = 1200,
                        PaymentMethod = "Cash",
                        PaidAt = DateTime.UtcNow.AddDays(-1) // вчера
                    }
                };

                _context.Payments.AddRange(payments);
                _context.SaveChanges();
            }


            if (!_context.Chats.Any())
            {
                var chat1 = new Chat
                {
                    ChatName = "Чат покупатель-1 и мастер-2",
                    User1Id = 1, // покупатель
                    User2Id = 2, // мастер
                    CreatedAt = DateTime.UtcNow,
                    Messages = new List<Message>
                    {
                        new Message
                        {
                            SenderId = 1,
                            Text = "Интересует ваш продукт, есть ли доставка?",
                            SendAt = DateTime.UtcNow.AddMinutes(-30),
                            IsRead = true
                        },
                        new Message
                        {
                            SenderId = 2,
                            Text = "Добрый день! Да, есть",
                            SendAt = DateTime.UtcNow.AddMinutes(-25),
                            IsRead = true
                        },
                        new Message
                        {
                            SenderId = 1,
                            Text = "Супер",
                            SendAt = DateTime.UtcNow.AddMinutes(-20),
                            IsRead = false
                        }
                    }
                };

                var chat2 = new Chat
                {
                    ChatName = "Чат покупатель-1 и мастер-2",
                    User1Id = 1, // покупатель
                    User2Id = 4, // мастер
                    CreatedAt = DateTime.UtcNow,
                    Messages = new List<Message>
                    {
                        new Message
                        {
                            SenderId = 4,
                            Text = "Готова взяться",
                            SendAt = DateTime.UtcNow.AddMinutes(-30),
                            IsRead = true
                        },
                        new Message
                        {
                            SenderId = 1,
                            Text = "Добрый день! Супер",
                            SendAt = DateTime.UtcNow.AddMinutes(-25),
                            IsRead = true
                        },
                        new Message
                        {
                            SenderId = 4,
                            Text = "Супер",
                            SendAt = DateTime.UtcNow.AddMinutes(-20),
                            IsRead = false
                        }
                    }
                };

                _context.Chats.AddRange(chat1, chat2);
                _context.SaveChanges();
            }


        }
    }
}
