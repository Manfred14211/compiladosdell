using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace redchapinapayout.Models.Peticiones
{
    public class GetRelationship
    {
        [Required]
        public string usuarioWebService { get; set; }
        [Required]
        public string claveWebService { get; set; }
        [Required]
        public string token { get; set; }
        [Required]
        public long? idCooperativa { get; set; }
    }
}