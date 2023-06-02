using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace redchapinapayout.Models.RespuestasMetodos.Relationship
{
    public class ResponseRelationShip
    {
        public long? idRelationShip { get; set; }
        public string descripcion { get; set; }
        public long? idTipoRemesa { get; set; }
    }
}