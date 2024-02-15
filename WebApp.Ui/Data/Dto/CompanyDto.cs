namespace WebApp.Ui.Data.Dto;

public class CompanyDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string ParentId { get; set; }
    public CompanyType CompanyType { get; set; }
    public bool IsActive { get; set; }
    public string TenantId { get; set; }
    public string Label { get; set; }

    //UI
    public virtual string[] LabelArray { get; set; }
    public virtual bool Selected { get; set; }//for ui


    //private class Mapping : Profile
    //{
    //    public Mapping()
    //    {
    //        CreateMap<TbCompany, CompanyDto>(MemberList.None).ReverseMap(); //list table
    //    }
    //}
}