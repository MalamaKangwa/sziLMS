using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.DataTransferObjectsForCreation;
using Entities.DataTransferObjectsForUpdate;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using sziLMS.ModelBinders;


namespace sziLMS.Controllers
{
    [Route("api/v1/courses/{courseId}/assignments")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public AssignmentsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAssignmentsForCompany(Guid courseId)
        {
            var course = _repository.Course.GetCourse(courseId, trackChanges: false);
            if (course == null)
            {
                _logger.LogInfo($"Course with id: {courseId} doesn't exist in the database.");
                return NotFound();
            }

            var assignmentsFromDb = _repository.Assignment.GetAssignments(courseId, trackChanges: false);
            var assignmentsDto = _mapper.Map<IEnumerable<AssignmentDto>>(assignmentsFromDb);

            return Ok(assignmentsDto);
        }


        [HttpGet("{id}", Name = "GetAssignmentForCourse")]
        public IActionResult GetAssignmentForCompany(Guid courseId, Guid id)
        {
            var course = _repository.Course.GetCourse(courseId, trackChanges: false);
            if (course == null)
            {
                _logger.LogInfo($"Course with id: {courseId} doesn't exist in the database.");
                return NotFound();
            }

            var assignmentsDb = _repository.Assignment.GetAssignment(courseId, id, trackChanges: false);
            if (assignmentsDb == null)
            {
                _logger.LogInfo($"Assignment with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var assignments = _mapper.Map<AssignmentDto>(assignmentsDb);

            return Ok(assignments);

        }


        [HttpGet("collection/({ids})", Name = "AssignmentCollection")]
        public IActionResult GetAssignmentCollection(Guid courseId, [ModelBinder(BinderType =
            typeof(ArrayModelBinders))]IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }

            var course = _repository.Course.GetCourse(courseId, trackChanges: false);
            if (course == null)
            {
                _logger.LogInfo($"Course with id: {courseId} doesn't exist in the database.");
                return NotFound();
            }

            var assignmentEntities = _repository.Assignment.GetAssignmentsByIds(courseId, ids, trackChanges: false);

            if (ids.Count() != assignmentEntities.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }

            var assignmentsToReturn = _mapper.Map<IEnumerable<AssignmentDto>>(assignmentEntities);
            return Ok(assignmentsToReturn);

        }




        [HttpPost]
        public IActionResult CreateAssignmentForCourse(Guid courseId, [FromBody]AssignmentForCreationDto assignment)
        {
            if (assignment == null)
            {
                _logger.LogError("AssignmentForCreationDto object sent from client is null.");
                return BadRequest("AssignmentForCreationDto object is null");
            }

            var course = _repository.Course.GetCourse(courseId, trackChanges: false);
            if (course == null)
            {
                _logger.LogInfo($"Course with id: {courseId} doesn't exist in the database.");
                return NotFound();
            }

            var assignmentEntity = _mapper.Map<Assignment>(assignment);

            _repository.Assignment.CreateAssignmentForCourse(courseId, assignmentEntity);
            _repository.Save();

            var assignmentToReturn = _mapper.Map<AssignmentDto>(assignmentEntity);

            return CreatedAtRoute("GetAssignmentForCourse", new { courseId, id = assignmentToReturn.Id }, assignmentToReturn);
        }


        [HttpPost("collection")]
        public IActionResult CreateAssignmentsCollection(Guid courseId, [FromBody] IEnumerable<AssignmentForCreationDto> assignmentsCollection)
        {
            if (assignmentsCollection == null)
            {
                _logger.LogError("Assignment collection sent from client is null.");
                return BadRequest("Assignment collection is null");
            }

            var course = _repository.Course.GetCourse(courseId, trackChanges: false);
            if (course == null)
            {
                _logger.LogInfo($"Course with id: {courseId} doesn't exist in the database.");
                return NotFound();
            }

            var assignmentEntities = _mapper.Map<IEnumerable<Assignment>>(assignmentsCollection);
            foreach (var assignment in assignmentEntities)
            {
                _repository.Assignment.CreateAssignmentForCourse(courseId, assignment);
            }

            _repository.Save();

            var assignmentCollectionDto = _mapper.Map<IEnumerable<AssignmentDto>>(assignmentEntities);
            return Ok(assignmentCollectionDto);

        }


        [HttpPut("{id}")]
        public IActionResult UpdateAssignment(Guid courseId, Guid id, [FromBody] AssignmentForUpdateDto assignment)
        {
            if (assignment == null)
            {
                _logger.LogError("AssignmentForUpdateDto object sent from client is null.");
                return BadRequest("ssignmentForUpdateDto object is null");
            }

            var course = _repository.Course.GetCourse(courseId, trackChanges: false);
            if (course == null)
            {
                _logger.LogInfo($"Course with id: {courseId} doesn't exist in the database.");
                return NotFound();
            }

            var assignmentEntity = _repository.Assignment.GetAssignment(courseId, id, trackChanges: true);
            if (assignmentEntity == null)
            {
                _logger.LogInfo($"Aassignment with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _mapper.Map(assignment, assignmentEntity);
            _repository.Save();

            return NoContent();
        }



        [HttpDelete("{id}")]
        public IActionResult DeleteAssignmentForCourse(Guid courseId, Guid companyId, Guid id)
        {
            var course = _repository.Course.GetCourse(courseId, trackChanges: false);
            if (course == null)
            {
                _logger.LogInfo($"Course with id: {courseId} doesn't exist in the database.");
                return NotFound();
            }

            var assignmentEntity = _repository.Assignment.GetAssignment(courseId, id, trackChanges: true);
            if (assignmentEntity == null)
            {
                _logger.LogInfo($"Aassignment with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _repository.Assignment.DeleteAssignment(assignmentEntity);
            _repository.Save();

            return NoContent();
        }

    }
}