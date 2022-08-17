using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SudokuSolverAPI.Models;

namespace SudokuSolverAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolutionController : ControllerBase
    {
        [HttpGet("{board}")]
        public async Task<string> Get(string board)
        {
            Solution solution = new Solution();
            var result = await solution.GetReturn(board);
            return result;
        }
    }
}
