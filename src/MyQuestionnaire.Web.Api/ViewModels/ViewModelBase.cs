using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyQuestionnaire.Web.Api.ViewModels
{
    public class ViewModelBase
    {
        [Timestamp]
        [ReadOnly(true)]
        public Byte[] Timestamp { get; set; }
    }
}