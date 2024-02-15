using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace WebApp.Shared;

//jika value  enum gunakan callback
[StreamRendering]

public partial class DropdownMultiple<TValue> : InputBase<TValue>, IAsyncDisposable
{

    public string Id { get; set; } = Guid.NewGuid().ToString();
    [Parameter] public string Css { get; set; } = "form-select";
    [Parameter] public bool EmptyText { get; set; } 
    [Parameter] public ICollection<KeyValuePair<string, string>> DataSource { get; set; }
   
    [Parameter]
    public EventCallback<string[]> EventDropdown { get; set; }


    [Inject] private IJSRuntime Js { get; set; }
    private IJSObjectReference _jsRef;

    public DotNetObjectReference<DropdownMultiple<TValue>> DotNetRef;

    protected override void OnInitialized()
    {
        base.OnInitialized();
        DotNetRef = DotNetObjectReference.Create(this);
    }
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
        {
            _jsRef = await Js.InvokeAsync<IJSObjectReference>("import", "./_content/WebApp.Shared/dropdown-select2.js");
            await Js.InvokeVoidAsync("select2Component.init", Id);
            await Js.InvokeVoidAsync("select2Component.onChange", Id, DotNetRef, "Change_SelectWithFilterBase");
            await Js.InvokeVoidAsync("select2Component.onUnSelect", Id, DotNetRef, "Change_SelectWithFilterBase");
        }
    }


    protected override bool TryParseValueFromString(string value, out TValue result, out string validationErrorMessage)
    {
        if (value == "null" || value == null) value = null;
        if (typeof(TValue) == typeof(string))
        {
            result = (TValue)(object)value!;
            validationErrorMessage = null!;

            return true;
        }

        if (typeof(TValue) == typeof(int) || typeof(TValue) == typeof(int))
        {
            int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsedValue);
            result = (TValue)(object)parsedValue;
            validationErrorMessage = null!;

            return true;
        }

        throw new InvalidOperationException($"{GetType()} does not support the type '{typeof(TValue)}'.");
    }


    [JSInvokable("Change_SelectWithFilterBase")]
    public void Change(string[] value)
    {
        if (!value.Any()) value =  new []{""};
        if (typeof(TValue) == typeof(string[]))
        {
            CurrentValue = (TValue)(object)value;
        }

        EventDropdown.InvokeAsync(value);
    }

   
    public async ValueTask DisposeAsync()
    {
        try
        {
            if (_jsRef is not null)
            {
               await _jsRef.DisposeAsync();
            }
        }
        catch (JSDisconnectedException e)
        {
        }
    }

}
