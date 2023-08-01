namespace myfreelas.Repositories.Installment;

public interface IInstallmentRepository
{
    Task<decimal> MonthlyBillingAsync(int year, int month);
}
