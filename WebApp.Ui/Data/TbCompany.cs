using System.ComponentModel.DataAnnotations;

namespace WebApp.Ui.Data;

public partial class TbCompany
{

    [StringLength(155)] public string Id { get; set; }
    [StringLength(155)] public string Name { get; set; }
    [StringLength(155)] public string Email { get; set; }
    [StringLength(155)] public string Phone { get; set; }
    [StringLength(155)] public CompanyType CompanyType { get; set; }
    [StringLength(155)] public bool IsActive { get; set; }
    [StringLength(500)] public string Label { get; set; }
    [StringLength(155)] public string TenantId { get; set; }

    [StringLength(155)] public string ParentId { get; set; }

    //public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }

    public TbCompany()
    {
        //ApplicationUsers = new List<ApplicationUser>();
    }


}