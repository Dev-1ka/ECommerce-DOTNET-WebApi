using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Invites.Commands.AcceptInvite
{
    public class AcceptInviteCommandHandler : IRequestHandler<AcceptInviteCommand, string>
    {
        private readonly IApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AcceptInviteCommandHandler(
            IApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<string> Handle(
            AcceptInviteCommand request,
            CancellationToken cancellationToken)
        {
    
            var invite = await _context.Invites
                .FirstOrDefaultAsync(x => x.Token == request.Token, cancellationToken);

            if (invite == null)
                throw new Exception("Invalid invite token");

            if (invite.IsUsed)
                throw new Exception("Invite already used");

   
            if (invite.ExpiryDate < DateTime.UtcNow)
                throw new Exception("Invite expired");


            var existingUser = await _userManager.FindByEmailAsync(invite.Email);

            if (existingUser != null)
                throw new Exception("User already exists");


            var user = new ApplicationUser
            {
                UserName = invite.Email,
                Email = invite.Email,
                FullName = request.FullName,
                EmailConfirmed = true
            };

            var createResult = await _userManager.CreateAsync(user, request.Password);

            if (!createResult.Succeeded)
            {
                throw new Exception(
                    string.Join(", ", createResult.Errors.Select(e => e.Description)));
            }


            var roleResult = await _userManager.AddToRoleAsync(user, invite.Role);

            if (!roleResult.Succeeded)
            {
                throw new Exception(
                    string.Join(", ", roleResult.Errors.Select(e => e.Description)));
            }

            invite.IsUsed = true;

      
            await _context.SaveChangesAsync(cancellationToken);

            return "Account created successfully";
        }
    }
}