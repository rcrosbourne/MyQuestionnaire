﻿using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using log4net;
using MyQuestionnaire.Web.Api.DBContext;
using MyQuestionnaire.Web.Api.TypeMappers;
using MyQuestionnaire.Web.Api.ViewModels;
using MyQuestionnaire.Web.Common;
using Thinktecture.IdentityModel.Authorization.WebApi;

namespace MyQuestionnaire.Web.Api.Controllers
{
    [LoggingSession]
    [Authorize]
    //[ClaimsAuthorize]
    public class OpenEndedQuestionController : ApiController
    {
        private readonly IDbContext _db;
        private readonly ILog _log;
        private readonly IOpenEndedQuestionMap _openEndedQuestionMap;
        

        public OpenEndedQuestionController(IDbContext dbContext, ILog log, IOpenEndedQuestionMap openEndedQuestionMap)
        {
            _db = dbContext;
            _log = log;
            _openEndedQuestionMap = openEndedQuestionMap;
        }
        // GET api/OpenEndedQuestion
        [ClaimsAuthorize("GetAllOpenEndedQuestion", "OpenEndedQuestion")]
        [HttpGet]
        public IQueryable<OpenEndedQuestionViewModel> GetAllOpenEndedQuestion()
        {
            return _db.OpenEndedQuestions.AsEnumerable().Select(q => _openEndedQuestionMap.CreateViewModel(q)).AsQueryable();
        }

        // GET api/OpenEndedQuestion/5
        [ClaimsAuthorize("GetOneOpenEndedQuestion", "OpenEndedQuestion")]
        public IHttpActionResult GetOneOpenEndedQuestion(int id)
        {
            var openendedquestion = _db.OpenEndedQuestions.Find(id);
            if (openendedquestion == null)
            {
                return NotFound();
            }

            return Ok(_openEndedQuestionMap.CreateViewModel(openendedquestion));
        }

        // PUT api/OpenEndedQuestion/5
        [ClaimsAuthorize("PutOpenEndedQuestion", "OpenEndedQuestion")]
        public IHttpActionResult PutOpenEndedQuestion(int id, OpenEndedQuestionViewModel openendedquestionViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != openendedquestionViewModel.Id)
            {
                return BadRequest();
            }
            _db.Entry(_openEndedQuestionMap.CreateModel(openendedquestionViewModel)).State = EntityState.Modified;

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
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
        [ClaimsAuthorize("PostOpenEndedQuestion", "OpenEndedQuestion")]
        // POST api/OpenEndedQuestion
        [ResponseType(typeof(OpenEndedQuestionViewModel))]
        public IHttpActionResult PostOpenEndedQuestion(OpenEndedQuestionViewModel openendedQuestionViewModeluestion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var openendedquestionModel = _openEndedQuestionMap.CreateModel(openendedQuestionViewModeluestion);
            _db.OpenEndedQuestions.Add(openendedquestionModel);
            _db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = openendedquestionModel.Id }, _openEndedQuestionMap.CreateViewModel(openendedquestionModel));
        }
        [ClaimsAuthorize("DeleteOpenEndedQuestion", "OpenEndedQuestion")]
        // DELETE api/OpenEndedQuestion/5
        [ResponseType(typeof(OpenEndedQuestionViewModel))]
        public IHttpActionResult DeleteOpenEndedQuestion(int id)
        {
            var openendedquestion = _db.OpenEndedQuestions.Find(id);
            if (openendedquestion == null)
            {
                return NotFound();
            }

            _db.OpenEndedQuestions.Remove(openendedquestion);
            _db.SaveChanges();

            return Ok(_openEndedQuestionMap.CreateViewModel(openendedquestion));
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