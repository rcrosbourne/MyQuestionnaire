﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyQuestionnaire.Web.Api.Models
{
    public abstract class ModelBase 
    {
        [Timestamp]
        [ReadOnly(true)]
        public Byte[] Timestamp { get; set; }
    }
}
