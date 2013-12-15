using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyQuestionnaire.Web.Api.ViewModels
{
    public class ViewModelBase
    {
        [Timestamp]
        [ReadOnly(true)]
        public Byte[] Timestamp { get; set; }
    }
}