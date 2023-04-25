using CI_Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Entity.ViewModel
{
    public class AdminCmsPageVM
    {
        public List<CmsPage> CmsPages { get; set; }

        public long CmsPageId { get; set; }
        [Required(ErrorMessage = "Title is a Required field.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Slug is a Required field.")]
        public string Slug { get; set; }
        [Required(ErrorMessage = "Discription is a Required field.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Select a status field.")]
        public string Status { get; set; }
    }
}

