using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions.Authentication;
using Application.Abstractions.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Features.Auth.Commands.Register
{
    public class RegisterCommandHandler
    : IRequestHandler<RegisterCommand, Guid>
    {
        private readonly IIdentityService _identityService;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterCommandHandler(
            IIdentityService identityService,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _identityService = identityService;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(
        RegisterCommand request,
        CancellationToken cancellationToken)
        {
            var userId = Guid.NewGuid();

            await _unitOfWork.BeginTransactionAsync(
                cancellationToken);

            try
            {

                var domainUserId = Guid.NewGuid();

                var user = User.Create(
                    domainUserId,
                    request.Name,
                    request.Email);

                await _userRepository.AddAsync(
                    user,
                    cancellationToken);
                // Identity User
                var identityUserId = Guid.NewGuid();

                await _identityService.CreateUserAsync(
                    identityUserId,
                    request.Email,
                    request.Password,
                    domainUserId,
                    cancellationToken);

                // Domain User




                await _unitOfWork.SaveChangesAsync(
                    cancellationToken);

                await _unitOfWork.CommitTransactionAsync(
                    cancellationToken);

                return userId;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(
                    cancellationToken);

                throw;
            }
        }
    }
}