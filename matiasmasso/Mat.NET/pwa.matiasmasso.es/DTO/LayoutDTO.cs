using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class LayoutDTO
    {
        public UserModel? User { get; set; }
        public NavDTO Nav { get; set; } = new();

        public List<Box> News { get; set; } = new();
        public List<Box> BlogPosts { get; set; } = new();

        public string AboutTitle { get; set; } = string.Empty;
        public string AboutText { get; set; } = string.Empty;

        public string HelpTitle { get; set; } = string.Empty;
        public List<Box> HelpItems { get; set; } = new();

        public string ContactTitle { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string FollowUsTitle { get; set; } = string.Empty;
        public List<Box> SocialMediaProfiles { get; set; } = new();
        public string Copyright { get; set; } = String.Empty;
        public List<Box> BottomLinks { get; set; } = new();


    }
}
