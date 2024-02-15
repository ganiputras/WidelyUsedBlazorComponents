//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Components;

//namespace WebApp.Ui.Components.Pages.Company
//{
//    [StreamRendering]
//    [Authorize]
//    public partial class Companies
//    {
//        [Inject] private ICurrentUserService CurrentUser { get; set; }

//        #region internal
//        private bool OpenDialogAdd { get; set; }
//        private bool OpenDialogClone { get; set; }
//        private bool OpenDialogEdit { get; set; }
//        private bool OpenDialogDelete { get; set; }

//        private bool IsLoading { get; set; }
//        private bool IsSelectAll { get; set; }

//        private CompanyDto Filter { get; set; } = new();
//        private CompanyDto Model { get; set; } = new();
//        private CompanyGetAllQuery Query { get; } = new();

//        private IEnumerable<CompanyDto> _response;
//        private List<CompanyDto> _table = new();
//        private HashSet<CompanyDto> _selectedItems = new();
//        private int SelectedItemsCount => _selectedItems.Count;


//        #endregion

//        protected override async Task OnInitializedAsync()
//        {
//            Filter = new();
//            await ServerReload();
//        }

//        private async Task ServerReload()
//        {
//            IsLoading = true;
//            IsSelectAll = false;

//            Filter = new CompanyDto();
//            Model = new CompanyDto();
//            _table = new List<CompanyDto>();
//            _selectedItems.Clear();
//            // await Task.Delay(500);
//            Query.TenantId = CurrentUser.TenantId;
//            _response = await Mediator.Send(Query);
//            _table = _response.ToList();
//            foreach (var e in _table)
//            {
//                e.Selected = false;
//            }
//            IsLoading = false;

//        }

//        private async Task OnRefresh()
//        {
//            CompanyCacheKey.Refresh();
//            await ServerReload();
//            await Task.Delay(500);
//            await InvokeAsync(StateHasChanged);

//        }
//        private void OnSelectAll(ChangeEventArgs e)
//        {
//            IsSelectAll = (bool)e.Value!;

//            foreach (var eac in _response)
//            {
//                eac.Selected = IsSelectAll;
//            }
//            if (!IsSelectAll)
//            {
//                _selectedItems.Clear();
//            }
//            _selectedItems = _response.Where(x => x.Selected).ToHashSet();
//        }

//        private void OnSelect(CompanyDto item, ChangeEventArgs e)
//        {
//            item.Selected = (bool)e.Value!; //replace item
//            _selectedItems.Clear();
//            _selectedItems = _response.Where(x => x.Selected).ToHashSet();

//            IsSelectAll = _selectedItems.Count.Equals(_response.Count());
//        }

//        private async Task DialogEvenAction(bool value)
//        {
//            OpenDialogAdd = false;
//            OpenDialogClone = false;
//            OpenDialogEdit = false;
//            OpenDialogDelete = false;
//            await ServerReload();
//            await InvokeAsync(StateHasChanged);
//        }
//        private void DialogEvenData(dynamic data)
//        {
//            Model = _table.FirstOrDefault(x => x.Id == data.ToString()) ?? new CompanyDto();
//        }
//        private async Task OnAdd()
//        {
//            OpenDialogAdd = true;
//            await InvokeAsync(StateHasChanged);
//        }

//        private async Task OnEdit(CompanyDto item)
//        {
//            Model = item;
//            OpenDialogEdit = true;
//            await InvokeAsync(StateHasChanged);
//        }

//        private async Task OnClone(CompanyDto item)
//        {
//            Model = item;
//            OpenDialogClone = true;
//            await InvokeAsync(StateHasChanged);
//        }

//        private async Task OnDelete(CompanyDto item)
//        {
//            _selectedItems.Clear();
//            _selectedItems.Add(item);

//            OpenDialogDelete = true;
//            await InvokeAsync(StateHasChanged);
//        }


//        private async Task OnDeleteAny()
//        {
//            if (!_selectedItems.Any()) return;
//            OpenDialogDelete = true;
//            await InvokeAsync(StateHasChanged);
//        }

//        private void OnRowSelected(CompanyDto item)
//        {
//            Model = item;
//        }

//        private string SetActiveRow(string rowId)
//        {
//            if (rowId == Model.Id) return "table-active";
//            return "";
//        }
//        private async Task QueryName(ChangeEventArgs changeEventArgs)
//        {
//            var query = changeEventArgs.Value!.ToString()!.Trim().ToLower();
//            _table = _response.Where(x => x.Name.Trim().ToLower().Contains(query)).ToList();
//            await InvokeAsync(StateHasChanged);
//        }
//    }
//}
