using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ProyectoFinalUltimate.Models;

namespace ProyectoFinalUltimate.Controllers
{
    public class MensajesController : ApiController
    {
        private ProyectoFinalUltimateContext db = new ProyectoFinalUltimateContext();

        // GET: api/Mensajes
        public IQueryable<Mensajes> GetMensajes()
        {
            return db.Mensajes;
        }

        // GET: api/Mensajes/5
        [ResponseType(typeof(Mensajes))]
        public async Task<IHttpActionResult> GetMensajes(int id)
        {
            Mensajes mensajes = await db.Mensajes.FindAsync(id);
            if (mensajes == null)
            {
                return NotFound();
            }

            return Ok(mensajes);
        }

        // PUT: api/Mensajes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMensajes(int id, Mensajes mensajes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mensajes.Id)
            {
                return BadRequest();
            }

            db.Entry(mensajes).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MensajesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Mensajes
        [ResponseType(typeof(Mensajes))]
        public async Task<IHttpActionResult> PostMensajes(Mensajes mensajes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Mensajes.Add(mensajes);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = mensajes.Id }, mensajes);
        }

        // DELETE: api/Mensajes/5
        [ResponseType(typeof(Mensajes))]
        public async Task<IHttpActionResult> DeleteMensajes(int id)
        {
            Mensajes mensajes = await db.Mensajes.FindAsync(id);
            if (mensajes == null)
            {
                return NotFound();
            }

            db.Mensajes.Remove(mensajes);
            await db.SaveChangesAsync();

            return Ok(mensajes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MensajesExists(int id)
        {
            return db.Mensajes.Count(e => e.Id == id) > 0;
        }
    }
}