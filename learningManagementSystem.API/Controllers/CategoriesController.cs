

using Microsoft.AspNetCore.Authorization;

namespace learningManagementSystem.API.Controllers;



[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
	private readonly ICategoryService _categoryService;

	public CategoriesController(ICategoryService categoryService)
	{
		_categoryService = categoryService;
	}

	[HttpGet("GetAllToShow/{pageNumber}")]
	[Authorize]
	public async Task<ActionResult> GetAllToShow(int pageNumber)
	{
		var categories = await _categoryService.GetAllAsync(pageNumber);
		if (categories is null)
		{
			return NotFound();
		}

		return Ok(categories);
	}

	[HttpPost("GetAll")]
	[Authorize]
	public async Task<ActionResult> GetAll([FromBody] CategoryQueryHandler query)
	{
		var categories = await _categoryService.GetAllByQueryAsync(query);
		if (categories is null)
		{
			return NotFound();
		}

		return Ok(categories);
	}

	[HttpGet("GetById/{id}")]
	[Authorize]
	public async Task<ActionResult> GetById(Guid id)
	{
		var category = await _categoryService.GetByIdWithIncludesAsync(id);
		if (category is null)
		{
			return NotFound();
		}

		return Ok(category);
	}


	[HttpPost]
	[Authorize(policy: "Admin")]
	public async Task<ActionResult> Create(CreateCategoryDto model)
	{
		var result = await _categoryService.CreateCategoryAsync(model);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}

		return Ok(result);
	}

	[HttpPut("{id}")]
	[Authorize(policy: "Admin")]
	public async Task<ActionResult> Update([FromRoute] Guid id, UpdateCategoryDto model)
	{
		var result = await _categoryService.UpdateCategoryAsync(id, model);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}

		return Ok(result);
	}


	[HttpDelete("{id}")]
	[Authorize(policy: "Admin")]
	public async Task<ActionResult> Delete(Guid id)
	{
		var result = await _categoryService.DeleteCategoryAsync(id);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}

		return Ok(result);
	}
	
	[HttpDelete("MarkAsDeleted/{id}")]
	[Authorize(policy: "Admin")]
	public async Task<ActionResult> MarkAsDeleted(Guid id)
	{
		var result = await _categoryService.MarkCategoryAsDeletedAsync(id);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}

		return Ok(result);
	}


}
