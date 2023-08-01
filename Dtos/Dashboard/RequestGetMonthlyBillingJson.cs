using System.ComponentModel.DataAnnotations;

namespace myfreelas.Dtos.Dashboard;

public class RequestGetMonthlyBillingJson
{
    private int _dateYear;
    private int _dateMonth;

    public int DateYear
    {
        get => _dateYear;
        set
        {
            if ((value > 1900) && (value < 9999))
            {
                _dateYear = value;
            }

        }
    }

    public int DateMonth
    {
        get => _dateMonth;
        set
        {
            if ((value > 0) && (value <= 12))
            {
                _dateMonth = value;
            }
        }
    }
}
