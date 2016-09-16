using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using SimpleCQRS.Domain;
using SimpleCQRS.Domain.Services;
using SimpleCQRS.Messages;
using SimpleCQRS.Models.Data;
using SimpleCQRS.Models.Presentation;
using SimpleCQRS.ReadModel;

namespace SimpleCQRS.Controllers
{
    public class UsersController : ApiController
    {
        private readonly UsersDomainModel _domainLayer;
        private readonly UsersReadModelFacade _readModel;

        public UsersController()
        {
            _domainLayer = new UsersDomainModel(
                new UsersRepository(() => new SimpleCQRSContext()),
                new AspNetPasswordHasher(new PasswordHasher()));

            _readModel = new UsersReadModelFacade(
                () => new SimpleCQRSContext());
        }

        [ResponseType(typeof(UserPresentation))]
        public async Task<IHttpActionResult> Post(CreateUser command)
        {
            await _domainLayer.CreateUserAsync(command);
            UserPresentation user = await
                _readModel.FindByIdAsync(command.UserId);
            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        public Task<UserPresentation> Get(Guid id) =>
            _readModel.FindByIdAsync(id);
    }
}
