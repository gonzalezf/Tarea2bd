using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace foro2.Models
{
    public class ModeloPM
    {
        int id_mensaje;
        int id_remitente;
        int id_buzon;
        bool leido;
        String mensaje;
        String fecha_envio;
        
        public ModeloPM(int idm, int idr, int idb, bool l, string m, string f)
        {
            id_mensaje = idm;
            id_remitente = idr;
            id_buzon = idb;
            leido = l;
            mensaje = m;
            fecha_envio = f;
        }

    }
}