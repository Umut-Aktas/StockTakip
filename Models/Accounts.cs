namespace StokTakipProjesi2.Models
{
    public class Accounts
    {
        public static List<User> Users = new List<User>
        {
            new User { UserID = 1, Username = "admin", Password = "admin123", Role = "Admin", isRemembered = false},
            new User { UserID = 2, Username = "store1", Password = "store123", Role = "Store", isRemembered = false },
            new User { UserID = 3, Username = "store2", Password = "store456", Role = "Store", isRemembered = false },
            new User { UserID = 4, Username = "store3", Password = "store789", Role = "Store", isRemembered = false },
            new User { UserID = 5, Username = "customer1", Password = "customer123", Role = "Customer", isRemembered = false },
        };
    }
}
