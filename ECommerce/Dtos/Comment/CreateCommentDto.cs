using System.ComponentModel.DataAnnotations;

namespace ECommerce.Dtos.Comment;

public class CreateCommentDto
{
    [Required]
    [MinLength(5, ErrorMessage = "Title must be 5 characters")]
    [MaxLength(300, ErrorMessage = "Title cannot be over 300 characters")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MinLength(5, ErrorMessage = "Content must be 5 characters")]
    [MaxLength(300, ErrorMessage = "Content cannot be over 300 characters")]
    public string Content { get; set; } = string.Empty;
}
