﻿using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;
using NSE.Core.DomainObjects;

namespace NSE.WebAPP.MVC.Extensions
{
    public class CpfAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return Cpf.Validate(value.ToString()) ? ValidationResult.Success : new ValidationResult("CPF in invalid format");
        }
    }

    public class CpfAttributeAdapter : AttributeAdapterBase<CpfAttribute>
    {

        public CpfAttributeAdapter(CpfAttribute attribute, IStringLocalizer stringLocalizer) : base(attribute, stringLocalizer)
        {

        }
        public override void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-cpf", GetErrorMessage(context));
        }
        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            return "CPF in invalid format";
        }
    }

    public class CpfValidationAttributeAdapterProvider : IValidationAttributeAdapterProvider
    {
        private readonly IValidationAttributeAdapterProvider _baseProvider = new ValidationAttributeAdapterProvider();

        public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer stringLocalizer)
        {
            if (attribute is CpfAttribute CpfAttribute)
            {
                return new CpfAttributeAdapter(CpfAttribute, stringLocalizer);
            }

            return _baseProvider.GetAttributeAdapter(attribute, stringLocalizer);
        }
    }
}
