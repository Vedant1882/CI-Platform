using CI_Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Entity.ViewModel
{
    public class AdminBannerViewModel
    {
        public List<Banner> banner { get; set; }
        [Required(ErrorMessage = "Banner Text is a Required field.")]
        public string BannerText { get; set; }
        [Required(ErrorMessage = "Sort order is a Required field.")]
        public int? BannerSortOrder { get; set; }
        public string img { get; set;}
        public long BannerId { get; set; }
    }
}
