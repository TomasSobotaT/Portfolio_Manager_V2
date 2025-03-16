using PortfolioManager.Base.Entities.Base;

namespace PortfolioManager.Base.Entities;
public class CommentEntity : ExtendedBaseEntity<int>
{
    public string Content { get; set; }

    public int? ParentCommentId { get; set; }

    public CommentEntity ParentComment { get; set; }

    public ICollection<CommentEntity> Replies { get; set; } = [];
}
