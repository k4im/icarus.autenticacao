namespace autenticacao.domain.Dtos;

public class UserDTO
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public bool FlagDesativado { get; set; }
}