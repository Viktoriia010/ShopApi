using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDomain.Models;

public class PasswordResetToken
{
    public int Id { get; set; } //було змінено з інт на геайді але таблиця в бд при останній міграції не зявилась

    public Guid UserId { get; set; }

    public User User { get; set; } = null!;

    public string TokenHash { get; set; } = string.Empty;

    public DateTime ExpiresAt { get; set; }

    public bool IsUsed { get; set; }
}
