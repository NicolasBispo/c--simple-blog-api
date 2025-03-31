using Microsoft.EntityFrameworkCore;
using SimpleBlogApi.Core.Constants;
using SimpleBlogApi.Data;
using SimpleBlogApi.Dto.Common;
using SimpleBlogApi.Dto.Post;
using SimpleBlogApi.Models;
using System.Linq.Expressions;

namespace SimpleBlogApi.Services
{
  public class PostsService
  {
    private readonly AppDbContext _context;

    public PostsService(AppDbContext context)
    {
      _context = context;
    }

    public async Task<PaginatedResponse<Post>> ListPosts(PostSearchFiltersDto postSearchFiltersDto)
    {
      int page = postSearchFiltersDto.Page ?? 1;
      int perPage = postSearchFiltersDto.PerPage ?? AppConstants.DefaultPageSize;

      // Inicializa a consulta base
      IQueryable<Post> query = _context.Posts;

      // Cria uma lista de expressões de filtro
      List<Expression<Func<Post, bool>>> filters = [];

      if (!string.IsNullOrEmpty(postSearchFiltersDto.Title))
      {
        filters.Add(x => x.Title.Contains(postSearchFiltersDto.Title));
      }

      if (postSearchFiltersDto.CreatorId.HasValue)
      {
        filters.Add(x => x.AuthorId == postSearchFiltersDto.CreatorId);
      }

      // Aplica os filtros à consulta
      foreach (var filter in filters)
      {
        query = query.Where(filter);
      }

      // Obtém o total de registros que atendem aos filtros
      int totalCount = await query.CountAsync();

      // Executa a consulta com paginação
      var posts = await query
          .Skip((page - 1) * perPage)
          .Take(perPage)
          .ToListAsync();

      return new PaginatedResponse<Post>
      {
        Page = page,
        PerPage = perPage,
        Results = posts,
        TotalCount = totalCount
      };
    }
  }
}
