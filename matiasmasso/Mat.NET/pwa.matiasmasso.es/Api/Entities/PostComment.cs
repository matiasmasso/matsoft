using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Comments to website and blog posts
    /// </summary>
    public partial class PostComment
    {
        public PostComment()
        {
            InverseAnswerRootNavigation = new HashSet<PostComment>();
            InverseAnsweringNavigation = new HashSet<PostComment>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Post where this comment was posted
        /// </summary>
        public Guid Parent { get; set; }
        /// <summary>
        /// Enumerable DTOPostComment.ParentSources (news, blog...)
        /// </summary>
        public int ParentSource { get; set; }
        /// <summary>
        /// User who wrote the comment
        /// </summary>
        public Guid User { get; set; }
        /// <summary>
        /// ISO 639-2 language code
        /// </summary>
        public string Lang { get; set; } = null!;
        /// <summary>
        /// Comment date and time
        /// </summary>
        public DateTime Fch { get; set; }
        /// <summary>
        /// Comment text
        /// </summary>
        public string Text { get; set; } = null!;
        /// <summary>
        /// Previous comment in the thread to which this comment is answering; foreign key to self table PostComment
        /// </summary>
        public Guid? Answering { get; set; }
        /// <summary>
        /// The root comment who started the thred
        /// </summary>
        public Guid? AnswerRoot { get; set; }
        /// <summary>
        /// Date and time this comment was approved by the moderator
        /// </summary>
        public DateTime? FchApproved { get; set; }
        /// <summary>
        /// Date and time the moderator removed this commenty from the thread
        /// </summary>
        public DateTime? FchDeleted { get; set; }

        public virtual PostComment? AnswerRootNavigation { get; set; }
        public virtual PostComment? AnsweringNavigation { get; set; }
        public virtual Email UserNavigation { get; set; } = null!;
        public virtual ICollection<PostComment> InverseAnswerRootNavigation { get; set; }
        public virtual ICollection<PostComment> InverseAnsweringNavigation { get; set; }
    }
}
