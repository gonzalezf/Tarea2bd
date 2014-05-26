using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace foro2.Models
{
    public class ModeloPM
    {
        public String nombre_remitente;
        public int id_mensaje;
        public int id_remitente;
        public int id_buzon;
        public bool leido;
        public String mensaje;
        public String fecha_envio;
        
        public ModeloPM(string name, int idm, int idr, int idb, bool l, string m, string f)
        {
            nombre_remitente = name;
            id_mensaje = idm;
            id_remitente = idr;
            id_buzon = idb;
            leido = l;
            mensaje = m;
            fecha_envio = f;
        }

    }
}