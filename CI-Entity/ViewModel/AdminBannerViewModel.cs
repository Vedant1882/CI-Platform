using CI_Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Entity.ViewModel
{
    public class AdminBannerViewModel
    {
        public List<Banner> banner { get; set; }
        public string BannerText { get; set; }
        public int? BannerSortOrder { get; set; }
        public string img { get; set;}
        public long BannerId { get; set; }
    }
}
