using myfreelas.Data;

namespace myfreelas.Repositories.Installment;

public class InstallmentRepository : IInstallmentRepository
{

    private readonly Context _context;
    public InstallmentRepository(Context context)
    {
        _context = context;
    }
    public async Task RegisterInstallmentAsync(Models.Installment installment)
    {
        await _context.Installments.AddAsync(installment);
        await _context.SaveChangesAsync();
    }
}
