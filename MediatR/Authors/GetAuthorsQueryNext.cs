using MediatR;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRCQRS
{
    public record GetAuthorsQueryNext(int Page, int Count) : IRequest<List<AuthorDTO>>;
}
