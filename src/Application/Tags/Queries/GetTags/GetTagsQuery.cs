using MediatR;
using Todo_App.Application.Tags.Dtos;

namespace Todo_App.Application.Tags.Queries.GetTags;
public record GetTagsQuery : IRequest<List<TagDto>>;
