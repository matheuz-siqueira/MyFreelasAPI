namespace myfreelas.Repositories.Installment;

public interface IInstallmentRepository
{
    Task<decimal> MonthlyBillingAsync(DateTime date);
}
