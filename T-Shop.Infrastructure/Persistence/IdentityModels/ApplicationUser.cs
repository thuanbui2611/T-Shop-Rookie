using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace T_Shop.Infrastructure.Persistence.IdentityModels
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [Column("full_name")]
        public string? FullName { get; set; }

        [Column("date_of_birth")]
        public DateTime? DateOfBirth { get; set; } = null;
        [Column("gender")]
        public string? Gender { get; set; } = null;

        [Column("address")]
        public string? Address { get; set; }

        [Column("avatar")]
        public string? Avatar { get; set; }

        [Column("is_locked")]
        public bool IsLocked { get; set; } = false;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
