namespace Arrear.Domain.AbstractCore
{
    public interface IUser
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Password { get; set; }
        string[] Roles { get; set; }
        string? Token { get; set; }
    }
}