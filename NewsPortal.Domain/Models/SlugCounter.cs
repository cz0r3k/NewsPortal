using System.ComponentModel.DataAnnotations;

namespace NewsPortal.Domain.Models;

public class SlugCounter
{
    [Required] [Key] public string Slug { get; set; } = string.Empty;

    public int Count { get; set; } = 1;

    public void Increment() => Count += 1;
}