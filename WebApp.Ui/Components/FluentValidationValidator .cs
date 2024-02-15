//using FluentValidation;
//using FluentValidation.Results;
//using Microsoft.AspNetCore.Components;
//using Microsoft.AspNetCore.Components.Forms;

////tutorial https://blazor-university.com/forms/writing-custom-validation/
//uncomment if you want

//namespace WebApp.Ui.Components
//{
//    public class FluentValidationValidator : ComponentBase
//    {
//        [CascadingParameter]
//        private EditContext EditContext { get; set; }

//        [Parameter]
//        public Type ValidatorType { get; set; }

//        private IValidator _validator;
//        private ValidationMessageStore _validationMessageStore;
//        [Inject]
//        private IServiceProvider ServiceProvider { get; set; }


//        public override async Task SetParametersAsync(ParameterView parameters)
//        {
//            // Keep a reference to the original values so we can check if they have changed
//            EditContext previousEditContext = EditContext;
//            Type previousValidatorType = ValidatorType;

//            await base.SetParametersAsync(parameters);

//            if (EditContext == null)
//                throw new NullReferenceException($"{nameof(FluentValidationValidator)} must be placed within an {nameof(EditForm)}");

//            if (ValidatorType == null)
//                throw new NullReferenceException($"{nameof(ValidatorType)} must be specified.");

//            if (!typeof(IValidator).IsAssignableFrom(ValidatorType))
//                throw new ArgumentException($"{ValidatorType.Name} must implement {typeof(IValidator).FullName}");

//            if (ValidatorType != previousValidatorType)
//                ValidatorTypeChanged();

//            // If the EditForm.Model changes then we get a new EditContext
//            // and need to hook it up
//            if (EditContext != previousEditContext)
//                EditContextChanged();
//        }

//        private void ValidatorTypeChanged()
//        {
//            _validator = (IValidator)ServiceProvider.GetService(ValidatorType);
//        }

//        void EditContextChanged()
//        {
//            _validationMessageStore = new ValidationMessageStore(EditContext);
//            HookUpEditContextEvents();
//        }

//        private void HookUpEditContextEvents()
//        {
//            EditContext.OnValidationRequested += ValidationRequested;
//            EditContext.OnFieldChanged += FieldChanged;
//        }

//        async void ValidationRequested(object sender, ValidationRequestedEventArgs args)
//        {
//            _validationMessageStore.Clear();
//            var validationContext = new ValidationContext<object>(EditContext.Model);
//            ValidationResult result = await _validator.ValidateAsync(validationContext);
//            AddValidationResult(EditContext.Model, result);
//        }

//        void AddValidationResult(object model, ValidationResult validationResult)
//        {
//            foreach (ValidationFailure error in validationResult.Errors)
//            {
//                var fieldIdentifier = new FieldIdentifier(model, error.PropertyName);
//                _validationMessageStore.Add(fieldIdentifier, error.ErrorMessage);
//            }
//            EditContext.NotifyValidationStateChanged();
//        }

//        //child validation
//        async void FieldChanged(object sender, FieldChangedEventArgs args)
//        {
//            FieldIdentifier fieldIdentifier = args.FieldIdentifier;
//            _validationMessageStore.Clear(fieldIdentifier);

//            var propertiesToValidate = new string[] { fieldIdentifier.FieldName };
//            var fluentValidationContext = new ValidationContext<object>(
//                instanceToValidate: EditContext.Model,
//                propertyChain: new FluentValidation.Internal.PropertyChain(),
//                validatorSelector: new FluentValidation.Internal.MemberNameValidatorSelector(propertiesToValidate)
//            );

//            ValidationResult result = await _validator.ValidateAsync(fluentValidationContext);

//            AddValidationResult(EditContext.Model, result);
//        }



//        //An error occurred for child validation
//        //async void FieldChanged(object sender, FieldChangedEventArgs args)
//        //{
//        //    FieldIdentifier fieldIdentifier = args.FieldIdentifier;
//        //    _validationMessageStore.Clear(fieldIdentifier);

//        //    var propertiesToValidate = new string[] { fieldIdentifier.FieldName };
//        //    var fluentValidationContext = new ValidationContext<object>(
//        //            instanceToValidate: fieldIdentifier.Model,
//        //            propertyChain: new FluentValidation.Internal.PropertyChain(),
//        //            validatorSelector: new FluentValidation.Internal.MemberNameValidatorSelector(propertiesToValidate)
//        //        );

//        //    ValidationResult result = await _validator.ValidateAsync(fluentValidationContext);

//        //    AddValidationResult(fieldIdentifier.Model, result);
//        //}

//    }


//}
