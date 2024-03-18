// using Qi_practice_authentication.Data;
// using Qi_practice_authentication.Entities.User;
//
// namespace Qi_practice_authentication.Repository.Users;
//
// public class UserRepository : IUserRepository {
//     
//     private readonly OurHeroDbContext _dataContext;
//
//     public UserRepository(OurHeroDbContext context)
//     {
//         _dataContext = context;
//     }
//     
//     public List<User> GetAll()
//     {
//         var result = _dataContext.Users.ToList();
//         return result;
//     }
//
//     public User GetById(int id)
//     {
//         var result = _dataContext.Users.Find(id);
//         return result;
//     }
//
//     public User Create(User user)
//     {
//         var newUser = new User()
//         {
//             FirstName = user.FirstName,
//             LastName = user.LastName,
//             Username = user.Username
//         };
//         _dataContext.Users.Add(user);
//         _dataContext.SaveChanges();
//         return newUser;
//     }
//
//     public List<User> Update(int Id, User user)
//     {
//         var userToUpdate = _dataContext.Users.Find(Id);
//         if (userToUpdate == null)
//         {
//             throw new  Exception("No matching User found.");
//         }
//
//         var existingUser = _dataContext.Users
//             .Where(u => u.Id == user.Id)
//             .Select(x => new User()
//             {
//                 FirstName = user.FirstName,
//                 LastName = user.LastName,
//                 Username = user.Username
//             });
//         
//         _dataContext.SaveChanges();
//
//         return existingUser.ToList();
//     }
//
//     public void Delete(int Id)
//     {
//         var UsertoDelete = _dataContext.Users.Find(Id);
//
//         if (UsertoDelete != null)
//         {
//             _dataContext.Users.Remove(UsertoDelete);
//             _dataContext.SaveChanges();
//         }
//         else throw new Exception("No User found to Delete.");
//     }
// }