using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using log4net;
using Microsoft.Owin.Logging;
using MyQuestionnaire.Web.Api.DBContext;
using MyQuestionnaire.Web.Api.Models;

namespace MyQuestionnaire.Web.Api.Controllers
{
    public class OpenEndedQuestionController : ApiController
    {
        private readonly IDbContext _db;
        private readonly ILog _log;
       

        public OpenEndedQuestionController(IDbContext dbContext, ILog log)
        {
            _db = dbContext;
            _log = log;

        }


        // GET api/OpenEndedQuestion
        public IQueryable<OpenEndedQuestion> GetOpenEndedQuestions()
        {
            
            return _db.OpenEndedQuestions;
        }

        // GET api/OpenEndedQuestion/5
        [ResponseType(typeof(OpenEndedQuestion))]
        public IHttpActionResult GetOpenEndedQuestion(int id)
        {
            var openendedquestion = _db.OpenEndedQuestions.Find(id);
            if (openendedquestion == null)
            {
                return NotFound();
            }

            return Ok(openendedquestion);
        }

        // PUT api/OpenEndedQuestion/5
        public IHttpActionResult PutOpenEndedQuestion(int id, OpenEndedQuestion openendedquestion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != openendedquestion.Id)
            {
                return BadRequest();
            }

            _db.Entry(openendedquestion).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OpenEndedQuestionExists(id))
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

        // POST api/OpenEndedQuestion
        [ResponseType(typeof(OpenEndedQuestion))]
        public IHttpActionResult PostOpenEndedQuestion(OpenEndedQuestion openendedquestion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.OpenEndedQuestions.Add(openendedquestion);
            _db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = openendedquestion.Id }, openendedquestion);
        }

        // DELETE api/OpenEndedQuestion/5
        [ResponseType(typeof(OpenEndedQuestion))]
        public IHttpActionResult DeleteOpenEndedQuestion(int id)
        {
            var openendedquestion = _db.OpenEndedQuestions.Find(id);
            if (openendedquestion == null)
            {
                return NotFound();
            }

            _db.OpenEndedQuestions.Remove(openendedquestion);
            _db.SaveChanges();

            return Ok(openendedquestion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OpenEndedQuestionExists(int id)
        {
            return _db.OpenEndedQuestions.Count(e => e.Id == id) > 0;
        }
    }
}