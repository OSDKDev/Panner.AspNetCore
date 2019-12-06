# Panner.AspNetCore [![Nuget](https://img.shields.io/nuget/v/Panner.AspNetCore?label=NuGet&color=success)](https://www.nuget.org/packages/Panner.AspNetCore) [![Coverage Status](https://coveralls.io/repos/github/OSDKDev/Panner.AspNetCore/badge.svg?branch=master)](https://coveralls.io/github/OSDKDev/Panner.AspNetCore?branch=master)
Sorting and filtering made easy for your ASP.NET Core project! From CSV input to a filtered/sorted IQueryable with no effort using Panner, with some extra shortcuts.

For more options, see [Panner's documentation](https://github.com/OSDKDev/Panner).

## Usage
```csharp
// Your action method!
[HttpGet]
// Sample request: /entities?sorts=CreatedTimeStamp,Id&filters=Name=TestName
public async Task<IActionResult> GetAllPosts(
	[FromServices] DbContext myDbContext,

	// Add the following parameters to parse/validate csv inputs into particles.
	// Custom model binders take care of converting the csv string into a collection of particles.
	[FromQuery] IReadOnlyCollection<ISortParticle<MyEntity>> sorts,
	[FromQuery] IReadOnlyCollection<IFilterParticle<MyEntity>> filters
)
{
	var result = await myDbContext.MyEntity
			.Apply(filters)	// Apply validated and parsed filters
			.Apply(sorts)	// Apply validated and parsed sorts
			.ToArrayAsync();

	return Ok(result);
}
```

## Setup
```csharp
// Startup.cs
public void ConfigureServices(IServiceCollection services)
{

	services.AddMvc(); // Or maybe you're using .AddControllers()!

	services.UsePanner(c =>
	{
		c.Entity<MyEntity>()
			.Property(x => x.Id, o => o
				.IsSortableAs(nameof(Views.Post.Id))
				.IsFilterableAs(nameof(Views.Post.Id))
			)
			.Property(x => x.Title, o => o
				.IsSortableAs(nameof(Views.Post.Title))
			)
			.Property(x => x.CreatedOn, o => o
				.IsSortableAs(nameof(Views.Post.Creation))
				.IsFilterableAs(nameof(Views.Post.Creation))
			);
	});
}
```
