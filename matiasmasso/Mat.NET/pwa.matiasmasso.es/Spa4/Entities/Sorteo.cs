using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Raffles for consumers
    /// </summary>
    public partial class Sorteo
    {
        public Sorteo()
        {
            SorteoLeads = new HashSet<SorteoLead>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Raffle description
        /// </summary>
        public string Title { get; set; } = null!;
        /// <summary>
        /// Initial date the consumers can start to participate
        /// </summary>
        public DateTime FchFrom { get; set; }
        /// <summary>
        /// Termination date
        /// </summary>
        public DateTime FchTo { get; set; }
        /// <summary>
        /// Product prize of the raffle. Foreign key for Art table
        /// </summary>
        public Guid? Art { get; set; }
        /// <summary>
        /// Free text. The users have to answer a question to get a valid participation
        /// </summary>
        public string? Question { get; set; }
        /// <summary>
        /// Multiline string, one answer each line
        /// </summary>
        public string? Answers { get; set; }
        /// <summary>
        /// Index of the right answer within the string array from splitting the multiline Answers value into each line
        /// </summary>
        public int RightAnswer { get; set; }
        /// <summary>
        /// Name of the winner of the raffle
        /// </summary>
        public string? WinnerNom { get; set; }
        /// <summary>
        /// Foreign key for Email table
        /// </summary>
        public Guid? Winner { get; set; }
        /// <summary>
        /// Free text with the raffles rules
        /// </summary>
        public string? Bases { get; set; }
        /// <summary>
        /// If true, consumers have access to the raffle
        /// </summary>
        public bool? Visible { get; set; }
        /// <summary>
        /// Image 116 pixels width
        /// </summary>
        public byte[]? ImgFbFeatured116 { get; set; }
        /// <summary>
        /// Banner 600 pixels width
        /// </summary>
        public byte[]? ImgBanner600 { get; set; }
        /// <summary>
        /// Call to action image for social media 500 pixels width
        /// </summary>
        public byte[]? ImgCallToAction500 { get; set; }
        /// <summary>
        /// Picture sent by the distributor with the winner receiving the prize
        /// </summary>
        public byte[]? ImgWinner { get; set; }
        /// <summary>
        /// Friendly url to the raffle
        /// </summary>
        public string? UrlExterna { get; set; }
        /// <summary>
        /// Date the winner contacted us
        /// </summary>
        public DateTime? FchWinnerReaction { get; set; }
        /// <summary>
        /// Date the distributor answered our message
        /// </summary>
        public DateTime? FchDistributorReaction { get; set; }
        /// <summary>
        /// Date the prize was delivered to the distributor
        /// </summary>
        public DateTime? FchDelivery { get; set; }
        /// <summary>
        /// Date the distributor sent the picture of the winner
        /// </summary>
        public DateTime? FchPicture { get; set; }
        /// <summary>
        /// Foreign key for Alb table
        /// </summary>
        public Guid? Delivery { get; set; }
        /// <summary>
        /// Landed cost of the prize
        /// </summary>
        public decimal? CostPrize { get; set; }
        /// <summary>
        /// Cost of the publicity on social media
        /// </summary>
        public decimal? CostPubli { get; set; }
        /// <summary>
        /// ISO 639-2 Raffle language code (we issue different raffles for Spain and Portugal)
        /// </summary>
        public string Lang { get; set; } = null!;
        /// <summary>
        /// Foreign key for Country table
        /// </summary>
        public Guid? Country { get; set; }

        public virtual Art? ArtNavigation { get; set; }
        public virtual Country? CountryNavigation { get; set; }
        public virtual Alb? DeliveryNavigation { get; set; }
        public virtual SorteoLead? WinnerNavigation { get; set; }
        public virtual ICollection<SorteoLead> SorteoLeads { get; set; }
    }
}
