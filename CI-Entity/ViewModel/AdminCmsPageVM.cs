using CI_Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Entity.ViewModel
{
    public class AdminCmsPageVM
    {
        public List<CmsPage> CmsPages { get; set; }

        public long CmsPageId { get; set; }
        public string Title { get; set; }

        public string Slug { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }
    }
}
namespace CI_PlatformWeb.Models
{
    public class AdminCmsPageVM
    {
    }
}
