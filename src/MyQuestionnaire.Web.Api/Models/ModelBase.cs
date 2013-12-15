using System;
using System.ComponentModel.DataAnnotations;

namespace MyQuestionnaire.Web.Api.Models
{
    public abstract class ModelBase 
    {
        [Timestamp]
        public Byte[] Timestamp { get; set; }
    }
}
