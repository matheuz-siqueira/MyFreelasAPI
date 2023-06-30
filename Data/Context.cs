using Microsoft.EntityFrameworkCore;

namespace myfreelas.Data;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base (options) {}
}
