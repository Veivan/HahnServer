using Hahn.ApplicatonProcess.July2021.Data;
using Hahn.ApplicatonProcess.July2021.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.July2021.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        /*        public UsersController(IUnitOfWork unitOfWork)//using DI from Startup.cs
                {
                    this.unitOfWork = unitOfWork;
                }
        */

        // GET: Users     
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        public IEnumerable<User> GetUsers()
        {
            //return await _context.Users.ToListAsync();
            var users = unitOfWork.UserRepository.GetItemsList();
            return users;
        }

        // GET api/users/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<User>> GetUser(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
            var user = unitOfWork.UserRepository.GetItem((long)id);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        /// <summary>
        /// Creates a User.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /User
        ///     {
        ///        "name": "User1",
        ///        "age": 23,
        ///        "email": "user1@foo.com"
        ///     }
        ///
        /// </remarks>
        /// <param name="user"></param>
        /// <returns>A newly created user</returns>
        /// <response code="201">Returns the newly created user</response>
        /// <response code="400">If the item is null</response>          

        // POST: api/users
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> Create(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            //_context.Users.Add(user);
            //await _context.SaveChangesAsync();

            unitOfWork.UserRepository.Create(user);
            await Task.Run(() => unitOfWork.Save());

            //return CreatedAtRoute(@"~api/users/{user.Id}", user);
            return CreatedAtRoute("Get", new { id = user.Id }, user);
            // return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
            // return Ok(user);
        }

        // PUT: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[ValidateAntiForgeryToken]
        [HttpPut]
        public async Task<ActionResult<User>> Edit(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            if (unitOfWork.UserRepository.GetItem(user.Id) == null)
            {
                return NotFound();
            }
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.UserRepository.Update(user);
                    await Task.Run(() => unitOfWork.Save());
                    return Ok(user);
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return BadRequest();
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        //[Route("Home/Delete")]
        //[Route("Home/Delete/{id?}")]
        public async Task<ActionResult<User>> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
            var user = unitOfWork.UserRepository.GetItem((long)id);
            if (user == null)
            {
                return NotFound();
            }
            unitOfWork.UserRepository.Delete((long)id);
            await Task.Run(() => unitOfWork.Save());

            //_context.Users.Remove(user);
            //await _context.SaveChangesAsync();
            return Ok(user);
        }
        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }

    }
}
