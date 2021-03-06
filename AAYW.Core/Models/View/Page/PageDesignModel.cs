﻿using AAYW.Core.Annotations;
using AAYW.Core.Dependecies;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using AAYW.Core.Data.Managers;
using AAYW.Core.Api;

namespace AAYW.Core.Models.View.Page
{
    public class PageDesignModel
    {
        private List<string> ContentBlocksData; 

        public string Id { get; set; }
        [CustomRequired("Url")]
        [CustomMaxLength(100)]
        public string Url { get; set; }
        [CustomRequired("Title")]
        [CustomMaxLength(300)]
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
            ContentBlocksVariants = SiteApi.Data.ContentBlocks.GetList(0, 100);
        }

        public PageDesignModel(Guid id)
            : this()
        {
            Id = id.ToString();
        }
    }
}
