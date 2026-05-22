using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Invites.Commands.SendInvite
{
    public class SendInviteCommand : IRequest<string>
    {
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
