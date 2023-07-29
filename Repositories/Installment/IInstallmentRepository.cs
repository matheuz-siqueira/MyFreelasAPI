namespace myfreelas.Repositories.Installment;

public interface IInstallmentRepository
{
    Task RegisterInstallmentAsync(Models.Installment installment);
}
