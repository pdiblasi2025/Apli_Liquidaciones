﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Dtos
{
    public class DtoBase<ID>
    {
        [Key]
        public ID Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeleteBy { get; set; }
        //PSD
        //public bool Fecha_Appro { get; set; }

        public bool Enabled { get; set; }
        public bool Deleted { get; set; }
    }
}
