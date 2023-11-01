namespace MyFreelas.Domain.Interfaces;

public interface IInstallmentRepository
{
     Task<decimal> MonthlyBillingAsync(int year, int month);
     Task<List<Entities.Installment>> GetInstallments(int freelaId);
     Task RemoveInstallmentsAsync(List<Entities.Installment> installments);
}
