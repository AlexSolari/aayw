using AAYW.Core.Annotations;
using AAYW.Core.Dependecies;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using AAYW.Core.Extensions;
using System.Threading.Tasks;
using System.Xml;
using AAYW.Core.Data.Managers;

namespace AAYW.Core.Models.View.Page
{
    public class PageDesignModel
    {
        private List<string> ContentBlocksData; 

        public string Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public List<string> ContentBlocks 
        { 
            get
            {
                return ContentBlocksData;
            }
            set 
            {
                value = value.Where(x => !x.IsNullOrWhiteSpace()).ToList();
                ContentBlocksData = value;
            } 
        }
        public IEnumerable<Bussines.Admin.ContentBlock> ContentBlocksVariants { get; set; }

        public PageDesignModel()
        {
            ContentBlocks = new List<string>();
            ContentBlocksVariants = Resolver.GetInstance<IManager<Bussines.Admin.ContentBlock>>().GetList(0, 100);
        }

        public PageDesignModel(Guid id)
            : this()
        {
            Id = id.ToString();
        }
    }
}
