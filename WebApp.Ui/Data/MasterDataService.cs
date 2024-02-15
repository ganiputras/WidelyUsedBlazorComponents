using Microsoft.EntityFrameworkCore;
using WebApp.Ui.Data.Dto;
using WebApp.Ui.Helpers;

namespace WebApp.Ui.Data;

public class MasterDataService
{
    private readonly ApplicationDbContext _context;


    public MasterDataService(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<Dictionary<string, string>> DropdownCompany(string isRole = "")
    {
        //var data = await _context.TbCompanies.ToListAsync();


        var data = new List<CompanyDto>();
        //I use temporary data
        if (!data.Any())
        {
            data.Add(new CompanyDto
            {
                Id = "COMPANY1",
                Name = "COMPANY1",
                CompanyType = CompanyType.HeadOffice,
                ParentId = "",
                TenantId = "TENANT1",
                Phone = "12345"
            });
            data.Add(new CompanyDto
            {
                Id = "COMPANY2",
                Name = "COMPANY2",
                CompanyType = CompanyType.BranchOffice,
                ParentId = "COMPANY1",
                TenantId = "TENANT1",
                Phone = "12345"
            });
            data.Add(new CompanyDto
            {
                Id = "COMPANY3",
                Name = "COMPANY3",
                CompanyType = CompanyType.Office,
                ParentId = "TENANT1",
                TenantId = "TENANT1",
                Phone = "12345"
            });
            data.Add(new CompanyDto
            {
                Id = "COMPANY4",
                Name = "COMPANY4",
                CompanyType = CompanyType.Supplier,
                ParentId = "COMPANY1",
                TenantId = "TENANT1",
                Phone = "12345"
            });
            data.Add(new CompanyDto
            {
                Id = "COMPANY5",
                Name = "COMPANY5",
                CompanyType = CompanyType.Supplier,
                ParentId = "TENANT1",
                TenantId = "TENANT1",
                Phone = "12345"
            });
        }
        return data.ToDictionary(key => key.Id, value => value.Name);
    }

    public async Task<Dictionary<string, string>> DropdownLabel()
    {
        //var data = await _mediator.Send(new PlaceTypeGetAllQuery());
        //return data.ToDictionary(key => key.Name, value => value.Name);

        var data = new Dictionary<string, string>();
        data.Add("Apple", "Apple");
        data.Add("Avocado", "Avocado");
        data.Add("Watermelon", "Watermelon");
        data.Add("Brush Cherry", "Brush Cherry");
        data.Add("Orange", "Orange");
        data.Add("Blueberry", "Blueberry");
        data.Add("Grapes", "Grapes");
        data.Add("Mango", "Mango");
        data.Add("Coconut", "Coconut");
        data.Add("Papaya", "Papaya");
        data.Add("Java Plum", "Java Plum");
        return data;
    }
    public Dictionary<string, string> DropdownCompanyType()
    {
        var data = Enum.GetValues(typeof(CompanyType))
            .Cast<CompanyType>()
            .ToDictionary(x => x.ToString(), x => x.ToString().AddSpacesToSentence());

        return data;
    }


}