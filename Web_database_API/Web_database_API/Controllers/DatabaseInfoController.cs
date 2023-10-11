using Microsoft.AspNetCore.Mvc;
using Web_database_API.models;

namespace Web_database_API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class DatabaseInfoController : Controller
    {
        private static List<DatabaseInfo> databaseInfo = new List<DatabaseInfo>(new[] {
            new DatabaseInfo() { Id = 1, SecondName = "Maiorov", FirstName = "Andrei", LastName = "Sergeevich", Email = "apapa@gmail.com", PhoneNumber = "8992235XXXX", Position = "Start" },
            new DatabaseInfo() { Id = 2, SecondName = "Saveli", FirstName = "Nikita", LastName = "Sergeevich", Email = "apapa@gmail.com", PhoneNumber = "8992666XXXX", Position = "Start" },
            new DatabaseInfo() { Id = 3, SecondName = "Matvienko", FirstName = "Nikita", LastName = "Antonevich", Email = "apapa@gmail.com", PhoneNumber = "8992665XXXX", Position = "End" },
            new DatabaseInfo() { Id = 4, SecondName = "Ostapenko", FirstName = "Danil", LastName = "Hartivich", Email = "apapa@gmail.com", PhoneNumber = "8995435XXXX", Position = "End" }
        });

        [HttpGet("SelectAll")]
        public IEnumerable<DatabaseInfo> Get() => databaseInfo;

        [HttpGet("Select_id={id}")]
        public IActionResult GetID(int id)
        {
            var selectDatabaseId = databaseInfo.SingleOrDefault(x => x.Id == id);

            if (selectDatabaseId == null)
            {
                return NotFound();
            }

            return Ok(selectDatabaseId);
        }

        [HttpDelete("Delete_id={id}")]
        public IActionResult DeliteRecord(int id)
        {
            if (null != databaseInfo.SingleOrDefault(x => x.Id == id))
            {
                databaseInfo.Remove(databaseInfo.SingleOrDefault(x => x.Id == id));
                return Ok(new { message = "Deleted successfully" });
            }
            else
            {
                return NotFound();
            }
            
        }

        private int NextDatabaseID => databaseInfo.Count() == 0 ? 1 : databaseInfo.Max(x => x.Id) + 1;

        [HttpPost]
        public IActionResult PostDatabase(DatabaseInfo databaseRecord)
        {
            if (!ModelState.IsValid) // Проверка полноты запроса
            {
                return BadRequest(ModelState);
            }
            databaseRecord.Id = NextDatabaseID;
            databaseInfo.Add(databaseRecord);
            return CreatedAtAction(nameof(Get), new { id = databaseRecord.Id }, databaseRecord);
        }

        [HttpPost("Insert_json")]
        public IActionResult PostDatabaseBody([FromBody] DatabaseInfo databaseRecord) => PostDatabase(databaseRecord);



        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
