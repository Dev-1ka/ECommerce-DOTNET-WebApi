using Application.Interfaces;
using Application.Invites.Commands.SendInvite;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class SendInviteCommandHandler : IRequestHandler<SendInviteCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IEmailService _emailService;

    public SendInviteCommandHandler(
        IApplicationDbContext context,
        IEmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    public async Task<string> Handle(SendInviteCommand request, CancellationToken cancellationToken)
    {
        var allowedRoles = new[] { "ProductManager", "InventoryManager", "DataAnalyst" };

        if (!allowedRoles.Contains(request.Role))
            throw new Exception("Invalid role");

       
        var existingInvite = await _context.Invites
            .FirstOrDefaultAsync(x => x.Email == request.Email && !x.IsUsed, cancellationToken);

        if (existingInvite != null)
            throw new Exception("User already invited");

        var token = Guid.NewGuid().ToString();

        var invite = new Invite
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            Role = request.Role,
            Token = token,
            ExpiryDate = DateTime.UtcNow.AddHours(24),
            IsUsed = false
        };

        _context.Invites.Add(invite);
        await _context.SaveChangesAsync(cancellationToken);

        var link = $"http://localhost:4200/accept-invite?token={token}";

        var emailBody = $@"
            <html>
            <body style='font-family: Segoe UI, Arial, sans-serif; background-color:#f6f8fb; padding:20px;'>

                <div style='max-width:500px; margin:auto; background:#ffffff; padding:25px; border-radius:8px; border:1px solid #e5e7eb;'>
        
                    <h2 style='color:#111827; margin-bottom:10px;'>You're Invited</h2>

                    <p style='color:#374151; font-size:14px;'>
                        You have been invited to join as:
                    </p>

                    <p style='font-size:16px; font-weight:600; color:#2563eb; margin:10px 0;'>
                        {request.Role}
                    </p>

                    <p style='font-size:13px; color:#6b7280;'>
                        Your invite token:
                    </p>

                    <p style='font-size:12px; background:#f3f4f6; padding:8px; border-radius:5px; word-break:break-all;'>
                        {token}
                    </p>

                    <div style='text-align:center; margin:25px 0;'>
                        <a href='{link}' 
                           style='background:#2563eb; color:white; padding:10px 20px; text-decoration:none; border-radius:5px; font-size:14px;'>
                            Accept Invite
                        </a>
                    </div>

                    <p style='font-size:12px; color:#9ca3af;'>
                        If the button doesn't work, copy this link:
                    </p>

                    <p style='font-size:12px; word-break:break-all; color:#6b7280;'>
                        {link}
                    </p>

                </div>

            </body>
            </html>";


        await _emailService.SendEmailAsync(
             request.Email,
             "Invitation",
             emailBody
         );
        return "Invite sent successfully";
    }
}