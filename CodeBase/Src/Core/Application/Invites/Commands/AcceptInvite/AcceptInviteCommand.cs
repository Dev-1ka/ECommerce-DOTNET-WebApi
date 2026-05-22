using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Invites.Commands.AcceptInvite
{
    public class AcceptInviteCommand : IRequest<string>
    {
        public string FullName { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
    }
}
