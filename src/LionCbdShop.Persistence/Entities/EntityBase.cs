using System.ComponentModel.DataAnnotations;

namespace LionCbdShop.Persistence.Entities;

public class EntityBase
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
}
