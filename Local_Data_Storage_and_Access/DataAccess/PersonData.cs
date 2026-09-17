using SQLite;
using Local_Data_Storage_and_Access.Models;

namespace Local_Data_Storage_and_Access.DataAccess
{
    public class PersonData
    {
        SQLiteAsyncConnection? database;

        async Task Init()
        {
            if (database is not null)
            {
                return;
            }

            database = new SQLiteAsyncConnection(
                DatabaseConstants.DatabasePath,
                DatabaseConstants.Flags);

            await database.CreateTableAsync<Person>();
        }

        public async Task<List<Person>> GetPeopleAsync()
        {
            await Init();

            return await database!.Table<Person>().ToListAsync();
        }

        public async Task<Person?> GetPersonAsync(int id)
        {
            await Init();

            return await database!
                .Table<Person>()
                .Where(i => i.ID == id)
                .FirstOrDefaultAsync();
        }

        public async Task<int> SavePersonAsync(Person person)
        {
            await Init();

            if (person.ID != 0)
            {
                // Update an existing person
                return await database!.UpdateAsync(person);
            }
            else
            {
                // Save a new person
                return await database!.InsertAsync(person);
            }
        }

        public async Task<int> DeletePersonAsync(Person person)
        {
            await Init();

            return await database!.DeleteAsync(person);
        }
    }
}