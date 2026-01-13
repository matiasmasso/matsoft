using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Distribution channels
    /// </summary>
    public partial class DistributionChannel
    {
        public DistributionChannel()
        {
            ChannelDtos = new HashSet<ChannelDto>();
            ContactClasses = new HashSet<ContactClass>();
            ProductChannels = new HashSet<ProductChannel>();
            RepProducts = new HashSet<RepProduct>();
            Incentius = new HashSet<Incentiu>();
            Noticia = new HashSet<Noticium>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Channel name (Spanish)
        /// </summary>
        public string? NomEsp { get; set; }
        /// <summary>
        /// Channel name (Catalan, if different from Spanish)
        /// </summary>
        public string? NomCat { get; set; }
        /// <summary>
        /// Channel name (English, if different from Spanish)
        /// </summary>
        public string? NomEng { get; set; }
        /// <summary>
        /// Channel name (Portuguese, if different from Spanish)
        /// </summary>
        public string? NomPor { get; set; }
        /// <summary>
        /// Sort order
        /// </summary>
        public int Ord { get; set; }
        /// <summary>
        /// Priority when displayed to consumer
        /// </summary>
        public int ConsumerPriority { get; set; }

        public virtual ICollection<ChannelDto> ChannelDtos { get; set; }
        public virtual ICollection<ContactClass> ContactClasses { get; set; }
        public virtual ICollection<ProductChannel> ProductChannels { get; set; }
        public virtual ICollection<RepProduct> RepProducts { get; set; }

        public virtual ICollection<Incentiu> Incentius { get; set; }
        public virtual ICollection<Noticium> Noticia { get; set; }
    }
}
