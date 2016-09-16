using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using SimpleCQRS.Domain;
using SimpleCQRS.Domain.Services;
using SimpleCQRS.Messages;
using SimpleCQRS.Models.Data;
using SimpleCQRS.Models.Presentation;

namespace SimpleCQRS.Controllers
{
    public class UsersController : ApiController
    {
        private readonly UsersDomainModel _domainLayer;

        public UsersController()
        {
            _domainLayer = new UsersDomainModel(
                new UsersRepository(() => new SimpleCQRSContext()),
                new AspNetPasswordHasher(new PasswordHasher()));
        }

        [ResponseType(typeof(UserPresentation))]
        public async Task<IHttpActionResult> Post(CreateUser command)
        {
            UserPresentation user = await _domainLayer.CreateUserAsync(command);
            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        public Task<UserPresentation> Get(int id) =>
            _domainLayer.FindUserByIdAsync(id);
    }
}
