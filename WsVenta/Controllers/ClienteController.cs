using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WsVenta.Models;
using WsVenta.Models.Response;
using WsVenta.Models.Request;



namespace WsVenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            Respuesta orRespuesta = new Respuesta();


            try
            {


                using (VentaContext db = new VentaContext())
                {
                    var lst = db.Cliente.OrderByDescending(d => d.Id).ToList();
                    orRespuesta.Exito = 1;
                    orRespuesta.Data = lst;
                }

            }
            catch (Exception ex)
            {
                orRespuesta.Mensaje = ex.Message;

            }

            return Ok(orRespuesta);

        }


        [HttpPost]
        public IActionResult Add(ClienteRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();

            try
            {
                using (VentaContext db = new VentaContext())
                {
                    Cliente oCliente = new Cliente();
                    oCliente.Nombre = oModel.Nombre;
                    db.Cliente.Add(oCliente);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;



                }

            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);

        }


        [HttpPut]
        public IActionResult Editar(ClienteRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();

            try
            {
                using (VentaContext db = new VentaContext())
                {
                    // Usa `Find` y verifica que el Id en oModel sea del tipo correcto.
                    Cliente oCliente = db.Cliente.Find((long)oModel.Id); // Cambia a `long` si la clave primaria es `long`

                    if (oCliente != null) // Verifica si el cliente existe
                    {
                        oCliente.Nombre = oModel.Nombre;
                        db.Entry(oCliente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        db.SaveChanges();
                        oRespuesta.Exito = 1;
                    }
                    else
                    {
                        oRespuesta.Mensaje = "Cliente no encontrado.";
                    }
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(long id)
        {
            Respuesta oRespuesta = new Respuesta();

            try
            {
                using (VentaContext db = new VentaContext())
                {
                    Cliente oCliente = db.Cliente.Find(id);
                    db.Cliente.Remove(oCliente);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;



                }

            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);


        }

    }
}
