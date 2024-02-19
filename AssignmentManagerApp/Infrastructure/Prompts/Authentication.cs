using Entities.Abstraction;

namespace Infrastructure.Prompts;

public class Authentication {
  private readonly IUserRepository _userRepo;
  
  public Authentication(IUserRepository userRepository) {
    _userRepo = userRepository;
  }
  
  private static (string, string) PromptForUserCredentials() {
    Console.Write("User: ");
    var username = Console.ReadLine() ?? string.Empty;
    Console.Write("Password: ");
    var password = Console.ReadLine() ?? string.Empty;
    
    return (username, password);
  }

  private async Task<bool> AuthenticateUserAsync((string, string) user) {
    var userFromDb = await _userRepo.GetByUsernameAsync(user.Item1);
    if (userFromDb is null) return false;
    var passwordMatch = userFromDb.Password == user.Item2;
    return passwordMatch;
  }

  public async Task<(string, string)?> HandleAsync() {
    var user = PromptForUserCredentials();
    var success = await AuthenticateUserAsync(user);
    return success ? user : default;
  }
}