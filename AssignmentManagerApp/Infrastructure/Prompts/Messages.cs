namespace Infrastructure.Prompts;

public static class Messages {
  private static string ConstructExceptionMessage(Exception ex) {
    if (ex.InnerException is not null) {
      return $"### Outer Exception\n" +
             $"{ ex.Message }\n\n" +
             $"### Stack Trace\n" +
             $"{ ex.StackTrace ?? "No stack trace." }\n\n\n" +
             $"### Inner Exception\n" +
             $"{ ex.InnerException.Message }\n\n" +
             $"### Stack Trace\n" +
             $"{ ex.InnerException.StackTrace }";
    }
    
    return
      $"### Message\n{ ex.Message }\n\n### Stack Trace\n{ ex.StackTrace ?? "No stack trace." }";
  }

  public static void PrintException(Exception ex) {
    Console.WriteLine(ConstructExceptionMessage(ex));
  }
}