using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_database_API.database;
using Web_database_API.models;

namespace Web_database_API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ContactsController : Controller
    {
        //private static List<DatabaseInfo> databaseInfo = new List<DatabaseInfo>(new[] {
        //    new DatabaseInfo() { Id = 1, SecondName = "Maiorov", FirstName = "Andrei", LastName = "Sergeevich", Email = "apapa@gmail.com", PhoneNumber = "8992235XXXX", Position = "Start" },
        //    new DatabaseInfo() { Id = 2, SecondName = "Saveli", FirstName = "Nikita", LastName = "Sergeevich", Email = "apapa@gmail.com", PhoneNumber = "8992666XXXX", Position = "Start" },
        //    new DatabaseInfo() { Id = 3, SecondName = "Matvienko", FirstName = "Nikita", LastName = "Antonevich", Email = "apapa@gmail.com", PhoneNumber = "8992665XXXX", Position = "End" },
        //    new DatabaseInfo() { Id = 4, SecondName = "Ostapenko", FirstName = "Danil", LastName = "Hartivich", Email = "apapa@gmail.com", PhoneNumber = "8995435XXXX", Position = "End" }
        //});

        private readonly ContactsAPIDbContext dbContext;

        public ContactsController(ContactsAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("SelectAll")]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await dbContext.Contacts.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContactsId([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest)
        {

            var contact = new DatabaseInfo()
            {
                Id = Guid.NewGuid(),
                SecondName = addContactRequest.SecondName,
                FirstName = addContactRequest.FirstName,
                LastName = addContactRequest.LastName,
                Email = addContactRequest.Email,
                PhoneNumber = addContactRequest.PhoneNumber,
                Position = addContactRequest.Position
            };

            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();

            return Ok(contact);

        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateDatabase([FromRoute] Guid id, UpdateContactRequest updateContactRequest)
        {

            var contact = await dbContext.Contacts.FindAsync(id);

            if (contact != null)
            {

                contact.SecondName = updateContactRequest.SecondName;
                contact.FirstName = updateContactRequest.FirstName;
                contact.LastName = updateContactRequest.LastName;
                contact.Email = updateContactRequest.Email;
                contact.PhoneNumber = updateContactRequest.PhoneNumber;
                contact.Position = updateContactRequest.Position;

                await dbContext.SaveChangesAsync();

                return Ok(contact);

            }

            return NotFound();

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);

            if (contact != null)
            {
                dbContext.Remove(contact);
                await dbContext.SaveChangesAsync();
                return Ok(contact);
            }

            return NotFound();

        }

    }
}
