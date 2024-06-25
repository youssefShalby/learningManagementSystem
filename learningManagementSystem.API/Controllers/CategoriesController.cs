

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
	public async Task<ActionResult> GetAll([FromBody]CategoryQueryHandler query)
	{
		var categories = await _categoryService.GetAllByQueryAsync(query);
		if (categories is null)
		{
			return NotFound();
		}

		return Ok(categories);
	}

	[HttpGet("GetById/{id}")]
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
	public async Task<ActionResult> Create(CreateCategoryDto model)
	{
		var result = await _categoryService.CreateCategoryAsync(model);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}

		return Ok(result);
	}
	
	[HttpPut]
	public async Task<ActionResult> Update([FromHeader]Guid id, UpdateCategoryDto model)
	{
		var result = await _categoryService.UpdateCategoryAsync(id, model);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}

		return Ok(result);
	}
	
	
	[HttpDelete]
	public async Task<ActionResult> Delete([FromHeader]Guid id)
	{
		var result = await _categoryService.DeleteCategoryAsync(id);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}

		return Ok(result);
	}
	
	[HttpDelete("MarkAsDeleted")]
	public async Task<ActionResult> MarkAsDeleted([FromHeader]Guid id)
	{
		var result = await _categoryService.MarkCategoryAsDeletedAsync(id);
		if (!result.IsSuccessed)
		{
			return BadRequest(result);
		}

		return Ok(result);
	}


}
